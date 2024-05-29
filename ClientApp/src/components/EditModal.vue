<template>
    <div>
        <div v-if="showEditModal" class="fixed  inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
          <div class="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50 z-50">
    <div class="bg-white rounded-lg shadow-lg w-11/12 sm:w-1/3 md:w-1/2 inset-0 ">
    <div class="px-6 py-4 border-b border-gray-200">
      <h3 class="text-lg font-semibold text-gray-800">Edit Book</h3>
    </div>
    <div class="p-6 space-y-4">
      <div>
        <label class="block text-gray-700 font-medium mb-1">Title</label>
        <input
          :value="title" @input="$emit('update:newTitle', ($event.target as HTMLInputElement).value)"
          type="text" class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring focus:border-blue-300"
        />
      </div>
      <div>
        <label class="block text-gray-700 font-medium mb-1">Author</label>
        <input
          :value="author" @input="$emit('update:newAuthor', ($event.target as HTMLInputElement).value)"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring focus:border-blue-300"
        />
      </div>
      <div>
        <label class="block text-gray-700 font-medium mb-1">Category</label>
        <select
        :value="categoryId" @change="$emit('update:newCategoryId', ($event.target as HTMLInputElement).value)"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring focus:border-blue-300"
        >
        <option value="0" disabled selected hidden>Select a Category</option>
        <option value="0" disabled>Select category</option>
        <option  v-for="category in categories" :key="category.categoryId" :value="category.categoryId">{{ category.categoryName }}</option>        
      </select>
      </div>
    </div>
    <div class="px-6 py-4 border-t border-gray-200 flex justify-end space-x-4">
      <button
      @click="closeEditModal()"
        class="px-4 py-2 bg-gradient-to-r from-gray-200 via-gray-300 to-gray-400 hover:bg-gradient-to-br text-gray-800 rounded  focus:outline-none focus:ring-2 focus:ring-gray-500"
      >
        Cancel
      </button>
      <button
      @click="updateBook()"   
        class="px-4 py-2 text-white rounded hover:bg-blue-600 focus:outline-none focus:ring-2 bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br"
      >
        Update
      </button>
    </div>
  </div>
</div>
    </div>
    </div>
</template>

<script setup lang="ts">

defineProps([ 'getCategoryName', 'openModal', 'showModal', 'deleteBook','closeModal', 'openEditModal', 'showEditModal', 'closeEditModal',  'categoryId', 'categories','newTitle', 'updateBook', 'newAuthor', 'newCategoryId','books','title','author','categoryId']);

const emit = defineEmits(['update:newTitle', 'update:newAuthor', 'update:newCategoryId']);

</script>
