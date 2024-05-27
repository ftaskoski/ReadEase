import { createRouter, createWebHistory } from "vue-router";
import SettingsVue from "@/views/Settings.vue";
import HomeVue from "@/views/Home.vue";
import { isAuthenticated, role} from "@/store/authStore";
import LoginVue from "@/views/Login.vue";
import RegisterVue from "@/views/Register.vue";
import BooksVue from "@/views/Books.vue";
import AdminVue from "@/views/Admin.vue";
import Recovery from "@/views/Recovery.vue";
const url="https://localhost:7284/"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeVue,
      meta: {
        requiresAuth: true,
        title: "New York Times",
      },
    },
    {
      path: "/settings",
      name: "settings",
      component: SettingsVue,
      meta: {
        requiresAuth: true,
        title: "Settings",
      },
    },
    {
      path: "/recovery",
      name: "recovery",
      component: Recovery,
      meta: {
        title: "Recovery",
      },
    },
    {
      path: "/login",
      name: "login",
      component: LoginVue,
      meta: {
        title: "Login",
      },
    },
    {
      path: "/books",
      name: "books",
      component: BooksVue,
      meta: {
        requiresAuth: true,
        title: "Books",
      },
      props:(route)=>{
        query: route.query
      }
    },
    {
      path: "/register",
      name: "register",
      component: RegisterVue,
      meta: {
        title: "Register",
      },
    },
    {
      path: "/admin",
      name: "admin",
      component: AdminVue,
      meta: {
        requiresAuth: true,
        title: "Admin",
      },
      async beforeEnter(to, from, next) {
        if (!isAuthenticated.value) {
          next("/login");
        } else {
          try {
            
            if (role.value === "Admin") {
              next();
            } else {
              // Redirect to home if the user doesn't have the 'Admin' role
              next("/");
            }
          } catch (error) {
            console.error("Error fetching role:", error);
            // Handle error if needed
            next("/login");
          }
        }
      }
    },
  ],
});

router.beforeEach((to, from, next) => {
  document.title =`${to.meta.title} | ReadEase`
  if (to.matched.some((record) => record.meta.requiresAuth) && !isAuthenticated.value) {
    next("/login");
  } else if ((to.name === "login" || to.name === "register") && isAuthenticated.value) {
    next("/");
  } else {
    next();
  }
});

export default router;
