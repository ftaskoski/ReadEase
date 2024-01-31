<template>
    <div>
        <Card>
            <p class="text-3xl  mb-4 flex justify-center items-center ">Register</p>
            <form @submit.prevent="register">

<label class="block mb-2 text-sm font-medium text-gray-900" for="username">Username</label>
<input class="rounded-lg border-gray-200 border  w-full focus:border-blue-500  focus:outline-none  " type="text" v-model="username" name="username" id="username">











<label class="block  text-sm font-medium text-gray-900 mt-2" for="password">Password</label>
<input class="rounded-lg border-gray-200 border  w-full focus:border-blue-500  focus:outline-none  xcv" type="password" v-model="password" name="password" id="password">

<button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" type="submit">Submit</button>

<span class="text-sm mt-2 block">Alredy have an account? <RouterLink to="/login"><span class="text-blue-500 hover:text-blue-700 hover:underline">Login</span></RouterLink></span>

</form>
        </Card>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import axios from 'axios';
import Card from "@/components/Card.vue";
import { setAuthenticated, saveUserToCookie, loggedInUser } from "@/store/authStore";
import { RouterLink, useRouter } from "vue-router";

const router = useRouter();

const username = ref<string>('');
const password = ref<string>('');
    // const url = "https://readease-c20240125180045.azurewebsites.net/";
    const url="https://localhost:7284/"

const register = () => {
    axios.post(`${url}api/register`, {
        username: username.value,
        password: password.value
    },{withCredentials:true}).then((response) => {
        console.log(response.data)
        saveUserToCookie(response.data);
      setAuthenticated(true);
      loggedInUser.value = response.data;
      console.log(loggedInUser.value.id);
      
        
        router.push("/");

    })
}
</script>

