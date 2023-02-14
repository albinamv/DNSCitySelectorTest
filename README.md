# Тестовое задание для DNS

## Объём тестирования

Реализованы UI-автотесты, которые осуществляют проверку корректной работы функциональности выбора города на сайте https://www.dns-shop.ru

[Полный текст задания](https://github.com/albinamv/DNSCitySelectorTest/tree/main/Task)

## Реализация

* В ходе работ были созданы классы PageObject: [MainMenuPageObject.cs](https://github.com/albinamv/DNSCitySelectorTest/blob/main/PageObjects/MainMenuPageObject.cs) для описания главного меню сайта и [CitySelectorPageObject.cs](https://github.com/albinamv/DNSCitySelectorTest/blob/main/PageObjects/CitySelectorPageObject.cs) для описания модального окна с выбором города
* Дополнительно созданы классы-хелперы для генерации данных ([TestGenerateData.cs](https://github.com/albinamv/DNSCitySelectorTest/blob/main/Helpers/TestGenerateData.cs)), для создания ожиданий прогрузки элементов ([WaitUntil.cs](https://github.com/albinamv/DNSCitySelectorTest/blob/main/Helpers/WaitUntil.cs))
* Запуск автотестов осуществляется в браузерах Chrome и Firefox (без Opera, согласно [документации Selenium](https://www.selenium.dev/documentation/webdriver/getting_started/install_drivers/), драйвер для Opera больше не поддерживается)
* Параллельный запуск тестов реализован с помощью аннотации NUnit [Parallelizable].
* Тесты запускаются для разрешения 1920*1080 px. Тесты для других разрешений не реализованы ввиду ограничений времени на выполнение работы.


## Сложности в ходе выполнения задания

1. [Отсуствие поддержки драйвера для Opera](https://www.selenium.dev/documentation/webdriver/getting_started/install_drivers/)
2. При многократном выполнении автотестов происходит временная "блокировка" модуля для выбора города — при загрузке модального окна не подгружаются списки городов, областей, округов. Из-за этого тесты могут падать, т.к. не получается найти нужные элементы в течение заданного времени. Решается подключением к другой сети.
![pic](img/CantLoadData.png)