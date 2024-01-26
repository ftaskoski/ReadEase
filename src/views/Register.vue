<template>
    <div>
        <h1>Register</h1>
        <form action="post" @submit.prevent="register">
            <label for="username">Username</label>
            <input type="text" v-model="username" name="username" id="username">
            <label for="password">Password</label>
            <input type="password" v-model="password" name="password" id="password">
            <button type="submit">Submit</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import axios from 'axios';

const username = ref<string>('');
const password = ref<string>('');
    const url = "https://readease-c20240125180045.azurewebsites.net/";

const register = () => {
    axios.post(`${url}api/register`, {
        username: username.value,
        password: password.value
    }).then((response) => {
        console.log(response.data)
    })
}
</script>

