<template>
   <header class="header__navigation" @click="cruiseStore.checkArrivedFrom = false; cruiseStore.checkArrivedTo = false; cruiseStore.checkArrivedDate = false">
      <div class="container main">
         <img src="../img/reg-auth/logo.png" alt="" class="icon-logo" @click="$router.push('/mainpage');">
         <nav class="navigation">
            <ul>
               <li><a @click="scrollToTarget('Cruises')">ПОДОРОЖІ</a></li>
               <li><a @click="scrollToTarget('AboutCruise')">ПРО КРУЇЗИ</a></li>
               <li><a @click="scrollToTarget('AboutCompany')">ПРО КОМПАНІЮ</a></li>
               <li><a @click="scrollToTarget('CompanyInfo')">КОНТАКТИ</a></li>
            </ul>
         </nav>
         <div class="wrapper__btns">
            <button class="singin" @click="$router.push('/auth');" v-if="!authStore.isAuth">ВХІД</button>
            <button class="singin" @click="$router.push('/reg');" v-if="!authStore.isAuth">РЕЄСТРАЦІЯ</button>
            <button class="singin" @click="$router.push('/profile');" v-if="authStore.isAuth">Особистий кабінет</button>
         </div>
      </div>
   </header>
   <div class="wrapper__find-cruise">
      <div class="container__findallcruise">
         <div class="container__find-cruise" :class="{active: cruiseStore.checkArrivedFrom || cruiseStore.checkArrivedTo}">
            <div class="wrapper__find-something">
               <div class="title__find">ВІДПРАВЛЕННЯ З</div>
               <div class="subtitle__find">{{ cruiseStore.activeArrivedFrom == -1 ? 'Будь-якого міста' : cruiseStore.travelFrom[cruiseStore.activeArrivedFrom] }}</div>
               <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedFrom}" @click.stop="cruiseStore.checkArrivedFrom = !cruiseStore.checkArrivedFrom; cruiseStore.checkArrivedTo = false">
            </div>
            <div class="wrapper__find-something">
               <div class="title__find">ПРИБУТТЯ У</div>
               <div class="subtitle__find">{{ cruiseStore.activeArrivedTo == -1 ? 'Будь-яке місто' : cruiseStore.travelTo[cruiseStore.activeArrivedTo] }}</div>
               <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedTo}" @click.stop="cruiseStore.checkArrivedTo = !cruiseStore.checkArrivedTo; cruiseStore.checkArrivedFrom = false">
            </div>
            <div class="wrapper__find-something">
               <div class="title__find">ДАТА ВІДПРАВЛЕННЯ</div>
               <div class="subtitle__find">{{ cruiseStore.currentDate == undefined ? 'Будь-яка дата' : new Date(cruiseStore.currentDate).toLocaleDateString('uk-UA', {day: '2-digit', month: '2-digit', year: 'numeric'}) }}</div>
               <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedDate}" @click.stop="cruiseStore.checkArrivedDate = !cruiseStore.checkArrivedDate">
            </div>
            <button class="find__cruise" @click="cruiseStore.FindCruise()">Пошук</button>
         </div>
         <div class="wrapper__find-cruises" :class="{visible: cruiseStore.checkArrivedFrom}">
            <ul class="list__name__cruises">
               <li class="item__cruise" v-for="(cruise, index) in cruiseStore.travelFrom" :key="index" 
                  @click.stop="cruiseStore.activeArrivedFrom = index; cruiseStore.checkArrivedFrom = false" 
                  :class="{active: cruiseStore.activeArrivedFrom == index}"
                  v-show="cruiseStore.checkArrivedFrom">
                  {{ cruise }} 
               </li>
            </ul>
         </div>
         <div class="wrapper__find-cruises" :class="{visibletwo: cruiseStore.checkArrivedTo}">
            <ul class="list__name__cruises">
               <li class="item__cruise" v-for="(cruise, index) in cruiseStore.travelTo" :key="index" 
                  @click.stop="cruiseStore.activeArrivedTo = index; cruiseStore.checkArrivedTo = false" 
                  :class="{active: cruiseStore.activeArrivedTo == index}"
                  v-show="cruiseStore.checkArrivedTo">
                  {{ cruise }}
               </li>
            </ul>
         </div>
         <VDatePicker v-model="cruiseStore.currentDate" v-if="cruiseStore.checkArrivedDate" :class="{visibletwo: cruiseStore.checkArrivedDate}"/>
      </div>
   </div>
   <div class="countfind__sort">
      <div class="container main">
         Результати пошуку круїзів ({{ cruiseStore.filteredCruises.length }})
         <div class="wrapper__sort">Сортувати за: Датою (найближчі) </div>
      </div>
      <div class="container main">
         <ul class="list__cruises">
            <li class="item__cruise" v-for="(cruise, index) in cruiseStore.filteredCruises" :key="index">
               <div class="wrapper__icon">
                  <img :src="require(`../img/mainpage/travel${index}.jpg`)" alt="" class="icon-cruise">
                  <div class="raiting__cruise">
                     <svg width="48" height="48" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg" 
                        class="star" :class="{active: cruise.rating >= index}" v-for="index in 5" :key="index">
                        <g id="stars">
                           <path id="Vector" d="M24 35.3824L14.7959 40.9421C14.4262 41.1645 14.0843 41.2664 13.7701 41.2479C13.4559 41.2293 13.1694 41.1089 12.9107 40.8865C12.6889 40.7012 12.5133 40.4695 12.3839 40.1915C12.2545 39.9135 12.2453 39.5707 12.3562 39.163L14.7404 28.6552L6.6452 21.4832C6.34948 21.2978 6.15542 21.0477 6.06301 20.7326C5.9706 20.4176 5.97984 20.0933 6.09073 19.7597C6.20162 19.4632 6.38645 19.213 6.6452 19.0091C6.90395 18.8053 7.19966 18.6848 7.53234 18.6477L18.289 17.7582L22.5029 7.75074C22.6508 7.41716 22.8633 7.16698 23.1406 7.00019C23.4178 6.8334 23.7043 6.75 24 6.75C24.2957 6.75 24.5822 6.8334 24.8594 7.00019C25.1367 7.16698 25.3492 7.41716 25.4971 7.75074L29.7665 17.7582L40.4677 18.6477C40.8003 18.6848 41.0961 18.8053 41.3548 19.0091C41.6136 19.213 41.7984 19.4632 41.9093 19.7597C42.0202 20.0933 42.0294 20.4176 41.937 20.7326C41.8446 21.0477 41.6505 21.2978 41.3548 21.4832L33.2596 28.6552L35.6992 39.163C35.7732 39.5707 35.7454 39.9135 35.6161 40.1915C35.4867 40.4695 35.3111 40.7012 35.0893 40.8865C34.8306 41.1089 34.5441 41.2293 34.2299 41.2479C33.9157 41.2664 33.5923 41.1645 33.2596 40.9421L24 35.3824Z"/>
                        </g>
                     </svg>
                  </div>
                  <div class="travel__to">{{ cruise.arrivedTo }}</div>
               </div>
               <div class="right__side">
                  <div class="header__ship-fav">
                     <div class="name__ship">
                        <img src="../img/find-cruise/ship_icon.svg" alt="" class="icon-ship">
                        {{ cruise.nameShip }}
                     </div>
                     <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" fill="none" class="icon-fav" v-if="!cruise.fav" @click="cruise.fav = true">
                        <path d="M16 29C15.65 29 15.2938 28.9387 14.9313 28.8161C14.5688 28.6935 14.25 28.4974 13.975 28.2277L11.3875 25.9109C8.7375 23.5328 6.34375 21.173 4.20625 18.8317C2.06875 16.4903 1 13.9099 1 11.0905C1 8.78595 1.7875 6.86139 3.3625 5.31683C4.9375 3.77228 6.9 3 9.25 3C10.575 3 11.825 3.27581 13 3.82744C14.175 4.37907 15.175 5.13296 16 6.08911C16.825 5.13296 17.825 4.37907 19 3.82744C20.175 3.27581 21.425 3 22.75 3C25.1 3 27.0625 3.77228 28.6375 5.31683C30.2125 6.86139 31 8.78595 31 11.0905C31 13.9099 29.9375 16.4965 27.8125 18.8501C25.6875 21.2037 23.275 23.5695 20.575 25.9477L18.025 28.2277C17.75 28.4974 17.4313 28.6935 17.0688 28.8161C16.7063 28.9387 16.35 29 16 29ZM14.575 9.03112C13.85 8.02593 13.075 7.25978 12.25 6.73267C11.425 6.20556 10.425 5.94201 9.25 5.94201C7.75 5.94201 6.5 6.43234 5.5 7.41301C4.5 8.39368 4 9.61952 4 11.0905C4 12.3654 4.4625 13.7199 5.3875 15.1542C6.3125 16.5884 7.41875 17.9797 8.70625 19.3281C9.99375 20.6766 11.3188 21.9392 12.6812 23.116C14.0437 24.2928 15.15 25.2612 16 26.0212C16.85 25.2612 17.9563 24.2928 19.3188 23.116C20.6813 21.9392 22.0063 20.6766 23.2938 19.3281C24.5813 17.9797 25.6875 16.5884 26.6125 15.1542C27.5375 13.7199 28 12.3654 28 11.0905C28 9.61952 27.5 8.39368 26.5 7.41301C25.5 6.43234 24.25 5.94201 22.75 5.94201C21.575 5.94201 20.575 6.20556 19.75 6.73267C18.925 7.25978 18.15 8.02593 17.425 9.03112C17.25 9.27628 17.0375 9.46016 16.7875 9.58274C16.5375 9.70533 16.275 9.76662 16 9.76662C15.725 9.76662 15.4625 9.70533 15.2125 9.58274C14.9625 9.46016 14.75 9.27628 14.575 9.03112Z" fill="#0070CC"/>
                     </svg>
                     <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" fill="none" class="icon-fav" v-else @click="cruise.fav = false">
                        <path d="M16 29C15.65 29 15.2938 28.9387 14.9313 28.8161C14.5688 28.6935 14.25 28.4974 13.975 28.2277L11.3875 25.9109C8.7375 23.5328 6.34375 21.173 4.20625 18.8317C2.06875 16.4903 1 13.9099 1 11.0905C1 8.78595 1.7875 6.86139 3.3625 5.31683C4.9375 3.77228 6.9 3 9.25 3C10.575 3 11.825 3.27581 13 3.82744C14.175 4.37907 15.175 5.13296 16 6.08911C16.825 5.13296 17.825 4.37907 19 3.82744C20.175 3.27581 21.425 3 22.75 3C25.1 3 27.0625 3.77228 28.6375 5.31683C30.2125 6.86139 31 8.78595 31 11.0905C31 13.9099 29.9375 16.4965 27.8125 18.8501C25.6875 21.2037 23.275 23.5695 20.575 25.9477L18.025 28.2277C17.75 28.4974 17.4313 28.6935 17.0688 28.8161C16.7063 28.9387 16.35 29 16 29Z" fill="#0070CC"/>
                     </svg>
                  </div>
                  <div class="countday__togo">
                     {{ cruise.countDays }} &nbsp;•&nbsp;  {{new Date(cruise.arrivedFromDate).toLocaleString('uk-UA', {day: '2-digit', month: '2-digit', year: 'numeric'})}} &nbsp;-&nbsp; {{new Date(cruise.arrivedToDate).toLocaleString('uk-UA', {day: '2-digit', month: '2-digit', year: 'numeric'})}} &nbsp;•&nbsp; {{ cruise.arrivedFrom }} &nbsp;→&nbsp; {{ cruise.arrivedTo }}
                  </div>
                  <div class="place__visit">
                     ПОДОРОЖ ({{ cruise.distance }} км): &nbsp;
                     <span v-for="(point, index) in cruise.cruisePoint" :key="index">{{ point.pointName }} &nbsp;{{ cruise.cruisePoint.length == index + 1 ? '' : '•' }}&nbsp; </span>
                  </div>
                  <div class="countcost__bonus">
                     від ₴{{ cruise.startPrice }}
                     <div class="wrapper__bonus">
                        <img src="../img/find-cruise/ticket.svg" alt="" class="icon-bonus">
                        +5555555 бонусів
                     </div>
                  </div>
                  <button class="take__cruise" @click="selectCruise(cruise.idCruise); $router.push('/reservcabin')">Більше інформації</button>
               </div>
            </li>
         </ul>
      </div>
   </div>
   <footer class="foter__info" id="CompanyInfo">
      <div class="lower__part">
         <div class="container main"> 
            <div class="social__info">
               <div class="item__social">Instagram</div>
               <div class="item__social">Viber</div>
               <div class="item__social">Telegram</div>
            </div>
            <img src="../img/find-cruise/logoblack.png" alt="" class="icon-logo">
            <div class="social__info">
               <div class="item__social">м. Київ, вул. Хрещатик 322/8</div>
               <div class="item__social">+38 (098) 765-34-21</div>
               <div class="item__social">support@cruisevoyage.com</div>
            </div>
         </div>
      </div>
   </footer>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from 'vue';
