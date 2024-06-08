import {defineStore} from 'pinia';
import axios from 'axios';

interface Auth {
   idCustomer: number,
   nameCustomer: string,
   lastNameCustomer: string,
   phoneCustomer: string,
}

export const useUserInfo = defineStore('userStore', {
   state: (): Auth => ({
      idCustomer: -1,
      nameCustomer: '',
      lastNameCustomer: '', 
      phoneCustomer: '', 
   }),
   actions: {
      // handleFileUpload(event: any) {
      //    console.log(this.idCustomer)
      //    const file = event.target.files[0];
      //    const formData = new FormData();
      //    formData.append('image', file);
      //    // Отправляем изображение на сервер
      //    axios.post('http://localhost:5282/api/Photo/upload', formData, {
      //       headers: {
      //          'Content-Type': 'multipart/form-data', idCustomer: this.idCustomer
      //       }
      //    }).then(response => {
      //      // Обработка успешной загрузки
      //      console.log('Image uploaded successfully');
      //    }).catch(error => {
      //      // Обработка ошибки загрузки
      //      console.error('Error uploading image:', error);
      //    });
      // }
   },
   getters: {
   
   }
})