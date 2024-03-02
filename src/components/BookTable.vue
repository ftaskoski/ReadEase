<template>
  <div class="flex justify-center items-center overflow-auto mt-2">
    <table class="w-full divide-y divide-gray-200">
      <thead class="bg-gray-50">
        <tr>
          <th class="px-2 text-left py-2 sm:px-3 sm:py-3 text-xs sm:text-sm font-medium text-gray-500 uppercase tracking-wider">Author</th>
          <th class="px-2 text-left py-2 sm:px-3 sm:py-3 text-xs sm:text-sm font-medium text-gray-500 uppercase tracking-wider">Title</th>
          <th class="px-2 text-left py-2 sm:px-3 sm:py-3 text-xs sm:text-sm font-medium text-gray-500 uppercase tracking-wider">Category</th>
          <th class="px-2 text-left py-2 sm:px-3 sm:py-3 text-xs sm:text-sm font-medium text-gray-500 uppercase tracking-wider">Actions</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr class="hover:bg-gray-100" v-for="book in books" :key="book.bookId">
          <td class="px-2 py-2 sm:px-3 sm:py-3">{{ book.author }}</td>
          <td class="px-2 py-2 sm:px-3 sm:py-3">{{ book.title }}</td>
          <td class="px-2 py-2 sm:px-3 sm:py-3">{{ getCategoryName(book.categoryId) }}</td>
          <td class="text-sm text-gray-500">
            <button
              class="inline-flex justify-center my-1 py-2 px-[2px] sm:px-2 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 mr-2"
              @click="openModal(book.bookId)"
            >
              Delete book <i class="fa-sharp fa-solid fa-trash ml-1 mt-0.5"></i>
            </button>
            <button
              class="inline-flex justify-center my-1 py-2 px-2 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
              @click="openEditModal(book.bookId)"
            >
              Edit book <i class="fa-sharp fa-solid fa-pen-to-square ml-1 mt-0.5"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>






<script setup lang="ts">

defineProps([ 'getCategoryName', 'openModal', 'showModal', 'deleteBook','closeModal', 'openEditModal', 'showEditModal', 'closeEditModal',  'categoryId', 'categories','newTitle', 'updateBook', 'newAuthor', 'newCategoryId','books']);

</script>
