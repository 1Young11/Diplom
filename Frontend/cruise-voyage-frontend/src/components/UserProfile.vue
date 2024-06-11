<template>
   <div class="wrapper">
      <div class="blue__bg"></div>
      <header class="header__navigation">
         <div class="container main">
            <img src="../img/reg-auth/logo.png" alt="" class="icon-logo">
            <div class="wrapper__btns">
               <button class="singin">ВИЙТИ З АКАУНТА</button>
            </div>
         </div>
      </header>
      <main class="main__wrapper">
         <div class="wrapper__leftpart">
            <div class="circle__photo">
               <img src="../img/user/image.png" alt="" class="icon-user">
            </div>
            <div class="wrapper__info">
               {{ userStore.nameCustomer }} <br> {{ userStore.lastNameCustomer }}
            </div>
            <div class="sub__info">{{ userStore.phoneCustomer }}</div>
            <div class="sub__info">{{ userStore.email }}</div>
            <button class="choose__item" :class="{active: userStore.activeBlock == 0}" @click="userStore.activeBlock = 0">Бронювання</button>
            <button class="choose__item" :class="{active: userStore.activeBlock == 1}" @click="userStore.activeBlock = 1">Магазин бонусів</button>
         </div>
         <div class="wrapper__rightpart">
            <div class="item__hisstory" v-for="(history, index) in userStore.historyCruise" :key="index" :class="{notpayed: history.statusPayment == 0}">
               <div class="header__info">
                  <div class="name__travell">{{ history.cruises.arrivedTo }}</div>
                  <div class="count__bonus">
                     <img src="../img/find-cruise/ticket.svg" alt="" class="icon-bonus">
                        +{{ history.countBonus }} бонусів
                  </div>
                  <div class="count__days">
                     {{ new Date(history.cruises.cruisePoints[0].dateArrived).toLocaleString('uk-UA', {day: '2-digit'}) }} &nbsp; –  &nbsp; 
                     {{ new Date(history.cruises.cruisePoints[history.cruises.cruisePoints.length - 1].dateArrived).toLocaleString('uk-UA', {day: '2-digit'}) }}
                     {{ cruiseStore.formatDate(history.cruises.cruisePoints[0].dateArrived).slice(2) }} 
                     {{ new Date(history.cruises.cruisePoints[history.cruises.cruisePoints.length - 1].dateArrived).toLocaleString('uk-UA', {year: 'numeric'}) }}
                  </div>
                  <div class="make__rating">
                     Оцініть подорож 
                     <div class="container__star" @mouseleave="userStore.RatingFlag(-1)">
                        <svg width="48" height="48" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg"  @mouseover="userStore.RatingFlag(index)"
                           class="star" v-for="index in 5" :key="index" :class="{active: userStore.rating >= index}">
                           <g id="stars">
                              <path id="Vector" d="M24 35.3824L14.7959 40.9421C14.4262 41.1645 14.0843 41.2664 13.7701 41.2479C13.4559 41.2293 13.1694 41.1089 12.9107 40.8865C12.6889 40.7012 12.5133 40.4695 12.3839 40.1915C12.2545 39.9135 12.2453 39.5707 12.3562 39.163L14.7404 28.6552L6.6452 21.4832C6.34948 21.2978 6.15542 21.0477 6.06301 20.7326C5.9706 20.4176 5.97984 20.0933 6.09073 19.7597C6.20162 19.4632 6.38645 19.213 6.6452 19.0091C6.90395 18.8053 7.19966 18.6848 7.53234 18.6477L18.289 17.7582L22.5029 7.75074C22.6508 7.41716 22.8633 7.16698 23.1406 7.00019C23.4178 6.8334 23.7043 6.75 24 6.75C24.2957 6.75 24.5822 6.8334 24.8594 7.00019C25.1367 7.16698 25.3492 7.41716 25.4971 7.75074L29.7665 17.7582L40.4677 18.6477C40.8003 18.6848 41.0961 18.8053 41.3548 19.0091C41.6136 19.213 41.7984 19.4632 41.9093 19.7597C42.0202 20.0933 42.0294 20.4176 41.937 20.7326C41.8446 21.0477 41.6505 21.2978 41.3548 21.4832L33.2596 28.6552L35.6992 39.163C35.7732 39.5707 35.7454 39.9135 35.6161 40.1915C35.4867 40.4695 35.3111 40.7012 35.0893 40.8865C34.8306 41.1089 34.5441 41.2293 34.2299 41.2479C33.9157 41.2664 33.5923 41.1645 33.2596 40.9421L24 35.3824Z"/>
                           </g>
                        </svg>
                     </div> 
                  </div>
               </div>
               <div class="travell__point">ПОДОРОЖ: &nbsp;
                  <div class="item__point" v-for="(point, index) in history.cruises.cruisePoints" :key="index">{{ point.pointName }} &nbsp;{{ history.cruises.cruisePoints.length - 1 == index ? '' : '•' }}&nbsp;</div> 
               </div>
               <div class="dop__poslygi">Додаткові послуги: Триразове люкс-харчування, Комплекс спа та фітнесу, VIP-доступ, пріоритет у обслуговуванні.</div>
               <div class="footer__info">
                  <div class="left__info">
                     <div class="title__info">{{ history.orderCabinBed[0].cabinBed.cabin.cabinType.nameType }}, Каюта №{{ history.orderCabinBed[0].cabinBed.cabin.idCabin }} ({{ history.orderCabinBed[0].cabinBed.cabin.floor }} поверх)</div>
                     <div class="subtitle__info">{{ history.orderCabinBed.length }} ліжка, {{ history.orderCabinBed.length }} пасажира</div>
                  </div>
                  <div class="right__info">
                     {{ history.statusPayment == 0 ? 'До сплати:' : 'Cтатус оплачено'}} ₴{{ history.price }}
                  </div>
               </div>
            </div>
         </div>
      </main>
   </div>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from "vue";
