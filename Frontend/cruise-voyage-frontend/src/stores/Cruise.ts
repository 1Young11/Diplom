import {defineStore} from 'pinia';
import axios from 'axios';
import router from '@/main';

interface Cruise {
   checkArrivedFrom: boolean,
   checkArrivedTo: boolean,
   checkArrivedDate: boolean,
   cruisesList: Array<InfoCruise>,
   filteredCruises: Array<InfoCruise>,
   travelFrom: string[],
   travelTo: string[],
   activeArrivedFrom: number,
   activeArrivedTo: number,
   currentDate: Date | undefined,
   swiper: number,
   activeCruise: number,
   countFloors: number[],
   activeFloor: number,
   cabin: {[key : number]: Array<{
         cabinBeds: Array<{idCabin: number, idCabinbed: number, isBooked: boolean, lastName: string, name: string, phoneNumber: string}>,  
         cabinType: number,
         idCabin: number, 
         idCabintype: number,
         idShip: number,
      }>
   },
   pageReserve: number | null,
   currentTypeCabin: number | null,
   activeCabin: number,
   currentReservation: number[],
   typeCabins: Array<{type: string, price: number}>
}

interface InfoCruise {
   idCruise: number, arrivedFrom: string, arrivedTo: string, arrivedFromDate: string, arrivedToDate: string, countDays: string, startPrice: number, rating: number, fav: boolean, distance: number, cruisePoint: Array<{pointName: string, listNo: number, dateArrived: string}>, nameShip: string
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
      travelFrom: [],
      travelTo: [],
      filteredCruises: [],
      activeArrivedFrom: -1,
      activeArrivedTo: -1,
      currentDate: undefined,
      swiper: -1,
      activeCruise: -1,

      countFloors: [],
      activeFloor: -1,
      pageReserve: 0,
      currentTypeCabin: null,
      activeCabin: -1,
      currentReservation: [1,2,3],

      cabin: {},
      typeCabins: [],
   }),
   actions: {
      onSwiper(swiper: any) {
         this.swiper = swiper;
      },
      async fetchCruises() {
         try {
            const response = await axios.get('http://localhost:5282/api/Cruises/GetCruises');
            const cruisesData = response.data;
            console.log(cruisesData)
            this.cruisesList = [];
            this.travelFrom = [];
            this.travelTo = [];
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

            const cruisePoints = []
            for (const point of item.cruisePoints) {
               cruisePoints.push({
                  pointName: point.pointName,
                  listNo: point.listNo,
                  dateArrived: point.dateArrived
               })
            }

            this.cruisesList.push({
               idCruise: item.idCruises,
               arrivedFrom: item.arrivedFrom,
               arrivedTo: item.arrivedTo,
               arrivedFromDate: item.arrivedFromDate,
               arrivedToDate: item.arrivedToDate,
               countDays: countDays,
               startPrice: 5000,
               rating: item.rating,
               fav: false,
               distance: item.travelDistance, 
               cruisePoint: cruisePoints,
               nameShip: item.ship.nameShip
            })
            this.filteredCruises = this.cruisesList;

            if (!this.travelFrom.includes(item.arrivedFrom)) {
               this.travelFrom.push(item.arrivedFrom);
            }
            if (!this.travelTo.includes(item.arrivedTo)) {
               this.travelTo.push(item.arrivedTo);
            }
         }
      },
      FindCruise() {
         const arrivefrom = this.travelFrom[this.activeArrivedFrom];
         const arriveto = this.travelTo[this.activeArrivedTo];
         const searchDate = this.currentDate ? new Date(this.currentDate) : null;
         const dateToday = new Date();
     
         if (searchDate && searchDate < dateToday) return;
     
         this.filteredCruises = this.cruisesList.filter(cruise => {
            const matchArriveFrom = !arrivefrom || cruise.arrivedFrom === arrivefrom;
            const matchArriveTo = !arriveto || cruise.arrivedTo === arriveto;
            const matchDate = !searchDate || new Date(cruise.arrivedFromDate) >= searchDate;
            return matchArriveFrom && matchArriveTo && matchDate;
         });
         router.push('/findcruise');
      },
      SelectPlaceInCabin(index: number) {
         // const isBooked = this.cabinFirstFloor[this.activeCabin].place[index];
         // if (this.currentReservation.includes(index)) return;
         // if (isBooked.isBooked) {
         //    return;
         // } else {
         //    this.currentReservation.push(index);
         // }
      },
      async fetchCabinsByCruise(cruiseId: number) {
         try {
            const API = 'http://localhost:5282';
            const response = await axios.get(`${API}/api/cruises/GetCabinsByCruise/${cruiseId}`);
            const cabinsArray = response.data;
            console.log(cabinsArray)
            const cabinInfo: { [key: number]: Array<any> } = {};
            for (const item of cabinsArray) {
               const { floor, ...rest } = item;
               if (!cabinInfo[floor]) {
                  cabinInfo[floor] = [];
               }
               cabinInfo[floor].push(rest);
               if (!this.countFloors.includes(item.floor)) {
                  this.countFloors.push(item.floor);  
               }
            }
            console.log(cabinInfo)
            this.cabin = cabinInfo;
         } catch (error) {
            console.error('Error fetching cabins:', error);
         }
      },
      async fetchCabinsType() {
         try {
            const API = 'http://localhost:5282';
            const response = await axios.get(`${API}/api/cruises/GetCabinType`);
            this.typeCabins = [];
            for (const item of response.data) {
               this.typeCabins.push({
                  type: item.nameType,
                  price: item.price
               })
            }
         } catch (error) {
            console.error('Error fetching cabins:', error);
         }
      },
      formatDate(dateStr: string) {
         const options: Intl.DateTimeFormatOptions = { day: 'numeric', month: 'long' };
         const date = new Date(dateStr);
         return date.toLocaleDateString('uk-UA', options);
      }
   },
   getters: {
   
   }
})