<template>
  <div>
    <RouterLink to="/about">About</RouterLink>
    <RouterLink to="/books">Books</RouterLink>
    <button @click="logout">Logout</button>
  </div>
</template>

<script setup lang="ts">
import { setAuthenticated,clearUserFromLocalStorage } from '@/store/authStore';
import { useRouter } from "vue-router";
const router = useRouter();
const logout = () : void => {
  clearUserFromLocalStorage();
  router.push("/login");
  setAuthenticated(false);
}

</script>
