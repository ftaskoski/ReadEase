<template>
  <div class="order-1 sm:order-2 sm:ml-64 p-4">

    <div class="container mx-auto">
      <div class="grid lg:grid-cols-3 md:grid-cols-1 gap-4">
            <!-- Add New Category -->
            <Card>
                <h2 class="text-lg font-semibold mb-4">Add New Category</h2>
                <form @submit.prevent="addCategory()">
                <input v-model="newCategory" type="text" placeholder="Add New Category" class="w-full px-4 py-2 border rounded-md">
                <button  class="mt-4 bg-blue-500 text-white px-4 py-2 rounded-md w-full">Add</button>
              </form>
            </Card>
            
            <!-- Search for User -->
            <Card>
                <h2 class="text-lg font-semibold mb-4">Search For User</h2>
                <input v-model="searchQuery" @input="handleInput()" type="text" placeholder="Username" class="w-full px-4 py-2 border rounded-md">
            </Card>
            
            <!-- Delete Category -->
            <Card>
                <h2 class="text-lg font-semibold mb-4">Delete Category</h2>
                <div class="flex flex-wrap gap-2" v-for="category in categories" :key="category.categoryId">
                    <label class="flex items-center">
                        <input v-model="selectedCategories" :value="category.categoryId" type="checkbox" class="mr-2"> {{ category.categoryName }}
                    </label>
                   
                </div>
                <button @click="deleteCategories()" class="mt-4 bg-red-500 text-white px-4 py-2 rounded-md w-full">Delete</button>
              </Card>
        </div>
        
        <!-- User List -->
        <div class="bg-white shadow-md rounded-lg p-6 mt-20">
            <div class="flex justify-between items-center mb-4">
                <div class="flex items-center">
                 <SelectPerPage
        :itemsPerPage="usersPerPage"
        :itemsPerPageArr="usersPerPageArr"
        @update:itemsPerPage="changeUsersPerPage"
      />
                </div>

            </div>
            <div class="overflow-x-auto mt-4">
            <table class="min-w-full divide-y divide-gray-200 ">
                <thead class="bg-gray-50">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Username</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Action</th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    <tr v-for="user in paginatedUsers" :key="user.username">
                        <td class="px-6 py-4">{{ user.username }}</td>
                        <td class="px-6 py-4">
                            <button @click="openModal(user.id)" class="bg-red-500 text-white px-4 py-2 rounded-md">Delete User</button>
                        </td>
                    </tr>
                 
                </tbody>
            </table>
            <DeleteModal :showModal="showModal" :closeModal="closeModal" :confirmDelete="deleteUser" :bodyText="`Are you sure you want to delete this user?`" />
          </div>
            <!-- Pagination -->
            <div class="">
      <Pagination
        @page-changed="changePage"
        :currPage="currPage"
        :totalPages="totalPages"
        :visiblePages="visiblePages"
      />
    </div>
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { onMounted, ref, watch, computed } from "vue";
import { url } from "@/store/authStore";
import Pagination from "@/components/Pagination.vue";
import { useRouter } from "vue-router";
import SelectPerPage from "@/components/SelectPerPage.vue";
import Card from "@/components/Card.vue";
import DeleteModal from "@/components/DeleteModal.vue";
const router = useRouter();
const users = ref<any[]>([]);
const newCategory = ref<string>("");
const currPage = ref<number>(1);
const windowSize = ref<number>(3);
const usersPerPageArr = ref<number[]>([1,2, 5, 10, 20, 30, 40, 50]);
const usersPerPage = ref<number>(10);
const paginatedUsers = ref<any[]>([]);
const categories = ref<any[]>([]);
const selectedCategories = ref<any[]>([]);
const searchQuery = ref<string>("");
const totalFilteredusers = ref<any[]>([]);
const showModal = ref<boolean>(false);
const userIdToDelete = ref<number | null>(null);
let debounceTimer = 0;

const totalPages = computed(() => {
  if (searchQuery.value.trim() !== "") {
    const filteredUsers = totalFilteredusers.value.length;
  
    return Math.ceil(filteredUsers / usersPerPage.value);
  } else {
    const totalUsers = users.value.filter(user => user.role !== 'Admin').length;
    return Math.ceil(totalUsers / usersPerPage.value);
  }
});

