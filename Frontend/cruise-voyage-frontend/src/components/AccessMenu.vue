<template>
   <div class="container">
      <div class="left__part">
         <h1 class="left__title">Ваша незабутня подорож<br>починається тут!</h1>
         <img src="../img/reg-auth/logo.png" alt="" class="icon-logo">
      </div>
      <div class="right__part">
         <router-view :key="$route.name" />
      </div>
   </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
// import AuthorizationMenu from './AuthorizationMenu.vue';
// import RegistrationMenu from './RegistrationMenu.vue';

export default defineComponent({
   // components: {
   //    AuthorizationMenu,
   //    RegistrationMenu
   // },
   setup () {

      return {}
   }
})
</script>

<style lang="scss" scoped>
   .left__part {
      position: relative;
      height: 100vh;
      width: 61%;
      background-image: url('../img/reg-auth/login_wall.jpg');
      background-size: cover;
      background-repeat: no-repeat;
      background-position: center center;
      & > .left__title {
         position: absolute;
         left: 45px;
         top: 45px;
         font-size: get-vh(72px);
         font-weight: 700;
         line-height: 1.25;
         letter-spacing: -0.03em;
      }
      & > .icon-logo {
         position: absolute;
         left: 45px;
         bottom: 45px;
         width: 210px;
         height: 70px;
      }
   }  
   .right__part {
      position: relative;
      height: 100vh;
      width: 39%;
      @include centerHorizontal;
      flex-direction: column;
      justify-content: center;
      text-align: center;
   }
</style>