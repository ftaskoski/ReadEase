<template>
  <div >
    <div class="order-1 sm:order-2 sm:ml-64 p-4 ">
      <div>
    <div >


      <div class="flex flex-col  xl:flex-row  sm:space-x-10 space-y-4">

      <Card class="max-w-md w-full flex justify-center items-center">
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
  <select v-model="selectedCategory" class="peer block appearance-none  w-full  border border-gray-300 hover:border-gray-400 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:border-blue-500">
    <option value="null" disabled selected hidden>Select a Category</option>
    <option value="">Select category</option>
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
      <div class="flex justify-center items-center">
    <Card class="space-y-2 sm:space-y-2 ">
      <div class="mb-4 ">
        <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2" >SEARCH BY AUTHOR</p>
        <div class="flex items-center justify-center p-5">
  <div >
    <div class="flex  border border-gray-200 rounded-lg">
      <div class="flex w-10 items-center justify-center rounded-tl-lg rounded-bl-lg border-r border-gray-200 bg-white p-5">
        <svg viewBox="0 0 20 20" aria-hidden="true" class="pointer-events-none absolute w-5 fill-gray-500 transition">
          <path d="M16.72 17.78a.75.75 0 1 0 1.06-1.06l-1.06 1.06ZM9 14.5A5.5 5.5 0 0 1 3.5 9H2a7 7 0 0 0 7 7v-1.5ZM3.5 9A5.5 5.5 0 0 1 9 3.5V2a7 7 0 0 0-7 7h1.5ZM9 3.5A5.5 5.5 0 0 1 14.5 9H16a7 7 0 0 0-7-7v1.5Zm3.89 10.45 3.83 3.83 1.06-1.06-3.83-3.83-1.06 1.06ZM14.5 9a5.48 5.48 0 0 1-1.61 3.89l1.06 1.06A6.98 6.98 0 0 0 16 9h-1.5Zm-1.61 3.89A5.48 5.48 0 0 1 9 14.5V16a6.98 6.98 0 0 0 4.95-2.05l-1.06-1.06Z"></path>
        </svg>
      </div>
      <input type="text" v-model="searchQuery" @input="handleInput" class="w-full rounded-tr-lg rounded-br-lg max-w-[160px] bg-white pl-2 text-base font-semibold outline-0" placeholder="Enter author's name" id="">
    
    </div>
  </div>
</div>
      </div>
    </Card>


      </div>

  </div>
  
      <button
        class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
        @click="downloadBooks"
      >
        Download books
      </button>
    </div>

<Card>
  <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2" >SEARCH BY CATEGORY</p>

    <div class=" flex-wrap mt-2 flex items-center justify-center">
  <div v-for="category in categories" :key="category.categoryId" class="mr-4 mb-2">
    <div class="">
  <label >
    <input v-model="checkedCategories" :value="category.categoryId" @change="handleChange" class="dark:border-white-400/20 dark:scale-100 transition-all duration-500 ease-in-out dark:hover:scale-110 dark:checked:scale-100 w-6 h-6" type="checkbox">{{ category.categoryName }}
  </label>
</div>
</div>
</div>
</Card>

