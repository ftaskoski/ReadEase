<template>
  <div
    class="order-1 sm:order-3 sm:ml-64 px-4 sm:px-8 py-8 sm:py-16 md:py-32 lg:py-52"
  >
    <form @submit.prevent="changeUsername" class="max-w-md lg:mx-10 mx-auto">
      <div class="w-full">

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
   
    </form>
    <p class="text-2xl mt-6 md:mt-0 font-semibold text-gray-900 flex justify-center pb-2">Upload Profile Picture</p>
    <div class="mx-auto w-64 text-center ">
    <form @submit.prevent="changeProfilePicture">
      <div class="relative w-64  ">
      <img class="w-64 h-64 rounded-full absolute object-cover" :src="profilePictureUrl ? profilePictureUrl : 'https://www.svgrepo.com/show/33565/upload.svg'" alt="" />
      <label for="fileInput" class="w-64 h-64 group hover:bg-gray-200 opacity-60 rounded-full absolute flex  justify-center items-center cursor-pointer transition duration-500">
        <img class="hidden group-hover:block w-12" src="https://www.svgrepo.com/show/33565/upload.svg" alt="" />
      </label>
      <input id="fileInput" type="file" accept="image/*" name="file" @change="changeProfilePicture" style="display: none;" />
    </div>




    </form>
</div>

  </div>
  





</template>

<script setup lang="ts">
import { ref,onMounted } from "vue";
import axios from "axios";
const newUsername = ref<string>("");
const url = "https://localhost:7284/";
const profilePictureUrl = ref<string | null>(null);

function changeUsername() {
  axios
    .put(
      `${url}api/update`,
      {
       UpdatedUsername: newUsername.value,
      },
      {
        withCredentials: true,
      }
    )
    .then((response) => {
      newUsername.value = "";
    })
    .catch((error) => {
      console.error('Error updating username:', error);
    });
}

function changeProfilePicture() {
  const formData = new FormData();
  const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
  if (!fileInput) {
    console.error('File input element not found.');
    return;
  }

  const file = fileInput.files?.[0];

  if (!file) {
    console.error('No file selected.');
    return;
  }

  formData.append('file', file);

  axios
    .post(
      `${url}api/photo`,
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
        withCredentials: true,
      }
    )
    .then((response) => {
      // Handle success
      getProfilePicture();
    })
    .catch((error) => {
      // Handle error
      console.error('Error uploading photo:', error);
    });
}

function getProfilePicture() {
  axios
    .get(`${url}api/getphoto`, {
      withCredentials: true,
      responseType: 'blob' // Ensure response is treated as a blob
    })
    .then((response) => {
      // Convert blob to URL
      const blob = new Blob([response.data]);
      profilePictureUrl.value = URL.createObjectURL(blob);
    })
    .catch((error) => {
      console.error('Error retrieving photos:', error);
    });
}


onMounted(() => {
  getProfilePicture();
});
</script>

