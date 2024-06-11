import { defineStore } from 'pinia';
import axios from 'axios';
import router from '@/main';
import CryptoJS from 'crypto-js';

interface Registration {
   name: string,
   trublName: boolean,
   lastName: string,
   trublLastName: boolean,
   mobilePhone: string,
   trublPhone: boolean,
   newEmail: string,
   trublEmail: boolean,
   newPassword: string,
   visibleNewPassword: boolean,
   newPasswordRepeat: string,
   visibleRepeat: boolean,
   acceptRules: boolean,
}

export const useRegInfo = defineStore('regStore', {
   state: (): Registration => ({
      name: '',
      trublName: false,

      lastName: '',
      trublLastName: false,

      mobilePhone: '',
      trublPhone: false,

      newEmail: '',
      trublEmail: false,

      newPassword: '',
      visibleNewPassword: false,
      newPasswordRepeat: '',
      visibleRepeat: false,
      acceptRules: false,
   }),
   actions: {
      async SendInfoRegistration() {
         const hashedPassword = CryptoJS.SHA256(this.newPassword).toString();
         if (this.name.length == 0 || this.name.length == 1) {
            this.trublName = true;
            setTimeout(() => {
               this.trublName = false;
            }, 3000);
            return;
         }
         if (this.lastName.length == 0 || this.lastName.length == 1) {
            this.trublLastName = true;
            setTimeout(() => {
               this.trublLastName = false;
            }, 3000);
            return;
         }
         if (this.mobilePhone.length != 13) {
            this.trublPhone = true;
            setTimeout(() => {
               this.trublPhone = false;
            }, 3000);
            return;
         }
         const validemail = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(this.newEmail);
         if (!validemail) {
            this.trublEmail = true;
            setTimeout(() => {
               this.trublEmail = false;
            }, 3000);
            return;
         } 
         try {
            await axios.post('http://localhost:5282/api/Customer/addCustomer', {
               name: this.name,
               lastName: this.lastName,
               phone: this.mobilePhone,
               email: this.newEmail,
               password: hashedPassword
            })
            .then(response => {
               localStorage.setItem('userData', JSON.stringify({
                  email: this.newEmail,
                  password: hashedPassword
               }));
               this.setExpireIn();
               router.push('/mainpage');
            })
            .catch(e => {
               console.error(e)
            });
         } catch (error) {
            console.error('Ошибка при отправке запроса:', error);
         }
      },
      setExpireIn() {
         localStorage.setItem('userdata_expireIn', JSON.stringify(new Date().getTime() + 1000 * 60 * 60 * 24));
         return;
      },
   },
   getters: {

   }
})