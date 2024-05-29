<template>
  <div class="flex justify-center items-center h-screen">
    <div
      class="relative flex w-[400px] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    >
      <div
        class="relative mx-4 -mt-6 mb-4 grid h-28 place-items-center overflow-hidden rounded-xl bg-blue-600 bg-clip-border text-white shadow-lg"
      >
        <h3
          class="block font-sans text-3xl font-semibold leading-snug tracking-normal text-white antialiased"
        >
          Recover Password
        </h3>
      </div>
      <div class="flex flex-col p-6 space-y-4">
        <form @submit.prevent="recovery()" class="space-y-4">
          <div class="relative h-11 w-full min-w-[200px]">
            <input
              required
              v-model="email"
              type="email"
              placeholder=""
              id="email"
              name="email"
              class="peer h-full w-full rounded-md border border-blue-gray-200 border-t-transparent bg-transparent px-4 py-3  text-sm font-normal text-blue-gray-700 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-blue-500 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            />
            <label
              class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none text-[11px] font-normal leading-tight text-blue-gray-400 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-blue-500 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-blue-500 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-blue-500 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
            >
              Email
            </label>
          </div>

          <button
            data-ripple-light="true"
            type="submit"
            :disabled="loading"
            class="block w-full select-none rounded-lg bg-blue-600 py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white shadow-md  transition-all hover:bg-blue-700 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
          >
            Recover Now
          </button>
        </form>
        <div v-if="successMsg" class="text-center text-green-500">
          {{ successMsg }}
        </div>
        <div v-else class="text-center text-red-500">{{ errorMsg }}</div>
        <p
          class="mt-6 flex justify-center font-sans text-sm font-light leading-normal text-inherit antialiased"
        >
          Back to the 
          <RouterLink
            to="/login"
            class="ml-1 block font-sans text-sm font-bold leading-normal text-blue-500 antialiased hover:underline"
          >
            Login
          </RouterLink>
        </p> 
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { url } from "../store/authStore";

const email = ref<string>("");
const successMsg = ref<string>("");
const errorMsg = ref<string>("");
const loading = ref<boolean>(false);

function recovery(): void {
  loading.value = true;

  axios
    .post(`${url}api/recover`, {
      email: email.value,
    })
    .then((respone) => {
      successMsg.value = respone.data;
    })
    .catch((error) => {
      errorMsg.value = error.response.data;
    })
    .finally(() => {
      email.value = "";
      loading.value = false;
      setTimeout(() => {
          successMsg.value = "";
          errorMsg.value = "";
        }, 2000);
    });
}
</script>
