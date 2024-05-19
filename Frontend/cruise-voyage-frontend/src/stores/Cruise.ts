import {defineStore} from 'pinia';
import axios from 'axios';

interface Cruise {
   checkArrivedFrom: boolean,
   checkArrivedTo: boolean,
   checkArrivedDate: boolean,
   cruisesList: Array<{arrivedFrom: string, arrivedTo: string, countDays: string, startPrice: number}>,
   activeArrivedFrom: number,
   activeArrivedTo: number,
   currentDate: Date | undefined,
   swiper: number,
}

export const useCruiseInfo = defineStore('cruiseStore', {
   state: (): Cruise => ({
      checkArrivedFrom: false,
      checkArrivedTo: false,
      checkArrivedDate: false,

      cruisesList: [
         // {arrivedFrom: 'Барселона, Іспанія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000,},
         // {arrivedFrom: 'Рим, Італія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Стамбул, Туреччина', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Сідней, Австралія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Сідней, Австралія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Сідней, Австралія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Сідней, Австралія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
         // {arrivedFrom: 'Сідней, Австралія', arrivedTo: 'Рим, Італія', countDays: '7 ночей', startPrice: 5000},
      ],
      activeArrivedFrom: -1,
      activeArrivedTo: -1,
      currentDate: undefined,
      swiper: -1,
   }),
   actions: {
      onSwiper(swiper: any) {
         this.swiper = swiper;
      },
      async fetchCruises() {
         try {
            const response = await axios.get('http://localhost:5282/api/Cruises/GetCruises');
            const cruisesData = response.data;
            this.cruisesList = [];
            this.TakeDateFromCruises(cruisesData);
         } catch (error) {
            console.error('Помилка при отриманні круїзів:', error);
         }
      },
      TakeDateFromCruises(data: any) {
         for (const item of data) {
            const arrivedFromDate = new Date(item.arrivedFromDate).getTime();
            const arrivedToDate = new Date(item.arrivedToDate).getTime();
            const differenceInMilliseconds = arrivedToDate - arrivedFromDate;
            const differenceInDays = differenceInMilliseconds / (1000 * 3600 * 24);
            let countDays: string;

            if (differenceInDays === 1) {
               countDays = '1 ніч';
            } else if (differenceInDays >= 22 && differenceInDays <= 23) {
               countDays = `${differenceInDays} ночі`;
            } else {
               countDays = `${differenceInDays} ночей`;
            }
            this.cruisesList.push({
               arrivedFrom: item.arrivedFrom,
               arrivedTo: item.arrivedTo,
               countDays: countDays,
               startPrice: 5000
            })
         }
      },
   },
   getters: {
   
   }
})