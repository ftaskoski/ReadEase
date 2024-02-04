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

const url="https://localhost:7284/"
const author = ref<string>("");
const title = ref<string>("");
const user = loadUserFromCookie();
const id = user ? user.id : null;
const bookCollection = ref<any[]>();
const bookPaginated = ref<any[]>();



const downloadBooks = () => {
  axios
    .get(`${url}api/downloadbooks/${id}`, { responseType: "arraybuffer", withCredentials: true })
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


const selectedCategory = ref<{ categoryName: string; categoryId: number } | null>(null);
const categories = ref<any[]>([]);
    const getCategoryIdFromSelectedCategory = () => {
      console.log(selectedCategory.value ? selectedCategory.value.categoryId : null);
};
const addBook = () => {
  const categoryIdToAdd = selectedCategory.value ? selectedCategory.value.categoryId : null;
  axios
    .post(`${url}api/insertbook/${id}`, {
      author: author.value,
      title: title.value,
      CategoryId: categoryIdToAdd,
    },{withCredentials: true,})
    
    .then((response) => {
      author.value = "";
      title.value = "";
      selectedCategory.value = null;
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

const booksPerPage = ref(10);
const currPage = ref(1);
const calculatedTotalPages = computed(() => {
  if (checkedCategories.value.length > 0) {
    return Math.ceil(checkedbooksAll.value.length / booksPerPage.value);
  } else if (searchedBooks.value && searchQuery.value) {
    return Math.ceil(searchedBooksAll.value.length / booksPerPage.value);
  } else {
    return Math.ceil((bookCollection.value?.length ?? 0) / booksPerPage.value);
  }
});
const totalPages = computed(() => calculatedTotalPages.value);

const changePage = (page: number) => {
  if (page >= 1 && page <= totalPages.value) {
    currPage.value = page;

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


const getBooks = () => {
  axios
    .get(`${url}api/getbooks/${id}`, {
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
    .get(`${url}api/getallbooks/${id}`, {
      withCredentials: true,
    })
    .then((response) => {
      bookCollection.value = response.data;
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

const searchQuery = ref<string>("");
const searchedBooks = ref<any[]>([]);
const searchedBooksAll = ref<any[]>([]);

watch(searchQuery, () => {
  currPage.value = 1;
  if (searchQuery.value) {
    handleInput();
  } else {
    getAllBooks();
    getBooks();
  }
});
const getCategoryName = (categoryId : number) => {
  const category = categories.value.find((cat) => cat.categoryId === categoryId);
  return category ? category.categoryName : 'Unknown Category';
};
const searchedBooksFull = () => {
  axios
    .get(`${url}api/searchbooksall/${id}`, 
    
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
    .get(`${url}api/searchbooks/${id}`, {
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

let debounceTimer = 0;
const API_KEY='egM8BbGFHGODa7lpiV0SFCAKhJlzG72G';
const getnybooks = async () => {
  const url = `https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=egM8BbGFHGODa7lpiV0SFCAKhJlzG72G`;
  const response = await axios.get(url);
  //console.log(response.data);
}
const handleInput = () => {
  clearTimeout(debounceTimer);
  searchedBooks.value = [];
  debounceTimer = setTimeout(() => {
    searchedBooksFull();
  }, 1000);
};



const getAllCategories = () =>{

  axios.get(`${url}api/categories`,{withCredentials: true}).then((response)=>{
    categories.value=response.data;
  })

}

onMounted(() => {
  getAllCategories();
  getAllBooks();
  getBooks();
  getnybooks();
});

const checkedCategories = ref<number[]>([]);
const checkedBooks = ref<any[]>([]);
  const checkedbooksAll = ref<any[]>([]);

function getAllCheckedBooks() {
  axios.get(`${url}api/checkedall/${id}`,{
        params: {
          categories: checkedCategories.value.join(","),
          
        },
        withCredentials: true,
      }).then((response) => {
    checkedbooksAll.value = response.data;
        bookCollection.value = [];
        bookPaginated.value = [];

      
      
      });
}
  function check() {
  if (checkedCategories.value.length > 0) {
    getAllCheckedBooks();
    axios
      .get(`${url}api/checked/${id}`, {
        params: {
          categories: checkedCategories.value.join(","),
          pageNumber: currPage.value,
        },
        withCredentials: true,
      })
      .then((response) => {
        //bookPaginated.value = [];
        //bookCollection.value = [];
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
}

watch(checkedCategories, () => {
  if (checkedCategories.value.length > 0) {
    getAllCheckedBooks();
  } else {
    currPage.value = 1; 
    getBooks();
    getAllBooks();
  }
});
</script>