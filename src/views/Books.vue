<template>
  <div >
    <div class="order-1 sm:order-2 sm:ml-64 p-4 ">
      <div>
    <div >

      <Card class="max-w-md flex justify-center items-center">
      <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2" >INSERT BOOKS</p>
      <form @submit.prevent="addBook">
        <div class="relative w-full h-10 ">
          <input
            required
            v-model="author"
            class="peer  flex items-center justify-center w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
            placeholder=" "
          />
          <label
            class="flex   w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
            :class="{ 'placeholder-shown': !author }"
          >
            Author
          </label>
        
        </div>
        <div class="relative w-full h-10 mt-2">
          <input
            required
            v-model="title"
            class="peer   w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
            placeholder=" "
          />
          <label
            class="flex  w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
            :class="{ 'placeholder-shown': !title }"
          >
            Title
          </label>
        
        </div>

<div class="relative inline-block w-full mt-2 text-gray-500">
  <select @change="getCategoryIdFromSelectedCategory" v-model="selectedCategory" class="peer block appearance-none  w-full  border border-gray-300 hover:border-gray-400 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:border-blue-500">
    <option value="null" disabled selected hidden>Select a Category</option>
    <option v-for="category in categories" :key="category" :value="category">{{ category.categoryName }}</option>
  </select>
  <div class="pointer-events-none absolute text-sm inset-y-0 right-0 flex items-center px-2 text-gray-700">
    <span class="flex items-center">&#9660;</span>
  </div>
</div>



        <button
          class="text-white flex justify-center items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
          type="submit"
        >
          Submit
        </button>
      </form>
    </Card>

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

    <div class="flex flex-wrap">
  <div v-for="category in categories" :key="category.categoryId" class="mr-4 mb-2">
    <input v-model="checkedCategories" @change="check" type="checkbox" :value="category.categoryId" class="form-checkbox h-5 w-5 text-blue-600" />
    <label class="ml-2">{{ category.categoryName }}</label>
  </div>
</div>

<div>
    <select v-model="booksPerPage" @change="handleChange" class="w-80 px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300">
      <option v-for="page in booksPerPageArr" :key="page" :value="page">{{ page }}</option>
    </select>
  </div>


    <div v-if="searchandcategorybooks && searchandcategorybooks.length > 0">
  <div class="flex justify-center items-center overflow-auto">
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
              @click="openModal(book.bookId)"
            >
              Delete book <i class="fa-sharp fa-solid fa-trash"></i>
            </button>
          </td>
        </tr>
      </tbody>

      <div v-if="showModal" class="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
      <div class="relative w-auto max-w-md mx-auto my-6">
        <!-- Content -->
        <div class="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
          <!-- Header -->
          <div class="flex items-start justify-between p-5 border-b border-solid rounded-t border-blueGray-200">
            <h3 class="text-3xl font-semibold">
              Confirm Delete
            </h3>
            <button
              class="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
            >
              <span class="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
            </button>
          </div>
          <!-- Body -->
          <div class="relative p-6 flex-auto">
            <p class="my-4 text-blueGray-500 text-lg leading-relaxed">
              Are you sure you want to delete this book?
            </p>
          </div>
          <!-- Footer -->
          <div class="flex items-center justify-end p-6 border-t border-solid rounded-b border-blueGray-200">
            <button
            @click="deleteBook()"
              class="text-red-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
            >
              Delete
            </button>
            <button
              @click="showModal = false"
              class="text-blueGray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
    
    </table>
  </div>
</div>


<div  v-else>
  <BookTable
    :searchedBooks="searchedBooks"
    :searchQuery="searchQuery"
    :bookPaginated="bookPaginated"
    :checkedBooks="checkedBooks"
    :getCategoryName="getCategoryName"
    :deleteBook="deleteBook"
    :openModal="openModal"
    :showModal="showModal"
    :closeModal="closeModal"
  />
</div>


<div class="flex justify-center items-center mt-4">
    <button
      class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-l border border-black"
      @click="changePage(currPage - 1)"
      :disabled="currPage === 1"
      :class="{ 'cursor-not-allowed': currPage === 1, 'opacity-50': currPage === 1 }"
    >
      Previous
    </button>
    <div class="flex">
      <button
        v-for="page in visiblePages"
        :key="page"
        class=" bg-blue-500  hover:bg-blue-700 text-white font-bold py-2 px-4 border border-black"
        @click="changePage(page as number)"
        :disabled="currPage === page"
        :class="{ 'cursor-not-allowed': currPage === page, 'animate-pulse': currPage === page }"
      >
        {{ page }}
      </button>
    </div>
    <button
      class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-r border border-black"
      @click="changePage(currPage + 1)"
      :disabled="currPage === totalPages"
      :class="{ 'cursor-not-allowed': currPage === totalPages, 'opacity-50': currPage === totalPages }"
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
import BookTable from "@/components/BookTable.vue";
import { useRouter } from "vue-router";
import Card from "@/components/Card.vue";
const router = useRouter();
const url = "https://localhost:7284/";
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
const searchandcategoryall = ref<any[]>([]);
const searchandcategorybooks = ref<any[]>([]);
const booksPerPageArr = ref<number[]>([2,5,10, 20, 30, 40, 50]);
const booksPerPage = ref<number>(10);
const showModal = ref<boolean>(false);
const bookIdToDelete = ref<number | null>(null);


  const openModal = (bookId: number) => {
    document.body.style.overflow = 'hidden';
  showModal.value = !showModal.value;
  bookIdToDelete.value = bookId;
};

  const closeModal = () => {
    document.body.style.overflow = 'auto';
  showModal.value = false;
  }

  function handleChange() {
  currPage.value = 1;
  sessionStorage.setItem("booksPerPage", String(booksPerPage.value));
  sessionStorage.setItem("page", String(currPage.value));
  router.push({ query: { booksPerPage: String(booksPerPage.value) } });

  if(searchQuery.value){
    handleInput();
    searchedBooksFull();
  } else if(checkedCategories.value.length > 0){
    check();
    getAllCheckedBooks();
  } else if(searchandcategorybooks.value.length > 0){
    searchAndCategory();
    searchAndCategoryPaginated();
  }else {
    getBooks();
    getAllBooks();

  }

}

// Pagination
const currPage = ref<number>(1);
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

const windowSize=ref<number>(3);

const visiblePages = computed(() => {
  let start = currPage.value - Math.floor(windowSize.value / 2);
  start = Math.max(start, 1);
  let end = start + windowSize.value - 1;
  end = Math.min(end, totalPages.value);
  start = Math.max(1, end - windowSize.value + 1); // Adjusted start to be at least 1
  return Array.from({ length: end - start + 1 }, (_, i) => start + i);
});

const changePage = (page: number) => {
  sessionStorage.setItem("booksPerPage", String(booksPerPage.value));
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
        booksPerPage: booksPerPage.value
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
        pageSize: booksPerPage.value
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

const deleteBook = () => {
  showModal.value = false;
  axios
    .delete(`${url}api/deletebook/${bookIdToDelete.value}`, {
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
      console.error(`Error deleting book with ID ${bookIdToDelete.value}:`, error);
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
        pageSize: booksPerPage.value
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
          pageSize: booksPerPage.value

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
        pageNumber: currPage.value,
        pageSize: booksPerPage.value

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
  const booksPerPageFromStorage = sessionStorage.getItem("booksPerPage");

  if (booksPerPageFromStorage) {
    booksPerPage.value = parseInt(booksPerPageFromStorage, 10);
  }

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
    
  }

  getAllCategories();
  getAllBooks();
  getBooks();
});


</script>