import { useUserInfo } from "@/stores/UserInfo";
import { useCruiseInfo } from "@/stores/Cruise";

export default defineComponent({
   setup() {
      const userStore = useUserInfo();
      const cruiseStore = useCruiseInfo();

      onBeforeMount(() => {
         userStore.fetchHistoryOrder(4); 
      });

      return {
         userStore,
         cruiseStore,
      };
   },
});
</script>

<style lang="scss" scoped>
   .wrapper {
      position: relative;
      width: 100%;
      height: 100vh;
      background: #F4F4F5;
      & > .blue__bg {
         position: absolute;
         top: 0;
         z-index: 0;
         width: 100%;
         height: get-vh(404px);
         background: linear-gradient(0deg, rgba(24, 123, 205, 0.10) 0%, rgba(24, 123, 205, 0.10) 100%), linear-gradient(180deg, rgba(0, 81, 148, 0.90) 0%, rgba(0, 84, 153, 0.90) 2.7%, rgba(0, 86, 156, 0.90) 6.28%, rgba(0, 87, 159, 0.90) 10.64%, rgba(0, 88, 161, 0.90) 15.71%, rgba(0, 89, 163, 0.90) 21.42%, rgba(0, 90, 165, 0.90) 27.69%, rgba(0, 91, 166, 0.90) 34.44%, rgba(0, 92, 168, 0.90) 41.6%, rgba(0, 93, 170, 0.90) 49.09%, rgba(0, 95, 173, 0.90) 56.83%, rgba(0, 97, 177, 0.90) 64.74%, rgba(0, 99, 182, 0.90) 72.75%, rgba(0, 103, 188, 0.90) 80.79%, rgba(0, 107, 195, 0.90) 88.76%, rgba(0, 112, 204, 0.90) 96.61%);
      }
      & > .header__navigation {
         position: relative;
         z-index: 1;
         height: get-vh(128px);
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
      & > .main__wrapper {
         position: absolute;
         top: get-vh(164px);
         left: 50%;
         transform: translateX(-50%);
         z-index: 1;
         display: flex;
         gap: get-vh(24px);
         & > .wrapper__leftpart {
            width: get-vh(424px);
            height: get-vh(840px);
            border-radius: get-vh(32px);
            background: #FFF;
            box-shadow: 0px get-vh(2px) get-vh(32px) 0px rgba(0, 0, 0, 0.08);
            @include centerHorizontal;
            flex-direction: column;
            & > .circle__photo {
               margin-top: get-vh(50px);
               width: get-vh(200px);
               height: get-vh(200px);
               border-radius: 50%;
               overflow: hidden;
               & > .icon-user {
                  width: 100%;
                  height: 100%;
                  object-fit: cover;
               }
            }
            & > .wrapper__info {
               margin-top: get-vh(50px);
               text-align: center;
               color: #1A1A1A;
               font-size: get-vh(36px);
               font-style: italic;
               font-weight: 600;
               line-height: 1.4;
               letter-spacing: -0.02em;
            }
            & > .sub__info {
               margin-top: get-vh(50px);
               color: $txtUserInfo;
               text-align: center;
               font-size: get-vh(24px);
               font-weight: 600;
               line-height: 1.5;
               letter-spacing: -0.02em;
               &:nth-child(4) {
                  margin-top: get-vh(12px);
               }
            }
            & > .choose__item {
               margin-top: get-vh(120px);
               width: get-vh(360px);
               height: get-vh(72px);
               border-radius: get-vh(24px);
               background: rgba(24, 123, 205, 0.08);
               color: $txtFind;
               text-align: center;
               font-size: get-vh(28px);
               font-weight: 600;
               letter-spacing: -0.03em;
               &:hover {
                  background: rgba(24, 123, 205, 0.20);
               }
               &.active {
                  background: #177BCF;
                  color: $txtPrimary;
               }
               &:nth-child(6) {
                  margin-top: get-vh(10px);
               }
            }
         }
         & > .wrapper__rightpart {
            width: get-vh(1112px);
            height: get-vh(840px);
            border-radius: get-vh(32px);
            background: #FFF;
            box-shadow: 0px get-vh(2px) get-vh(32px) 0px rgba(0, 0, 0, 0.08);
            overflow-y: scroll;
            &::-webkit-scrollbar {
               width: 0;
            }
            & > .item__hisstory {
               width: 100%;
               height: get-vh(238px);
               padding: 0 get-vh(36px);
               color: $txtBlack;
               &:nth-child(even) {
                  background: rgba(26, 26, 26, 0.03);
               }
               &.notpayed {
                  background: rgba(24, 123, 205, 0.08);
                  color: $txtFind;
               }
               & > .header__info {
                  padding-top: get-vh(30px);
                  @include centerHorizontal;
                  & > .name__travell {
                     font-size: get-vh(40px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                  }
                  & > .count__bonus {
                     margin-left: get-vh(15px);
                     @include centerHorizontal;
                     gap: get-vh(8px);
                     color: $txtBonus;
                     font-size: get-vh(20px);
                     font-weight: 700;
                     letter-spacing: -0.03em;
                     & > .icon-bonus {
                        width: get-vh(24px);
                        height: get-vh(24px);
                     }
                  }
                  & > .count__days {
                     margin-left: get-vh(40px);
                     text-align: right;
                     font-size: get-vh(24px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                  }
                  & > .make__rating {
                     margin-left: auto;
                     @include centerHorizontal;
                     font-size: get-vh(18px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                     gap: get-vh(4px);
                     & > .container__star {
                        display: flex;
                        gap: get-vh(4px);
                        & > .star {
                           width: get-vh(30px);
                           height: get-vh(30px);
                           fill: rgba(0, 0, 0, 0.20);
                           &.active {
                              fill: #F48F00;;
                           }
                        }
                     }
                  }
               }
               & > .travell__point { 
                  margin-top: get-vh(20px);
                  display: flex;
                  flex-wrap: wrap;
                  text-wrap: nowrap;
                  font-size: get-vh(16px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
               }
               & > .dop__poslygi {
                  color: $txtBonus;
                  font-size: get-vh(16px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
               }
               & > .footer__info {
                  @include centerHorizontal;
                  justify-content: space-between;
                  margin-top: get-vh(20px);
                  & > .left__info {
                     & > .title__info {
                        font-size: get-vh(20px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                     }
                     & > .subtitle__info {
                        font-size: get-vh(16px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                     }
                  }
                  & > .right__info {
                     font-size: get-vh(26px);
                     font-weight: 700;
                     letter-spacing: -0.03em;
                  }
               }
            }
         }
      }
   }
</style>