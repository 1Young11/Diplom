<template>
   <header class="header__navigation" @click="cruiseStore.checkArrivedFrom = false; cruiseStore.checkArrivedTo = false; cruiseStore.checkArrivedDate = false">
      <div class="container main">
         <img src="../img/reg-auth/logo.png" alt="" class="icon-logo">
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
      <div class="container main" id="Cruises">
         <div class="wrapper__find-cruise">
            <div class="container__findallcruise">
               <div class="container__find-cruise" :class="{active: cruiseStore.checkArrivedFrom || cruiseStore.checkArrivedTo}">
                  <div class="wrapper__find-something">
                     <div class="title__find">ВІДПРАВЛЕННЯ З</div>
                     <div class="subtitle__find">{{ cruiseStore.activeArrivedFrom == -1 ? 'Будь-якого міста' : cruiseStore.cruisesList[cruiseStore.activeArrivedFrom].arrivedFrom }}</div>
                     <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedFrom}" @click.stop="cruiseStore.checkArrivedFrom = !cruiseStore.checkArrivedFrom; cruiseStore.checkArrivedTo = false">
                  </div>
                  <div class="wrapper__find-something">
                     <div class="title__find">ПРИБУТТЯ У</div>
                     <div class="subtitle__find">{{ cruiseStore.activeArrivedTo == -1 ? 'Будь-яке місто' : cruiseStore.cruisesList[cruiseStore.activeArrivedTo].arrivedTo }}</div>
                     <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedTo}" @click.stop="cruiseStore.checkArrivedTo = !cruiseStore.checkArrivedTo; cruiseStore.checkArrivedFrom = false">
                  </div>
                  <div class="wrapper__find-something">
                     <div class="title__find">ДАТА ВІДПРАВЛЕННЯ</div>
                     <div class="subtitle__find">{{ cruiseStore.currentDate == undefined ? 'Будь-яка дата' : new Date(cruiseStore.currentDate).toLocaleDateString('uk-UA', {day: '2-digit', month: '2-digit', year: 'numeric'}) }}</div>
                     <img src="../img/mainpage/icon_expand.svg" alt="" class="icon-arrow" :class="{rotate: cruiseStore.checkArrivedDate}" @click.stop="cruiseStore.checkArrivedDate = !cruiseStore.checkArrivedDate">
                  </div>
                  <button class="find__cruise">Пошук</button>
               </div>
               <div class="wrapper__find-cruises" :class="{visible: cruiseStore.checkArrivedFrom}">
                  <ul class="list__name__cruises">
                     <li class="item__cruise" v-for="(cruise, index) in cruiseStore.cruisesList" :key="index" 
                        @click.stop="cruiseStore.activeArrivedFrom = index" 
                        :class="{active: cruiseStore.activeArrivedFrom == index}"
                        v-show="cruiseStore.checkArrivedFrom">
                        {{ cruise.arrivedFrom }}
                     </li>
                  </ul>
               </div>
               <div class="wrapper__find-cruises" :class="{visibletwo: cruiseStore.checkArrivedTo}">
                  <ul class="list__name__cruises">
                     <li class="item__cruise" v-for="(cruise, index) in cruiseStore.cruisesList" :key="index" 
                        @click.stop="cruiseStore.activeArrivedTo = index" 
                        :class="{active: cruiseStore.activeArrivedTo == index}"
                        v-show="cruiseStore.checkArrivedTo">
                        {{ cruise.arrivedTo }}
                     </li>
                  </ul>
               </div>
               <VDatePicker v-model="cruiseStore.currentDate" v-if="cruiseStore.checkArrivedDate" :class="{visibletwo: cruiseStore.checkArrivedDate}"/>
            </div>
         </div>
      </div>
   </header>
</template>

<script lang="ts">
import { ref, defineComponent, onBeforeMount } from 'vue';
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

      return {
         authStore,
         cruiseStore,
         Calendar,
         DatePicker,
         scrollToTarget
      }
   }
})
</script>

<style lang="scss" scoped>
   .header__navigation {
      background-image: url('../img/find-cruise/headerbg.jpg');
      background-size: cover;
      min-height: get-vh(360px);
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
            & > .wrapper__find-cruise {
               width: 100%;
               margin-top: get-vh(56px);
               @include centerHorizontal;
               justify-content: center;
               & > .container__findallcruise {
                  position: relative;
                  width: get-vh(912px);
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
                           width: get-vh(194px);
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
                           left: get-vh(204px);
                           transition: transform 0.25s ease-out;
                           &.rotate {
                              transform: rotate(180deg);
                           }
                        }
                     }
                     & > .find__cruise {
                        margin-left: auto;
                        width: get-vh(126px);
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
                     width: get-vh(325px);
                     transition: height 0.25s ease-out;
                     &:nth-child(3) {
                        left: get-vh(280px);
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
                        width: get-vh(314px);
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
            & > .subtitle__text {
               margin-top: get-vh(14px);
               text-shadow: 0px get-vh(2px) get-vh(16px) rgba(0, 0, 0, 0.25);
               font-size: get-vh(20px);
               font-weight: 500;
               letter-spacing: -0.02em;
            }
         }
      }
   }
</style>