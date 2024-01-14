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
import { loadUserFromLocalStorage } from "@/store/authStore";
const newUsername = ref<string>("");
    const url = "http://localhost:5000/";
    const user = loadUserFromLocalStorage();
const id = user ? user.id : null;
const password = user.password;
function changeUsername(){
    axios.put(`${url}api/update/${id}`, {
        username: newUsername.value,
        id: id,
        password: password

    }).then((response) => {
        console.log(response);
    })
}
</script>

