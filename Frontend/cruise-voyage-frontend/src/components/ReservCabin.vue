<template>
   <div class="bg__rout">
      <div class="rout__part">
         <img src="../img/reg-auth/logo.png" alt="" class="icon-logo">
         <div class="name__totravell">{{ cruiseStore.cruisesList[cruiseStore.activeCruise].arrivedFrom }}</div>
         <div class="wrapper__info">
            <div class="item__info">
               <div class="title__info">{{ new Date(cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint[0].dateArrived).toLocaleString('uk-UA', {day: '2-digit'}) }} &nbsp; –  &nbsp; {{ new Date(cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint[cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint.length - 1].dateArrived).toLocaleString('uk-UA', {day: '2-digit'}) }} {{ cruiseStore.formatDate(cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint[0].dateArrived).slice(2) }} {{ new Date(cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint[cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint.length - 1].dateArrived).toLocaleString('uk-UA', {year: 'numeric'}) }}</div>
               <div class="subtitle__info">{{ cruiseStore.cruisesList[cruiseStore.activeCruise].countDays }}</div>
            </div>
            <div class="sep"></div>
            <div class="item__info">
               <div class="title__info">На борту корабля</div>
               <div class="subtitle__info">{{ cruiseStore.cruisesList[cruiseStore.activeCruise].nameShip }}</div>
            </div>
            <div class="sep"></div>
            <div class="item__info">
               <div class="title__info">Оцінка</div>
               <div class="subtitle__info">{{ cruiseStore.cruisesList[cruiseStore.activeCruise].rating }}/5</div>
            </div>
         </div>
         <ul class="list__cruisepoint">
            <div class="item__cruisepoint" v-for="(point, index) in cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint" :key="index"> 
               <div class="day__item">{{ index + 1 }}</div>
               <div class="wrapper__travellto">
                  <div class="title__travell">{{ point.pointName }}</div>
                  <div class="subtitle__travell">{{ cruiseStore.formatDate(point.dateArrived) }} {{ index == 0 ? '•  Початок подорожі' : ''}} {{ index == cruiseStore.cruisesList[cruiseStore.activeCruise].cruisePoint.length - 1 ? '•  Кінець подорожі' : '' }}</div>
               </div>
            </div>
            
         </ul>
      </div>
      <div class="other__info">
         <div class="type__cabin" v-if="cruiseStore.pageReserve == 0">
            <div class="title__cabin">Оберіть тип каюти:</div>
            <ul class="list__cabin">
               <div class="item__cabin" @click="cruiseStore.pageReserve = 1; cruiseStore.currentTypeCabin = 1; cruiseStore.activeFloor = 1">
                  <div class="free__place">Доступно 456 місць</div>
                  <div class="type__cabin">{{ cruiseStore.typeCabins[0].type }}</div>
                  <div class="cost__foroneplace">₴{{ cruiseStore.typeCabins[0].price }} за ліжко</div>
               </div>
               <div class="item__cabin" @click="cruiseStore.pageReserve = 1; cruiseStore.currentTypeCabin = 2; cruiseStore.activeFloor = 1">
                  <div class="free__place">Доступно 456 місць</div>
                  <div class="type__cabin">{{ cruiseStore.typeCabins[1].type }}</div>
                  <div class="cost__foroneplace">₴{{ cruiseStore.typeCabins[1].price }} за ліжко</div>
               </div>
               <div class="item__cabin" @click="cruiseStore.pageReserve = 1; cruiseStore.currentTypeCabin = 3; cruiseStore.activeFloor = 1">
                  <div class="free__place">Доступно 456 місць</div>
                  <div class="type__cabin">{{ cruiseStore.typeCabins[2].type }}</div>
                  <div class="cost__foroneplace">₴{{ cruiseStore.typeCabins[2].price }} за ліжко</div>
               </div>
            </ul>
         </div>
         <div class="choose__numbercabin" v-if="cruiseStore.pageReserve == 1">
            <div class="title__cabin">Оберіть поверх та каюту</div>
            <div class="subtitle__cabin">Реальний вигляд та розташування кают може частково відрізнятися від зображеного на схемі, <br>
