<template>
    <div>
        <div v-if="showEditModal" class="fixed  inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
      <div class="relative w-auto max-w-md  mx-auto my-6">
        <!-- Content -->
        <div class="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
          <!-- Header -->
          <div class="flex items-start justify-between p-5 border-b border-solid rounded-t border-blueGray-200">
            <h3 class="text-3xl font-semibold">
             Edit book
            </h3>
            <button
              class="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
            >
              <span class="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
            </button>
          </div>
          <!-- Body -->
          <div class="relative p-6 flex-auto">  
            <input type="text"  :value="title"  @input="$emit('update:newTitle', ($event.target as HTMLInputElement).value)" class="mb-4 appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Title" >
            <input type="text" :value="newAuthor" @input="$emit('update:newAuthor', ($event.target as HTMLInputElement).value)" class="mb-4 appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Author">
            <select :value="newCategoryId" @change="$emit('update:newCategoryId', ($event.target as HTMLInputElement).value)"  class="mb-4 appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline">
              <option value="0" disabled selected hidden>Select a Category</option>
              <option value="0">Select category</option>
              <option  v-for="category in categories" :key="category.categoryId" :value="category.categoryId">{{ category.categoryName }}</option>
            </select>
          </div>
          <!-- Footer -->
          <div class="flex items-center justify-end p-6 border-t border-solid rounded-b border-blueGray-200">
            <button
              class="text-blue-500 rounded-full hover:bg-gray-200 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
              @click="updateBook()"
            >
              Update
            </button>
            <button
            @click="closeEditModal()"
              class="text-blueGray-500 rounded-full hover:bg-gray-200 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
    </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
const props = defineProps([ 'getCategoryName', 'openModal', 'showModal', 'deleteBook','closeModal', 'openEditModal', 'showEditModal', 'closeEditModal',  'categoryId', 'categories','newTitle', 'updateBook', 'newAuthor', 'newCategoryId','books','title']);

const emit = defineEmits(['update:newTitle', 'update:newAuthor', 'update:newCategoryId']);

</script>
