<template>
  <div>
    <RouterLink class="mr-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" to="/settings">Settings</RouterLink>
    <RouterLink class="mr-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" to="/books">Books</RouterLink>
    <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" @click="logout">Logout</button>
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
