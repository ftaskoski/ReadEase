import { ref } from 'vue';

export const isAuthenticated = ref<boolean>(false);
export const loggedInUser = ref<any>();

export const setAuthenticated = (value: boolean) => {
  isAuthenticated.value = value;
};

export const loadUserFromCookie = (): any => {
  const user = getCookie("loggedInUser");
  if (user) {
    loggedInUser.value = JSON.parse(user);
    setAuthenticated(true);
    return loggedInUser.value;
  }
  return null;
};

export const saveUserToCookie = (user: any): void => {
  const expirationTime = new Date();
  expirationTime.setTime(expirationTime.getTime() + 1 * 24 * 60 * 60 * 1000);

  setCookie('loggedInUser', JSON.stringify(user), expirationTime);
  loggedInUser.value = user;
  setAuthenticated(true);
};

export const clearUserFromCookie = (): void => {
  clearCookie('loggedInUser');
  loggedInUser.value = null;
  setAuthenticated(false);
};

// Function to get a cookie value by name
const getCookie = (name: string): string | null => {
  const cookieValue = document.cookie
    .split('; ')
    .find(row => row.startsWith(name))
    ?.split('=')[1];
  return cookieValue || null;
};

// Function to set a cookie
const setCookie = (name: string, value: string, expirationTime: Date): void => {
  document.cookie = `${name}=${value}; expires=${expirationTime.toUTCString()}; path=/`;
};

// Function to clear a cookie by name
const clearCookie = (name: string): void => {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
};
