<template>
 <div class="flex justify-center items-center overflow-auto">
    <table class="table-auto w-full">
      <thead>
        <tr>
          <th class="px-4 py-2">Author</th>
          <th class="px-4 py-2">Title</th>
          <th class="px-4 py-2">Category</th>
          <th class="px-4 py-2">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in books" :key="book.bookId">
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
          <td class="border px-4 py-2">
            <button
              class="text-white bg-blue-500 hover:bg-blue-600 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
              @click="openEditModal(book.bookId)"
            >
              Edit book <i class="fa-sharp fa-solid fa-pen-to-square"></i>
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
