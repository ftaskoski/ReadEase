<template>
  <div class="order-1 sm:order-2 sm:ml-64 p-4">
    <form @submit.prevent="addCategory">
      <div class="relative w-60 h-10">
        <input
          v-model="newCategory"
          class="peer sm:w-[80%] w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
          placeholder=" "
        />
        <label
          class="flex sm:w-[80%] w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
        >
          Add New Category
        </label>
      </div>
    </form>
    


    <div class="mt-20">
      <SelectPerPage
        :itemsPerPage="usersPerPage"
        :itemsPerPageArr="usersPerPageArr"
        @update:itemsPerPage="changeUsersPerPage"
      />

    </div>
    <div class="flex justify-center items-center">


      
      <table class="table-auto w-full">
        <thead>
          <tr>
            <th class="px-4 py-2">Username</th>
            <th class="px-4 py-2">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in paginatedUsers">
            <td class="border px-4 py-2 text-center">{{ user.username }}</td>
            <td class="border px-4 py-2 text-center">
              <button
                @click="deleteUser(user.id)"
                class="text-white bg-red-500 hover:bg-red-600 focus:ring-4 focus:outline-none focus:ring-red-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
              >
                Delete User
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>



    <div class="">
      <Pagination
        @page-changed="changePage"
        :currPage="currPage"
        :totalPages="totalPages"
        :visiblePages="visiblePages"
      />
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
const router = useRouter();
const users = ref<any[]>([]);
const newCategory = ref<string>("");
const currPage = ref<number>(1);
const windowSize = ref<number>(3);
const usersPerPageArr = ref<number[]>([2, 5, 10, 20, 30, 40, 50]);
const usersPerPage = ref<number>(10);
const paginatedUsers = ref<any[]>([]);

const totalPages = computed(() => {
  let totalUsers;

  const filteredUsers = users.value.filter(user => user.role !== 'Admin');
  totalUsers = filteredUsers.length;

  return Math.ceil(totalUsers / usersPerPage.value);
});

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
const deleteUser = (id: number) => {
  axios
    .delete(`${url}api/delete/${id}`, {
      withCredentials: true,
    })
    .then(() => {
      getUser();
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
    });
};

function changePage(page: number) {
  currPage.value = page;
  router.push({
    query: { usersPerPage: String(usersPerPage.value), currPage: String(currPage.value) },
  })
  getPaginatedUsers();
}

function changeUsersPerPage(newValue: number) {
  usersPerPage.value = newValue;
  currPage.value = 1;
  router.push({ query: { usersPerPage: String(usersPerPage.value), currPage: String(currPage.value) }});
  getPaginatedUsers();
}

onMounted(() => {
  getUser();

});

if (router.currentRoute.value.query.usersPerPage) {
    usersPerPage.value = Number(router.currentRoute.value.query.usersPerPage);
  }
  if (router.currentRoute.value.query.currPage) {
    currPage.value = Number(router.currentRoute.value.query.currPage);
  }


</script>

