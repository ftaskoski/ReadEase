<template>
  <Card class="justify-center items-center h-screen">
      <div class="w-full">
          <div class="text-center">
              <h1 class="text-3xl font-semibold text-gray-900">Sign in</h1>
              <p class="mt-2 text-gray-500">Sign in below to access your account</p>
          </div>
          <div class="mt-5">
              <form @submit.prevent="login" >
                  <div class="relative mt-6 ">
                      <input v-model="loginUsername" id="email" placeholder="Username"  class="peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none" autocomplete="NA" />
                      <label for="email" class="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800">Username</label>
                  </div>
                  <div class="relative mt-6">
                      <input v-model="loginPassword" type="password" name="password" id="password" placeholder="Password" class="peer peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none" />
                      <label for="password" class="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800">Password</label>
                  </div>
                  <div class="my-6">
                      <button type="submit" class="w-full rounded-md bg-black px-3 py-4 text-white focus:bg-gray-600 focus:outline-none">Sign in</button>
                  </div>
                  <p class="text-center text-sm text-gray-500">Don&#x27;t have an account yet?
                      <RouterLink to="/register"
                          class="font-semibold text-gray-600 hover:underline focus:text-gray-800 focus:outline-none">Sign
                          up
                      </RouterLink>
                  </p>
              </form>
          </div>
      </div>
    </Card>
    </template>
  
  
  <script setup lang="ts">
  import { ref,onMounted } from "vue";
  import axios from "axios";
  import { RouterLink, useRouter } from "vue-router";
  import { AuthStatus,isAuthenticated } from "@/store/authStore";
  import Card from "@/components/Card.vue";
  const router = useRouter();
  //work const url = "https://readease-c20240125180045.azurewebsites.net/";
  //const url = "https://readease-c20240126222545.azurewebsites.net//";
   const url="https://localhost:7284/"
  
  const loginUsername = ref<string>("");
  const loginPassword = ref<string>("");
  const Incorrect = ref<boolean>(false);
  const Success = ref<boolean>(false);
    const login = async () => {
  axios
    .post(`${url}api/login`, {
      username: loginUsername.value,
      password: loginPassword.value,
    },{
      withCredentials: true
    })
    .then(async (response) => {

      await AuthStatus();
      if (isAuthenticated.value) {
        router.push("/");
      }
    })
    .catch((error) => {
      if (error.response && error.response.status === 401) {
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
  
  