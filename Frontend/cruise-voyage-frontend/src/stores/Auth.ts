import { defineStore } from 'pinia';
import axios from 'axios';
import router from '@/main';
import { useUserInfo } from "@/stores/UserInfo";

interface Auth {
   currentEmail: string,
   trublCurrentEmail: boolean,
   currentPassword: string,
   trublCurrentPassword: boolean,
   visibleCurrentPass: boolean,
   savePassword: boolean,
   isAuth: boolean,
}


export const useAuthInfo = defineStore('authStore', {
   state: (): Auth => ({
      currentEmail: '',
      trublCurrentEmail: false,
      currentPassword: '',
      trublCurrentPassword: false,
      visibleCurrentPass: false,
      savePassword: false,
      isAuth: false,
   }),
   actions: {
      async Login() {
         const userStore = useUserInfo();
         try {
            await axios.post('http://localhost:5282/api/Customer/Login', {
               email: this.currentEmail,
               password: this.currentPassword
            })
            .then(response => {
               const userData = response.data;
               console.log(userData)
               if (this.savePassword) {
                  localStorage.setItem('userData', JSON.stringify(userData));
                  this.setExpireIn();
               }
               userStore.idCustomer = userData.idCustomer;
               userStore.nameCustomer = userData.name;
               userStore.lastNameCustomer = userData.surname;
               userStore.phoneCustomer = userData.phone;
               userStore.email = userData.email;
               userStore.countPoint = userData.countPoint;
               this.isAuth = true;
               router.push('/mainpage');
            })
            .catch((error) => {
               console.log(error)
               const messageError = error.response?.data.errorCode;
               if (messageError == 'EMAIL_NOT_FOUND') {
                  this.trublCurrentEmail = true;
                  setTimeout(() => {
                     this.trublCurrentEmail = false;
                  }, 3000);
               }
               if (messageError == 'INVALID_PASSWORD') {
                  this.trublCurrentPassword = true;
                  setTimeout(() => {
                     this.trublCurrentPassword = false;
                  }, 3000);
               }
            });
         } catch (error) {
            console.error('Помилка при відправленні запроса:', error);
         }
      },
      setExpireIn() {
         localStorage.setItem('userdata_expireIn', JSON.stringify(new Date().getTime() + 1000 * 60 * 60 * 24));
         return;
      },
      isUserLoggedIn(): boolean {
         const userStore = useUserInfo();
         const user = localStorage.getItem('userData');
         const expireIn = localStorage.getItem('userdata_expireIn');
         if (user && expireIn) {
            const userObj = JSON.parse(user);
            const expireInDate = new Date(JSON.parse(expireIn));
            if (expireInDate > new Date()) {
               userStore.idCustomer = userObj.idCustomer;
               userStore.nameCustomer = userObj.name;
               userStore.lastNameCustomer = userObj.surname;
               userStore.phoneCustomer = userObj.phone;
               userStore.email = userObj.email;
               userStore.countPoint = userObj.countPoint;
               this.isAuth = true;
               return true;
            }
         }
         return false;
      },
      Logout() {
         localStorage.removeItem('userData');
         localStorage.removeItem('userdata_expireIn');
         router.push('/auth');
      },
   },
   getters: {

   }
})