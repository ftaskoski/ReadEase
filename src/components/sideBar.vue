<template>
  <div>
    <div>
      <!-- Toggle button for small screens -->
      <button
        id="toggle-button"
        @click="toggleSidebar"
        class="inline-flex items-center p-2 mt-2 ms-3 text-sm text-gray-500 rounded-lg sm:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
      >
        <svg
          class="w-6 h-6 text-gray-500"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M4 6h16M4 12h16m-7 6h7"
          ></path>
        </svg>
      </button>

      <!-- Sidebar -->
      <aside
        id="default-sidebar"
        class="fixed top-0 left-0 z-40 w-64 h-screen transition-transform sm:translate-x-0"
        :class="{ '-translate-x-full': !isSidebarOpen }"
        aria-label="Sidebar"
      >
        <div
          class="h-full w-64 px-3 py-16 overflow-y-auto bg-gray-50 dark:bg-gray-800"
        >
          <ul class="space-y-2 font-medium">
            <li @click="toggleSidebarOnPhone">
              <RouterLink
                to="/"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <svg
                  class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="currentColor"
                  viewBox="0 0 22 21"
                >
                  <path
                    d="M16.975 11H10V4.025a1 1 0 0 0-1.066-.998 8.5 8.5 0 1 0 9.039 9.039.999.999 0 0 0-1-1.066h.002Z"
                  />
                  <path
                    d="M12.5 0c-.157 0-.311.01-.565.027A1 1 0 0 0 11 1.02V10h8.975a1 1 0 0 0 1-.935c.013-.188.028-.374.028-.565A8.51 8.51 0 0 0 12.5 0Z"
                  />
                </svg>
                <span class="ms-3">Dashboard</span>
              </RouterLink>
            </li>
            <li>
              <a
                href="#"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <svg
                  class="flex-shrink-0 w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="currentColor"
                  viewBox="0 0 18 18"
                >
                  <path
                    d="M6.143 0H1.857A1.857 1.857 0 0 0 0 1.857v4.286C0 7.169.831 8 1.857 8h4.286A1.857 1.857 0 0 0 8 6.143V1.857A1.857 1.857 0 0 0 6.143 0Zm10 0h-4.286A1.857 1.857 0 0 0 10 1.857v4.286C10 7.169 10.831 8 11.857 8h4.286A1.857 1.857 0 0 0 18 6.143V1.857A1.857 1.857 0 0 0 16.143 0Zm-10 10H1.857A1.857 1.857 0 0 0 0 11.857v4.286C0 17.169.831 18 1.857 18h4.286A1.857 1.857 0 0 0 8 16.143v-4.286A1.857 1.857 0 0 0 6.143 10Zm10 0h-4.286A1.857 1.857 0 0 0 10 11.857v4.286c0 1.026.831 1.857 1.857 1.857h4.286A1.857 1.857 0 0 0 18 16.143v-4.286A1.857 1.857 0 0 0 16.143 10Z"
                  />
                </svg>
                <span class="flex-1 ms-3 whitespace-nowrap">Kanban</span>
                <span
                  class="inline-flex items-center justify-center px-2 ms-3 text-sm font-medium text-gray-800 bg-gray-100 rounded-full dark:bg-gray-700 dark:text-gray-300"
                  >Pro</span
                >
              </a>
            </li>
            <li>
              <a
                href="#"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <svg
                  class="flex-shrink-0 w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                >
                  <path
                    d="m17.418 3.623-.018-.008a6.713 6.713 0 0 0-2.4-.569V2h1a1 1 0 1 0 0-2h-2a1 1 0 0 0-1 1v2H9.89A6.977 6.977 0 0 1 12 8v5h-2V8A5 5 0 1 0 0 8v6a1 1 0 0 0 1 1h8v4a1 1 0 0 0 1 1h2a1 1 0 0 0 1-1v-4h6a1 1 0 0 0 1-1V8a5 5 0 0 0-2.582-4.377ZM6 12H4a1 1 0 0 1 0-2h2a1 1 0 0 1 0 2Z"
                  />
                </svg>
                <span class="flex-1 ms-3 whitespace-nowrap">Inbox</span>
                <span
                  class="inline-flex items-center justify-center w-3 h-3 p-3 ms-3 text-sm font-medium text-blue-800 bg-blue-100 rounded-full dark:bg-blue-900 dark:text-blue-300"
                  >3</span
                >
              </a>
            </li>
            <li @click="toggleSidebarOnPhone">
              <RouterLink
                to="/books"
                href="#"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <i
                  class="fa-solid fa-book flex-shrink-0 w-5 pt-0.5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                ></i>

                <span class="flex-1 ms-3 whitespace-nowrap">Collection</span>
              </RouterLink>
            </li>
            <li @click="toggleSidebarOnPhone">
              <RouterLink
                to="/settings"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <i
                  class="fa-solid fa-gear flex-shrink-0 w-5 mt-1 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                ></i>
                <span class="flex-1 ms-3 whitespace-nowrap">Settings</span>
              </RouterLink>
            </li>
            <li>
              <RouterLink
                @click="logout"
                to="/login"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <i
                  class="fa fa-sign-out flex-shrink-0 w-5 mt-1 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  aria-hidden="true"
                ></i>
                <span class="flex-1 ms-3 whitespace-nowrap mb-1">Sign Out</span>
              </RouterLink>
            </li>
     
            <li v-if="role == 'Admin'">
              <RouterLink
                to="/admin"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
              >
                <i
                  class="fa fa-user-tie flex-shrink-0 w-5 mt-0.5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  aria-hidden="true"
                ></i>
                <span class="flex-1 ms-3 whitespace-nowrap mb-1">Admin</span>
              </RouterLink>
            </li>
          </ul>
        </div>
      </aside>
      <div
        v-if="isSidebarOpen"
        class="fixed inset-0 bg-gray-900 bg-opacity-30 blur"
        @click="toggleSidebar"
        :class="{ 'overflow-hidden': isSidebarOpen }"
      >
        <!-- You can add any additional content here -->
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted, onBeforeUnmount } from "vue";
import { setAuthenticated, clearUserFromCookie, role } from "@/store/authStore";
import axios from "axios";
import { useRouter } from "vue-router";
const router = useRouter();
const url = "https://localhost:7284/";
const isSidebarOpen = ref(false); // Set initial value to false
const overflow = ref<boolean>(false);

