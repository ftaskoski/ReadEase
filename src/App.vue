<script setup lang="ts">
import { RouterView } from "vue-router";
import sideBar from "./components/sideBar.vue";
</script>

<template>
  <div>
    <sideBar />
  <RouterView />
</div>
</template>
