import { createRouter, createWebHistory } from 'vue-router'
import AboutVue from '@/views/About.vue'
import HomeVue from '@/views/Home.vue'
import { isAuthenticated } from '@/store/authStore'
import LoginVue from '@/views/Login.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeVue,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/about',
      name: 'about',
      component: AboutVue,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/login',
      name: 'login',
      component: () => LoginVue
    }
  ]
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated.value) {
    next('/login');
  } else {
    next();
  }
});
export default router
