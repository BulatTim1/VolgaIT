# VolgaIT
Создано специально для **Международная цифровая олимпиада «Волга-IT’22»** <br>
Участник: _Тимуршин Булат_

## Команды для запуска приложения:

    git clone https://github.com/BulatTim1/VolgaIT.git
    cd VolgaIT
    docker-compose up --build

> Сайт находится на http://localhost:80

Запрос для создания события: 
> POST http://localhost/Analytics/Create <br>
> {AppId = <Индетификатор приложения>, Event = <Тип события>, [Info = <Дополнительная информация>]}
