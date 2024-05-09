<template>
  <div>
    <div class="order-1 sm:order-2 sm:ml-64 p-4">
      <div>
        <div>
          <div class="flex flex-col lg:flex-row justify-between space-y-5 md:space-x-2">

<!-- First Section -->
<div class="w-full max-w-md lg:max-w-xl">
  <Card>
    <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2">INSERT BOOKS</p>
    <form @submit.prevent="addBook">
      <div class="relative w-full h-10">
        <input
          required
          v-model="author"
          class="peer flex items-center justify-center w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
          placeholder=""
        />
        <label
          class="flex w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
          :class="{ 'placeholder-shown': !author }"
        >
          Author
        </label>
      </div>
      <div class="relative w-full h-10 mt-2">
        <input
          required
          v-model="title"
          class="peer w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
          placeholder=""
        />
        <label
          class="flex w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
          :class="{ 'placeholder-shown': !title }"
        >
          Title
        </label>
      </div>
      <div class="relative inline-block w-full mt-2 text-gray-500">
        <select v-model="selectedCategory" required class="peer block appearance-none w-full border border-gray-300 hover:border-gray-400 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:border-blue-500">
          <option value="" disabled selected hidden>Select a Category</option>
          <option v-for="category in categories" :key="category" :value="category">{{ category.categoryName }}</option>
        </select>
        <div class="pointer-events-none absolute text-sm inset-y-0 right-0 flex items-center px-2 text-gray-700">
          <span class="flex items-center">&#9660;</span>
        </div>
      </div>
      <button class="text-white flex justify-center items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" type="submit">Submit</button>
    </form>
  </Card>
</div>

<!-- Second Section -->
<div class="w-full max-w-md lg:max-w-xl">
  <Card>
    <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2">Search For Books</p>
     
      <div class="relative w-full h-10 mt-2">
        <input
          required
          v-model="searchQuery"
          @input="handleInput"
          class="peer w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
          placeholder=""
        />
        <label
          class="flex w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
          :class="{ 'placeholder-shown': !title }"
        >
         By Author
        </label>
      </div>
      <div class="relative w-full h-10 mt-2">
        <input
          v-model="searchTitle"
          @input="handleInput"
          class="peer w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
          placeholder=""
        />
        <label
          class="flex w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
          :class="{ 'placeholder-shown': !title }"
        >
         By Title
        </label>
      </div>
  </Card>
</div>



<!-- Third Section -->
<div class="w-full max-w-md lg:max-w-xl">
  <Card>
    <p class="text-3xl font-semibold text-gray-900 flex justify-center pb-2">SEARCH BY CATEGORY</p>
    <div class="flex-wrap mt-2 flex items-center justify-center">
      <div v-for="category in categories" :key="category.categoryId" class="mr-4 mb-2">
        <div>
          <label>
            <input v-model="checkedCategories" :value="category.categoryId" @change="filterBooksAll" class="dark:border-white-400/20 dark:scale-100 transition-all duration-500 ease-in-out dark:hover:scale-110 dark:checked:scale-100 w-6 h-6" type="checkbox">{{ category.categoryName }}
          </label>
        </div>
      </div>
    </div>
  </Card>
</div>

</div>

        </div>

        <div v-if="books.length > 0" class="mt-20">
          <div>
            <select
              v-model="booksPerPage"
              @change="handleChange"
              class="w-80 px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300"
            >
              <option v-for="page in booksPerPageArr" :key="page" :value="page">
                {{ page }}
              </option>
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
            @update:newAuthor="(value) => (newAuthor = value)"
            @update:newTitle="(value) => (newTitle = value)"
            @update:newCategoryId="(value) => (newCategoryId = value)"
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
            :title="currTitle"
            :author="currAuthor"
            :categoryId="currCategoryId"
            @update:newAuthor="newAuthor = $event"
            @update:newTitle="newTitle = $event"
            @update:newCategoryId="newCategoryId = $event"
          />

          <Pagination
            :currPage="currPage"
            :totalPages="totalPages"
            :visiblePages="visiblePages"
            @page-changed="changePage"
          />
        </div>
        <div v-else>
          <p
            class="text-3xl font-semibold text-gray-900 flex justify-center pb-2 mt-40"
          >
            No books found. Start adding books
          </p>
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
import Pagination from "@/components/Pagination.vue";
import { url } from "@/store/authStore";

