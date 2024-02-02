<template>
   <div class="order-1 sm:order-2 sm:ml-64 p-4 ">
  <div class="relative group flex flex-wrap justify-center items-center">
    <div v-for="book in books" :key="book.rank" class="w-1/3 p-2">
      <img :src="book.book_image" :alt="book.title" class="" />
    </div>
  </div>
</div>
</template>

<script setup lang="ts">
import { setAuthenticated, clearUserFromCookie} from "@/store/authStore";
import axios from "axios";
import { useRouter,} from "vue-router";
import {ref , onMounted } from 'vue';
const router = useRouter();
const url = "https://localhost:7284/";

const logout = (): void => {
  axios.post(`${url}api/logout`, {}, {
    withCredentials: true
  })
  .then(response => {

  clearUserFromCookie();

  router.push("/login");
  
  setAuthenticated(false);
  })
  .catch(error => {
    console.error('Error during logout:', error);
  });
};
const books = ref<any[]>([]);
const getnybooks = async () => {
  const url = `https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=egM8BbGFHGODa7lpiV0SFCAKhJlzG72G`;
  const response = await axios.get(url).then((response)=>{
    console.log(response.data.results.books);
    books.value=response.data.results.books;
    console.log(books.value);
  })
}
onMounted(()=>{
  getnybooks();
})

</script>