const closeModal = () => {
  document.body.style.overflow = "auto";
  showModal.value = false;
};

const visiblePages = computed(() => {
  let start = currPage.value - Math.floor(windowSize.value / 2);
  start = Math.max(start, 1);
  let end = start + windowSize.value - 1;
  end = Math.min(end, totalPages.value);
  start = Math.max(1, end - windowSize.value + 1);
  return Array.from({ length: end - start + 1 }, (_, i) => start + i);
});

const getUser = () => {
  axios
    .get(`${url}api/users`, {
      withCredentials: true,
    })
    .then((response) => {
      users.value = response.data;
      getPaginatedUsers();
    })
    .catch((error) => {
      console.error(error);
    });
};

const getPaginatedUsers = () => {
  axios
    .get(`${url}api/paginatedusers`, {
      withCredentials: true,
      params: {
        pageNumber: currPage.value,
        pageSize: usersPerPage.value,
      },
    })
    .then((response) => {
      paginatedUsers.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};

const deleteUser = () => {
  axios
    .delete(`${url}api/delete/${userIdToDelete.value}`, {
      withCredentials: true,
    })
    .then(() => {
      getUser();
      closeModal();
    });
};
const addCategory = () => {
  axios
    .post(
      `${url}api/insertcategory`,
      {
        categoryName: newCategory.value,
      },
      {
        withCredentials: true,
      }
    )
    .then((response) => {
      newCategory.value = "";
      getAllCategories();
    });
};

function changePage(page: number) {
  currPage.value = page;
  router.push({
    query: { usersPerPage: String(usersPerPage.value), currPage: String(currPage.value) },
  })
  if(searchQuery.value.trim() !== "") {
    searchUser();
  }else{

    getPaginatedUsers();
  }
}

function changeUsersPerPage(newValue: number) {
  usersPerPage.value = newValue;
  currPage.value = 1;
  router.push({ query: { usersPerPage: String(usersPerPage.value), currPage: String(currPage.value) }});
  if(searchQuery.value.trim() !== "") {
    searchUser();
  }else{

    getPaginatedUsers();
  }
}

const getAllCategories = () => {
  axios
    .get(`${url}api/categories`, { withCredentials: true })
    .then((response) => {
      categories.value = response.data;
    });
};
function deleteCategories() {
  console.log(`selectedCategories`, selectedCategories.value.join(","));
  axios
    .delete(
      `${url}api/deletecategories`,
      {
        data: selectedCategories.value, 
        withCredentials: true,
      }
    )
    .then((response) => {
      selectedCategories.value = [];
      getAllCategories();
    });
}

function searchUser() {
  axios
    .get(`${url}api/searchusers`, {
      withCredentials: true,
      params: {
        search: searchQuery.value,
        pageNumber: currPage.value, 
        pageSize: usersPerPage.value,
      },
    })
    .then((response) => {
      paginatedUsers.value = response.data;
     searchUsersAll();
    });
}

function searchUsersAll() {
  axios
    .get(`${url}api/searchusersall`, {
      withCredentials: true,
      params: {
        search: searchQuery.value,
      },
    })
    .then((response) => {
      totalFilteredusers.value = response.data;
    });
}

function handleInput() {
  currPage.value = 1;
  clearTimeout(debounceTimer);
  if (searchQuery.value.trim() !== "") {
  
    debounceTimer = setTimeout(() => {
      searchUser();
      
    }, 1000);
  } else {
    getPaginatedUsers();
  }
}


function openModal (userId: number)  {
  document.body.style.overflow = "hidden";
  showModal.value = !showModal.value;
  userIdToDelete.value = userId;
};


onMounted(() => {
  getUser();
  getAllCategories();

});

if (router.currentRoute.value.query.usersPerPage) {
    usersPerPage.value = Number(router.currentRoute.value.query.usersPerPage);
  }
  if (router.currentRoute.value.query.currPage) {
    currPage.value = Number(router.currentRoute.value.query.currPage);
  }


</script>