const router = useRouter();
let debounceTimer = 0;
const author = ref<string>("");
const title = ref<string>("");
const selectedCategory = ref<
  | {
      categoryName: string;
      categoryId: number;
    }
  | ""
>("");
const categories = ref<any[]>([]);
const bookCollection = ref<any[]>([]);
const bookPaginated = ref<any[]>([]);
const checkedCategories = ref<number[]>([]);
const searchQuery = ref<string>("");
const booksPerPageArr = ref<number[]>([2, 5, 10, 20, 30, 40, 50]);
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
const currTitle = ref<string>("");
const currAuthor = ref<string>("");
const currCategoryId = ref<number | undefined>(0);
// Pagination !!!!!!!!!!!!
const currPage = ref<number>(1);
const searchTitle = ref<string>("");

const totalPages = computed(() => {
  let totalBooks;

  if (checkedCategories.value.length > 0 || searchQuery.value || searchTitle.value) {
    totalBooks = totalFilteredBooks.value.length;
  } else {
    totalBooks = bookCollection.value?.length ?? 0;
  }
  return Math.ceil(totalBooks / booksPerPage.value);
});

const windowSize = ref<number>(3);

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
    
    applyBookFilter();
    router.push({
      query: {
        page: currPage.value,
        search: searchQuery.value,
        categories: checkedCategories.value.join(","),
        booksPerPage: booksPerPage.value,
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
        pageSize: booksPerPage.value,
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
    searchTitle: searchTitle.value,
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
    searchTitle: searchTitle.value,
    categories: checkedCategories.value.join(","),
    pageNumber: currPage.value,
    pageSize: booksPerPage.value,
  };
  axios
    .get(`${url}api/searchandcategory`, {
      params: searchParams,
      withCredentials: true,
    })
    .then((response) => {
      books.value = response.data;      
      sessionStorage.setItem("search", searchQuery.value);
      sessionStorage.setItem("categories", String(checkedCategories.value));
      sessionStorage.setItem("page", String(currPage.value));
      sessionStorage.setItem("title", searchTitle.value);
      router.push({
        query: {
          page: currPage.value,
          search: searchQuery.value,
          categories: checkedCategories.value.join(","),
          booksPerPage: booksPerPage.value,
        },
      });
    })
    .catch((error) => {
      console.error(error);
    });
};

function checkFilter() {
  if(checkedCategories.value.length > 0 && !searchQuery.value && !searchTitle.value){
        filterBooksAll();
      } else if (checkedCategories.value.length === 0 && searchQuery.value || searchTitle.value) {
        handleInput();
      }else{
        getBooks();
        getAllBooks();
      }
}

function applyBookFilter() {
  if (checkedCategories.value.length > 0 || searchQuery.value || searchTitle.value) {
      filterBooksAll();
    } else {
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
  axios
    .get(`${url}api/categories`, { withCredentials: true })
    .then((response) => {
      categories.value = response.data;
    });
};

const addBook = () => {
  const categoryIdToAdd = selectedCategory.value
    ? selectedCategory.value.categoryId
    : null;
  axios
    .post(
      `${url}api/insertbook`,
      {
        author: author.value,
        title: title.value,
        CategoryId: categoryIdToAdd,
      },
      { withCredentials: true }
    )
    .then((response) => {
      author.value = "";
      title.value = "";
      selectedCategory.value = "";

      applyBookFilter();

      
    })
    .catch((error) => {
      console.error(error);
    });
};

const deleteBook = () => {
  showModal.value = false;
  document.body.style.overflow = "auto";
  axios
    .delete(`${url}api/deletebook/${bookIdToDelete.value}`, {
      withCredentials: true,
    })
    .then(() => {
      const totalBooks = totalFilteredBooks.value.length;
      const totalPagesToDelete = Math.ceil(totalBooks / booksPerPage.value);
      if (totalPagesToDelete < currPage.value) {
        // If the current page becomes empty after deletion, move to the previous page
        currPage.value - 1;
      }

      applyBookFilter();


    })
    .catch((error) => {
      console.error(
        `Error deleting book with ID ${bookIdToDelete.value}:`,
        error
      );
    });
};

// Modal functions !!!!!!!!!
const openEditModal = (book: any) => {
  document.body.style.overflow = "hidden";
  showEditModal.value = !showEditModal.value;
  bookIdToEdit.value = book.bookId;
  currTitle.value = book.title;
  currAuthor.value = book.author;
  currCategoryId.value = book.categoryId;
};

const updateBook = () => {
  axios
    .post(
      `${url}api/updatebook`,
      {
        Id: bookIdToEdit.value,
        NewTitle: newTitle.value,
        NewAuthor: newAuthor.value,
        NewCategory: newCategoryId.value,
      },
      { withCredentials: true }
    )
    .then((response) => {
      applyBookFilter();
      document.body.style.overflow = "auto";
      showEditModal.value = false;
      bookIdToEdit.value = null;
      newTitle.value = "";
      newAuthor.value = "";
      newCategoryId.value = 0;
    })
    .catch((error) => {
      console.error(error); // Log any errors
    });
};

function closeEditModal() {
  document.body.style.overflow = "auto";
  showEditModal.value = false;
  bookIdToEdit.value = null;
  newTitle.value = "";
  newAuthor.value = "";
  newCategoryId.value = 0;
}

const openModal = (bookId: number) => {
  document.body.style.overflow = "hidden";
  showModal.value = !showModal.value;
  bookIdToDelete.value = bookId;
};

const closeModal = () => {
  document.body.style.overflow = "auto";
  showModal.value = false;
};

// Handle fucntions !!!!!!!!!!!!

function handleChange() {
  currPage.value = 1;
  sessionStorage.setItem("booksPerPage", String(booksPerPage.value));
  sessionStorage.setItem("page", String(currPage.value));
  router.push({ query: { booksPerPage: String(booksPerPage.value) } });

  applyBookFilter();
}

const handleInput = () => {
  currPage.value = 1;

  clearTimeout(debounceTimer);
  books.value = [];
  sessionStorage.removeItem("search");
  sessionStorage.removeItem("title");
  if (searchQuery.value.trim() !== "" || searchTitle.value.trim() !== "") {
    debounceTimer = setTimeout(() => {
      filterBooksAll();
    }, 1000);
  }else{
    getBooks();
    getAllBooks();
  }
};

const getCategoryName = (categoryId: number) => {
  const category = categories.value.find(
    (cat) => cat.categoryId === categoryId
  );
  return category ? category.categoryName : "Unknown Category";
};

// Watchers !!!!!!!!!!!!!!!!!!!!
watch([checkedCategories, searchQuery], () => {
  // Check if the page is being reloaded
  const isReload = sessionStorage.getItem("isReload");

  // If the page is being reloaded, remove the sessionStorage item and return
  if (isReload) {
    sessionStorage.removeItem("isReload");
    return;
  }

  // Reset the current page to 1 whenever filters change
  currPage.value = 1;

  // Call your filter function or any other necessary actions
  checkFilter();
});

window.onbeforeunload = function () {
  sessionStorage.setItem("isReload", "true");
};

watch(
  () => newTitle.value,
  (newVal) => {
    currTitle.value = newVal;
  }
);

watch(
  () => newAuthor.value,
  (newVal) => {
    currAuthor.value = newVal;
  }
);

watch(
  () => newCategoryId.value,
  (newVal) => {
    currCategoryId.value = newVal;
  }
);

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
  const bookTitleFromStorage = sessionStorage.getItem("title");

  if (booksPerPageFromStorage) {
    booksPerPage.value = parseInt(booksPerPageFromStorage, 10);
  }

  if (pageFromStorage) {
    currPage.value = parseInt(pageFromStorage, 10);
  }

  if (categoriesFromStorage) {
    checkedCategories.value = categoriesFromStorage.split(",").map(Number);
    //checkFilter();
   // filterBooksAll();
    applyBookFilter();
  }

  if (bookTitleFromStorage) {
    searchTitle.value = bookTitleFromStorage;
    applyBookFilter();
  }
  if (searchFromStorage) {
    searchQuery.value = searchFromStorage;
    applyBookFilter();
  } else {
    getBooks();
    getAllBooks();
  }
  getAllCategories();
});
</script>
