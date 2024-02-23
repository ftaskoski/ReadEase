<template>
  <div class="flex justify-center items-center overflow-auto">
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
        <tr v-else-if="bookPaginated" v-for="book in bookPaginated" :key="book.id">
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
        <tr v-if="checkedBooks" v-for="book in checkedBooks">
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
</template>

<script setup lang="ts">
 defineProps(['searchedBooks', 'searchQuery', 'bookPaginated', 'checkedBooks', 'getCategoryName', 'deleteBook']);
</script>
