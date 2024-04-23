<template>
   <h2 class="title__access">Реєстрація</h2>
   <div class="propouse__reg">
      <h3 class="propouse__title">Вже зареєстровані? <span class="login" @click="$router.push('/auth');">Увійти</span></h3>
   </div>
   <div class="container__name-lname">
      <input type="text" class="item__info" placeholder="Ім’я" :class="{trubl: regStore.trublName}" v-model="regStore.name"
         @input="(e) => {
            regStore.name = regStore.name.charAt(0).toUpperCase() + regStore.name.slice(1);
            regStore.name = regStore.name.replace(/[^a-zA-Zа-яА-ЯІіЇїЄєҐґ\s]/g, '');
         }" 
      >
      <input type="text" class="item__info" placeholder="Прізвище" :class="{trubl: regStore.trublLastName}" v-model="regStore.lastName"
         @input="(e) => {
            regStore.lastName = regStore.lastName.charAt(0).toUpperCase() + regStore.lastName.slice(1);
            regStore.lastName = regStore.lastName.replace(/[^a-zA-Zа-яА-ЯІіЇїЄєҐґ\s]/g, '');
         }" 
      >
   </div>
   <div class="container__input-info">
      <input type="text" class="item__info" placeholder="Номер телефону" :class="{trubl: regStore.trublPhone}" v-model="regStore.mobilePhone" maxlength="13"
         @input = "(e) => {
            regStore.mobilePhone = regStore.mobilePhone.replace(/\D/g,'');
            if(regStore.mobilePhone[2] != undefined && regStore.mobilePhone[3] != ' ') 
            regStore.mobilePhone = regStore.mobilePhone.slice(0, 3) + '-' + regStore.mobilePhone.slice(3);
            if(regStore.mobilePhone[6] != undefined && regStore.mobilePhone[7] != ' ') 
               regStore.mobilePhone = regStore.mobilePhone.slice(0, 7) + '-' + regStore.mobilePhone.slice(7);
            if(regStore.mobilePhone[9] != undefined && regStore.mobilePhone[10] != ' ') 
               regStore.mobilePhone = regStore.mobilePhone.slice(0, 10) + '-' + regStore.mobilePhone.slice(10);
         }"
      >
      <input type="text" class="item__info" :class="{trubl: regStore.trublEmail}" placeholder="Email" v-model="regStore.newEmail">
      <div class="password__info-container">
         <input :type="regStore.visibleNewPassword ? 'text' : 'password'" class="item__info" placeholder="Пароль" v-model="regStore.newPassword">
         <img src="../img/reg-auth/pwd_visibility.svg" alt="" class="icon-visible" :class="{hiden: !regStore.visibleNewPassword && regStore.newPassword.length > 0}"
            v-show="regStore.newPassword.length > 0" 
            @click="regStore.visibleNewPassword = !regStore.visibleNewPassword">
      </div>
      <div class="password__info-container">
         <input :type="regStore.visibleRepeat ? 'text' : 'password'" class="item__info" placeholder="Повторіть пароль" v-model="regStore.newPasswordRepeat">
         <img src="../img/reg-auth/pwd_visibility.svg" alt="" class="icon-visible" :class="{hiden: !regStore.visibleRepeat && regStore.newPasswordRepeat.length > 0}"
            v-show="regStore.newPasswordRepeat.length > 0" 
            @click="regStore.visibleRepeat = !regStore.visibleRepeat">
      </div>
   </div>
   <div class="save__password">
      <div class="block__check" @click="regStore.acceptRules = !regStore.acceptRules">
         <img src="../img/reg-auth/check.svg" alt="" class="icon-check" v-show="regStore.acceptRules">
      </div>
      Я погоджуюся з умовами<br> користування сервісом
   </div>
   <button class="send__info" @click="regStore.SendInfoRegistration()" :class="{active: regStore.name.length > 0 && regStore.lastName.length > 0 && regStore.mobilePhone.length > 0 && regStore.newEmail.length > 0 && regStore.newPassword.length > 0 && regStore.newPasswordRepeat.length > 0 && regStore.acceptRules}">Зареєструватися</button>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRegInfo } from '@/stores/Reg';

export default defineComponent({
   setup () {
      const regStore = useRegInfo();

      return {
         regStore
      }
   }
})
</script>

<style lang="scss" scoped>
   .container__name-lname {
      margin-top: get-vh(40px);
      display: flex;
      gap: get-vh(23px);
      & > .item__info {
         width: get-vh(222px);
         height: get-vh(72px);
         &:nth-child(2) {
            width: get-vh(255px);
         }

         padding: 0 get-vh(10px);

         border-radius: get-vh(24px);
         background: rgba(24, 123, 205, 0.08);

         color: $txtTitleAccess;
         text-align: center;
         font-size: get-vh(28px);
         font-weight: 600;
         line-height: 1;
         letter-spacing: -0.03em;
         &::placeholder {
            color: $txtInputPlaceholder;
         }
         &:focus {
            background: rgba(24, 123, 205, 0.18);
         }
         &.trubl {
            background: rgba(229, 0, 0, 0.08);
            color: $txtTrubltext;
         }
      }
   }
   .container__input-info {
      margin-top: get-vh(24px);
   }
</style>