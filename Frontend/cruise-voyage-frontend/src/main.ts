import { createApp } from 'vue';
import { createPinia } from 'pinia'
import { createRouter, createWebHashHistory } from 'vue-router';
import App from './App.vue';

import './styles/_reset.scss';

import AccessMenu from "./components/AccessMenu.vue";
import AuthorizationMenu from "./components/AuthorizationMenu.vue";
import RegistrationMenu from "./components/RegistrationMenu.vue";

const pinia = createPinia();
const app = createApp(App);
app.use(pinia);

const routes = [
   {
      path: '/',
      component: AccessMenu,
      redirect: '/auth',
      children: [
         { path: 'auth', component: AuthorizationMenu },
         { path: 'reg', component: RegistrationMenu }
      ]
   }
 ];
export const router = createRouter({
   history: createWebHashHistory(),
   routes
 });
 

createApp(App).use(pinia).use(router).mount('#app');
