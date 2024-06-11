import {defineStore} from 'pinia';
import axios from 'axios';

interface Auth {
   idCustomer: number,
   nameCustomer: string,
   lastNameCustomer: string,
   phoneCustomer: string,
   passengers: Passenger[]
}

interface Passenger {
   name: string,
   lastName: string,
   phone: string,
}

export const useUserInfo = defineStore('userStore', {
   state: (): Auth => ({
      idCustomer: -1,
      nameCustomer: '',
      lastNameCustomer: '', 
      phoneCustomer: '', 

      passengers: [
         { name: '', lastName: '', phone: '' },
         { name: '', lastName: '', phone: '' },
         { name: '', lastName: '', phone: '' }
      ]
   }),
   actions: {
     
   },
   getters: {
   
   }
})