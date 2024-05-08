<template>
  <div v-if="isAuthenticated">
    <div>
      <!-- Toggle button for small screens -->
      <button
        id="toggle-button"
        @click="toggleSidebar"
        class="inline-flex items-center border-2 p-2 mt-2 ms-3 text-sm text-gray-500 rounded-lg sm:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
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
    class="fixed top-0 left-0 z-40 w-64 h-full transition-transform sm:translate-x-0"
    :class="{ '-translate-x-full': !isSidebarOpen }"
    aria-label="Sidebar"
  >
    <div class="h-full w-64 px-3 py-6 overflow-y-auto bg-gray-50 dark:bg-gray-800">
      <div class="flex items-center justify-between mb-6">
  <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mr-2">Logged in as: {{ username }}</p>

  <!-- Profile picture container -->
  <div class="relative w-10">
    <div class="rounded-full overflow-hidden w-10 h-10">
      <img
        class="w-full h-full object-cover"
        :src="profilePictureUrl ? profilePictureUrl : ''"
        alt=""
      />
    </div>
  </div>

</div>

        <ul class="space-y-2 font-medium">
            <li v-for="link in navLinks" :key="link.to" @click="link.onClick ? link.onClick() : toggleSidebarOnPhone()">
              <RouterLink
                :to="link.to"
                @click="activateLink(link.to)"
                class="flex items-center shake-on-hover p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group "
                :class="{ 'bg-gray-100 dark:bg-gray-700': activeLink === link.to }"
              >
                <i :class="link.iconClasses" class="flex-shrink-0 w-5 mt-1 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"></i>
                <span class="ms-3">{{ link.label }}</span>
              </RouterLink>
            </li>
          </ul>
    </div>
</aside>

      <div
        v-if="isSidebarOpen"
        class="fixed inset-0 bg-gray-900 bg-opacity-30 z-10" 
        @click="toggleSidebar"
        :class="{ 'overflow-hidden': isSidebarOpen,}"
      >
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted, onBeforeUnmount, computed } from "vue";
import { setAuthenticated, role, isAuthenticated, username } from "@/store/authStore";
import axios from "axios";
import { useRouter } from "vue-router";
import  { activeLink,activateLink,initializeActiveLink } from "@/store/activeLinks";
import { profilePictureUrl } from "@/store/picStore";

const router = useRouter();
const url = "https://localhost:7284/";
const isSidebarOpen = ref(false); // Set initial value to false
const overflow = ref<boolean>(false);
const isAdmin = computed(() => role.value === "Admin");


function getProfilePicture() {
  axios
    .get(`${url}api/getphoto`, {
      withCredentials: true,
      responseType: "blob", // Ensure response is treated as a blob
    })
    .then((response) => {
      // Convert blob to URL
      const blob = new Blob([response.data]);
      profilePictureUrl.value = URL.createObjectURL(blob);
    })
    .catch((error) => {
      console.error("Error retrieving photos:", error);
    });
}


const navLinks = ref([
  { to: "/", label: "NY Times Best Sellers", iconClasses: "fa-solid fa-medal " },
  { to: "/books", label: "Collection", iconClasses: "fa-solid fa-book " },
  {to: "", label: "Download Collection", iconClasses: "fa-solid fa-download" , onClick: () => downloadBooks()},
  { to: "/settings", label: "Settings", iconClasses: "fa-solid fa-gear " },
  {
    to: "/login",
    label: "Sign Out",
    iconClasses: "fa fa-sign-out ",
    onClick: () => logout(),
  },
]);


watch(role, (newRole) => {
  if (newRole === "Admin" && !navLinks.value.some(link => link.label === "Admin")) {
    navLinks.value.push({
      to: "/admin",
      label: "Admin",
      iconClasses: "fa fa-user-shield ",
    });
  } else if (newRole !== "Admin") {
    navLinks.value = navLinks.value.filter(link => link.label !== "Admin");
  }
});


const downloadBooks = () => {
  axios
    .get(`${url}api/downloadbooks`, { responseType: "arraybuffer", withCredentials: true })
    .then((response) => {
      const blob = new Blob([response.data], {
        type: response.headers["content-type"],
      });
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = "books.txt";
      link.click();
    })
    .catch((error) => {
      console.error(error);
    });
};

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
      router.push("/login");
      role.value = "";
      setAuthenticated(false);
      sessionStorage.clear();
      activeLink.value = null;
      profilePictureUrl.value = null;
    })
    .catch((error) => {
      console.error("Error during logout:", error);
    });
};

const toggleSidebar = () => {
  toggleOverflow();
  isSidebarOpen.value = !isSidebarOpen.value;
};

const toggleSidebarOnPhone = () => {
  
  if (window.innerWidth < 1024) {
    isSidebarOpen.value = !isSidebarOpen.value;
    toggleOverflow();

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
    toggleOverflow();
  }
};

// Watch for window width changes!
watch(
  () => window.innerWidth,
  (width) => {
    // Update the sidebar state based on the window width
    isSidebarOpen.value = width >= 1024;
  }
);

// Add click event listener to close sidebar on click outside
onMounted(() => {
  initializeActiveLink();
  if (isAdmin.value) {
  navLinks.value.push({
    to: "/admin",
    label: "Admin",
    iconClasses: "fa fa-user-shield ",
  });
}
  window.addEventListener("click", closeSidebarOnClickOutside);
});

// Remove the click event listener when the component is unmounted
onBeforeUnmount(() => {
  window.removeEventListener("click", closeSidebarOnClickOutside);
});
</script>

<style scoped>
.shake-on-hover:hover {
  animation: shake 0.5s ease-in-out;
}

@keyframes shake {
  0% { transform: translateX(0); }
  25% { transform: translateX(-2px); }
  50% { transform: translateX(2px); }
  75% { transform: translateX(-2px); }
  100% { transform: translateX(0); } /* Ensure the element returns to its original position */
}
</style>
