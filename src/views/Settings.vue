<template>
  <div
    class="order-1 sm:order-3 sm:ml-64 px-4 sm:px-8 py-8 sm:py-16 md:py-32 lg:py-52"
  >
  

    <form @submit.prevent="updateCredentials()" class="space-y-4 ">
      <!-- Input fields for email and password -->
      <div class="flex flex-col space-y-2 justify-center items-center xl:items-start lg:-mt-20 ">
      <div class="relative h-11 min-w-[200px] w-80">
        <input
          type="email"
          v-model="newUsername"
          placeholder=""
          id="email"
          name="email"
          class="peer h-full w-80 rounded-md border border-blue-gray-200 border-t-transparent bg-transparent px-4 py-3 font-sans text-sm font-normal text-blue-gray-700 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-cyan-500 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
        />
        <label
          class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-80 select-none text-[11px] font-normal leading-tight text-blue-gray-400 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-cyan-500 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-cyan-500 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-cyan-500 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
        >
          Email
        </label>
      </div>
      <div class="relative h-11 w-80 min-w-[200px]">
        <input
          type="password"
          v-model="newPass"
          placeholder=""
          class="peer h-full w-80 rounded-md border border-blue-gray-200 border-t-transparent bg-transparent px-3 py-3 font-sans text-sm font-normal text-blue-gray-700 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-cyan-500 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
        />
        <label
          class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-80 select-none text-[11px] font-normal leading-tight text-blue-gray-400 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-cyan-500 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-cyan-500 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-cyan-500 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
        >
          Password
        </label>
      </div>

      <!--Messages-->
      <div class="font-semibold ">
        <p v-if="successMsg" class="text-green-500">{{ successMsg }}</p>
        <p v-else class="text-red-500">{{ errorMsg }}</p>
      </div>


      <!-- Update button -->
      <button
        data-ripple-light="true"
        type="submit"
        :disabled="!newPass && !newUsername"
        class="block w-80 select-none rounded-lg bg-gradient-to-tr from-cyan-600 to-cyan-400 py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white shadow-md shadow-cyan-500/20 transition-all hover:shadow-lg hover:shadow-cyan-500/40 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
      >
        Update
      </button>
    </div>
    </form>


    <!-- Vertical line for LG and larger screens -->
    <div class="absolute inset-y-0 left-1/2 transform -translate-x-1/2 w-[0.2px] bg-black hidden xl:block"></div>
    <!-- Horizontal line for smaller screens -->
    <div class="absolute inset-x-0 top-1/2 transform -translate-y-1/2 h-[0.2px] bg-black block xl:hidden"></div>

    <!-- Upload Profile Picture section -->
    <div class="flex xl:-mt-60 xl:justify-end lg:justify-center md:justify-center  lg:mr-20 lg:mt-[400px] mt-56 md:mt-[400px] justify-center ">
      <p class="text-2xl md:mt-0 font-semibold text-gray-900 flex justify-end -mr-60 pb-2">
        Upload Profile Picture
      </p>
      <form @submit.prevent="changeProfilePicture" class="">
        <div class="relative w-64 mt-10">
          <img
            class="w-64 h-64 rounded-full absolute object-cover"
            :src="profilePictureUrl ? profilePictureUrl : 'https://www.svgrepo.com/show/33565/upload.svg'"
            alt=""
          />
          <label
            for="fileInput"
            class="w-64 h-64 group hover:bg-gray-200 opacity-60 rounded-full absolute flex justify-center items-center cursor-pointer transition duration-500"
          >
            <img class="hidden group-hover:block w-12" src="https://www.svgrepo.com/show/33565/upload.svg" alt="" />
          </label>
          <input id="fileInput" type="file" accept="image/*" name="file" @change="changeProfilePicture" style="display: none" />
        </div>
      </form>
    </div>
    
  </div>

</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { profilePictureUrl,getProfilePicture } from "@/store/picStore";
import axios from "axios";
import { AuthStatus, url } from "@/store/authStore";

const newUsername = ref<string>("");
const successMsg = ref<string>("");
const errorMsg = ref<string>("");
const newPass = ref<string>("");

function updateCredentials() {
  axios
    .put(
      `${url}api/update`,
      {
        UpdatedUsername: newUsername.value,
        UpdatedPassword: newPass.value,
      },
      {
        withCredentials: true,
      }
    )
    .then((response) => {

      successMsg.value = response.data;
      AuthStatus();
    })
    .catch((error) => {
      console.error("Error updating username:", error);
      errorMsg.value = error.response.data;
    })
    .finally(() => {
      newUsername.value = "";
      newPass.value = "";
      setTimeout(() => {
        successMsg.value = "";
        errorMsg.value = "";
      }, 2000);
    });
}

function changeProfilePicture() {
  const formData = new FormData();
  const fileInput = document.querySelector(
    'input[type="file"]'
  ) as HTMLInputElement;
  if (!fileInput) {
    console.error("File input element not found.");
    return;
  }

  const file = fileInput.files?.[0];

  if (!file) {
    console.error("No file selected.");
    return;
  }

  formData.append("file", file);

  axios
    .post(`${url}api/photo`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
      withCredentials: true,
    })
    .then((response) => {
      // Handle success
      getProfilePicture();
    })
    .catch((error) => {
      // Handle error
      console.error("Error uploading photo:", error);
    });
}

onMounted(() => {
  getProfilePicture();
});
</script>
