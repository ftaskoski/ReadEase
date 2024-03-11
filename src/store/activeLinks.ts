import { ref } from 'vue';

export const activeLink = ref<string | null>(null);

export const activateLink = (link: string): void => {
  activeLink.value = link;
};

export const initializeActiveLink = (): void => {

  const currUrl = window.location.pathname;
  activeLink.value =currUrl;
};
