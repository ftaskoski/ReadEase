import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import {AuthStatus } from '@/store/authStore';

const app = createApp(App)



await AuthStatus();

app.use(router)

app.mount('#app')
