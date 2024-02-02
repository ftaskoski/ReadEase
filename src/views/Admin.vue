<template>
<div class="order-1 sm:order-2 sm:ml-64 p-4">
    <div class="flex justify-center items-center h-screen">

        <table class="table-auto w-full">

            <thead>
          <tr>
            <th class="px-4 py-2">Author</th>
            <th class="px-4 py-2">Action</th>
          </tr>
        </thead>
        <tbody>
            <tr v-for="user in filteredUsers">
                <td class="border px-4 py-2 text-center">{{ user.username }}</td>
                <td class="border px-4 py-2 text-center"><button @click="deleteUser(user.id)" class="text-white bg-red-500 hover:bg-red-600 focus:ring-4 focus:outline-none focus:ring-red-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center mt-2">Delete User</button></td>
                
            </tr>
        </tbody>
        </table>
    </div>
</div>
</template>

<script setup lang="ts">
import axios from "axios";
import { onMounted, ref,watch } from "vue";
const url = "https://localhost:7284/";
const users = ref<any[]>([]);
const getUser = () => {
  axios
    .get(`${url}api/users`, {
      withCredentials: true,
    })
    .then((response) => {
      users.value = response.data;
      console.log(users);
    })
    .catch((error) => {
      console.error(error);
    });
};

const deleteUser = (id: number) => {

    axios.delete(`${url}api/delete/${id}`, {
        withCredentials: true
    }).then(() => {
        getUser();
    })

}

const filteredUsers = ref<any[]>([]);
watch(users, () => {
  filteredUsers.value = users.value.filter(user => user.username !== 'admin');
});

onMounted(() => {
  getUser();
});
</script>

<style scoped></style>
