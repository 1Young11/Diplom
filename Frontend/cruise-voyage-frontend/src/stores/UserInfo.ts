import {defineStore} from 'pinia';

interface Auth {
   currentEmail: string,
   currentPassword: string,
   visibleCurrentPass: boolean,
   savePassword: boolean,
   name: string,
   lastName: string,
   mobilePhone: string,
   newEmail: string,
   newPassword: string,
   visibleNewPassword: boolean,
   newPasswordRepeat: string,
   visibleRepeat: boolean, 
   acceptRules: boolean,
}

export const useUserInfo = defineStore('userStore', {
   state: (): Auth => ({
      currentEmail: '',
      currentPassword: '',
      visibleCurrentPass: false,
      savePassword: false,
      name: '',
      lastName: '',
      mobilePhone: '',
      newEmail: '',
      newPassword: '',
      visibleNewPassword: false,
      newPasswordRepeat: '',
      visibleRepeat: false,
      acceptRules: false,
   }),
   actions: {
   
   },
   getters: {
   
   }
})