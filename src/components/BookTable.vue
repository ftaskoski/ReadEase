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
              @click="openModal(book.bookId)"
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
              @click="openModal(book.bookId)"
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
              @click="openModal(book.bookId)"
            >
              Delete book <i class="fa-sharp fa-solid fa-trash"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
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
            @click="closeModal()"
              class="text-blueGray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
</template>

<script setup lang="ts">
 defineProps(['searchedBooks', 'searchQuery', 'bookPaginated', 'checkedBooks', 'getCategoryName', 'openModal', 'showModal', 'deleteBook','closeModal']);
</script>
