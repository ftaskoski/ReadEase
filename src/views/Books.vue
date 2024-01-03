<template>
  <div>
    <p>Insert books</p>
    <form @submit.prevent="addBook">
      <label for="author">Author</label>
      <input type="text" id="author" v-model="author" required />

      <label for="title">Title</label>
      <input type="text" id="title" v-model="title" required />

      <button type="submit">Submit</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { ref, onMounted } from "vue";
import { loadUserFromLocalStorage } from "@/store/authStore";

const url = "http://localhost:5000/";
const author = ref<string>("");
const title = ref<string>("");
const user = loadUserFromLocalStorage();
const id = user ? user.id : null;
interface ClinicTab {
  key: string;
  displayName: string;
}
const clinicTabs: ClinicTab[] = [
  {
    key: "items",
    displayName: "Items",
  },
  {
    key: "breeds",
    displayName: "Breeds",
  },
  {
    key: "colors",
    displayName: "Colors",
  },
  {
    key: "deathCauses",
    displayName: "Deceased Reasons",
  },
  {
    key: "declinedReasons",
    displayName: "Declined Reasons",
  },
  {
    key: "rabiesManufacturers",
    displayName: "Rabies Manufacturers",
  },
  {
    key: "microchipProviders",
    displayName: "Microchip Providers",
  },
];

const selectedTab = ref<string | null>(null);

const addBook = () => {
  axios
    .post(`${url}api/insertbook/${id}`, {
      author: author.value,
      title: title.value,
    })
    .then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.error(error);
    });
};

onMounted(() => {
  axios.get(`${url}api/getbooks/${id}`).then((response) => {
    console.log(response.data);
  });
});
</script>
