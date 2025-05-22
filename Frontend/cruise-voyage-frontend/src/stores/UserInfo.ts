import {defineStore} from 'pinia';
import axios from 'axios';

interface Auth {
   idCustomer: number,
   nameCustomer: string,
   lastNameCustomer: string,
   phoneCustomer: string,
   email: string,
   activeBlock: number,
   countPoint: number,
   historyCruise: any,
   rating: number,
   activeItem: number,
   listShops: Array<{img: string, name: string, cost: number}>,
}

export const useUserInfo = defineStore('userStore', {
   state: (): Auth => ({
      idCustomer: -1,
      nameCustomer: '',
      lastNameCustomer: '', 
      phoneCustomer: '', 
      email: '',
      countPoint: 0,
      activeBlock: 0,
      historyCruise: [],
      rating: -1,
      activeItem: -1,
      listShops: [
         {img: 'padushki', name: 'Подушка', cost: 600},
         {img: 'polotenco', name: 'Подушка', cost: 600},
         {img: 'termo', name: 'Подушка', cost: 600},
         {img: 'cofe', name: 'Подушка', cost: 600},
         {img: 'padushki', name: 'Подушка', cost: 600},
         {img: 'polotenco', name: 'Подушка', cost: 600},
         {img: 'termo', name: 'Подушка', cost: 600},
         {img: 'cofe', name: 'Подушка', cost: 600},
         {img: 'padushki', name: 'Подушка', cost: 600},
         {img: 'polotenco', name: 'Подушка', cost: 600},
         {img: 'termo', name: 'Подушка', cost: 600},
         {img: 'cofe', name: 'Подушка', cost: 600},
      ],
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