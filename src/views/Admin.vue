<template>
  <div>hi</div>
</template>

<script setup lang="ts">
import axios from "axios";
import { onMounted, ref } from "vue";
const url = "https://localhost:7284/";
const users = ref<any[]>([]);
const getUser = () => {
  axios
    .get(`${url}api/users`, {
      withCredentials: true,
    })
    .then((response) => {
      users.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};
onMounted(() => {
  getUser();
});
</script>

<style scoped></style>
