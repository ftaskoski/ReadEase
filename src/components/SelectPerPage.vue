<template>
  <div>
    <select
      v-model="selectedValue"
      @change="handleChange"
      class="w-80 px-4 py-2.5 rounded-full border border-gray-300 focus:outline-none focus:border-blue-500 transition duration-300"
    >
      <option v-for="item in props.itemsPerPageArr" :key="item" :value="item">
        {{ item }}
      </option>
    </select>
  </div>
</template>

<script setup>
import { ref, defineProps, defineEmits } from 'vue';

const props = defineProps(['itemsPerPage', 'itemsPerPageArr']);
const emits = defineEmits(['update:itemsPerPage']);

const selectedValue = ref(props.itemsPerPage);

function handleChange(event) {
  selectedValue.value = event.target.value;
  emits('update:itemsPerPage', selectedValue.value);
}
</script>
