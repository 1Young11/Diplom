<template>
   <input type="file" @change="userStore.handleFileUpload" />
   
   <img :src="require('C:/Users/Lenovo/Documents/GitHub/Diplom/Backend/CruiseVoyage/uploads/6415d641-02b8-40b7-b304-18d88416c947_images.jpg')" alt="">
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useUserInfo } from "@/stores/UserInfo";

export default defineComponent({
   setup() {
      const userStore = useUserInfo();

      return {
         userStore
      };
   },
});
</script>

<style scoped>
</style>