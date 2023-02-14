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


## Перечень тестов

### Чек-лист позитивных проверок для выбора города

1. Через поиск, корректное значение с заглавной буквы — не реализовано в данный момент, ввиду сложности выбора нужного города из нескольких значеий из-за тега <mark>
2. Через поиск, корретное значение в нижнем регистре — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L48-L58)
3. Через поиск, корректное значение в верхнем регистре — не реализовано в данный момент
4. Через поиск, корректное значение с лишними пробелами до и после значения — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L60-L71)
4. Через поиск, неполное название города — не реализовано в данный момент
5. Через поиск, название города на англ. раскладке — не реализовано в данный момент
6. Через список городов под полем для поиска — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L34-L44)
7. Через список округов и областей — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L100-L113)

### Чек-лист негативных проверок

8. Ввод цифр в поле для поиска города — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L73-L80)
9. Ввод спец. символов и знаков пунктуации (кроме дефиса) в поле для поиска города — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L82-L89)
10. Ввод только пробелов в поле для поиска города — [реализовано](https://github.com/albinamv/DNSCitySelectorTest/blob/5762839ca5f402a46b606447a5fedea9a41269d2/Tests/CitySelectorTests.cs#L91-L98)


## Сложности в ходе выполнения задания

1. [Отсуствие поддержки драйвера для Opera](https://www.selenium.dev/documentation/webdriver/getting_started/install_drivers/)
2. При многократном выполнении автотестов происходит временная "блокировка" модуля для выбора города — при загрузке модального окна не подгружаются списки городов, областей, округов. Из-за этого тесты могут падать, т.к. не получается найти нужные элементы в течение заданного времени. Решается подключением к другой сети.
![pic](img/CantLoadData.png)