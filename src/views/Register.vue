<template>
    <div class="flex justify-center items-center h-screen">
    <div
      class="relative flex w-[400px] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    >
      <div
        class="relative mx-4 -mt-6 mb-4 grid h-28 place-items-center overflow-hidden rounded-xl bg-gradient-to-tr from-cyan-600 to-cyan-400 bg-clip-border text-white shadow-lg shadow-cyan-500/40"
      >
        <h3
          class="block font-sans text-3xl font-semibold leading-snug tracking-normal text-white antialiased"
        >
          Create Account
        </h3>
      </div>
      <div class="flex flex-col gap-4 p-6">

        <form @submit.prevent="register()" class="space-y-4">
  <div class="relative h-11 w-full min-w-[200px]">
    <input
      type="email"
      v-model="username"
      placeholder=""
      id="email"
      name="email"
      class="peer h-full w-full rounded-md border border-blue-gray-200 border-t-transparent bg-transparent px-4 py-3 font-sans text-sm font-normal text-blue-gray-700 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-cyan-500 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 "
    />
    <label
      class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none text-[11px] font-normal leading-tight text-blue-gray-400 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-cyan-500 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-cyan-500 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-cyan-500 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
    >
      Email
    </label>
  </div>
  <div class="relative h-11 w-full min-w-[200px]">
    <input
      type="password"
      v-model="password"
      placeholder=""
      class="peer h-full w-full rounded-md border border-blue-gray-200 border-t-transparent bg-transparent px-3 py-3 font-sans text-sm font-normal text-blue-gray-700 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-cyan-500 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
    />
    <label
      class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none text-[11px] font-normal leading-tight text-blue-gray-400 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-cyan-500 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-cyan-500 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-cyan-500 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
    >
      Password
    </label>
  </div>
  <button
    data-ripple-light="true"
    type="submit"
    :disabled="loading"
    :class="{ 'cursor-not-allowed': loading, 'opacity-50': loading}"
    class="block w-full select-none rounded-lg bg-gradient-to-tr from-cyan-600 to-cyan-400 py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white shadow-md shadow-cyan-500/20 transition-all hover:shadow-lg hover:shadow-cyan-500/40 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
  >
    Sign Up
  </button>
</form>


        <div class="-ml-2.5"></div>
      </div>
      <div class="p-6 pt-0">
        <div v-if="errorMsg">
          <p class="text-red-500 justify-center items-center flex mb-2">{{ errorMsg }}</p>
        </div>

        <p
          class="mt-6 flex justify-center font-sans text-sm font-light leading-normal text-inherit antialiased"
        >
          Alredy have an account?
          <RouterLink
            to="/login"
            class="ml-1 block font-sans text-sm font-bold leading-normal text-cyan-500 antialiased"
          >
            Sign in
          </RouterLink>
        </p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import {
  AuthStatus,
  isAuthenticated
} from "@/store/authStore";
import { RouterLink, useRouter } from "vue-router";

const router = useRouter();
const username = ref<string>("");
const password = ref<string>("");
// const url = "https://readease-c20240125180045.azurewebsites.net/";
const url = "https://localhost:7284/";
const errorMsg=ref<string>("")
const loading = ref<boolean>(false);

const register = () => {
  loading.value = true;
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
      await AuthStatus();
      if (isAuthenticated.value) {
        router.push("/");
      }
      router.push("/");
      loading.value = false;
    }).catch(error =>{

          errorMsg.value=error.response.data
          setTimeout(() => {
            errorMsg.value=""
          }, 2000);

    })
};
</script>
