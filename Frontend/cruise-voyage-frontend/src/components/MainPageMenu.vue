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
            <h2 class="find__text">Час подорожувати!</h2>
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
         <div class="subtitle__text">Обирайте серед найпопулярніших круїзів нижче або шукайте необхідний за власними параметрами.</div>
      </div>
   </header>
   <div class="container">
      <swiper
         :ref="swiper"
         @swiper="cruiseStore.onSwiper"
         :scrollbar="{
            el: '.swiper-scrollbar',
            hide: false,
            draggable: true,
         }"
         :modules="modules"
         :slidesPerView="'auto'"
         :centeredSlides="false"
         :navigation="{
            prevEl: prev,
            nextEl: next,
         }"
         :allowTouchMove="false"
         :initialSlide="0"
         :speed="500"
         class="swiper__list"
      >
         <swiper-slide class="item__swiper" v-for="(cruise, index) in cruiseStore.cruisesList" :key="index">
            <img :src="require(`../img/mainpage/travel${index}.jpg`)" alt="" class="icon-cruise">
            <div class="name__cruise">{{ cruise.arrivedTo }}</div>
            <div class="count__days">{{ cruise.countDays }} • від ₴{{ cruise.startPrice }}</div>
            <button class="buy__cruise">Придбати</button>
         </swiper-slide>
         <div class="swiper-scrollbar"></div>
      </swiper>
   </div>
   <div class="wrapper__nextslide">
      <img src="../img/mainpage/arrow.svg" alt="" class="icon-arrow" ref="prev">
      <img src="../img/mainpage/arrow.svg" alt="" class="icon-arrow" ref="next">
   </div>
   <main class="main__info" id="AboutCruise">
      <section class="why__cruise">
         <div class="container main">
            <div class="wrapper__whycruise">
               <h3 class="title__whycruise">Чому варто</h3>
               <h3 class="title__whycruise">відправитися У круїз</h3>
               <div class="subtitle__whycruise">Круїзи вважаються захоплюючим та комфортним<br> способом подорожувати, дозволяючи відкрити для себе <br>різноманітність культур, краєвидів та вражень. </div>
               <div class="subtitle__whycruise">Це не лише спосіб пересування від пункту А до пункту Б,<br> але й унікальний шлях переживання нових емоцій,<br> відчуття свободи на морі та можливість відвідувати<br> різноманітні місця без постійного переїзду.</div>
            </div>
         </div>
      </section>
      <section class="why__ourcompany" id="AboutCompany">
         <div class="container small">
            <h3 class="title__whyourcompany">Чoму саме ми?</h3>
            <div class="subtitle__whyour">Досліджуйте з нами, як ніхто інший. Оберіть Cruise Voyage для вашої наступної <br> круїзної подорожі і відкрийте для себе світ по новому. Ми пропонуємо:</div>
            <ul class="list__propouse">
               <li class="item__propouse"></li>
               <li class="item__propouse"></li>
               <li class="item__propouse"></li>
               <li class="item__propouse"></li>
            </ul>
         </div>
      </section>
   </main>
   <footer class="foter__info" id="CompanyInfo">
      <div class="container main"> 
         <div class="description__title"><span class="bluetext">Розкішне обслуговування</span> з урахуванням всіх ваших потреб і побажань. <br>Наша команда професіоналів працює невтомно, щоб забезпечити вам незабутній відпочинок. </div>
         <div class="description__title">Вирушайте в незабутню подорож до найкращих місць на землі - ми пропонуємо <span class="bluetext">найцікавіші маршрути</span><br>на круїзних лайнерах. Насолоджуйтеся культурою, природою та історією кожного краю.</div>
         <div class="description__title">Наші круїзні лайнери <span class="bluetext">обладнані всім необхідним для вашого комфорту</span>. Насолоджуйтеся розкішними каютами,<br>смачною кухнею, розважальними програмами та безліччю зручностей на борту.</div>
         <div class="wrapper__trubl">
            <div class="trubl__left">
               <div class="trubl__left-title">Зацікавлені? <br> НАПИШІТЬ НАМ!</div>
               <div class="trubl__left-subtitle">Наші професійні турагенти передзвонять вам<br>у робочий час для уточнення усіх деталей</div>
            </div>
            <div class="wrapper__yourinfo">
               <div class="name__email-phone">
                  <input type="text" class="personal__info" placeholder="Ваше ім’я">
                  <input type="text" class="personal__info" placeholder="Номер телефону">
                  <input type="text" class="personal__info" placeholder="Електронна пошта">
               </div>
               <div class="your__quest">
                  <textarea class="question" placeholder="Все, що вас цікавить!"></textarea>
                  <div class="wrapper__send-msg">
                     Відправити повідомлення!
                     <svg xmlns="http://www.w3.org/2000/svg" width="22" height="20" viewBox="0 0 22 20" fill="none" class="icon-send">
                        <path d="M1.63333 18.9331C1.23333 19.0887 0.861111 19.0498 0.516667 18.8165C0.172222 18.5831 0 18.2554 0 17.8331V13.1998L9.6 9.99981L0 6.79981V2.16648C0 1.74426 0.172222 1.41648 0.516667 1.18314C0.861111 0.949811 1.23333 0.910922 1.63333 1.06648L21.2 8.89981C21.7111 9.09981 21.9667 9.46648 21.9667 9.99981C21.9667 10.5331 21.7111 10.8998 21.2 11.0998L1.63333 18.9331Z"/>
                     </svg>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <div class="lower__part">
         <div class="container main"> 
            <div class="social__info">
               <div class="item__social">Instagram</div>
               <div class="item__social">Viber</div>
               <div class="item__social">Telegram</div>
            </div>
            <img src="../img/reg-auth/logo.png" alt="" class="icon-logo">
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
import { ref, defineComponent, onBeforeMount } from 'vue';
import { useAuthInfo } from '@/stores/Auth';
import { useCruiseInfo } from '@/stores/Cruise';
import { Calendar, DatePicker } from 'v-calendar';
import 'v-calendar/style.css';
import { Navigation, Pagination, A11y, Scrollbar } from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/vue';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';

