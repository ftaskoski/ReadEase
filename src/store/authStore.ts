import { ref } from 'vue';
import axios from 'axios';

export const isAuthenticated = ref<boolean>(false);
export const loggedInUser = ref<any>();
export const role = ref<string>('');
const url="https://localhost:7284/"


export const setAuthenticated = (value: boolean) => {
  isAuthenticated.value = value;
};

export const AuthStatus = async () => {
  try {
    const response = await axios.get(`${url}api/lookup`, { withCredentials: true });
    isAuthenticated.value = response.data;
    getRole(url);
  } catch (error) {
    isAuthenticated.value = false;
    loggedInUser.value = null;
  }
};






export const getRole = async (url: string) => {
  try {
    const response = await axios.get(`${url}api/role`, { withCredentials: true });
    const data = response.data;
     role.value = data;
    return data; // Return the role data if needed
  } catch (error) {
    console.error("Error fetching role:", error);
    // Handle error if needed
    throw error; // Rethrow the error to propagate it
  }
};