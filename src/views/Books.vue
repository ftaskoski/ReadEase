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



    <div v-if="searchandcategorybooks && searchandcategorybooks.length > 0">
  <div class="flex justify-center items-center">
    <table class="table-auto w-full">
      <thead>
        <tr>
          <th class="px-4 py-2">Author</th>
          <th class="px-4 py-2">Title</th>
          <th class="px-4 py-2">Category</th>
          <th class="px-4 py-2">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in searchandcategorybooks" :key="book.bookId">
          <td class="border px-4 py-2">{{ book.author }}</td>
          <td class="border px-4 py-2">{{ book.title }}</td>
          <td class="border px-4 py-2">{{ getCategoryName(book.categoryId) }}</td>
          <td class="border px-4 py-2">
            <button
              class="text-white bg-red-500 hover:bg-red-600 focus:ring-4 focus:outline-none focus:ring-red-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
              @click="deleteBook(book.bookId)"
            >
              Delete book <i class="fa-sharp fa-solid fa-trash"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</div>

<div v-else>
  <BookTable
    :searchedBooks="searchedBooks"
    :searchQuery="searchQuery"
    :bookPaginated="bookPaginated"
    :checkedBooks="checkedBooks"
    :getCategoryName="getCategoryName"
    :deleteBook="deleteBook"
  />
</div>



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
  let totalBooks;
  if (searchQuery.value && checkedCategories.value.length > 0) {
    totalBooks = searchandcategoryall.value.length;
  } else if (checkedCategories.value.length > 0) {
    totalBooks = checkedbooksAll.value.length;
  } else if (searchedBooks.value && searchQuery.value) {
    totalBooks = searchedBooksAll.value.length;
  } else {
    totalBooks = bookCollection.value?.length ?? 0;
  }
  return Math.ceil(totalBooks / booksPerPage.value);
});

