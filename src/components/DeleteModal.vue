<template>
   <div>
        <div v-if="showModal" class="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
          <div class="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50">
          <div class="fixed inset-0 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
  <div class="bg-white rounded-lg shadow-lg w-11/12 sm:w-1/3 md:w-1/2 inset-0 ">
    <div class="px-6 py-4 border-b border-gray-200">
      <h3 class="text-lg font-semibold text-gray-800">Confirm Delete</h3>
    </div>
    <div class="p-6">
      <p class="text-gray-600">{{ bodyText }}</p>
    </div>
    <div class="px-6 py-4 border-t border-gray-200 flex justify-end space-x-4">
      <button
        @click="closeModal()"
        class="px-4 py-2 bg-gradient-to-r from-gray-200 via-gray-300 to-gray-400 hover:bg-gradient-to-br text-gray-800 rounded  focus:outline-none focus:ring-2 focus:ring-gray-500"
      >
        Cancel
      </button>
      <button
      @click="confirmDelete()"
        class="px-4 py-2 bg-gradient-to-r from-red-500 via-red-600 to-red-700 hover:bg-gradient-to-br text-white rounded  focus:outline-none focus:ring-2 focus:ring-red-500"
      >
        Delete
      </button>
    </div>
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
  confirmDelete: {
    type: Function,
    required: true
  },
  bodyText:{
    type: String,
    required: true
  }
    
})

</script>

