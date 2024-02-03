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

        <button
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
          type="submit"
        >
          Submit
        </button>
        Select a Category
        <select @change="getCategoryIdFromSelectedCategory" v-model="selectedCategory" class="w-full sm:w-auto px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300">
          <option v-for="category in categories" :key="category" :value="category">{{ category.categoryName }}</option>
        </select>
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

    <div class="flex justify-center items-center">
      <table class="table-auto w-full">
        <thead>
          <tr>
            <th class="px-4 py-2">Author</th>
            <th class="px-4 py-2">Title</th>
            <th class="px-4 py-2">Category</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="searchedBooks && searchQuery" v-for="book in searchedBooks">
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
          <tr v-else v-for="book in bookPaginated" :key="book.id">
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
const totalPages = computed(() => {
  if (searchedBooks.value && searchQuery.value) {
    return Math.ceil(searchedBooksAll.value.length / booksPerPage.value);
  } else {
    return Math.ceil((bookCollection.value?.length ?? 0) / booksPerPage.value);
  }
});

const changePage = (page: number) => {
  if (page >= 1 && page <= totalPages.value) {
    currPage.value = page;
    if (!searchQuery.value) {
      getBooks();
    } else {
      searchedBooksFull();
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

</script>