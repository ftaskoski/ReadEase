<template>

<transition name="fade">
    <div v-if="Incorrect" @click="Incorrect = false" class="flex items-center py-4 justify-center absolute bg-red-500 text-white inset-x-4 top-0 rounded-md shadow-md shadow-red-300">
      <p>Incorrect username or password</p>
    </div>
  </transition>

  <transition name="fade">
    <div v-if="Success" @click="Success = false" class="flex items-center py-4 justify-center absolute bg-green-500 text-white inset-x-4 top-0 rounded-md shadow-md shadow-green-300">
      <p>Successfully logged in</p>
    </div>
  </transition>




  <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
    <div class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
      <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
        <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
          Sign in to your account
        </h1>
        <form class="space-y-4 md:space-y-6" action="post" @submit.prevent="login">
          <div>
            <label for="email" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your Username</label>
            <input v-model="loginUsername" type="text" name="email" id="email" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your username">
          </div>
          <div>
            <label for="password" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
            <input v-model="loginPassword" type="password" name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
          </div>

          <button type="submit" class="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">
            Sign in
          </button>
          <p class="text-sm font-light text-gray-500 dark:text-gray-400">
            Don’t have an account yet? <RouterLink to="/register" href="#" class="font-medium text-primary-600 hover:underline dark:text-primary-500">Sign up</RouterLink>
          </p>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { RouterLink, useRouter } from "vue-router";
import { setAuthenticated, saveUserToLocalStorage, loggedInUser } from "@/store/authStore";
const router = useRouter();
const url = "http://localhost:5000/";
const loginUsername = ref<string>("");
const loginPassword = ref<string>("");
const Incorrect = ref<boolean>(false);
const Success = ref<boolean>(false);
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
      
      setTimeout(() => {
        
        Success.value = true;
        router.push("/");
      }, 2000);
      console.log(Success.value);
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
