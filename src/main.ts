import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { isAuthenticated, loadUserFromCookie, role } from '@/store/authStore';

const app = createApp(App)

const user = loadUserFromCookie();
if (user) {
  isAuthenticated.value = true;
  if (user.role === "Admin") {
    role.value = "Admin";
  }
}



app.use(router)

app.mount('#app')
