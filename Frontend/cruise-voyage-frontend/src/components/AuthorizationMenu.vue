<template>
   <h2 class="title__access">Авторизація</h2>
   <div class="propouse__reg">
      <h3 class="propouse__title">Ще не зареєстровані?</h3>
      <h3 class="propouse__title" @click="$router.push('/reg');">Створити новий акаунт</h3>
   </div>
   <div class="container__input-info">
      <input type="text" class="item__info" placeholder="Email" v-model="authStore.currentEmail">
      <div class="password__info-container">
         <input :type="authStore.visibleCurrentPass ? 'text' : 'password'" class="item__info" placeholder="Пароль" v-model="authStore.currentPassword">
         <img src="../img/reg-auth/pwd_visibility.svg" alt="" class="icon-visible" :class="{hiden: !authStore.visibleCurrentPass && authStore.currentPassword.length > 0}"
            v-show="authStore.currentPassword.length > 0" 
            @click="authStore.visibleCurrentPass = !authStore.visibleCurrentPass">
      </div>
   </div>
   <div class="save__password">
      <div class="block__check" @click="authStore.savePassword = !authStore.savePassword">
         <img src="../img/reg-auth/check.svg" alt="" class="icon-check" v-show="authStore.savePassword">
      </div>
      Запам’ятати пароль
   </div>
   <button class="send__info" 
      @click="authStore.Login()"
      :class="{active: authStore.currentEmail.length > 0 && authStore.currentPassword.length > 0 && authStore.savePassword}">Увійти</button>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useAuthInfo } from '@/stores/Auth';

export default defineComponent({
   setup () {
      const authStore = useAuthInfo();

      return {
         authStore
      }
   }
})
</script>

<style lang="scss" scoped>
</style>