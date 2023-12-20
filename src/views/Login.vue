<template>
  <transition name="fade">
  <div v-if="Incorrect" @click="Incorrect = false" class="flex items-center py-4 justify-center absolute bg-red-500 text-white inset-x-4 top-0 rounded-md shadow-md shadow-red-300 ">
    <p>Incorrect username or password</p>
  </div>
</transition >
  <div class="flex items-center justify-center h-screen">
    <div class="p-12 border-8 border-black shadow-2xl rounded">
      <form @submit.prevent="login">
        <h1 class="flex items-center justify-center">login</h1>
        <label for="username" class="">Username:</label>
        <input
          v-model="loginUsername"
          class="border border-black rounded"
          type="text"
          id="username"
          required
        />
        <label class="" for="password">Password:</label>
        <input
          v-model="loginPassword"
          class="border border-red-500 rounded"
          type="password"
          id="password"
          required
        />

        <button type="submit">Submit</button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
import { setAuthenticated, saveUserToLocalStorage,loggedInUser } from "@/store/authStore";
const router = useRouter();
const url = "http://localhost:5000/";
const loginUsername = ref<string>("");
const loginPassword = ref<string>("");
const Incorrect = ref<boolean>(false);
const login = () => {
  axios
    .post(`${url}api/login`, {
      username: loginUsername.value,
      password: loginPassword.value,
    })
    .then((response) => {
      saveUserToLocalStorage(response.data);
      setAuthenticated(true);
      loggedInUser.value = response.data;
      console.log(loggedInUser.value.id);
      router.push("/");
    })
    .catch((error) => {
      if (error.response && error.response.status === 401) {
        console.log("Incorrect username or password");
        Incorrect.value = true;
        setTimeout(() => {
          Incorrect.value = false;
        }, 2000);
      } else {
        console.log(`Unexpected error: ${error}`);
      }
    });
  loginUsername.value = "";
  loginPassword.value = "";
};
</script>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 1s ease-in-out;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}


</style>