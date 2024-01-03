import { ref } from 'vue';

export const isAuthenticated = ref<boolean>(false);
export const loggedInUser = ref<any>();

export const setAuthenticated = (value: boolean) => {
  isAuthenticated.value = value;
};

export const loadUserFromLocalStorage = (): any => {
  const user = localStorage.getItem("loggedInUser");
  if (user) {
    loggedInUser.value = JSON.parse(user);
    setAuthenticated(true);
    return loggedInUser.value;
  }
  return null;
};

export const saveUserToLocalStorage = (user: any): void => {
  localStorage.setItem("loggedInUser", JSON.stringify(user));
  loggedInUser.value = user;
  setAuthenticated(true);
};

export const clearUserFromLocalStorage = (): void => {
  localStorage.removeItem("loggedInUser");
  loggedInUser.value = null;
  setAuthenticated(false);
};
