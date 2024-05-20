<template>
    <div class="overflow-x-auto mt-4">

      <table class="min-w-full bg-white">
        <thead class="bg-gray-200 text-gray-600">
        <tr>
          <th
            class="py-2 px-4 text-left"
          >
            Author
          </th>
          <th
            class="py-2 px-4 text-left"
          >
            Title
          </th>
          <th
            class="py-2 px-4 text-left"
          >
            Category
          </th>
          <th
            class="py-2 px-4 text-center"
          >
            Actions
          </th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr class="hover:bg-gray-100" v-for="book in books" :key="book.bookId">
          <td class="px-2 py-2 sm:px-3 sm:py-3">{{ book.author }}</td>
          <td class="px-2 py-2 sm:px-3 sm:py-3">{{ book.title }}</td>
          <td class="px-2 py-2 sm:px-3 sm:py-3">
            {{ getCategoryName(book.categoryId) }}
          </td>

          <td
            class="text-sm text-gray-500 pb-2 flex justify-center flex-col lg:flex-row"
          >
            <button
              @click="openModal(book.bookId)"
              class="inline-flex items-center px-4 mt-2 mr-2 py-2  transition ease-in-out delay-75 bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br text-white text-sm font-medium rounded-md hover:-translate-y-1 hover:scale-2"
            >
              <svg
                stroke="currentColor"
                viewBox="0 0 24 24"
                fill="none"
                class="h-5 w-5 mr-2"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                  stroke-width="2"
                  stroke-linejoin="round"
                  stroke-linecap="round"
                ></path>
              </svg>
              Delete
            </button>
            <button
              @click="openEditModal(book)"
              class="inline-flex items-center px-4 mt-2 mr-2 py-2  transition ease-in-out delay-75 bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br text-white text-sm font-medium rounded-md hover:-translate-y-1 hover:scale-2"
            >
              <svg
                fill="#ffffff"
                class="h-5 w-5 mr-2"
                version="1.1"
                xmlns="http://www.w3.org/2000/svg"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                viewBox="0 0 348.882 348.882"
                xml:space="preserve"
                stroke-width="2"
                stroke-linejoin="round"
                stroke-linecap="round"
              >
                <g>
                  <path
                    d="M333.988,11.758l-0.42-0.383C325.538,4.04,315.129,0,304.258,0c-12.187,0-23.888,5.159-32.104,14.153L116.803,184.231 c-1.416,1.55-2.49,3.379-3.154,5.37l-18.267,54.762c-2.112,6.331-1.052,13.333,2.835,18.729c3.918,5.438,10.23,8.685,16.886,8.685 c0,0,0.001,0,0.001,0c2.879,0,5.693-0.592,8.362-1.76l52.89-23.138c1.923-0.841,3.648-2.076,5.063-3.626L336.771,73.176 C352.937,55.479,351.69,27.929,333.988,11.758z M130.381,234.247l10.719-32.134l0.904-0.99l20.316,18.556l-0.904,0.99 L130.381,234.247z M314.621,52.943L182.553,197.53l-20.316-18.556L294.305,34.386c2.583-2.828,6.118-4.386,9.954-4.386 c3.365,0,6.588,1.252,9.082,3.53l0.419,0.383C319.244,38.922,319.63,47.459,314.621,52.943z"
                  ></path>
                  <path
                    d="M303.85,138.388c-8.284,0-15,6.716-15,15v127.347c0,21.034-17.113,38.147-38.147,38.147H68.904 c-21.035,0-38.147-17.113-38.147-38.147V100.413c0-21.034,17.113-38.147,38.147-38.147h131.587c8.284,0,15-6.716,15-15 s-6.716-15-15-15H68.904c-37.577,0-68.147,30.571-68.147,68.147v180.321c0,37.576,30.571,68.147,68.147,68.147h181.798 c37.576,0,68.147-30.571,68.147-68.147V153.388C318.85,145.104,312.134,138.388,303.85,138.388z"
                  ></path>
                </g>
              </svg>
              Edit
            </button>
            <button
              @click="download(book.bookId)"
              class="inline-flex items-center px-4 mt-2 mr-2 py-2 transition ease-in-out delay-75 bg-gradient-to-r from-yellow-400 via-yellow-500 to-yellow-600 hover:bg-gradient-to-br text-white md:text-sm sm:text-xs font-medium rounded-md hover:-translate-y-1 hover:scale-2"
            >
              <i class="fa-solid fa-download mr-1"></i>
              Download
            </button>
          </td>
        </tr>  
      </tbody>      
    </table>
    <p class=" text-sm text-gray-700 mt-8 ">{{ paginationDetails }}</p>      
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { url } from "@/store/authStore";
defineProps([
  "getCategoryName",
  "openModal",
  "showModal",
  "deleteBook",
  "closeModal",
  "openEditModal",
  "showEditModal",
  "closeEditModal",
  "categoryId",
  "categories",
  "newTitle",
  "updateBook",
  "newAuthor",
  "newCategoryId",
  "books",
  "paginationDetails",
]);

function download(id: number) {
  axios
    .get(`${url}api/downloadbook/${id}`, {
      responseType: "blob",
      withCredentials: true,
    })
    .then((response) => {
      const blob = new Blob([response.data], {
        type: response.headers["content-type"],
      });
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = "book.txt";
      link.click();
    });
}



</script>
