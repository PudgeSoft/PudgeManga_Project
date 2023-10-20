
## Опис проекту
Цей проєкт є веб-додатком для перегляду манги та аніме із можливістю взаємодії користувачів за допомогою форуму та коментарів до манги або аніме. ASP.NET Core MVC використовується як основна технологія для створення веб-сайту. Веб- додаток надаватиме можливість перегляду аніме та манги, інформації про них, пошуку улюблених тайтлів за обраними категоріями та зручне використання незалежно від пристрою.

## Вимоги
1. ASP.NET Core 6.0 
2. Microsoft SQL Server, SQL Server Management Studio(бажано) або інша СУБД
3. Visual Studio або інший IDE для розробки .NET Core
4. Entity Framework

## Інструкція по встановленню

1. Завантажуємо IDE наприклад Visual Studio
2. Відкриваємо Visual Studio installer та із компонентів шукаємо виділений та встановлюємо: ![image](https://github.com/PudgeSoft/PudgeManga_Project/assets/119793234/3b914e14-2af4-4b4e-8ce5-94dbc889156d)
3. Відкриваємо NuGet package manager та встановлюємо наступні пакети ![image](https://github.com/PudgeSoft/PudgeManga_Project/assets/119793234/a9cb99f9-730e-4aa2-a35b-f220154fe20b) Microsoft.EntityFrameworkCore.Tools для створення міграцій БД, Microsoft.EntityFrameworkCore.SqlServer для взаємодії із SqlServer, Microsoft.EntityFrameworkCore - базові файли для роботи ORM.
4. Склонуте репозиторій із гітхабу git clone [https://github.com/PudgeSoft/PudgeManga_Project](https://github.com/PudgeSoft/PudgeManga_Project.git)
5. Для обновлення поточної версії проєкту необхідно робити git pull , у випадку якщло це необхідно.
6. Готово!

## Вкладені файли та структура проекту
wwwroot: Статичні ресурси, такі як зображення та CSS-файли.
Controllers: Контролери, які обробляють HTTP-запити.
Models: Моделі даних.
Data: Шар для взаємодії із базою данних.
Views: Представлення веб-сторінок.
Shared: Вміст цієї папки складається із layout.cshtml, який містить у собі header footer та background, які є загальними для усього сайту.
appsettings.json: Налаштування додатка, включаючи рядок підключення до бази данних(в недалекому майбутньому).
Program.cs: Точка входу в додаток. 
## Автори
PudgeSoft
