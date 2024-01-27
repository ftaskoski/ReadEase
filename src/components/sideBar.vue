<template>
    <div>
        <div>
    <!-- Toggle button for small screens -->
    <button
      @click="toggleSidebar"
      class="lg:hidden fixed top-0 left-0 z-50 p-4 focus:outline-none"
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
      id="sidebar-multi-level-sidebar"
      class="lg:flex flex-col h-screen w-64 bg-gray-50 dark:bg-gray-800 transition-transform"
      :class="{ '-translate-x-full': !isSidebarOpen }"
    >
      <!-- ... sidebar content ... -->
    </aside>

    <!-- Main content -->
    <div class="ml-0 lg:ml-64 transition-transform">
      <!-- ... main content ... -->
    </div>
  </div>
    </div>
</template>

<script setup lang="ts">
import { ref,watch } from "vue";
const isSidebarOpen = ref(window.innerWidth >= 1024);

const toggleSidebar = () => {
  isSidebarOpen.value = !isSidebarOpen.value;
};

watch(() => window.innerWidth, (width) => {
  // Update the sidebar state based on the window width
  isSidebarOpen.value = width >= 1024;
});

</script>

