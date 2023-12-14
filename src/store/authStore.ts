import { ref } from 'vue';

export const isAuthenticated = ref<boolean>(false);

export const setAuthenticated = (value : boolean) => {
  isAuthenticated.value = value;
  
};

export  const loadUserFromLocalStorage = () : any => {
  const user = localStorage.getItem("loggedInUser");
  return user ? JSON.parse(user) : null;
};
export const saveUserToLocalStorage = (user: any) : void => {
  localStorage.setItem("loggedInUser", JSON.stringify(user));
};

export const  clearUserFromLocalStorage = () : void => {
  localStorage.removeItem("loggedInUser");
  setAuthenticated(false);
  
};
