<template>
  <div class="order-1 sm:order-3 sm:ml-64 px-4 sm:px-8 py-8 sm:py-16 md:py-32 lg:py-52">
    <form @submit.prevent="changeUsername" class="max-w-md lg:mx-10 mx-auto">
      <div class="w-full">
        <!-- Current Username Input -->
        <div class="relative w-full min-w-[200px] h-10 mb-6">
          <input
            v-model="currentUsername"
            class="peer sm:w-[80%] w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
            placeholder=" "
          />
          <label
            class="flex sm:w-[80%] w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
            :class="{ 'placeholder-shown': !currentUsername }"
          >
            Current Username
          </label>
        </div>

        <!-- New Username Input -->
        <div class="relative w-full h-10">
          <input
            v-model="newUsername"
            class="peer sm:w-[80%] w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
            placeholder=" "
          />
          <label
            class="flex sm:w-[80%] w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
            :class="{ 'placeholder-shown': !newUsername }"
          >
            New Username
          </label>
        </div>
      </div>

      <button
        class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2"
        type="submit"
      >
        Submit
      </button>
      <div class="absolute sm:left-[55%] sm:top-[20%]   sm:w-[1px] sm:h-[60%] bg-gray-600"></div>
    </form>
  </div>

</template>


<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import {
  loadUserFromCookie,
  saveUserToCookie,
} from "@/store/authStore";
const newUsername = ref<string>("");
const url = "https://localhost:7284/";
const user = loadUserFromCookie();
const id = user ? user.id : null;
const password = user.password;
const currentUsername = ref<string>();


async function changeUsername() {
  await axios
    .put(
      `${url}api/update/${id}`,
      {
        username: newUsername.value,
        password: password,
      },
      {withCredentials: true}
    )
    .then((response) => {
      const updatedUser = { ...user, username: newUsername.value };
      saveUserToCookie(updatedUser);
      newUsername.value = "";
    });
}
</script>
