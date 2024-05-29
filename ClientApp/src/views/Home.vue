<template>
 <div class="order-1 sm:order-2 sm:ml-64 p-4 m">
<div class="bg-white rounded-lg p-8">
  <h2 class="text-3xl text-center mb-6 text-gray-800">This Week's Best Sellers - New York Times</h2>
  <div class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5">

    <div  v-for="book in books" class="bg-white rounded-lg shadow-lg overflow-hidden transition-transform transform hover:-translate-y-2">
      <img :src="book.book_image"  class="w-full">
      <div class="p-4 text-center">
        <h3 class="text-xl text-gray-800 mb-2">{{book.title}}</h3>
        <p class="text-gray-600">by {{book.author}}</p>
      </div>
    </div>
  </div>
</div>
 </div>
</template>

<script setup lang="ts">

import axios from "axios";
import {ref , onMounted } from 'vue';

const books = ref<any[]>([]);
async function getnybooks () : Promise<void> {
    const url = `https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=egM8BbGFHGODa7lpiV0SFCAKhJlzG72G`;
    await axios.get(url).then((response)=>{
    books.value=response.data.results.books;
  })
}
onMounted(()=>{
  getnybooks();
})

</script>