export default defineComponent({
   components: {
      Swiper,
      SwiperSlide,
   },
   setup () {
      const authStore = useAuthInfo();
      const cruiseStore = useCruiseInfo();

      const prev = ref(null);
      const next = ref(null);

      const swiper = 'swiper';

      onBeforeMount(() => {
         cruiseStore.fetchCruises(); 
         cruiseStore.fetchCabinsType(); 
         if (!authStore.isUserLoggedIn()) {
            authStore.Logout();
         }
      });

      const scrollToTarget = (target: string) => {
         const element = document.getElementById(target);
         if (element) {
            element.scrollIntoView({
               behavior: 'smooth'
            });
         }
      };


      return {
         modules: [Navigation, Pagination, A11y, Scrollbar],
         authStore,
         cruiseStore,
         Calendar,
         DatePicker,
         prev,
         next,
         swiper,
         scrollToTarget
      }
   }
})
</script>

<style lang="scss" scoped>
   .header__navigation {
      background-image: url('../img/mainpage/headbg.png');
      background-size: cover;
      min-height: get-vh(655px);
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
               margin-top: get-vh(84px);
               @include centerHorizontal;
               justify-content: space-between;
               & > .find__text {
                  text-shadow: 0px get-vh(2px) get-vh(16px) rgba(0, 0, 0, 0.25);
                  font-size: get-vh(56px);
                  font-style: italic;
                  font-weight: 600;
                  letter-spacing: -0.03em;
               }
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
   .container {
      width: get-vh(1980px);
   }
   .swiper__list {
      margin-top: get-vh(-262px);
      width: 100%;
      height: get-vh(499px);
   }
   .item__swiper {
      position: relative;
      width: get-vh(450px);
      height: get-vh(450px);
      border-radius: get-vh(20px);
      overflow: hidden;
      margin: 0 get-vh(16px);
      flex-shrink: 0;
      display: flex;
      flex-direction: column;
      justify-content: end;
      align-items: center;
      &:first-child {
         margin-left: get-vh(100px);
      }
      & > .icon-cruise {
         position: absolute;
         z-index: -1;
         width: 100%;
         height: 100%;
         object-fit: cover;
      }
      & > .name__cruise {
         width: get-vh(400px);
         height: get-vh(120px);
         margin-bottom: get-vh(24px);
         display: flex;
         flex-direction: column;
         justify-content: end;
         align-items: center;
         text-shadow: 0px get-vh(2px) get-vh(24px) rgba(0, 0, 0, 0.50);
         font-size: get-vh(48px);
         font-weight: 600;
         line-height: 1.12;
         letter-spacing: -0.03em;
      }
      & > .count__days {
         margin-bottom: get-vh(40px);
         text-shadow: 0px get-vh(2px) get-vh(24px) rgba(0, 0, 0, 0.50);
         font-size: get-vh(20px);
         font-weight: 600;
         line-height: 1;
         letter-spacing: -0.02em;
      }
      & > .buy__cruise {
         width: 100%;
         height: get-vh(96px);
         border-radius: 0px 0px get-vh(20px) get-vh(20px);
         background: rgba(255, 255, 255, 0.30);
         font-size: get-vh(32px);
         font-weight: 600;
         letter-spacing: -0.02em;
         &:hover {
            background: rgba(0, 112, 204, 1);
         }
      }
   }
   .swiper-scrollbar {
      height: get-vh(16px);
      width: get-vh(1414px);
      margin-left: get-vh(80px);
   }
   .wrapper__nextslide {
      position: relative;
      z-index: 1;
      width: get-vh(100px);
      left: get-vh(1546px);
      top: get-vh(-2px);
      margin-top: get-vh(-30px);
      @include centerHorizontal;
      justify-content: end;
      gap: get-vh(16px);
      cursor: pointer;
      & > .icon-arrow {
         width: get-vh(42px);
         height: get-vh(42px);
         &:nth-child(1) {
            transform: rotate(180deg);
         }
      }
   }
   .main__info {
      margin-top: get-vh(49px);
      & > .why__cruise {
         width: get-vh(1980px);
         height: get-vh(600px);
         background: linear-gradient(270deg, #F4F4F5 18.57%, rgba(244, 244, 245, 0.88) 37.84%, rgba(244, 244, 245, 0.68) 51.04%, rgba(244, 244, 245, 0.00) 74.9%), url('../img/mainpage/whycruise.jpg') lightgray 0px -237.731px / 100% 213.634% no-repeat;
         background-size: cover;
         background-position: 10% 30%;
         & > .container {
            &.main {
               display: flex;
               justify-content: end;
               & > .wrapper__whycruise {
                  text-align: right;
                  padding-top: get-vh(60px);
                  color: $txtBlack;
                  & > .title__whycruise {
                     font-size: get-vh(54px);
                     font-weight: 500;
                     line-height: 1.4;
                     text-transform: uppercase;
                     &:nth-child(2) {
                        background: linear-gradient(195deg, #33A3FF 6.94%, #006FCB 86.32%);
                        background-clip: text;
                        -webkit-background-clip: text;
                        -webkit-text-fill-color: transparent;
                        leading-trim: both;
                        text-edge: cap;
                     }
                  }
                  & > .subtitle__whycruise {
                     margin-top: get-vh(50px);
                     font-size: get-vh(24px);
                     font-weight: 500;
                     line-height: 1.3;
                     letter-spacing: -0.01em;
                     &:nth-child(4) {
                        margin-top: get-vh(30px);
                     }
                  }
               }
            }
         }
      }
      & > .why__ourcompany {
         padding: get-vh(100px) 0;
         display: flex;
         flex-direction: column;
         justify-content: center;
         & > .container {
            &.small {
               display: flex;
               flex-direction: column;
               justify-content: center;
               text-align: center;
               gap: get-vh(60px);
               & > .title__whyourcompany {
                  font-size: get-vh(54px);
                  font-weight: 500;
                  text-transform: uppercase;
                  background: linear-gradient(195deg, #33A3FF 6.94%, #006FCB 86.32%);
                  background-clip: text;
                  -webkit-background-clip: text;
                  -webkit-text-fill-color: transparent;
               }
               & > .subtitle__whyour {
                  color: $txtBlack;
                  font-size: get-vh(28px);
                  font-weight: 500;
                  line-height: 1.3;
                  letter-spacing: -0.01em;
               }
               & > .list__propouse {
                  display: flex;
                  justify-content: center;
                  gap: get-vh(64px);
                  & > .item__propouse {
                     width: get-vh(210px);
                     height: get-vh(221px);
                     background-image: url('../img/mainpage/rest.jpg');
                     background-size: cover;
                     &:nth-child(2) {
                        width: get-vh(270px);
                        background-image: url('../img/mainpage/comfort.jpg');
                     }
                     &:nth-child(3) {
                        background-image: url('../img/mainpage/eat.jpg');
                     }
                     &:nth-child(4) {
                        width: get-vh(280px);
                        background-image: url('../img/mainpage/oldyoung.jpg');
                     }
                  }
               }
            }
         }
      }
   }
   .foter__info {
      padding-top: get-vh(100px);
      height: get-vh(980px);
      background-image: url('../img/mainpage/bottom.jpg');
      background-size: cover;
      & > .container {
         &.main {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            text-align: center;
            & > .description__title {
               text-align: center;
               font-size: get-vh(24px);
               font-weight: 500;
               line-height: 1.3;
               letter-spacing: -0.01em;
               & > .bluetext {
                  font-style: italic;
                  color: #259CFF;
               }
               &:not(:first-child) {
                  margin-top: get-vh(30px);
               }
            }
            & > .wrapper__trubl {
               margin-top: get-vh(100px);
               display: flex;
               align-items: center;
               gap: get-vh(50px);
               & > .trubl__left {
                  text-align: left;
                  width: get-vh(600px);
                  & > .trubl__left-title {
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(30, 30, 30, 0.75);
                     font-size: get-vh(54px);
                     font-style: italic;
                     font-weight: 700;
                     line-height: 1.4;
                     letter-spacing: -0.03em;
                     text-transform: uppercase;
                  }
                  & > .trubl__left-subtitle {
                     margin-top: get-vh(12px);
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(30, 30, 30, 0.75), 0px get-vh(2px) get-vh(24px) rgba(30, 30, 30, 0.75);
                     font-size: get-vh(24px);
                     font-style: normal;
                     font-weight: 500;
                     line-height: 1.3;
                     letter-spacing: -0.03em;
                  }
               }
               & > .wrapper__yourinfo {
                  display: flex;
                  gap: get-vh(12px);
                  & > .name__email-phone {
                     display: flex;
                     flex-direction: column;
                     gap: get-vh(12px);
                     & > .personal__info {
                        width: get-vh(398px);
                        height: get-vh(64px);
                        padding: 0 get-vh(24px);
                        border-radius: get-vh(24px);
                        background: rgba(0, 0, 0, 0.50);
                        font-size: get-vh(20px);
                        font-weight: 500;
                        letter-spacing: -0.03em;
                        &::placeholder {
                           color: $txtPlaceholder;
                        }
                     }
                  }
                  & > .your__quest {
                     display: flex;
                     flex-direction: column;
                     gap: get-vh(12px);
                     & > .question {
                        width: get-vh(500px);
                        height: get-vh(140px);
                        padding: get-vh(23px) get-vh(24px);
                        border-radius: get-vh(24px);
                        background: rgba(0, 0, 0, 0.50);
                        resize: none;
                        font-size: get-vh(20px);
                        font-weight: 500;
                        line-height: 0.9;
                        letter-spacing: -0.03em;
                        &::placeholder {
                           color: $txtPlaceholder;
                        }
                        &::-webkit-scrollbar {
                           width: 0;
                        }
                     }
                     & > .wrapper__send-msg {
                        width: get-vh(500px);
                        height: get-vh(64px);
                        padding: get-vh(23px) get-vh(24px);
                        display: flex;
                        align-items: center;
                        justify-content: space-between;
                        border-radius: get-vh(24px);
                        background: #FFF;
                        color: $txtBlack;
                        font-size: get-vh(20px);
                        font-weight: 600;
                        letter-spacing: -0.03em;
                        cursor: pointer;
                        transition: background 0.25s ease-out, color 0.25s ease-out;
                        &:hover {
                           background: #0070CC;
                           color: $txtPrimary;
                           & > .icon-send { 
                              fill: #fff;
                           }
                        }
                        & > .icon-send {
                           fill: #000;
                           width: get-vh(24px);
                           height: get-vh(32px);
                           transition: fill 0.25s ease-out;
                        }
                     }
                  }
               }
            }
         }
      }
      & > .lower__part {
         margin-top: get-vh(126px);
         width: 100%;
         height: get-vh(180px);
         background: rgba(0, 0, 0, 0.50);
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
                  }
                  & > .item__social {
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(30, 30, 30, 0.75), 0px 2px 24px rgba(30, 30, 30, 0.75);
                     font-size: get-vh(20px);
                     font-weight: 600;
                     line-height: 1.3;
                     letter-spacing: -0.03em;
                  }
               }
            }
         }
      }
   }
</style>