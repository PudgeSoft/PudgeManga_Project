// const daysContainer = document.querySelector(".calendar-days"),
// /*шукає елемент на веб-сторінці з класом "calendar-days" 
// і зберігає його посилання в змінній daysContainer.*/
// nextBtn = document.querySelector(".c-next-btn"),
// prevBtn = document.querySelector(".c-prev-btn"),
// month = document.querySelector(".calendar-month");

// /*місяці*/
// const months = [
// 	"Січень",
// 	"Лютий",
// 	"Березень",
// 	"Квітень",
// 	"Травень",
// 	"Червень",
// 	"Липень",
// 	"Серпень",
// 	"Вересень",
// 	"Жовтень",
// 	"Листопад",
// 	"Грудень",
// 	];

// //get current date
// /*Date - використовується для роботи з датами та часом
// new Date() - поточна дата і час з моменту створення*/
// const date = new Date();

// //get current month
// /*let - створення змінної(можна змінити)
// січень 0, лютий 1....грудень 11
// currentMonth - містить номер поточного місяця*/
// let currentMonth = date.getMonth();

// //get current year
// /*поточний рік
// currentYear - містить номер поточного року*/
// let currentYear = date.getFullYear();

// // функція відображення дня
// function renderCalendar(){
// 	//get previous month and next month days
// 	//отримати дні попереднього місяця та наступного місяця(не потрібно)
// 	date.setDate(1); /*день місяця - 1 день*/
// 	/*1 день певного місяця*/
// 	const firstDay = new Date(currentYear, currentMonth, 1);

// 	/*currentMonth + 1 - для номеру наступного місяці(0-11)
// 	1 - 1 день місяця, 0 - останній день місяця, прикол*/
// 	const lastDay = new Date(currentYear, currentMonth + 1, 0);

// // const lastDayIndex = lastDay.getDay();!!

// 	const lastDayDate = lastDay.getDate();

// // const prevLastDay = new Date(currentYear, currentMonth, 0);!!
// // const prevLastDayDate = prevLastDay.getDate();!!

// 	/**/
// 	const nextDays = 7 - lastDay.getDay();


// //оновити поточний рік і місяць у заголовку!!!
// 	/*.іnnerHTML - оновлює вміст на хтмл
// 	${months[currentMonth]} - months[] - масив назв місяців
// 	currentMonth - номер поточного місяця 

// 	${currentYear} - поточний рік*/
// 	/*поточний рік і місяць*/
// month.innerHTML = `${months[currentMonth]} ${currentYear}`;	

// //update days html дні оновлення html
//       let days = "";

//       // //попередні дні html !!
//       // for(let x = 0)


// //дні поточного місяця
//       for(let i = 1;  i <= lastDayDate; i++){

//       	if(
//       		i === )

//       }

//       daysContainer.innerHTML = days; 

// }


    const daysContainer = document.querySelector(".days");
    const nextBtn = document.querySelector(".next");
    const prevBtn = document.querySelector(".prev");
  
    const month = document.querySelector(".month");

    const months = [
	"Січень",
	"Лютий",
	"Березень",
	"Квітень",
	"Травень",
	"Червень",
	"Липень",
	"Серпень",
	"Вересень",
	"Жовтень",
	"Листопад",
	"Грудень",
	];
//get current date
    const date = new Date();

    //get current month
    let currentMonth = date.getMonth();
    
    //get current year
    let currentYear = date.getFullYear();

    const renderCalendar = () => {
      date.setDate(1);
      const firstDay = new Date(currentYear, currentMonth, 1);
      const lastDay = new Date(currentYear, currentMonth + 1, 0);
      const lastDayDate = lastDay.getDate();
      const nextDays = 7 - lastDay.getDay();

      month.textContent = `${months[currentMonth]} ${currentYear}`;

      let days = "";

      for (let i = 1; i <= lastDayDate; i++) {
        if (
          i === date.getDate() &&
          currentMonth === date.getMonth() &&
          currentYear === date.getFullYear()
        ) {
          days += `<div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 mb-4">
            <div class="image-container">
                <img src="https://img.championat.com/c/1200x900/news/big/z/e/ign-25-luchshih-anime-v-istorii_1672055411452433690.jpg" class="img-fluid img-fit">
                <div class="day-number-box">
                    <h3 class="day-number ">${i}</h3>
                </div>
            </div>
        </div>`;
        } else {
          days += `        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 mb-4">
            <div class="image-container">
                <img src="https://img.championat.com/c/1200x900/news/big/z/e/ign-25-luchshih-anime-v-istorii_1672055411452433690.jpg" class="img-fluid img-fit">
                <div class="day-number-box">
                    <h3 class="day-number ">${i}</h3>
                </div>
            </div>
        </div>`;
        }
      }

      daysContainer.innerHTML = days;
      hideTodayBtn();
    };

    nextBtn.addEventListener("click", () => {
      currentMonth++;
      if (currentMonth > 11) {
        currentMonth = 0;
        currentYear++;
      }
      renderCalendar();
    });

    prevBtn.addEventListener("click", () => {
      currentMonth--;
      if (currentMonth < 0) {
        currentMonth = 11;
        currentYear--;
      }
      renderCalendar();
    });




    renderCalendar();