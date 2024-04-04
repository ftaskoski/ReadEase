<template>
    <div>
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
              class="text-red-500 rounded-full hover:bg-gray-200 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1"
              type="button"
            >
              Delete
            </button>
            <button
            @click="closeModal()"
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

 defineProps({
  showModal: {
    type: Boolean,
    default: false
  },
  closeModal: {
    type: Function,
    required: true

  },
  deleteBook: {
    type: Function,
    required: true
    
  }
})

</script>

