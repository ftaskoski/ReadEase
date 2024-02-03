<template>
<div class="order-1 sm:order-2 sm:ml-64 p-4">
  <form @submit.prevent="addCategory">
        <div class="relative w-60 h-10">
          <input
            v-model="newCategory"
            class="peer sm:w-[80%] w-full h-full bg-transparent text-blue-gray-700 font-sans font-normal outline outline-0 focus:outline-0 disabled:bg-blue-gray-50 disabled:border-0 transition-all placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 border focus:border-2 border-t-transparent focus:border-t-transparent text-sm px-3 py-2.5 rounded-[7px] border-blue-gray-200 focus:border-gray-900"
            placeholder=" "
          />
          <label
            class="flex sm:w-[80%] w-full h-full select-none pointer-events-none absolute left-0 font-normal !overflow-visible truncate peer-placeholder-shown:text-blue-gray-500 leading-tight peer-focus:leading-tight peer-disabled:text-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500 transition-all -top-1.5 peer-placeholder-shown:text-sm text-[11px] peer-focus:text-[11px] before:content[' '] before:block before:flex-shrink before:box-border before:w-2.5 before:h-1.5 before:mt-[6.5px] before:mr-1 peer-placeholder-shown:before:border-transparent before:rounded-tl-md before:border-t peer-focus:before:border-t-2 before:border-l peer-focus:before:border-l-2 before:pointer-events-none before:transition-all peer-disabled:before:border-transparent after:content[' '] after:block after:flex-grow after:box-border after:w-2.5 after:h-1.5 after:mt-[6.5px] after:ml-1 peer-placeholder-shown:after:border-transparent after:rounded-tr-md after:border-t peer-focus:after:border-t-2 after:border-r peer-focus:after:border-r-2 after:pointer-events-none after:transition-all peer-disabled:after:border-transparent peer-placeholder-shown:leading-[3.75] text-gray-500 peer-focus:text-gray-900 before:border-blue-gray-200 peer-focus:before:!border-gray-900 after:border-blue-gray-200 peer-focus:after:!border-gray-900"
          >
            Add New Category
          </label>
          
        </div>
    </form>
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
const newCategory= ref<string>("");
const addCategory = () =>{
  axios.post(`${url}api/insertcategory`,{
    categoryName: newCategory.value
  },{
    withCredentials: true
  }
  ).then((response)=>{
    newCategory.value="";
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
