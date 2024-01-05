<template>
  <div>
    <p>Insert books</p>
    <form @submit.prevent="addBook">
      <label for="author">Author</label>
      <input class="rounded-lg border-gray-200 border  focus:border-blue-500  focus:outline-none" type="text" id="author" v-model="author" required />

      <label for="title">Title</label>
      <input class="rounded-lg border-gray-200 border  focus:border-blue-500  focus:outline-none" type="text" id="title" v-model="title" required />

      <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" type="submit">Submit</button>
    </form>
  </div>

  <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" @click="downloadBooks">Download Books</button>
</template>

<script setup lang="ts">
import axios from "axios";
import { ref, onMounted } from "vue";
import { loadUserFromLocalStorage } from "@/store/authStore";

const url = "http://localhost:5000/";
const author = ref<string>("");
const title = ref<string>("");
const user = loadUserFromLocalStorage();
const id = user ? user.id : null;

const downloadBooks = () => {
  axios
    .get(`${url}api/getbooks/${id}`, { responseType: 'arraybuffer' })
    .then((response) => {
      const blob = new Blob([response.data], { type: response.headers['content-type'] });
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = 'books.txt';
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
      console.log(response.data);
    })
    .catch((error) => {
      console.error(error);
    });
};


</script>
