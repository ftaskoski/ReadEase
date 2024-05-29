import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import {AuthStatus } from '@/store/authStore';
import { getProfilePicture } from './store/picStore';

const app = createApp(App)



await AuthStatus();
getProfilePicture();
app.use(router)

app.mount('#app')