import { useAuthInfo } from '@/stores/Auth';
import { useCruiseInfo } from '@/stores/Cruise';
import { Calendar, DatePicker } from 'v-calendar';
import 'v-calendar/style.css';

export default defineComponent({
   components: {

   },
   setup () {
      const authStore = useAuthInfo();
      const cruiseStore = useCruiseInfo();

      const scrollToTarget = (target: string) => {
         const element = document.getElementById(target);
         if (element) {
            element.scrollIntoView({
               behavior: 'smooth'
            });
         }
      };

      const selectCruise = (cruiseId: number) => {
         cruiseStore.fetchCabinsByCruise(cruiseId);
         const indexCruise = cruiseStore.cruisesList.findIndex(cruise => cruise.idCruise == cruiseId);
         if (indexCruise != -1) {
            cruiseStore.activeCruise = indexCruise;
         }
      };

      onBeforeMount(() => {
         cruiseStore.fetchCruises(); 
         // if (!authStore.isUserLoggedIn()) {
         //    authStore.Logout();
         // }
      });

      return {
         authStore,
         cruiseStore,
         Calendar,
         DatePicker,
         scrollToTarget,
         selectCruise
      }
   }
})
</script>

<style lang="scss" scoped>
   .header__navigation {
      background-image: url('../img/find-cruise/headerbg.jpg');
      background-size: cover;
      object-fit: cover;
      height: get-vh(350px);
      background-repeat: no-repeat;
      padding-top: get-vh(40px);
      & > .container {
         &.main {
            display: flex;
            align-items: center;
            justify-content: space-between;
            &:nth-child(2) {
               align-items: start;
               flex-direction: column;
            }
            & > .icon-logo {
               cursor: pointer;
               width: get-vh(208px);
               height: get-vh(64px);
            }
            & > .navigation {
               & > ul {
                  display: flex;
                  gap: get-vh(38px);
                  & > li {
                     font-size: get-vh(22px);
                     font-weight: 500;
                     letter-spacing: -0.02em;
                     & > a {
                        position: relative;
                        color: #FFF;
                        &::before {
                           content: "";
                           position: absolute;
                           right: 0;
                           bottom: get-vh(-6px);
                           width: 0%;
                           height: get-vh(2px);
                           border-radius: get-vh(4px);
                           background: #FFF;
                           transition: width 0.25s ease-out;
                        }
                        &:hover {
                           &::before { 
                              left: 0;
                              width: 100%;
                           }
                        }
                     }
                  }
               }
            }
            & > .wrapper__btns {
               display: flex;
               gap: get-vh(16px);
               & > .singin {
                  height: get-vh(48px);
                  padding: 0px get-vh(24px);
                  font-size: get-vh(22px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
                  border-radius: get-vh(16px);
                  background: rgba(255, 255, 255, 0.20);
                  &:hover {
                     background: linear-gradient(0deg, rgba(255, 255, 255, 0.20) 0%, rgba(255, 255, 255, 0.20) 100%), rgba(255, 255, 255, 0.20);
                  }
                  &:nth-child(2) {
                     background: #0070CC;
                     &:hover {
                        background: linear-gradient(0deg, rgba(255, 255, 255, 0.20) 0%, rgba(255, 255, 255, 0.20) 100%), #0070CC;
                     }
                  }
               }
            }
         }
      }
   }
   .wrapper__find-cruise {
      position: absolute;
      top: get-vh(163px);
      width: 100%;
      @include centerHorizontal;
      justify-content: center;
      & > .container__findallcruise {
         position: relative;
         width: get-vh(1236px);
         height: get-vh(72px);
         & > .container__find-cruise {
            position: absolute;
            left: 0;
            z-index: 3;
            width: 100%;
            height: 100%;
            padding-left: get-vh(24px);
            border-radius: get-vh(16px);
            background: #FFF;
            display: flex;
            gap: get-vh(64px);
            transition: box-shadow 0.25s ease-out;
            &.active {
               box-shadow: 0px get-vh(4px) get-vh(16px) 0px rgba(0, 0, 0, 0.50);
            }
            & > .wrapper__find-something {
               position: relative;
               display: flex;
               flex-direction: column;
               justify-content: center;
               & > .title__find {
                  color: $txtFindTitle;
                  font-size: get-vh(14px);
                  font-weight: 500;
                  letter-spacing: -0.03em;
               }
               & > .subtitle__find {
                  width: get-vh(266px);
                  white-space: nowrap;
                  overflow: hidden;
                  text-overflow: ellipsis;
                  color: $txtFind;
                  font-size: get-vh(20px);
                  font-weight: 600;
                  line-height: 1;
                  letter-spacing: -0.02em;
               }
               & > .icon-arrow {
                  cursor: pointer;
                  position: absolute;
                  width: get-vh(20px);
                  height: get-vh(20px);
                  top: get-vh(26px);
                  left: get-vh(280px);
                  transition: transform 0.25s ease-out;
                  &.rotate {
                     transform: rotate(180deg);
                  }
               }
            }
            & > .find__cruise {
               margin-left: auto;
               width: get-vh(206px);
               height: 100%;
               background: #0070CC;
               border-radius: 0 get-vh(15px) get-vh(15px) 0;
               font-size: get-vh(22px);
               font-weight: 600;
               letter-spacing: -0.03em;
               &:hover {
                  background: linear-gradient(0deg, rgba(255, 255, 255, 0.20) 0%, rgba(255, 255, 255, 0.20) 100%), #0070CC;
               }
            }
         }
         & > .wrapper__find-cruises {
            position: absolute;
            z-index: 2;
            top: get-vh(56px);
            border-radius: 0px 0px get-vh(16px) get-vh(16px);
            background: #FFF;
            box-shadow: 0px get-vh(4px) get-vh(16px) 0px rgba(0, 0, 0, 0.50);
            height: 0;
            width: get-vh(365px);
            transition: height 0.25s ease-out;
            &:nth-child(3) {
               left: get-vh(340px);
            }
            &.visible, &.visibletwo {
               height: get-vh(318px);
               & > .list__name__cruises { 
                  opacity: 1;
                  transition: opacity 0.25s ease-out 0.3s;
               }
            }
            & > .list__name__cruises {
               opacity: 0;
               height: get-vh(318px);
               width: get-vh(344px);
               padding-top: get-vh(34px);
               padding-bottom: get-vh(18px);
               padding-left: get-vh(24px);
               display: flex;
               flex-direction: column;
               gap: get-vh(24px);
               overflow-y: scroll;
               &::-webkit-scrollbar {
                  width: get-vh(8px);
                  border-radius: get-vh(100px);
               } 
               &::-webkit-scrollbar-track {
                  background: rgba(0, 112, 204, 0.15);
                  border-radius: get-vh(100px);
                  margin-top: get-vh(34px);
                  margin-bottom: get-vh(18px);
               } 
               &::-webkit-scrollbar-thumb {
                  background: #0070CC;
                  border-radius: get-vh(100px);
               }
               & > .item__cruise {
                  cursor: pointer;
                  color: $txtBlack;
                  font-size: get-vh(20px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
                  transition: color 0.25s ease-out;
                  &.active {
                     color: $txtFind;
                  }
               }
            }
         }
      }
   }
   .countfind__sort {
      margin-top: get-vh(36px);
      color: black;
      & > .container {
         &.main {
            @include centerHorizontal;
            justify-content: space-between;
            color: $txtBlack;
            font-size: get-vh(22px);
            font-weight: 600;
            letter-spacing: -0.02em;
         }
      }
      & > .container {
         &.main { 
            & > .list__cruises {
               margin-top: get-vh(36px);
               width: 100%;
               display: flex;
               flex-direction: column;
               gap: get-vh(24px);
               & > .item__cruise {
                  width: 100%;
                  height: get-vh(276px);
                  border-radius: get-vh(20px);
                  background: #FFF;
                  box-shadow: 0px get-vh(2px) get-vh(64px) 0px rgba(0, 0, 0, 0.10);
                  display: flex;
                  overflow: hidden;
                  & > .wrapper__icon {
                     position: relative;
                     width: get-vh(450px);
                     height: 100%;
                     flex-shrink: 0;
                     & > .icon-cruise {
                        width: 100%;
                        height: 100%;
                        object-fit: cover;
                     }
                     & > .raiting__cruise {
                        position: absolute;
                        top: get-vh(32px);
                        right: get-vh(32px);
                        @include centerHorizontal;
                        & > .star {
                           position: relative;
                           width: get-vh(17px);
                           height: get-vh(17px);
                           fill: white;
                           transition: fill .25s ease;
                           &.active {
                              fill: #FFB800;
                           }
                        }
                        // & > .yellow {
                        //    position: absolute;
                        //    height: get-vh(17px);
                        //    width: get-vh(17px);
                        //    background: linear-gradient(90deg, #FFBF00 94.23%, rgba(255, 255, 255, 0.40) 94.24%);
                        // }
                     }
                     & > .travel__to {
                        position: absolute;
                        left: get-vh(30px);
                        bottom: get-vh(30px);
                        width: get-vh(386px);
                        height: get-vh(120px);
                        display: flex;
                        align-items: end;
                        color: $txtPrimary;
                        text-shadow: 0px get-vh(2px) get-vh(20px) rgba(0, 0, 0, 0.50);
                        font-size: get-vh(44px);
                        font-weight: 600;
                        line-height: 1.04;
                        letter-spacing: -0.03em;
                     }
                  }
                  & > .right__side {
                     position: relative;
                     width: 100%;
                     height: 100%;
                     & > .header__ship-fav {
                        margin-top: get-vh(36px);
                        padding: 0 get-vh(36px);
                        width: 100%;
                        display: flex;
                        justify-content: space-between;
                        & > .name__ship {
                           @include centerHorizontal;
                           gap: get-vh(6px);
                           color: $txtFind;
                           font-size: get-vh(22px);
                           font-weight: 700;
                           letter-spacing: -0.03em;
                           & > .icon-ship {
                              width: get-vh(20px);
                              height: get-vh(20px);
                           }
                        }
                        & > .icon-fav {
                           width: get-vh(32px);
                           height: get-vh(32px);
                        }
                     }
                     & > .countday__togo {
                        margin-top: get-vh(20px);
                        padding: 0 get-vh(36px);
                        display: flex;
                        font-size: get-vh(24px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                     }
                     & > .place__visit {
                        margin-top: get-vh(20px);
                        padding-left: get-vh(36px);
                        width: get-vh(950px);
                        height: get-vh(40px);
                        display: flex;
                        font-size: get-vh(16px);
                        font-weight: 600;
                        line-height: 1.25;
                        letter-spacing: -0.03em;
                     }
                     & > .countcost__bonus {
                        margin-top: get-vh(24px);
                        padding-left: get-vh(36px);
                        @include centerHorizontal;
                        gap: get-vh(20px);
                        font-size: get-vh(36px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                        & > .wrapper__bonus {
                           @include centerHorizontal;
                           gap: get-vh(8px);
                           color: $txtBonus;
                           font-size: get-vh(20px);
                           font-weight: 700;
                           letter-spacing: -0.03em;
                        }
                     }
                     & > .take__cruise {
                        position: absolute;
                        right: 0;
                        bottom: 0;
                        width: get-vh(335px);
                        height: get-vh(64px);
                        border-radius: get-vh(20px) 0px 0px 0px;
                        background: #0070CC;  
                        font-size: get-vh(24px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                        &:hover {
                           background: linear-gradient(0deg, rgba(255, 255, 255, 0.20) 0%, rgba(255, 255, 255, 0.20) 100%), #0070CC;
                        }
                     }
                  }
               }
            }
         }
      }
   }
   .foter__info {
      height: get-vh(180px);
      & > .lower__part {
         margin-top: get-vh(72px);
         width: 100%;
         height: get-vh(180px);
         background: #E4E4E7;
         & > .container {
            &.main {
               height: 100%;
               display: flex;
               justify-content: center;
               align-items: center;
               gap: get-vh(72px);
               & > .icon-logo {
                  width: get-vh(208px);
                  height: get-vh(64px);
               }
               & > .social__info {
                  width: get-vh(300px);
                  display: flex;
                  flex-direction: column;
                  gap: get-vh(16px);
                  text-align: right;
                  &:nth-child(3) {
                     text-align: left;
                     & > .item__social { 
                        &:hover {
                           color: $txtBlack;
                        }
                     }
                  }
                  & > .item__social {
                     color: $txtBlack;
                     font-size: get-vh(20px);
                     font-weight: 600;
                     line-height: 1.3;
                     letter-spacing: -0.03em;
                     transition: color 0.25s ease-out;
                     &:hover {
                        color: $txtFind;
                     }
                  }
               }
            }
         }
      }
   }
</style>