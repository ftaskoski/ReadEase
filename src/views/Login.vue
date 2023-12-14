<template>
  <div class="">
    <form @submit.prevent="login">
      <h1>login</h1>
      <label for="username" class="">Username:</label>
      <input
        v-model="loginUsername"
        class="border border-black rounded " 
        type="text"
        id="username"
        required
      />

      <label for="password">Password:</label>
      <input v-model="loginPassword" type="password" id="password" required />

      <button type="submit">Submit</button>
    </form>
    <div v-if="Incorrect" class="error-message">
      <p>Incorrect username or password</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
import { setAuthenticated, saveUserToLocalStorage } from "@/store/authStore";
const router = useRouter();
const url = "http://localhost:5000/";
const loginUsername = ref<string>("");
const loginPassword = ref<string>("");
const Incorrect = ref<boolean>(false);
const login = () => {
  axios
    .post(`${url}api/login`, {
      username: loginUsername.value,
      password: loginPassword.value,
    })
    .then((response) => {
      saveUserToLocalStorage(response.data);
      setAuthenticated(true);
      router.push("/");
    })
    .catch((error) => {
      if (error.response && error.response.status === 401) {
        console.log("Incorrect username or password");
        Incorrect.value = true;
        setTimeout(() => {
          Incorrect.value = false;
        }, 3000);
      } else {
        console.log(`Unexpected error: ${error}`);
      }
    });
  loginUsername.value = "";
  loginPassword.value = "";
};
</script>