<div>
    <select v-model="booksPerPage" @change="handleChange" class="w-80 px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300">
      <option v-for="page in booksPerPageArr" :key="page" :value="page">{{ page }}</option>
    </select>
  </div>


  <BookTable
      :books="books"
      :getCategoryName="getCategoryName"
      :openModal="openModal"
      :showModal="showModal"
      :deleteBook="deleteBook"
      :closeModal="closeModal"
      :categories="categories"
      :currPage="currPage"
      :totalPages="totalPages"
      :newAuthor="newAuthor"
      :newTitle="newTitle"
      :newCategoryId="newCategoryId"
      :updateBook="updateBook"
      :openEditModal="openEditModal"
      :showEditModal="showEditModal"
      :closeEditModal="closeEditModal"
      @update:newAuthor="value => newAuthor = value"
      @update:newTitle="value => newTitle = value"
      @update:newCategoryId="value => newCategoryId = value"
    />

    <DeleteModal
      :showModal="showModal"
      :closeModal="closeModal"
      :deleteBook="deleteBook"
    />

    <EditModal
      :showEditModal="showEditModal"
      :closeEditModal="closeEditModal"
      :updateBook="updateBook"
      :categories="categories"
      :newTitle="newTitle"
      :newAuthor="newAuthor"
      :newCategoryId="newCategoryId"
      @update:newAuthor="newAuthor = $event"
      @update:newTitle="newTitle = $event"
      @update:newCategoryId="newCategoryId = $event"
    />
    



    <div class="flex justify-center items-center mt-4 text-sm text-gray-700 ">
  <button
    class="bg-gray-50 hover:bg-gray-200  font-semibold py-2 px-4 rounded-l border border-black"
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
      class="bg-gray-50 hover:bg-gray-200  font-semibold py-2 px-4 border border-black"
      @click="changePage(page as number)"
      :disabled="currPage === page"
      :class="{ 'cursor-not-allowed': currPage === page , 'bg-gray-300': currPage === page }"
    >
      {{ page }}
    </button>
  </div>
  <button
    class="bg-gray-50 hover:bg-gray-200  font-semibold py-2 px-4 rounded-r border border-black"
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
import { useRouter } from "vue-router";
import Card from "@/components/Card.vue";
import DeleteModal from "@/components/DeleteModal.vue";
import BookTable from "@/components/BookTable.vue";
import EditModal from "@/components/EditModal.vue";

const router = useRouter();
const url = "https://localhost:7284/";
let debounceTimer = 0;
const author = ref<string>("");
const title = ref<string>("");
const selectedCategory = ref<{ categoryName: string; categoryId: number } | null>(null);
const categories = ref<any[]>([]);
const bookCollection = ref<any[]>([]);
const bookPaginated = ref<any[]>([]);
const checkedCategories = ref<number[]>([]);
const searchQuery = ref<string>("");
const booksPerPageArr = ref<number[]>([2,5,10, 20, 30, 40, 50]);
const booksPerPage = ref<number>(10);
const showModal = ref<boolean>(false);
const bookIdToDelete = ref<number | null>(null);
const showEditModal = ref<boolean>(false);
const bookIdToEdit = ref<number | null>(null);
const newTitle = ref<string>("");
const newAuthor = ref<string>("");
const newCategoryId = ref<number | undefined>(0);
const books = ref<any[]>([]);
const totalFilteredBooks = ref<any[]>([]);





// Pagination !!!!!!!!!!!!
const currPage = ref<number>(1);