const changePage = (page: number) => {
  if (page >= 1 && page <= totalPages.value) {
    currPage.value = page;
    sessionStorage.setItem("page", String(currPage.value)); // Update session storage with current page
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

    if(checkedCategories.value.length > 0 && searchQuery.value){
      
      searchAndCategory();
      searchAndCategoryPaginated();
    }

    router.push({
      query: {
        page: currPage.value,
        search: searchQuery.value,
        categories: checkedCategories.value.join(","),
      },
    });
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
      if (checkedCategories.value.length > 0 && searchQuery.value) {
        searchAndCategory();
        searchAndCategoryPaginated();
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
        sessionStorage.setItem("page", String(currPage.value));
        getAllCheckedBooks();
        check();
      }
      if (checkedCategories.value.length  && searchQuery.value) {
        searchAndCategory();
        searchAndCategoryPaginated();
      }
      if (searchQuery.value) {
        searchedBooksFull();
      } else {
        getAllBooks();
        getBooks();
      }
      if ((bookPaginated.value?.length === 1 || searchedBooks.value?.length === 1) && currPage.value > 1) {
        currPage.value = currPage.value - 1;
        sessionStorage.setItem("page", String(currPage.value));
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

const searchandcategoryall = ref<any[]>([]);
const searchandcategorybooks = ref<any[]>([]);
const searchAndCategory =  () => {
  if (searchQuery.value && checkedCategories.value.length > 0) {
  axios.get(`${url}api/searchandcategoryall`, {
    withCredentials: true,
      params: {
        search: searchQuery.value,
        categories: checkedCategories.value.join(',')
      }
    }).then((response) => {
      searchandcategoryall.value = response.data;
      searchAndCategoryPaginated();
    })


  }
};

const searchAndCategoryPaginated =  () => {
  if (searchQuery.value && checkedCategories.value.length > 0) {
  axios.get(`${url}api/searchandcategory`, {
    withCredentials: true,
      params: {
        search: searchQuery.value,
        categories: checkedCategories.value.join(','),
        pageNumber: currPage.value
      }
  }).then((response) => {

    searchandcategorybooks.value = response.data;
  })
  }
}



watch([checkedCategories, searchQuery],  () => {
  if (checkedCategories.value.length > 0 && !searchQuery.value ) {
    if (!sessionStorage.getItem("categories")) {
      currPage.value = 1;
    }
    searchandcategorybooks.value=[];
    searchandcategoryall.value = [];
    check();
    router.push({ query: { categories: checkedCategories.value.join(','), page: currPage.value } });
    sessionStorage.setItem("categories", checkedCategories.value.join(","));
    sessionStorage.removeItem("search");
    searchedBooks.value = [];

  } else if (searchQuery.value && checkedCategories.value.length === 0) {
    if (!sessionStorage.getItem("search")) {
      currPage.value = 1;
    }
    searchandcategorybooks.value=[];
    searchandcategoryall.value = [];
    router.push({ query: { search: searchQuery.value, page: currPage.value } });
    sessionStorage.setItem("search", searchQuery.value);
    sessionStorage.setItem("page", String(currPage.value));
    sessionStorage.removeItem("categories");
    searchQuery.value = sessionStorage.getItem("search") || "";
    handleInput();
    searchBook();
    searchedBooksFull();

  } else if(checkedCategories.value.length > 0 && searchQuery.value){
    searchAndCategory();
    searchAndCategoryPaginated();
    checkedBooks.value = [];
    searchedBooks.value = [];
    router.push({ query: { categories: checkedCategories.value.join(','), search: searchQuery.value, page: currPage.value } });
    sessionStorage.setItem("categories", checkedCategories.value.join(","));
    sessionStorage.setItem("search", searchQuery.value);
    sessionStorage.setItem("page", String(currPage.value));
  }
  
  else if(searchQuery.value && checkedCategories.value.length === 0){
    handleInput();
    sessionStorage.setItem("page", String(currPage.value));
    searchBook();
    searchedBooksFull();
    router.push({ query: { categories: null, search: searchQuery.value, page: currPage.value } });
    sessionStorage.setItem("search", searchQuery.value);
    sessionStorage.removeItem("categories");
  }

  else {
    currPage.value = 1;
    router.push({ query: { categories: null, search: null, page: currPage.value } });
    sessionStorage.setItem("page", String(currPage.value));
    sessionStorage.removeItem("categories");
    sessionStorage.removeItem("search");
    getBooks();
    getAllBooks();
    handleInput();
    searchBook();
    searchedBooksFull();
  }
});


watch(totalPages, () => {
  if (currPage.value > totalPages.value) {
    currPage.value = Math.max(totalPages.value, 1);
    sessionStorage.setItem("page", String(currPage.value));
    if (checkedCategories.value.length > 0) {
      getAllCheckedBooks();
      check();
    }
     else if(searchQuery.value){
      searchedBooksFull();
      handleInput();
    }
    if (checkedCategories.value.length  && searchQuery.value) {
        searchAndCategory();
        searchAndCategoryPaginated();
      }

    router.push({
      query: {
        page: currPage.value,
        search: searchQuery.value,
        categories: checkedCategories.value.join(","),
      },
    });
  }
});

// Lifecycle Hook
onMounted(() => {
  const categoriesFromStorage = sessionStorage.getItem("categories");
  const searchFromStorage = sessionStorage.getItem("search");
  const pageFromStorage = sessionStorage.getItem("page");

  if (pageFromStorage) {
    currPage.value = parseInt(pageFromStorage, 10);
  }

  if (categoriesFromStorage) {
    checkedCategories.value = categoriesFromStorage.split(",").map(Number);
    getAllCheckedBooks();
    check();
  }

  if (searchFromStorage) {
    searchQuery.value = searchFromStorage;
    searchBook();
  }

  getAllCategories();
  getAllBooks();
  getBooks();
});


</script>