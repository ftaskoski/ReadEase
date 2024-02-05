import { createRouter, createWebHistory } from "vue-router";
import SettingsVue from "@/views/Settings.vue";
import HomeVue from "@/views/Home.vue";
import { isAuthenticated, loadUserFromCookie,role } from "@/store/authStore";
import LoginVue from "@/views/Login.vue";
import BooksVue from "@/views/Books.vue";
import AdminVue from "@/views/Admin.vue";
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeVue,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/settings",
      name: "settings",
      component: SettingsVue,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/login",
      name: "login",
      component: () => LoginVue,
    },
    {
      path: "/books",
      name: "books",
      component: () => BooksVue,
      meta: {
        requiresAuth: true,
      },
      props:(route)=>{
        query: route.query
      }
    },
    {
      path: "/register",
      name: "register",
      component: () => import("@/views/Register.vue"),
    },
    {
      path: "/admin",
      name: "admin",
      component: () => AdminVue,
      meta: {
        requiresAuth: true,
      },
      beforeEnter: (to, from, next) => {
        // Check if the user is authenticated
        if (!isAuthenticated.value) {
          next("/login");
        } else {
          // Check if the user has the 'Admin' role
          if (role.value === "Admin") {
            next();
          } else {
            // Redirect to home if the user doesn't have the 'Admin' role
            next("/");
          }
        }
        
      },
      
    },
  ],
});

router.beforeEach((to, from, next) => {
  loadUserFromCookie();
  if (to.matched.some((record) => record.meta.requiresAuth) && !isAuthenticated.value) {
    next("/login");
  } else if ((to.name === "login" || to.name === "register") && isAuthenticated.value) {
    next("/");
  } else {
    next();
  }
});

export default router;
