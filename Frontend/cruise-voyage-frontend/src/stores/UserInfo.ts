import {defineStore} from 'pinia';
import axios from 'axios';

interface Auth {
   idCustomer: number,
   nameCustomer: string,
   lastNameCustomer: string,
   phoneCustomer: string,
   email: string,
   activeBlock: number,
   historyCruise: any,
   rating: number,
}

export const useUserInfo = defineStore('userStore', {
   state: (): Auth => ({
      idCustomer: -1,
      nameCustomer: '',
      lastNameCustomer: '', 
      phoneCustomer: '', 
      email: '',
      activeBlock: 0,
      historyCruise: [],
      rating: -1,
   }),
   actions: {
      async fetchHistoryOrder(id: number) { 
         const API = 'http://localhost:5282';
         const response = await axios.get(`${API}/api/order/GetOrder/${id}`);
         const history = response.data;
         this.historyCruise = [];
         for (const item of history) {
            this.historyCruise.unshift(item)
         }
      },
      RatingFlag(index: number) {
         if (index == -1) {
            this.rating = -1;
         }
         this.rating = index;
      }
   },
   getters: {
   }
})