яка носить лише інформаційний характер. Для отримання справжніх зображень <br> інтер’єру та екстер’єру конкретних кают зверніться у Службу підтримки.</div>
            <div class="current__floor">
               <div class="block__floor" v-for="(floor, index) in cruiseStore.countFloors" :key="index" :class="{active: cruiseStore.activeFloor == floor}" @click="cruiseStore.activeFloor = floor">
                  {{ floor }} {{ cruiseStore.activeFloor == floor ? '(Палуба)' : '' }}
               </div>
            </div>
            <div class="wrapper__floors">
               <img src="../img/reserv-cabin/deck.png" alt="" class="icon__floor">
               <ul class="list__cabin">
                  <li class="item__cabin" v-for="(cabin, index) in cruiseStore.cabin[cruiseStore.activeFloor]" :key="index" @click="cruiseStore.HandleClickActiveCabin(cabin.idCabin)">
                     <div class="status__cabin" 
                        :class="{
                           'active-type-cabin': cruiseStore.currentTypeCabin == cabin.idCabintype, 
                           active: cruiseStore.activeCabin.ind == index, 
                           busy: cabin.cabinBeds.some(p => p.isBooked) && !cabin.cabinBeds.every(p => p.isBooked), 
                           block: cabin.cabinBeds.every(p => p.isBooked)
                        }" 
                        v-if="cruiseStore.currentTypeCabin == cabin.idCabintype">
    
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="20" viewBox="0 0 25 20" fill="none" class="icon-check" v-if="!cabin.cabinBeds.some(p => p.isBooked)">
                           <path d="M11.5674 10.9333L10.2508 9.61665C10.0841 9.44999 9.87856 9.36665 9.63411 9.36665C9.38967 9.36665 9.18411 9.44999 9.01745 9.61665C8.85078 9.78332 8.76745 9.98888 8.76745 10.2333C8.76745 10.4778 8.85078 10.6833 9.01745 10.85L10.9508 12.7833C11.1266 12.9611 11.3316 13.05 11.5659 13.05C11.8003 13.05 12.0063 12.9611 12.1841 12.7833L16.0174 8.94999C16.1841 8.78332 16.2674 8.57777 16.2674 8.33332C16.2674 8.08888 16.1841 7.88332 16.0174 7.71665C15.8508 7.54999 15.6452 7.46665 15.4008 7.46665C15.1563 7.46665 14.9508 7.54999 14.7841 7.71665L11.5674 10.9333ZM12.5008 17.2C11.5048 17.2 10.5688 17.011 9.69278 16.633C8.81678 16.255 8.05478 15.742 7.40678 15.094C6.75878 14.446 6.24578 13.684 5.86778 12.808C5.48978 11.932 5.30078 10.996 5.30078 9.99999C5.30078 9.0011 5.49011 8.06243 5.86878 7.18399C6.24745 6.30543 6.76134 5.54127 7.41045 4.89149C8.05956 4.2416 8.82156 3.73054 9.69645 3.35832C10.5713 2.9861 11.5061 2.79999 12.5008 2.79999C13.4996 2.79999 14.4381 2.9861 15.3164 3.35832C16.1949 3.73054 16.9591 4.24165 17.6091 4.89165C18.2591 5.54165 18.7702 6.30599 19.1424 7.18466C19.5147 8.06343 19.7008 9.00232 19.7008 10.0013C19.7008 11.0004 19.5147 11.9361 19.1424 12.8083C18.7702 13.6805 18.2592 14.4412 17.6093 15.0903C16.9595 15.7394 16.1953 16.2533 15.3168 16.632C14.4383 17.0107 13.4997 17.2 12.5008 17.2Z" fill="#006A80"/>
                        </svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="20" viewBox="0 0 25 20" fill="none" class="icon-check" v-if="cabin.cabinBeds.some(p => p.isBooked) && !cabin.cabinBeds.every(p => p.isBooked)">
                           <path d="M4.97974 15.96C4.76963 15.96 4.60329 15.8987 4.48072 15.7761C4.35816 15.6534 4.29688 15.4929 4.29688 15.2944V14.0158C4.29688 13.5604 4.41069 13.1488 4.63831 12.7809C4.86593 12.4131 5.18401 12.1416 5.59257 11.9665C6.46803 11.5812 7.26471 11.3009 7.98259 11.1258C8.70047 10.9506 9.43878 10.863 10.1975 10.863C10.9563 10.863 11.6887 10.9506 12.3949 11.1258C13.1012 11.3009 13.892 11.5812 14.7675 11.9665C15.176 12.1416 15.497 12.4102 15.7305 12.7722C15.9639 13.1342 16.0807 13.5487 16.0807 14.0158V15.3119C16.0807 15.4987 16.0194 15.6534 15.8968 15.7761C15.7742 15.8987 15.6137 15.96 15.4153 15.96H4.97974ZM16.746 15.96C16.8744 15.9366 16.9853 15.8665 17.0787 15.7498C17.1721 15.633 17.2188 15.4754 17.2188 15.2769V13.9983C17.2188 13.2743 17.0606 12.8015 16.6929 12.2819C16.3252 11.7623 15.3972 11.2427 14.7669 10.8807C15.6074 10.9858 16.7956 11.0615 17.5252 11.2484C18.2547 11.4352 18.8646 11.6512 19.3549 11.8964C19.7635 12.13 20.0903 12.4277 20.3354 12.7897C20.5806 13.1517 20.7031 13.5604 20.7031 14.0158V15.3119C20.7031 15.4987 20.6389 15.6534 20.5105 15.7761C20.3821 15.8987 20.2187 15.96 20.0203 15.96H16.746ZM10.1975 9.70703C9.3454 9.70703 8.6567 9.44138 8.13142 8.91009C7.60614 8.37879 7.3435 7.69862 7.3435 6.86957C7.3435 6.02884 7.60614 5.3399 8.13142 4.80277C8.6567 4.26564 9.33956 3.99707 10.18 3.99707C11.0205 3.99707 11.7092 4.26564 12.2461 4.80277C12.7831 5.3399 13.0515 6.023 13.0515 6.85205C13.0515 7.69278 12.7831 8.37879 12.2461 8.91009C11.7092 9.44138 11.0263 9.70703 10.1975 9.70703ZM17.1662 6.85205C17.1662 7.69278 16.8978 8.37879 16.3608 8.91009C15.8239 9.44138 15.141 9.70703 14.3122 9.70703C14.1488 9.70703 13.9766 9.69535 13.7957 9.672C13.6148 9.64864 13.3428 9.53785 13.1911 9.47947C13.4829 9.15252 13.8074 8.83127 13.9533 8.37587C14.0992 7.92048 14.1721 7.41254 14.1721 6.85205C14.1721 6.30324 14.0992 5.81281 13.9533 5.38077C13.8074 4.94873 13.4829 4.61024 13.1911 4.2249C13.3428 4.1782 13.6177 4.07005 13.8044 4.04086C13.9912 4.01167 14.1605 3.99707 14.3122 3.99707C15.141 3.99707 15.8239 4.26564 16.3608 4.80277C16.8978 5.3399 17.1662 6.023 17.1662 6.85205Z" fill="#074D87"/>
                        </svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="20" viewBox="0 0 25 20" fill="none" class="icon-check" v-if="cabin.cabinBeds.every(p => p.isBooked)">
                           <path d="M8.59606 17C8.16625 17 7.80711 16.855 7.51864 16.565C7.23017 16.275 7.08594 15.9152 7.08594 15.4855V9.42773C7.08594 8.99812 7.23094 8.63831 7.52096 8.34829C7.80858 8.06067 8.16487 7.91567 8.5898 7.9133C8.59333 7.91328 9.09686 7.91327 9.10039 7.91327V6.42973C9.10039 5.4749 9.43223 4.66441 10.0959 3.99825C10.7595 3.3321 11.5621 2.99902 12.5037 2.99902C13.4453 2.99902 14.2467 3.3321 14.908 3.99825C15.5693 4.66441 15.9 5.4749 15.9 6.42973V7.91327C15.9034 7.91327 16.4068 7.91328 16.4102 7.9133C16.8353 7.91558 17.1917 8.06058 17.4794 8.34829C17.7694 8.63831 17.9144 8.99812 17.9144 9.42773V15.4855C17.9144 15.9152 17.7694 16.275 17.4793 16.565C17.1891 16.855 16.8292 17 16.3994 17H8.59606ZM12.5059 13.8166C12.873 13.8166 13.1853 13.6859 13.4429 13.4245C13.7004 13.1631 13.8292 12.8489 13.8292 12.4818C13.8292 12.1148 13.6985 11.8024 13.4371 11.5449C13.1758 11.2873 12.8615 11.1585 12.4945 11.1585C12.1274 11.1585 11.8151 11.2892 11.5575 11.5506C11.3 11.812 11.1712 12.1262 11.1712 12.4933C11.1712 12.8603 11.3019 13.1727 11.5632 13.4302C11.8246 13.6878 12.1388 13.8166 12.5059 13.8166ZM10.6148 7.91327H14.3855V6.42973C14.3855 5.9043 14.202 5.45357 13.8351 5.07753C13.4681 4.7015 13.0225 4.51348 12.4983 4.51348C11.9741 4.51348 11.5292 4.7015 11.1634 5.07753C10.7977 5.45357 10.6148 5.9043 10.6148 6.42973V7.91327Z" fill="black" fill-opacity="0.4"/>
                        </svg>
                     </div>
                  </li>
               </ul>
            </div>
            <div class="status__cabins" v-if="cruiseStore.activeCabin.ind == -1">
               <div class="item__status">
                  <div class="block__status">
                     <svg xmlns="http://www.w3.org/2000/svg" width="26" height="20" viewBox="0 0 26 20" fill="none" class="icon-status">
                        <path d="M12.0674 10.9333L10.7508 9.61665C10.5841 9.44999 10.3786 9.36665 10.1341 9.36665C9.88967 9.36665 9.68411 9.44999 9.51745 9.61665C9.35078 9.78332 9.26745 9.98888 9.26745 10.2333C9.26745 10.4778 9.35078 10.6833 9.51745 10.85L11.4508 12.7833C11.6266 12.9611 11.8316 13.05 12.0659 13.05C12.3003 13.05 12.5063 12.9611 12.6841 12.7833L16.5174 8.94999C16.6841 8.78332 16.7674 8.57777 16.7674 8.33332C16.7674 8.08888 16.6841 7.88332 16.5174 7.71665C16.3508 7.54999 16.1452 7.46665 15.9008 7.46665C15.6563 7.46665 15.4508 7.54999 15.2841 7.71665L12.0674 10.9333ZM13.0008 17.2C12.0048 17.2 11.0688 17.011 10.1928 16.633C9.31678 16.255 8.55478 15.742 7.90678 15.094C7.25878 14.446 6.74578 13.684 6.36778 12.808C5.98978 11.932 5.80078 10.996 5.80078 9.99999C5.80078 9.0011 5.99011 8.06243 6.36878 7.18399C6.74745 6.30543 7.26134 5.54127 7.91045 4.89149C8.55956 4.2416 9.32156 3.73054 10.1964 3.35832C11.0713 2.9861 12.0061 2.79999 13.0008 2.79999C13.9996 2.79999 14.9381 2.9861 15.8164 3.35832C16.6949 3.73054 17.4591 4.24165 18.1091 4.89165C18.7591 5.54165 19.2702 6.30599 19.6424 7.18466C20.0147 8.06343 20.2008 9.00232 20.2008 10.0013C20.2008 11.0004 20.0147 11.9361 19.6424 12.8083C19.2702 13.6805 18.7592 14.4412 18.1093 15.0903C17.4595 15.7394 16.6953 16.2533 15.8168 16.632C14.9383 17.0107 13.9997 17.2 13.0008 17.2Z" fill="#006A80"/>
                     </svg>
                  </div>
                  Доступна <br>уся каюта
               </div>
               <div class="item__status">
                  <div class="block__status">
                     <svg xmlns="http://www.w3.org/2000/svg" width="26" height="20" viewBox="0 0 26 20" fill="none" class="icon-status">
                        <path d="M5.47974 15.96C5.26963 15.96 5.10329 15.8987 4.98072 15.7761C4.85816 15.6534 4.79688 15.4929 4.79688 15.2944V14.0158C4.79688 13.5604 4.91069 13.1488 5.13831 12.7809C5.36593 12.4131 5.68401 12.1416 6.09257 11.9665C6.96803 11.5812 7.76471 11.3009 8.48259 11.1258C9.20047 10.9506 9.93878 10.863 10.6975 10.863C11.4563 10.863 12.1887 10.9506 12.8949 11.1258C13.6012 11.3009 14.392 11.5812 15.2675 11.9665C15.676 12.1416 15.997 12.4102 16.2305 12.7722C16.4639 13.1342 16.5807 13.5487 16.5807 14.0158V15.3119C16.5807 15.4987 16.5194 15.6534 16.3968 15.7761C16.2742 15.8987 16.1137 15.96 15.9153 15.96H5.47974ZM17.246 15.96C17.3744 15.9366 17.4853 15.8665 17.5787 15.7498C17.6721 15.633 17.7188 15.4754 17.7188 15.2769V13.9983C17.7188 13.2743 17.5606 12.8015 17.1929 12.2819C16.8252 11.7623 15.8972 11.2427 15.2669 10.8807C16.1074 10.9858 17.2956 11.0615 18.0252 11.2484C18.7547 11.4352 19.3646 11.6512 19.8549 11.8964C20.2635 12.13 20.5903 12.4277 20.8354 12.7897C21.0806 13.1517 21.2031 13.5604 21.2031 14.0158V15.3119C21.2031 15.4987 21.1389 15.6534 21.0105 15.7761C20.8821 15.8987 20.7187 15.96 20.5203 15.96H17.246ZM10.6975 9.70703C9.8454 9.70703 9.1567 9.44138 8.63142 8.91009C8.10614 8.37879 7.8435 7.69862 7.8435 6.86957C7.8435 6.02884 8.10614 5.3399 8.63142 4.80277C9.1567 4.26564 9.83956 3.99707 10.68 3.99707C11.5205 3.99707 12.2092 4.26564 12.7461 4.80277C13.2831 5.3399 13.5515 6.023 13.5515 6.85205C13.5515 7.69278 13.2831 8.37879 12.7461 8.91009C12.2092 9.44138 11.5263 9.70703 10.6975 9.70703ZM17.6662 6.85205C17.6662 7.69278 17.3978 8.37879 16.8608 8.91009C16.3239 9.44138 15.641 9.70703 14.8122 9.70703C14.6488 9.70703 14.4766 9.69535 14.2957 9.672C14.1148 9.64864 13.8428 9.53785 13.6911 9.47947C13.9829 9.15252 14.3074 8.83127 14.4533 8.37587C14.5992 7.92048 14.6721 7.41254 14.6721 6.85205C14.6721 6.30324 14.5992 5.81281 14.4533 5.38077C14.3074 4.94873 13.9829 4.61024 13.6911 4.2249C13.8428 4.1782 14.1177 4.07005 14.3044 4.04086C14.4912 4.01167 14.6605 3.99707 14.8122 3.99707C15.641 3.99707 16.3239 4.26564 16.8608 4.80277C17.3978 5.3399 17.6662 6.023 17.6662 6.85205Z" fill="#074D87"/>
                     </svg>
                  </div>
                  Є вільні <br>ліжка
               </div>
               <div class="item__status">
                  <div class="block__status">
                     <svg xmlns="http://www.w3.org/2000/svg" width="26" height="20" viewBox="0 0 26 20" fill="none" class="icon-status">
                        <path d="M9.09606 17C8.66625 17 8.30711 16.855 8.01864 16.565C7.73017 16.275 7.58594 15.9152 7.58594 15.4855V9.42773C7.58594 8.99812 7.73094 8.63831 8.02096 8.34829C8.30858 8.06067 8.66487 7.91567 9.0898 7.9133C9.09333 7.91328 9.59686 7.91327 9.60039 7.91327V6.42973C9.60039 5.4749 9.93223 4.66441 10.5959 3.99825C11.2595 3.3321 12.0621 2.99902 13.0037 2.99902C13.9453 2.99902 14.7467 3.3321 15.408 3.99825C16.0693 4.66441 16.4 5.4749 16.4 6.42973V7.91327C16.4034 7.91327 16.9068 7.91328 16.9102 7.9133C17.3353 7.91558 17.6917 8.06058 17.9794 8.34829C18.2694 8.63831 18.4144 8.99812 18.4144 9.42773V15.4855C18.4144 15.9152 18.2694 16.275 17.9793 16.565C17.6891 16.855 17.3292 17 16.8994 17H9.09606ZM13.0059 13.8166C13.373 13.8166 13.6853 13.6859 13.9429 13.4245C14.2004 13.1631 14.3292 12.8489 14.3292 12.4818C14.3292 12.1148 14.1985 11.8024 13.9371 11.5449C13.6758 11.2873 13.3615 11.1585 12.9945 11.1585C12.6274 11.1585 12.3151 11.2892 12.0575 11.5506C11.8 11.812 11.6712 12.1262 11.6712 12.4933C11.6712 12.8603 11.8019 13.1727 12.0632 13.4302C12.3246 13.6878 12.6388 13.8166 13.0059 13.8166ZM11.1148 7.91327H14.8855V6.42973C14.8855 5.9043 14.702 5.45357 14.3351 5.07753C13.9681 4.7015 13.5225 4.51348 12.9983 4.51348C12.4741 4.51348 12.0292 4.7015 11.6634 5.07753C11.2977 5.45357 11.1148 5.9043 11.1148 6.42973V7.91327Z" fill="black" fill-opacity="0.4"/>
                     </svg>
                  </div>
                  Повністю <br> викуплено
               </div>
            </div>
            <div class="choose__place" v-else>
               <div class="title__cabinreserve">Доступно {{ cruiseStore.cabin[cruiseStore.activeFloor][cruiseStore.activeCabin.ind].cabinBeds.length }} місця. Оберіть необхідну кількість:</div>
               <div class="wrapper__hotel">
                  <div class="item__status" v-for="(place, index) in cruiseStore.cabin[cruiseStore.activeFloor][cruiseStore.activeCabin.ind].cabinBeds" :key="index" 
                     :class="{block: place.isBooked, active: cruiseStore.currentReservation.some(p => p.idCabinBed === place.idCabinbed)}" 
                     @click="cruiseStore.SelectPlaceInCabin(place.idCabinbed)"
                  >
                     {{ index + 1 }}  
                     <img src="../img/reserv-cabin/hotel.svg" alt="" class="icon-hotel" v-if="!place.isBooked">
                     <img src="../img/reserv-cabin/block.svg" alt="" class="icon-block" v-else>
                  </div>
               </div>
            </div>
            <div class="description__cabin" v-if="cruiseStore.activeCabin.ind == -1">Без позначки - каюта недоступна для обраного типу (Люкс). <br>  Поверніться назад та оберіть інший тип каюти.</div>
            <button class="choose__nexttype" v-if="cruiseStore.activeCabin.ind == -1" @click="cruiseStore.pageReserve = 0">Обрати інший тип каюти</button>
            <div class="wrapper__btn" v-if="cruiseStore.activeCabin.ind != -1">
               <button class="choose" @click="cruiseStore.activeCabin.ind = -1; cruiseStore.currentReservation = []">Скасувати</button>
               <button class="choose" @click="cruiseStore.pageReserve = 2">Підтвердити {{ cruiseStore.currentReservation.length }} місця</button>
            </div>
         </div>
         <div class="add__member" v-if="cruiseStore.pageReserve == 2">
            <div class="title__cabin">Вкажіть дані осіб, <br> з якими ви подорожуєте</div>
            <div class="item__member" v-for="(member, index) in cruiseStore.currentReservation" :key="index">
               <div class="title__member">{{ index == 0 ? 'Пасажир 1 - Ви (інформація з профіля користувача)' : `Пасажир ${index+1}` }}</div>
               <div class="wrapper__block" v-if="index == 0">
                  <div class="item__block">{{ userStore.nameCustomer }}</div>
                  <div class="item__block">{{ userStore.lastNameCustomer }}</div>
                  <div class="item__block">{{ userStore.phoneCustomer }}</div>
               </div>
               <div class="wrapper__block inp" v-else>
                  <input type="text" class="item__block" v-model="cruiseStore.currentReservation[index - 1].name">
                  <input type="text" class="item__block" v-model="cruiseStore.currentReservation[index - 1].lastName">
                  <input type="text" class="item__block" v-model="cruiseStore.currentReservation[index - 1].phoneNumber">
               </div>
               <div class="want__buyall" v-if="index == 0">
                  <div class="circle__check">
                     <img src="../img/reserv-cabin/check.svg" alt="" class="icon-check">
                  </div>
                  Я хочу викупити усі місця на своє ім’я
               </div>
            </div>
            <div class="wrapper__btn">
               <button class="choose" @click="cruiseStore.pageReserve = 1; cruiseStore.currentReservation = []; cruiseStore.activeCabin.ind = -1">Обрати іншу каюту</button>
               <button class="choose" @click="cruiseStore.pageReserve = 3">Підтвердити</button>
            </div>
         </div>
         <div class="accept__info" v-if="cruiseStore.pageReserve == 3">
            <div class="title__cabin">Ваша подорож!</div>
            <div class="subtitle__reserve">Бронювання на: {{ userStore.nameCustomer }} {{ userStore.lastNameCustomer }}</div>
            <div class="wrapper__floors">
               <img src="../img/reserv-cabin/deck.png" alt="" class="icon__floor">
               <ul class="list__cabin">
                  <li class="item__cabin" v-for="(cabin, index) in cruiseStore.cabin[cruiseStore.activeFloor]" :key="index">
                     <div class="status__cabin" 
                        :class="{active: cruiseStore.activeCabin.ind == index}" 
                        v-if="cruiseStore.activeCabin.ind == index">
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="20" viewBox="0 0 25 20" fill="none" class="icon-check" v-if="cruiseStore.activeCabin.ind == index">
                           <path d="M4.97974 15.96C4.76963 15.96 4.60329 15.8987 4.48072 15.7761C4.35816 15.6534 4.29688 15.4929 4.29688 15.2944V14.0158C4.29688 13.5604 4.41069 13.1488 4.63831 12.7809C4.86593 12.4131 5.18401 12.1416 5.59257 11.9665C6.46803 11.5812 7.26471 11.3009 7.98259 11.1258C8.70047 10.9506 9.43878 10.863 10.1975 10.863C10.9563 10.863 11.6887 10.9506 12.3949 11.1258C13.1012 11.3009 13.892 11.5812 14.7675 11.9665C15.176 12.1416 15.497 12.4102 15.7305 12.7722C15.9639 13.1342 16.0807 13.5487 16.0807 14.0158V15.3119C16.0807 15.4987 16.0194 15.6534 15.8968 15.7761C15.7742 15.8987 15.6137 15.96 15.4153 15.96H4.97974ZM16.746 15.96C16.8744 15.9366 16.9853 15.8665 17.0787 15.7498C17.1721 15.633 17.2188 15.4754 17.2188 15.2769V13.9983C17.2188 13.2743 17.0606 12.8015 16.6929 12.2819C16.3252 11.7623 15.3972 11.2427 14.7669 10.8807C15.6074 10.9858 16.7956 11.0615 17.5252 11.2484C18.2547 11.4352 18.8646 11.6512 19.3549 11.8964C19.7635 12.13 20.0903 12.4277 20.3354 12.7897C20.5806 13.1517 20.7031 13.5604 20.7031 14.0158V15.3119C20.7031 15.4987 20.6389 15.6534 20.5105 15.7761C20.3821 15.8987 20.2187 15.96 20.0203 15.96H16.746ZM10.1975 9.70703C9.3454 9.70703 8.6567 9.44138 8.13142 8.91009C7.60614 8.37879 7.3435 7.69862 7.3435 6.86957C7.3435 6.02884 7.60614 5.3399 8.13142 4.80277C8.6567 4.26564 9.33956 3.99707 10.18 3.99707C11.0205 3.99707 11.7092 4.26564 12.2461 4.80277C12.7831 5.3399 13.0515 6.023 13.0515 6.85205C13.0515 7.69278 12.7831 8.37879 12.2461 8.91009C11.7092 9.44138 11.0263 9.70703 10.1975 9.70703ZM17.1662 6.85205C17.1662 7.69278 16.8978 8.37879 16.3608 8.91009C15.8239 9.44138 15.141 9.70703 14.3122 9.70703C14.1488 9.70703 13.9766 9.69535 13.7957 9.672C13.6148 9.64864 13.3428 9.53785 13.1911 9.47947C13.4829 9.15252 13.8074 8.83127 13.9533 8.37587C14.0992 7.92048 14.1721 7.41254 14.1721 6.85205C14.1721 6.30324 14.0992 5.81281 13.9533 5.38077C13.8074 4.94873 13.4829 4.61024 13.1911 4.2249C13.3428 4.1782 13.6177 4.07005 13.8044 4.04086C13.9912 4.01167 14.1605 3.99707 14.3122 3.99707C15.141 3.99707 15.8239 4.26564 16.3608 4.80277C16.8978 5.3399 17.1662 6.023 17.1662 6.85205Z" fill="#074D87"/>
                        </svg>
                     </div>
                  </li>
               </ul>
            </div>
            <div class="wrapper__costinfo">
               <div class="number__cabin">Каюта №{{ cruiseStore.activeCabin.choosen }} ({{ cruiseStore.activeFloor }} поверх) <span>₴{{ cruiseStore.typeCabins[cruiseStore.currentTypeCabin].price * cruiseStore.currentReservation.length }}</span></div>
               <div class="status__cabin">{{ cruiseStore.typeCabins[cruiseStore.currentTypeCabin].type }}, ₴{{ cruiseStore.typeCabins[cruiseStore.currentTypeCabin].price }} за ліжко <span>{{ cruiseStore.currentReservation.length }} {{ cruiseStore.currentReservation.length == 1 ? 'ліжко' : 'ліжка'}}, {{ cruiseStore.currentReservation.length }} {{ cruiseStore.currentReservation.length == 1 ? 'пасажир' : 'пасажири'}}</span></div>
            </div>
            <div class="wrapper__submit">
               <div class="circle__check" @click="cruiseStore.acceptCondition = !cruiseStore.acceptCondition">
                  <img src="../img/reserv-cabin/check.svg" alt="" class="icon-check" v-if="cruiseStore.acceptCondition">
               </div>
               Я приймаю Умови користування сервісом
            </div>
            <div class="description__reserve">Уважно перевірте всі деталі бронювання, перед тим як підтверджувати їх. Для внесення змін у бронювання ви можете звернутися до Служби підтримки, але не пізніше ніж за 7 діб до відправлення. Бронювання може бути скасоване не пізніше ніж за 14 діб до відправлення. Оплата бронювання здійснюється у повному обсязі одразу після підтвердження даних. При скасуванні бронювання раніше, ніж за 14 діб до відправлення, кошти будуть повністю повернуті протягом 60 календарних днів. При пізнішому скасуванні кошти можуть бути повернуті лише частково.</div>
            <div class="wrapper__btn">
               <button class="choose" @click="cruiseStore.pageReserve = 1; cruiseStore.currentReservation = []; cruiseStore.activeCabin.ind = -1; cruiseStore.activeCabin.choosen = -1">Скасувати</button>
               <button class="choose" :class="{unactive: !cruiseStore.acceptCondition}" @click="cruiseStore.ReserveCabin()">Підтвердити та сплатити</button>
            </div>
         </div>
      </div>
   </div>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from "vue";
