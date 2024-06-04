<template>
  <div>
  <div
    class="order-1 sm:order-3 sm:ml-64 px-4 sm:px-8 py-8 sm:py-16 md:py-32 lg:py-42"
  >
  <div class="flex justify-center items-center">
  <div class="bg-white p-8 rounded-lg shadow-lg flex flex-col items-center w-full max-w-xl">
        <form @submit.prevent="changeProfilePicture">    
        <div class="relative mb-6">
            <label for="profile-picture" class="block text-sm font-medium text-gray-700 mb-2 text-center">Upload Profile Picture</label>
            <input @change="changeProfilePicture" type="file" id="profile-picture" name="profile-picture" class="hidden" accept="image/*">
            <div class="relative w-32 h-32 rounded-full overflow-hidden border-4 border-blue-500 cursor-pointer" onclick="document.getElementById('profile-picture').click();">
                <img :src="profilePictureUrl ? profilePictureUrl : 'https://www.svgrepo.com/show/33565/upload.svg'" alt="Profile Picture" class="w-full h-full object-cover">
                <div class="absolute inset-0 flex items-center justify-center opacity-0 hover:opacity-100 transition-opacity bg-black bg-opacity-50 rounded-full">
                    <span class="text-white text-sm">Change</span>
                </div>
            </div>
        </div>
      </form>
        <form @submit.prevent="updateCredentials()" class="w-full">
            <div class="mb-4">
                <label for="email" class="block text-sm font-medium text-gray-700 mb-2">Email</label>
                <input v-model="newUsername" type="email" id="email" name="email" class="shadow-sm focus:ring-blue-500 focus:border-blue-500 block w-full sm:text-sm border-gray-300 border rounded-md p-2" placeholder="New Email">
            </div>
            <div class="mb-6">
                <label for="password" class="block text-sm font-medium text-gray-700 mb-2">Password</label>
                <input v-model="newPass" type="password" id="password" name="password" class="shadow-sm focus:ring-blue-500 focus:border-blue-500 block w-full sm:text-sm border-gray-300 rounded-md p-2 border" placeholder="New Password">
            </div>
            <button type="submit" class="w-full bg-gradient-to-r from-blue-500 to-purple-500 hover:from-blue-600 hover:to-purple-600 text-white font-semibold py-2 px-4 rounded-md shadow-md">
                Update
            </button>
        </form>
        <div v-if="successMsg" class="text-green-500 text-center mt-6">{{ successMsg }}</div>
        <div v-if="errorMsg" class="text-red-500 text-center mt-6">{{ errorMsg }}</div>
    </div>
  </div>
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
      getProfilePicture();
    })
    .catch((error) => {
      console.error("Error uploading photo:", error);
    });
}

onMounted(() => {
  getProfilePicture();
});
</script>
