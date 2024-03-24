<template>
  <div class="pagination flex justify-center items-center mt-4 text-sm text-gray-700">
    <button
      @click="changePage(currPage - 1)"
      :disabled="currPage === 1"
      :class="{ 'cursor-not-allowed': currPage === 1, 'opacity-50': currPage === 1 }"
      class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4 rounded-l border border-black"
    >
      Previous
    </button>
    <div class="flex">
      <button
        v-for="page in visiblePages"
        :key="page"
        @click="changePage(page)"
        :disabled="currPage === page"
        :class="{ 'cursor-not-allowed': currPage === page, 'bg-sky-200': currPage === page }"
        class="bg-gray-50 hover:bg-sky-200 font-semibold py-2 px-4 border border-black"
      >
        {{ page }}
      </button>
    </div>
    <button
      @click="changePage(currPage + 1)"
      :disabled="currPage === totalPages"
      :class="{ 'cursor-not-allowed': currPage === totalPages, 'opacity-50': currPage === totalPages }"
      class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4 rounded-r border border-black"
    >
      Next
    </button>
  </div>
</template>

<script setup>
import { defineProps, defineEmits } from "vue";


defineProps(["currPage", "totalPages", "visiblePages"]);


const emits = defineEmits(["page-changed"]);


const changePage = (page ) => {
    emits("page-changed", page);
};
</script>