import { useCruiseInfo } from "@/stores/Cruise";
import { useUserInfo } from "@/stores/UserInfo";

export default defineComponent({
   setup() {
      const cruiseStore = useCruiseInfo();
      const userStore = useUserInfo();
      onBeforeMount(() => {
         cruiseStore.fetchCabinsType(); 
      });

      return {
         cruiseStore,
         userStore
      };
   },
});
</script>

<style lang="scss" scoped>
   .bg__rout {
      width: 100%;
      height: 100vh;
      background: linear-gradient(270deg, rgba(0, 0, 0, 0.00) 0%, rgba(0, 0, 0, 0.30) 100%), linear-gradient(0deg, rgba(0, 0, 0, 0.50) 0%, rgba(0, 0, 0, 0.50) 100%), url('../img/reserv-cabin/bgspain.png') lightgray no-repeat;
      background-size: cover;
      display: flex;
      justify-content: space-between;
      & > .rout__part {
         height: 100%;
         width: get-vh(960px);
         padding: 0px get-vh(130px) 0px get-vh(130px);
         display: flex;
         align-items: flex-start;
         justify-content: center;
         flex-direction: column;
         & > .icon-logo {
            width: get-vh(156px);
            height: get-vh(48px); 
         }
         & > .name__totravell {
            margin-top: get-vh(34px);
            font-size: get-vh(56px);
            font-style: italic;
            font-weight: 600;
            line-height: 1;
            letter-spacing: -0.02em;
         }
         & > .wrapper__info {
            margin-top: get-vh(20px);
            padding-left: get-vh(6px);
            @include centerHorizontal;
            gap: get-vh(24px);
            & > .item__info {
               & > .title__info {
                  font-size: get-vh(18px);
                  font-weight: 500;
                  line-height: 1.22;
                  letter-spacing: -0.02em;
               }
               & > .subtitle__info {
                  margin-top: get-vh(5px);
                  font-size: get-vh(24px);
                  font-weight: 600;
                  line-height: 0.91;
                  letter-spacing: -0.02em;
               }
            }
            & > .sep {
               width: get-vh(2px);
               height: get-vh(48px);
               border-radius: get-vh(100px);
               background: #FFF;
            }
         }
         & > .list__cruisepoint {
            margin-top: get-vh(50px);
            width: get-vh(688px);
            height: get-vh(508px);
            gap: get-vh(28px);
            display: flex;
            flex-wrap: wrap;
            flex-direction: column;
            & > .item__cruisepoint {
               width: get-vh(304px);
               @include centerHorizontal;
               gap: get-vh(20px);
               & > .day__item {
                  width: get-vh(38px);
                  height: get-vh(32px);
                  text-align: center;
                  font-size: get-vh(36px);
                  line-height: 0.9;
                  font-weight: 600;
                  letter-spacing: -0.05em;
               }
               & > .wrapper__travellto {
                  & > .title__travell {
                     font-size: get-vh(22px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                  }
                  & > .subtitle__travell {
                     font-size: get-vh(16px);
                     font-weight: 500;
                     letter-spacing: -0.02em;
                  }
               }
            }
         }
      }
      & > .other__info {
         height: 100%;
         width: get-vh(960px);
         padding: 0px get-vh(130px) 0px get-vh(130px);
         display: flex;
         align-items: flex-start;
         justify-content: center;
         flex-direction: column;
         background: rgba(166, 166, 166, 0.15);
         backdrop-filter: blur(get-vh(16px));
         & > .type__cabin {
            width: get-vh(720px);
            text-align: center;
            & > .title__cabin {
               font-size: get-vh(48px);
               font-weight: 500;
               letter-spacing: -0.03em;
            }
            & > .list__cabin {
               margin-top: get-vh(40px);
               display: flex;
               flex-direction: column;
               gap: get-vh(14px);
               & > .item__cabin {
                  position: relative;
                  width: 100%;
                  height: get-vh(190px);
                  border-radius: get-vh(18px);
                  overflow: hidden;
                  background-image: url('../img/reserv-cabin/economy.png');
                  background-repeat: no-repeat;
                  background-size: cover;
                  &:hover {
                     &::before {
                        background: linear-gradient(270deg, rgba(21, 21, 21, 0.00) 0%, rgb(21, 21, 21, 0.95) 100%);
                     }
                  }
                  &:nth-child(2) {
                     background-image: url('../img/reserv-cabin/standard.png');
                  }
                  &:nth-child(3) {
                     background-image: url('../img/reserv-cabin/luxury.png');
                  }
                  &::before {
                     content: "";
                     position: absolute;
                     top: 0;
                     left: 0;
                     width: 100%;
                     height: 100%;
                     background: linear-gradient(270deg, rgba(21, 21, 21, 0.00) 0%, #151515 100%);
                  }
                  & > .free__place {
                     position: absolute;
                     top: get-vh(24px);
                     left: get-vh(28px);
                     z-index: 1;
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(0, 0, 0, 0.50);
                     font-size: get-vh(20px);
                     font-style: normal;
                     font-weight: 600;
                     letter-spacing: -0.02em;
                  }
                  & > .type__cabin {
                     position: absolute;
                     bottom: get-vh(60px);
                     left: get-vh(30px);
                     z-index: 1;
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(0, 0, 0, 0.50);
                     font-size: get-vh(36px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                  }
                  & > .cost__foroneplace {
                     position: absolute;
                     bottom: get-vh(30px);
                     left: get-vh(29px);
                     z-index: 1;
                     text-shadow: 0px get-vh(2px) get-vh(24px) rgba(0, 0, 0, 0.50);
                     font-size: get-vh(20px);
                     font-weight: 600;
                     letter-spacing: -0.02em;
                  }
               }
            }
         }
         & > .choose__numbercabin {
            text-align: center;
            & > .title__cabin {
               font-size: get-vh(48px);
               font-weight: 500;
               letter-spacing: -0.03em;
            }
            & > .subtitle__cabin { 
               margin-top: get-vh(26px);
               color: $txtDescription;
               font-size: get-vh(14px);
               font-weight: 600;
               line-height: 1.21;
               letter-spacing: -0.03em;
            }
            & > .current__floor {
               margin-top: get-vh(80px);
               display: flex;
               justify-content: center;
               gap: get-vh(8px);
               & > .block__floor {
                  width: get-vh(48px);
                  height: get-vh(48px);
                  @include centerHorizontal;
                  justify-content: center;
                  border-radius: get-vh(12px);
                  background: rgba(255, 255, 255, 0.12);
                  font-size: get-vh(22px);
                  font-weight: 700;
                  letter-spacing: -0.03em;
                  transition: background 0.25s ease-out;
                  &:hover {
                     background: rgba(255, 255, 255, 0.2);
                  }
                  &.active {
                     padding: 0 get-vh(20px);
                     width: auto;
                     display: flex;
                     text-wrap: nowrap;
                     background: rgba(255, 255, 255, 1);
                     color: $txtBlack;
                  }
               }
            }
            & > .wrapper__floors {
               position: relative;
               margin-top: get-vh(34px);
               & > .icon__floor {
                  width: get-vh(720px);
                  height: get-vh(180px);
               }
               & > .list__cabin {
                  position: absolute;
                  top: 0;
                  left: 0;
                  z-index: 1;
                  & > .item__cabin {
                     position: absolute;
                     top: get-vh(10px);
                     width: get-vh(41px);
                     height: get-vh(30px);
                     border-radius: get-vh(4px);
                     background: rgba(0, 0, 0, 0.12);
                     overflow: hidden;
                     @for $index from 1 through 10 {
                        &:nth-child(#{$index}) {
                           $left: get-vh(44px * ($index - 1) + 72px);
                           left: $left;
                        }
                     }
                     @for $index from 11 through 16 {
                        &:nth-child(#{$index}) {
                           @if $index <= 13 {
                              top: get-vh(58px);
                              left: get-vh(522px + 45px * ($index - 11));
                           } @else {
                              top: get-vh(92px);
                              left: get-vh(522px + 45px * ($index - 14));
                           }
                        }
                     }
                     @for $index from 17 through 26 {
                        &:nth-child(#{$index}) {
                           top: get-vh(140px);
                           $left: get-vh(44px * ($index - 17) + 72px);
                           left: $left;
                        }
                     }
                     &:nth-child(27) {
                        top: get-vh(126px);
                        left: get-vh(27px);
                     }
                     &:nth-child(28) {
                        top: get-vh(92px);
                        left: get-vh(16px);
                     }
                     &:nth-child(29) {
                        top: get-vh(58px);
                        left: get-vh(16px);
                     }
                     &:nth-child(30) {
                        top: get-vh(24px);
                        left: get-vh(27px);
                     }
                     &:nth-child(31) {
                        top: get-vh(58px);
                        left: get-vh(252px);
                     }
                     &:nth-child(32) {
                        top: get-vh(58px);
                        left: get-vh(297px);
                     }
                     &:nth-child(33) {
                        top: get-vh(58px);
                        left: get-vh(342px);
                     }
                     &:nth-child(34) {
                        top: get-vh(92px);
                        left: get-vh(252px);
                     }
                     &:nth-child(35) {
                        top: get-vh(92px);
                        left: get-vh(297px);
                     }
                     &:nth-child(36) {
                        top: get-vh(92px);
                        left: get-vh(342px);
                     }
                     & > .status__cabin {
                        width: 100%;
                        height: 100%;
                        &.active-type-cabin {
                           background: #00D4FF;
                           @include centerHorizontal;
                           justify-content: center;
                           & > .icon-check {
                              width: get-vh(25px);
                              height: get-vh(20px);
                           }
                           &:hover {
                              background: rgba(0, 212, 255, 0.85);
                              & > .icon-check path {
                                 fill: #006A80;
                              }
                           }
                           &.active {
                              background: #fff;
                              & > .icon-check path {
                                 fill: #333333;
                              }
                           }
                        }
                        &.busy {
                           background: rgba(41, 176, 255, 1);
                           &:hover {
                              background: rgba(41, 176, 255, 0.85);
                              & > .icon-check path {
                                 fill: #074D87;
                              }
                           }
                        }
                        &.block {
                           background: rgba(255, 255, 255, 0.12);
                           &:hover {
                              background: rgba(255, 255, 255, 0.12);
                              & > .icon-check path {
                                 fill: #000;
                              }
                           }
                        }
                     }
                  }
               }
            }
            & > .status__cabins {
               margin-top: get-vh(68px);
               @include centerHorizontal;
               justify-content: center;
               gap: get-vh(60px);
               & > .item__status {
                  @include centerHorizontal;
                  gap: get-vh(14px);
                  text-align: left;
                  font-size: get-vh(14px);
                  font-weight: 600;
                  line-height: 1.21; 
                  letter-spacing: -0.04em;
                  &:nth-child(2) {
                     & > .block__status { 
                        background: #29B0FF;
                     }
                  }
                  &:nth-child(3) {
                     & > .block__status { 
                        background: linear-gradient(0deg, rgba(255, 255, 255, 0.12) 0%, rgba(255, 255, 255, 0.12) 100%), rgba(255, 255, 255, 0.12);
                     }
                  }
                  & > .block__status {
                     width: get-vh(41px);
                     height: get-vh(30px);
                     border-radius: get-vh(4px);
                     background: #00D4FF;
                     @include centerHorizontal;
                     justify-content: center;
                     & > .icon-status {
                        width: get-vh(25px);
                        height: get-vh(20px);
                     }
                  }
               }
            }
            & > .choose__place {
               margin-top: get-vh(80px);
               & > .title__cabinreserve {
                  font-size: get-vh(24px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
               }
               & > .wrapper__hotel {
                  margin-top: get-vh(30px);
                  display: flex;
                  justify-content: center;
                  gap: get-vh(14px);
                  & > .item__status {
                     width: get-vh(100px);
                     height: get-vh(56px);
                     border-radius: get-vh(16px);
                     background: rgba(255, 255, 255, 0.12);
                     @include centerHorizontal;
                     justify-content: center;
                     gap: get-vh(6px);
                     font-size: get-vh(26px);
                     font-weight: 600;
                     letter-spacing: -0.03em; 
                     &.active {
                        background: #0070CC;
                     }
                     & > .icon-hotel {
                        width: get-vh(32px);
                        height: get-vh(20px);
                     }
                     & > .icon-block {
                        margin-right: get-vh(-10px);
                        width: get-vh(50px);
                        height: get-vh(40px);
                     }
                  }
               }
            }
            & > .description__cabin {
               margin-top: get-vh(30px);
               color: $txtDescription;
               font-size: get-vh(14px);
               font-weight: 500;
               line-height: 1.21;
               letter-spacing: -0.03em;
            }
            & > .choose__nexttype {
               margin-top: get-vh(80px);
               width: get-vh(409px);
               height: get-vh(56px);
               border-radius: get-vh(18px);
               background: rgba(255, 255, 255, 0.12);
               font-size: get-vh(22px);
               font-weight: 600;
               letter-spacing: -0.03em;
               &:hover {
                  background: rgba(255, 255, 255, 1);
                  color: $txtBlack;
               }
            }
            & > .wrapper__btn {
               margin-top: get-vh(88px);
               @include centerHorizontal;
               justify-content: center;
               gap: get-vh(16px);
               & > .choose {
                  height: get-vh(56px);
                  padding: 0px get-vh(64px);
                  border-radius: get-vh(18px);
                  background: rgba(255, 255, 255, 0.12);
                  font-size: get-vh(22px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
                  &:hover {
                     background: rgba(255, 255, 255, 1);
                     color: $txtBlack;
                  }
               }
            }
         }
         & > .add__member {
            text-align: center;
            & > .title__cabin {
               font-size: get-vh(48px);
               font-weight: 500;
               letter-spacing: -0.03em;
            }
            & > .item__member {
               width: get-vh(720px);
               &:nth-child(2) {
                  margin-top: get-vh(60px);
               }
               &:nth-child(3) {
                  margin-top: get-vh(60px);
               }
               &:nth-child(4), &:nth-child(5) {
                  margin-top: get-vh(25px);
               }
               & > .title__member {
                  font-size: get-vh(18px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
               }
               & > .wrapper__block {
                  margin-top: get-vh(16px);
                  width: 100%;
                  display: flex;
                  gap: get-vh(16px);
                  &.inp {
                     & > .item__block { 
                        text-align: center;
                        width: get-vh(245px);
                        padding: 0 get-vh(12px);
                     }
                  }
                  & > .item__block {
                     width: get-vh(245px);
                     height: get-vh(56px);
                     border-radius: get-vh(18px);
                     background: rgba(0, 0, 0, 0.18);
                     @include centerHorizontal;
                     justify-content: center;
                     font-size: get-vh(22px);
                     font-weight: 600;
                     letter-spacing: -0.03em;
                  }
               }
               & > .want__buyall {
                  margin-top: get-vh(20px);
                  @include centerHorizontal;
                  justify-content: center;
                  gap: get-vh(24px);
                  font-size: get-vh(20px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
                  & > .circle__check {
                     width: get-vh(37.5px);
                     height: get-vh(37.5px);
                     @include centerHorizontal;
                     justify-content: center;
                     border-radius: get-vh(14px);
                     background: rgba(255, 255, 255, 0.10);
                     & > .icon-check {
                        width: get-vh(27px);
                        height: get-vh(27px);
                     }
                  }
               }
            }
            & > .wrapper__btn {
               margin-top: get-vh(88px);
               @include centerHorizontal;
               justify-content: center;
               gap: get-vh(16px);
               & > .choose {
                  height: get-vh(56px);
                  padding: 0px get-vh(64px);
                  border-radius: get-vh(18px);
                  background: rgba(255, 255, 255, 0.12);
                  font-size: get-vh(22px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
                  &:hover {
                     background: rgba(255, 255, 255, 1);
                     color: $txtBlack;
                  }
               }
            }
         }
         & > .accept__info {
            text-align: center;
            & > .title__cabin {
               font-size: get-vh(48px);
               font-weight: 500;
               letter-spacing: -0.03em;
            }
            & > .subtitle__reserve {
               margin-top: get-vh(7px);
               font-size: get-vh(20px);
               font-weight: 600;
               letter-spacing: -0.02em;
            }
            & > .wrapper__floors {
               position: relative;
               margin-top: get-vh(34px);
               & > .icon__floor {
                  width: get-vh(720px);
                  height: get-vh(180px);
               }
               & > .list__cabin {
                  position: absolute;
                  top: 0;
                  left: 0;
                  z-index: 1;
                  & > .item__cabin {
                     position: absolute;
                     top: get-vh(10px);
                     width: get-vh(41px);
                     height: get-vh(30px);
                     border-radius: get-vh(4px);
                     background: rgba(0, 0, 0, 0.12);
                     overflow: hidden;
                     @for $index from 1 through 10 {
                        &:nth-child(#{$index}) {
                           $left: get-vh(44px * ($index - 1) + 72px);
                           left: $left;
                        }
                     }
                     @for $index from 11 through 16 {
                        &:nth-child(#{$index}) {
                           @if $index <= 13 {
                              top: get-vh(58px);
                              left: get-vh(522px + 45px * ($index - 11));
                           } @else {
                              top: get-vh(92px);
                              left: get-vh(522px + 45px * ($index - 14));
                           }
                        }
                     }
                     @for $index from 17 through 26 {
                        &:nth-child(#{$index}) {
                           top: get-vh(140px);
                           $left: get-vh(44px * ($index - 17) + 72px);
                           left: $left;
                        }
                     }
                     &:nth-child(27) {
                        top: get-vh(126px);
                        left: get-vh(27px);
                     }
                     &:nth-child(28) {
                        top: get-vh(92px);
                        left: get-vh(16px);
                     }
                     &:nth-child(29) {
                        top: get-vh(58px);
                        left: get-vh(16px);
                     }
                     &:nth-child(30) {
                        top: get-vh(24px);
                        left: get-vh(27px);
                     }
                     &:nth-child(31) {
                        top: get-vh(58px);
                        left: get-vh(252px);
                     }
                     &:nth-child(32) {
                        top: get-vh(58px);
                        left: get-vh(297px);
                     }
                     &:nth-child(33) {
                        top: get-vh(58px);
                        left: get-vh(342px);
                     }
                     &:nth-child(34) {
                        top: get-vh(92px);
                        left: get-vh(252px);
                     }
                     &:nth-child(35) {
                        top: get-vh(92px);
                        left: get-vh(297px);
                     }
                     &:nth-child(36) {
                        top: get-vh(92px);
                        left: get-vh(342px);
                     }
                     & > .status__cabin {
                        width: 100%;
                        height: 100%;
                        @include centerHorizontal;
                        justify-content: center;
                        & > .icon-check {
                           width: get-vh(25px);
                           height: get-vh(20px);
                        }
                        &.active {
                           background: #fff;
                           & > .icon-check path {
                              fill: #333333;
                           }
                        }
                     }
                  }
               }
            }
            & > .wrapper__costinfo {
               margin-top: get-vh(44px);
               width: get-vh(720px);
               & > .number__cabin {
                  display: flex;
                  justify-content: space-between;
                  font-size: get-vh(36px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
               }
               & > .status__cabin {
                  margin-top: get-vh(6px);
                  display: flex;
                  justify-content: space-between;
                  font-size: get-vh(20px);
                  font-weight: 600;
                  letter-spacing: -0.02em;
               }
            }
            & > .wrapper__submit {
               margin-top: get-vh(46px);
               @include centerHorizontal;
               justify-content: center;
               gap: get-vh(24px);
               font-size: get-vh(20px);
               font-weight: 600;
               letter-spacing: -0.02em;
               & > .circle__check { 
                  width: get-vh(36px);
                  height: get-vh(36px);
                  @include centerHorizontal;
                  justify-content: center;
                  border-radius: get-vh(14px);
                  background: rgba(255, 255, 255, 0.10);
                  & > .icon-check {
                     width: get-vh(27px);
                     height: get-vh(27px);
                  }
               }
            }
            & > .description__reserve {
               margin-top: get-vh(60px);
               color: $txtDescription;
               font-size: get-vh(12px);
               font-weight: 600;
               line-height: 1.16;
               letter-spacing: -0.03em;
            }
            & > .wrapper__btn {
               margin-top: get-vh(60px);
               @include centerHorizontal;
               justify-content: center;
               gap: get-vh(16px);
               & > .choose {
                  height: get-vh(56px);
                  padding: 0px get-vh(64px);
                  border-radius: get-vh(18px);
                  background: rgba(255, 255, 255, 0.12);
                  font-size: get-vh(22px);
                  font-weight: 600;
                  letter-spacing: -0.03em;
                  transition: color 0.25s ease-out;
                  &.unactive {
                     color: $txtDescription;
                  }
                  &:not(.unactive):hover {
                     background: rgba(255, 255, 255, 1);
                     color: $txtBlack;
                  }
               }
            }
         }
      }
   }
</style>