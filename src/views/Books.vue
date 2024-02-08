<template>
  <div >
    <div class="order-1 sm:order-2 sm:ml-64 p-4 ">
      <div>
    <div>
      <p>Insert books</p>
      <form @submit.prevent="addBook">
        <label for="author">Author</label>
        <input
          class="rounded-lg border-gray-200 border focus:border-blue-500 focus:outline-none"
          type="text"
          id="author"
          v-model="author"
          required
        />

        <label for="title">Title</label>
        <input
          class="rounded-lg border-gray-200 border focus:border-blue-500 focus:outline-none"
          type="text"
          id="title"
          v-model="title"
          required
        />
        <select required @change="getCategoryIdFromSelectedCategory" v-model="selectedCategory" class="w-full sm:w-auto px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300">
          <option value="null" disabled selected hidden>Select a Category</option>
          <option v-for="category in categories" :key="category" :value="category">{{ category.categoryName }}</option>
        </select>
        <button
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
          type="submit"
        >
          Submit
        </button>
      </form>
      <button
        class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
        @click="downloadBooks"
      >
        Download books
      </button>
    </div>

    <div class="mb-4">
      <label for="search" class="block text-sm font-medium text-gray-600"
        >Search by Author</label
      >
      <input
        v-model="searchQuery"
        @input="handleInput"
        type="text"
        id="search"
        placeholder="Enter author's name..."
        class="w-80 px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300"
      />
    </div>

    <div v-for="category in categories">
      <input v-model="checkedCategories" @change="check" type="checkbox" :value="category.categoryId"  />
      <label>{{ category.categoryName }}</label>
    </div>

    <BookTable
      :searchedBooks="searchedBooks"
      :searchQuery="searchQuery"
      :bookPaginated="bookPaginated"
      :checkedBooks="checkedBooks"
      :getCategoryName="getCategoryName"
      :deleteBook="deleteBook"
    />

    <div class="flex justify-center items-center mt-4">
      <button
        class="ml-2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
        @click="changePage(currPage - 1)"
        :disabled="currPage === 1"
      >
        Previous
      </button>
      <span>Page {{ currPage }} of {{ totalPages }}</span>
      <button
        class="ml-2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
        @click="changePage(currPage + 1)"
        :disabled="currPage === totalPages"
      >
        Next
      </button>
    </div>
  </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { ref, onMounted, computed, watch } from "vue";
import { loadUserFromCookie } from "@/store/authStore";
import BookTable from "@/components/BookTable.vue";
import { useRouter } from "vue-router";

const router = useRouter();
const url = "https://localhost:7284/";
const user = loadUserFromCookie();
const id = user ? user.id : null;
let debounceTimer = 0;

// Form and Book Data
const author = ref<string>("");
const title = ref<string>("");
const selectedCategory = ref<{ categoryName: string; categoryId: number } | null>(null);
const categories = ref<any[]>([]);
const bookCollection = ref<any[]>([]);
const bookPaginated = ref<any[]>([]);
const checkedCategories = ref<number[]>([]);
const checkedBooks = ref<any[]>([]);
const checkedbooksAll = ref<any[]>([]);
const searchQuery = ref<string>("");
const searchedBooks = ref<any[]>([]);
const searchedBooksAll = ref<any[]>([]);

// Pagination
const booksPerPage = ref(10);
const currPage = ref(1);
const totalPages = computed(() => {
  const totalBooks = checkedCategories.value.length > 0
    ? checkedbooksAll.value.length
    : searchedBooks.value && searchQuery.value
    ? searchedBooksAll.value.length
    : (bookCollection.value?.length ?? 0);
  return Math.ceil(totalBooks / booksPerPage.value);
});
const changePage = (page: number) => {
if (page >= 1 && page <= totalPages.value) {
  currPage.value = page;
  router.push({ query: { page: currPage.value, search: searchQuery.value, categories: checkedCategories.value.join(",") } });

  if (checkedCategories.value.length > 0) {
    getAllCheckedBooks();
    check();
  } else {
    if (!searchQuery.value) {
      getBooks();
    } else {
      searchedBooksFull();
    }
  }

}

};

