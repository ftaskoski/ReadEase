<template>
    <Card class="justify-center items-center h-screen">
      <div class="w-full">
          <div class="text-center">
              <h1 class="text-3xl font-semibold text-gray-900">Sign Up</h1>
              <p class="mt-2 text-gray-500">Don&#x27;t have an account yet? Create one below</p>
          </div>
          <div class="mt-5">
              <form @submit.prevent="register" >
                  <div class="relative mt-6 ">
                      <input v-model="username" id="email" placeholder="Username"  class="peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none" autocomplete="NA" />
                      <label for="email" class="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800">Username</label>
                  </div>
                  <div class="relative mt-6">
                      <input v-model="password" type="password" name="password" id="password" placeholder="Password" class="peer peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none" />
                      <label for="password" class="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800">Password</label>
                  </div>
                  <div class="my-6">
                      <button type="submit" class="w-full rounded-md bg-black px-3 py-4 text-white focus:bg-gray-600 focus:outline-none">Sign up</button>
                  </div>
                  <p class="text-center text-sm text-gray-500">Already have an account?
                      <RouterLink to="/login"
                          class="font-semibold text-gray-600 hover:underline focus:text-gray-800 focus:outline-none">Sign
                          in
                      </RouterLink>.
                      <p v-if="errorMsg" class="text-red-500">{{errorMsg}}</p>
                  </p>
              </form>
          </div>
      </div>
    </Card>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import Card from "@/components/Card.vue";
import {
  setAuthenticated,
  saveUserToCookie,
  loggedInUser,
  role,
  getRole
} from "@/store/authStore";
import { RouterLink, useRouter } from "vue-router";

const router = useRouter();
const username = ref<string>("");
const password = ref<string>("");
// const url = "https://readease-c20240125180045.azurewebsites.net/";
const url = "https://localhost:7284/";
const errorMsg=ref<string>("")


const register = () => {
    if (password.value.length < 3) {
    errorMsg.value="Password too short"
    setTimeout(() => {
        errorMsg.value=""
    }, 2000);
    return;
  }
    axios
    .post(
      `${url}api/register`,
      {
        username: username.value,
        password: password.value,
      },
      { withCredentials: true }
    )
    .then(async(response) => {
      await saveUserToCookie(response.data);
      await setAuthenticated(true);
      loggedInUser.value = response.data;

      role.value = await getRole(url);
      router.push("/");
    }).catch(error =>{

        console.log(error);
        if(error.response && error.response.status === 409){
          errorMsg.value="User already exists"
          setTimeout(() => {
            errorMsg.value=""
          }, 2000);
        }

    })
};
</script>
