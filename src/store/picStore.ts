import { ref } from 'vue';
import axios from 'axios';
import { url } from '../store/authStore';
export const profilePictureUrl = ref<string | null>(null);


export function getProfilePicture() {
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
  