// Functions
// Get Functions
const getBooks = () => {
  axios
    .get(`${url}api/getbooks`, {
      params: {
        pageNumber: currPage.value,
      },
      withCredentials: true,
    })
    .then((response) => {
      bookPaginated.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};

const getAllBooks = () => {
  axios
    .get(`${url}api/getallbooks`, {
      withCredentials: true,
    })
    .then((response) => {
      bookCollection.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};

const getAllCategories = () => {
  axios.get(`${url}api/categories`, { withCredentials: true }).then((response) => {
    categories.value = response.data;
  });
};

const getCategoryIdFromSelectedCategory = () => {
  console.log(selectedCategory.value ? selectedCategory.value.categoryId : null);
};

// Post Functions
const addBook = () => {
  const categoryIdToAdd = selectedCategory.value ? selectedCategory.value.categoryId : null;
  axios
    .post(`${url}api/insertbook`, {
      author: author.value,
      title: title.value,
      CategoryId: categoryIdToAdd,
    }, { withCredentials: true })
    .then((response) => {
      author.value = "";
      title.value = "";
      selectedCategory.value = null;

      if (checkedCategories.value.length > 0) {
        getAllCheckedBooks();
        check();
      }
      getAllBooks();
      getBooks();
      if (searchQuery.value) {
        searchedBooksFull();
      }
    })
    .catch((error) => {
      console.error(error);
    });
};

const downloadBooks = () => {
  axios
    .get(`${url}api/downloadbooks`, { responseType: "arraybuffer", withCredentials: true })
    .then((response) => {
      const blob = new Blob([response.data], {
        type: response.headers["content-type"],
      });
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = "books.txt";
      link.click();
    })
    .catch((error) => {
      console.error(error);
    });
};

const deleteBook = (id: number) => {
  axios
    .delete(`${url}api/deletebook/${id}`, {
      withCredentials: true,
    })
    .then(() => {
      if (checkedCategories.value.length > 0) {
        getAllCheckedBooks();
        check();
      }
      if ((checkedbooksAll.value?.length === 1 || checkedBooks.value?.length === 1) && currPage.value > 1) {
        currPage.value = currPage.value - 1;
        getAllCheckedBooks();
        check();
      }
      if (searchQuery.value) {
        searchedBooksFull();
      } else {
        getAllBooks();
        getBooks();
      }
      if ((bookPaginated.value?.length === 1 || searchedBooks.value?.length === 1) && currPage.value > 1) {
        currPage.value = currPage.value - 1;
        getBooks();
      }
    })
    .catch((error) => {
      console.error(`Error deleting book with ID ${id}:`, error);
    });
};

// Search Functions
const searchedBooksFull = () => {
  axios
    .get(`${url}api/searchbooksall`,
      {
        params: {
          search: searchQuery.value,
        },
        withCredentials: true,
      })
    .then((response) => {
      searchedBooksAll.value = response.data;
      searchBook(); 
    });
};

const searchBook = () => {
  axios
    .get(`${url}api/searchbooks`, {
      params: {
        search: searchQuery.value,
        pageNumber: currPage.value,
      },
      withCredentials: true,
    })
    .then((response) => {
      searchedBooks.value = response.data;
    });
};

const handleInput = () => {
  clearTimeout(debounceTimer);
  searchedBooks.value = [];
  debounceTimer = setTimeout(() => {
    searchedBooksFull();
  }, 1000);
};

// Checked Books Functions
const getAllCheckedBooks = () => {
  axios.get(`${url}api/checkedall`, {
    params: {
      categories: checkedCategories.value.join(","),
    },
    withCredentials: true,
  }).then((response) => {
    checkedbooksAll.value = response.data;
    bookCollection.value = [];
    bookPaginated.value = [];
  });
};

const check = () => {
  if (checkedCategories.value.length > 0) {
    getAllCheckedBooks();
    axios
      .get(`${url}api/checked`, {
        params: {
          categories: checkedCategories.value.join(","),
          pageNumber: currPage.value,
        },
        withCredentials: true,
      })
      .then((response) => {
        checkedBooks.value = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  } else {
    getBooks();
    getAllBooks();
    checkedBooks.value = [];
  }
};

const getCategoryName = (categoryId : number) => {
  const category = categories.value.find((cat) => cat.categoryId === categoryId);
  return category ? category.categoryName : 'Unknown Category';
};

watch(checkedCategories, () => {
  if (!router.currentRoute.value.query.categories) {
    currPage.value = 1;
  }
  if (checkedCategories.value.length > 0) {
    if (currPage.value > 1 && currPage.value <= totalPages.value) {
      currPage.value = currPage.value - 1;
    }
    getAllCheckedBooks();
    router.push({ query: { categories: checkedCategories.value.join(','), page: currPage.value } });
    sessionStorage.setItem("categories", checkedCategories.value.join(","));
  } else {
    currPage.value = 1;
    getBooks();
    getAllBooks();
    router.push({ query: { categories: null, page: null } });
    sessionStorage.removeItem("categories");
  }
});

watch(searchQuery, () => {
  if (!router.currentRoute.value.query.search) {
    currPage.value = 1;
  }
  if (searchQuery.value) {
    handleInput();
    router.push({ query: { search: searchQuery.value, page: currPage.value } });
  } else {
    currPage.value = 1;
    router.push({ query: { search: null, page: 1 } });
    getAllBooks();
    getBooks();
  }
});




// Lifecycle Hook
onMounted(() => {
  const categoriesFromStorage = sessionStorage.getItem("categories");
  const searchFromURL = router.currentRoute.value.query.search as string;
  const pageFromURL = Number(router.currentRoute.value.query.page);
  if (pageFromURL) {
    currPage.value = pageFromURL;
  }
  if (categoriesFromStorage) {
    router.push({ query: { categories: checkedCategories.value.join(','), page: currPage.value } });
    checkedCategories.value = categoriesFromStorage.split(',').map(Number);
    getAllCheckedBooks();
    check();
  }
  if (searchFromURL) {
    searchQuery.value = searchFromURL;
    searchBook();
  }
  getAllCategories();
  getAllBooks();
  getBooks();
});


</script>