const totalPages = computed(() => {
  let totalBooks;

  if(checkedCategories.value.length > 0 || searchQuery.value){
    totalBooks = totalFilteredBooks.value.length;
  }else{
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
  start = Math.max(1, end - windowSize.value + 1); 
  return Array.from({ length: end - start + 1 }, (_, i) => start + i);
});

const changePage = (page: number) => {
  sessionStorage.setItem("booksPerPage", String(booksPerPage.value));
  if (page >= 1 && page <= totalPages.value) {
    currPage.value = page;
    sessionStorage.setItem("page", String(currPage.value)); 
    
    checkFilter();

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





// CRUD FUNCTIONS !!!!!!!!!!!

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
      books.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};

const filterBooksAll = () => {
  books.value = [];
  bookPaginated.value = [];
  const searchParams = {
    search: searchQuery.value,
    categories: checkedCategories.value.join(","),

  };
  axios
    .get(`${url}api/searchandcategoryall`, {
      params: searchParams,
      withCredentials: true,
    })
    .then((response) => {
      totalFilteredBooks.value = response.data;
      filterBooksPaginated();
    })
    .catch((error) => {
      console.error(error);
    });
};

const filterBooksPaginated = () => {
  const searchParams = {
    search: searchQuery.value,
    categories: checkedCategories.value.join(","),
    pageNumber: currPage.value,
    pageSize: booksPerPage.value
  };
  axios.get(`${url}api/searchandcategory`, {
    params: searchParams,
    withCredentials: true,
  }).then((response) => {
    books.value = response.data;
    sessionStorage.setItem("search", searchQuery.value);
    sessionStorage.setItem("categories", String(checkedCategories.value));
    sessionStorage.setItem("page", String(currPage.value));

  }).catch((error) => {
    console.error(error);
  })
}

function checkFilter(){
  if (checkedCategories.value.length > 0 && !searchQuery.value) {
    filterBooksAll();
  }else if(searchQuery.value && checkedCategories.value.length == 0){
    handleInput();
  } 
  else if(checkedCategories.value.length == 0 && !searchQuery.value){
    getBooks();
    getAllBooks();
  }

}

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

      checkFilter();
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
  document.body.style.overflow = 'auto';
  axios
    .delete(`${url}api/deletebook/${bookIdToDelete.value}`, {
      withCredentials: true,
    })
    .then(() => {
        checkFilter();
    })
    .catch((error) => {
      console.error(`Error deleting book with ID ${bookIdToDelete.value}:`, error);
    });
};



// Modal functions !!!!!!!!!

const openEditModal = (bookId: number) => {
  document.body.style.overflow = 'hidden';
  showEditModal.value = !showEditModal.value;
  bookIdToEdit.value = bookId;
}

const updateBook = () => {
  axios.post(`${url}api/updatebook`, {
    Id: bookIdToEdit.value,
    NewTitle: newTitle.value,
    NewAuthor: newAuthor.value,
    NewCategory: newCategoryId.value
  }, { withCredentials: true })
  .then(response => {
    checkFilter();

    document.body.style.overflow = 'auto';
    showEditModal.value = false;
    bookIdToEdit.value = null;
    newTitle.value = "";
    newAuthor.value = "";
    newCategoryId.value = 0;
    
  })
  .catch(error => {
    console.error(error); // Log any errors
  });
}


function closeEditModal(){
  document.body.style.overflow = 'auto';
  showEditModal.value = false;
  bookIdToEdit.value = null;
  newTitle.value = "";
  newAuthor.value = "";
  newCategoryId.value = 0;
}

  const openModal = (bookId: number) => {
    document.body.style.overflow = 'hidden';
  showModal.value = !showModal.value;
  bookIdToDelete.value = bookId;
};

  const closeModal = () => {
    document.body.style.overflow = 'auto';
  showModal.value = false;
  }

// Handle fucntions !!!!!!!!!!!!

  function handleChange() {
  currPage.value = 1;
  sessionStorage.setItem("booksPerPage", String(booksPerPage.value));
  sessionStorage.setItem("page", String(currPage.value));
  router.push({ query: { booksPerPage: String(booksPerPage.value) } });

  checkFilter();

}

const handleInput = () => {
  clearTimeout(debounceTimer);
  books.value = [];
  sessionStorage.removeItem("search");  
  if (searchQuery.value.trim() !== "") { 
    debounceTimer = setTimeout(() => {
      filterBooksAll();
    }, 1000);
  }
};


const getCategoryName = (categoryId : number) => {
  const category = categories.value.find((cat) => cat.categoryId === categoryId);
  return category ? category.categoryName : 'Unknown Category';
};

// Watchers !!!!!!!!!!!!!!!!!!!!
watch([checkedCategories, searchQuery],  () => {
    currPage.value = 1;
    checkFilter();
});


watch(totalPages, () => {
  if (currPage.value > totalPages.value) {
    currPage.value = Math.max(totalPages.value, 1);
    sessionStorage.setItem("page", String(currPage.value));
    books.value = [];
    bookPaginated.value = [];
    checkFilter();

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




  if(categoriesFromStorage){

      checkedCategories.value = categoriesFromStorage.split(',').map(Number);
      filterBooksAll();
    } 
     if(searchFromStorage){
    
      searchQuery.value = searchFromStorage;
      handleInput();
      
    }
    else{
      getBooks();
      getAllBooks();
    }
  getAllCategories();
 
});


</script>

