# Аукцион автомобилей

## Назначение
Тестовое задание для крупной IT компании

## Краткое описание (из тех.задания)

"The goal of the online auction is to automate the car pricing process before its sale. The person enters information about the car going to be offered at the auction. After that, the system shows the recommended price, and the user can submit the car for beforehand created auction. The system has an internal API to manage auctions available only for administrators."

## Основные функции (из тех.задания)

* Ability to get, delete, update and add a car.
* Functionality to calculate car grade based on its condition.
* Functionality to calculate the optimized price for a vehicle.
* Ability to get, update, list, delete and add an auction.
* It should contain functionality to assign a car to an auction that hasn't started yet.
* Functionality to allow users to add their bids to the specific car(s) which were assigned to an opened auction.
* Once the auction has closed, system should automatically select a person who gave the best bid per each car.
* Functionality to get a list of cars with their bids that are placed in the auction.

## Технологии

* ASP.NET 5 CORE / WEB API / 3-слойная архитектура
* DI: Autofac
* Авторизация: JWT-token
* ORM: Entity Framework Core
* MediatR + CQRS
* Валидация: Fluent Validation, IPipelineBehavior
* Фоновая служба: Quartz
* Шифрование паролей: Pbkdf2 / HMACSHA256
* AutoMapping
