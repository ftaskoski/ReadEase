import { ref } from 'vue';
import axios from 'axios';

export const isAuthenticated = ref<boolean>(false);
export const loggedInUser = ref<any>();
export const role = ref<string>('');
export const url="https://localhost:7284/"


export const setAuthenticated = (value: boolean) => {
  isAuthenticated.value = value;
};

export const AuthStatus = async () => {
  try {
    const response = await axios.get(`${url}api/lookup`, { withCredentials: true });
    isAuthenticated.value = response.data.isAuthenticated;
   role.value = response.data.role;
  } catch (error) {
    isAuthenticated.value = false;
    loggedInUser.value = null;
  }
};