function toggleOverflow(): void {
  overflow.value = !overflow.value;
  if (overflow.value) {
    document.body.style.overflow = "hidden";
  } else {
    document.body.style.overflow = "auto";
  }
}

const logout = (): void => {
  axios
    .post(
      `${url}api/logout`,
      {},
      {
        withCredentials: true,
      }
    )
    .then((response) => {
      clearUserFromCookie();

      router.push("/login");
      role.value = "";
      setAuthenticated(false);
    })
    .catch((error) => {
      console.error("Error during logout:", error);
    });
};

const toggleSidebar = () => {
  isSidebarOpen.value = !isSidebarOpen.value;
};

const toggleSidebarOnPhone = () => {
  toggleOverflow();

  if (window.innerWidth < 1024) {
    isSidebarOpen.value = !isSidebarOpen.value;
  }
};

const closeSidebarOnClickOutside = (event: MouseEvent) => {
  const sidebar = document.getElementById("default-sidebar");
  const toggleButton = document.getElementById("toggle-button");

  // Check if the clicked element is outside the sidebar, not the toggle button, and the sidebar is open
  if (
    sidebar &&
    !sidebar.contains(event.target as Node) &&
    toggleButton &&
    !toggleButton.contains(event.target as Node) &&
    isSidebarOpen.value
  ) {
    isSidebarOpen.value = false;
  }
};

// Watch for window width changes
watch(
  () => window.innerWidth,
  (width) => {
    // Update the sidebar state based on the window width
    isSidebarOpen.value = width >= 1024;
  }
);

// Add click event listener to close sidebar on click outside
onMounted(() => {
  window.addEventListener("click", closeSidebarOnClickOutside);
});

// Remove the click event listener when the component is unmounted
onBeforeUnmount(() => {
  window.removeEventListener("click", closeSidebarOnClickOutside);
});
</script>

<style scoped></style>
