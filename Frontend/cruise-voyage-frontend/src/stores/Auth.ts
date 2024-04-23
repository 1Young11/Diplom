import {defineStore} from 'pinia';

interface Auth {
   currentEmail: string,
   currentPassword: string,
   visibleCurrentPass: boolean,
   savePassword: boolean,
}

export const useAuthInfo = defineStore('authStore', {
   state: (): Auth => ({
      currentEmail: '',
      currentPassword: '',
      visibleCurrentPass: false,
      savePassword: false,
   }),
   actions: {
      Login() {
         console.log(this.currentEmail, this.currentPassword)
      }
   },
   getters: {
   
   }
})