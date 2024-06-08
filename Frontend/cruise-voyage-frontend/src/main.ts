import { createApp } from 'vue';
import { createPinia } from 'pinia'
import { createRouter, createWebHashHistory } from 'vue-router';
import App from './App.vue';
import VCalendar from 'v-calendar';
import 'v-calendar/style.css';

import './styles/_reset.scss';

import AccessMenu from "./components/AccessMenu.vue";
import AuthorizationMenu from "./components/AuthorizationMenu.vue";
import RegistrationMenu from "./components/RegistrationMenu.vue";
import MainPageMenu from "./components/MainPageMenu.vue";
import UserProfile from "./components/UserProfile.vue";
import FindCruise from "./components/FindCruise.vue";
import ReservCabin from "./components/ReservCabin.vue"

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
         { path: 'reg', component: RegistrationMenu },
      ]
   },
   {
      path: '/mainpage',
      component: MainPageMenu
   },
   {
      path: '/profile',
      component: UserProfile
   },
   {
      path: '/findcruise',
      component: FindCruise
   },
   {
      path: '/reservcabin',
      component: ReservCabin
   },
];
export const router = createRouter({
   history: createWebHashHistory(),
   routes
 });
 export default router;

createApp(App).use(VCalendar, {}).use(pinia).use(router).mount('#app');
