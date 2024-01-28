<template>
    <div class="flex justify-center items-center h-screen">
     <form @submit.prevent="changeUsername">
      <label for="username">Username</label>
      <input type="text" v-model="newUsername" name="username" id="username">
      <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" type="submit">Submit</button>
     </form>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { loadUserFromLocalStorage, saveUserToLocalStorage } from "@/store/authStore";
const newUsername = ref<string>("");
    const url="https://localhost:7284/"
    const user = loadUserFromLocalStorage();
const id = user ? user.user.id : null;
const token = user ? user.token : null;
const password = user.user.password;
const authorization = `Bearer ${token}`;

async function changeUsername() {
         await axios.put(`${url}api/update/${id}`, {
            username: newUsername.value,
            password: password
            
        }, { headers: { Authorization: authorization } }).then((response) => {
            const updatedUser = { ...user, username: newUsername.value };
            saveUserToLocalStorage(updatedUser);
            newUsername.value = "";
        })


  
}
</script>

