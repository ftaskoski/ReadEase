<template>



<Card>

<h1 class="text-3xl  mb-4 flex justify-center items-center ">Login</h1>
<form @submit.prevent="login">

  <label class="block mb-2 text-sm font-medium text-gray-900" for="username">Username</label>
  <input class="rounded-lg border-gray-200 border  w-full focus:border-blue-500  focus:outline-none  " type="text" v-model="loginUsername" name="username" id="username">











  <label class="block  text-sm font-medium text-gray-900 mt-2" for="password">Password</label>
  <input class="rounded-lg border-gray-200 border  w-full focus:border-blue-500  focus:outline-none  xcv" type="password" v-model="loginPassword" name="password" id="password">

  <button class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2" type="submit">Submit</button>



  <p v-if="Incorrect" class="text-red-500 mt-2">Incorrect Username or Password</p> 
</form>
</Card>


</template>

<script setup lang="ts">
import { ref,onMounted } from "vue";
import axios from "axios";
import { RouterLink, useRouter } from "vue-router";
import { setAuthenticated, saveUserToLocalStorage, loggedInUser } from "@/store/authStore";
import Card from "@/components/Card.vue";
const router = useRouter();
//work const url = "https://readease-c20240125180045.azurewebsites.net/";
//const url = "https://readease-c20240126222545.azurewebsites.net//";
 const url="https://localhost:7284/"

const loginUsername = ref<string>("");
const loginPassword = ref<string>("");
const Incorrect = ref<boolean>(false);
const Success = ref<boolean>(false);
const login = () : void => {
  axios
    .post(`${url}api/login`, {
      username: loginUsername.value,
      password: loginPassword.value,
    })
    .then((response) => {
      console.log('Login successful:', response);

    saveUserToLocalStorage(response.data);
      setAuthenticated(true);
      loggedInUser.value = response.data;
      console.log(loggedInUser.value.id);
      
        
        Success.value = true;
        router.push("/");
      console.log(Success.value);
    })
    .catch((error) => {
      if (error.response && error.response.status === 401) {
        console.log("Incorrect username or password");
        Incorrect.value = true;
        setTimeout(() => {
          Incorrect.value = false;
        }, 2000);
      } else {
        console.log(`Unexpected error: ${error}`);
      }
    });

  loginUsername.value = "";
  loginPassword.value = "";
};
</script>

