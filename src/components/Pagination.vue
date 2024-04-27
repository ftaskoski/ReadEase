<template>
  <div class="pagination flex justify-center items-center mt-4 text-sm text-gray-700">
    <button @click="changePage(1)" :disabled="currPage === 1" class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4 border border-black rounded-l" :class="{ 'cursor-not-allowed': currPage === 1, 'opacity-50': currPage === 1 }">
      <i class="fa-solid fa-angle-double-left"></i>
      </button>
    <button
      @click="changePage(currPage - 1)"
      :disabled="currPage === 1"
      :class="{ 'cursor-not-allowed': currPage === 1, 'opacity-50': currPage === 1 }"
      class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4  border border-black"
    >
    <i class="fa-solid fa-angle-left"></i>
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
      class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4  border border-black"
    >
    <i class="fa-solid fa-angle-right"></i>
    </button>
    <button @click="changePage(totalPages)" class="bg-gray-50 hover:bg-gray-200 font-semibold py-2 px-4 border border-black rounded-r" :disabled="currPage === totalPages" :class="{ 'cursor-not-allowed': currPage === totalPages, 'opacity-50': currPage === totalPages }">
      <i class="fa-solid fa-angle-double-right"></i>
    </button>
  </div>
</template>

<script setup>


defineProps(["currPage", "totalPages", "visiblePages"]);


const emits = defineEmits(["page-changed","to-end"]);


const changePage = (page ) => {
    emits("page-changed", page);
};

</script>

