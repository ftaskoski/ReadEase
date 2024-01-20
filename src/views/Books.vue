<template>
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
      </form>
      <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" @click="downloadBooks">Download books</button>
    </div>

    <div class="flex justify-center items-center h-screen">
      <table class="table-auto w-full">
        <thead>
          <tr>
            <th class="px-4 py-2">Author</th>
            <th class="px-4 py-2">Title</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="book in bookPaginated" :key="book.id">
            <td class="border px-4 py-2">{{ book.author }}</td>
            <td class="border px-4 py-2">{{ book.title }}</td>
            <td class="border px-4 py-2"><button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" @click="deleteBook(book.bookId)">Delete book</button></td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="flex justify-center items-center">
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
        :disabled="currPage >= totalPages"
      >
        Next
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { ref, onMounted,computed } from "vue";
import { loadUserFromLocalStorage } from "@/store/authStore";

const url = "http://localhost:5000/";
const author = ref<string>("");
const title = ref<string>("");
const user = loadUserFromLocalStorage();
const id = user ? user.id : null;
const bookCollection = ref<any[]>();
const bookPaginated = ref<any[]>();
const downloadBooks = () => {
  axios
    .get(`${url}api/downloadbooks/${id}`, { responseType: "arraybuffer" })
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

const addBook = () => {
  axios
    .post(`${url}api/insertbook/${id}`, {
      author: author.value,
      title: title.value,
    })
    .then((response) => {
      author.value = "";
      title.value = "";
      getBooks();
    })
    .catch((error) => {
      console.error(error);
    });
};

const booksPerPage = ref(10);
const currPage = ref(1);
const totalPages = computed(() => {
  return Math.ceil((bookCollection.value?.length ?? 0) / booksPerPage.value);
});


const changePage = (page: number) => {
  if (page >= 1 && page <= totalPages.value) {
    currPage.value = page;
    getBooks();
  }
};

const getBooks = () => {
  axios
  .get(`${url}api/getbooks/${id}`, {
    params: {
      pageNumber: currPage.value,
    }
  })
    .then((response) => {
      bookPaginated.value = response.data;
      console.log(bookCollection.value);
    })
    .catch((error) => {
      console.error(error);
    });
};
const getAllBooks = () => {
  axios
    .get(`${url}api/getallbooks/${id}`)
    .then((response) => {
      bookCollection.value = response.data;
      console.log(bookCollection.value);
    })
    .catch((error) => {
      console.error(error);
    });
};
const deleteBook = (id: number) => {
  
  axios
    .delete(`${url}api/deletebook/${id}`)
    .then(() => {
      getBooks();
    })
    .catch((error) => {
      console.error(`Error deleting book with ID ${id}:`, error);
    });
}
onMounted(() => {
  getAllBooks();
  getBooks();
});












</script>
