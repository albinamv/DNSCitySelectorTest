

namespace DnsCitySelectorTests.Helpers
{
    public static class TestGenerateData
    {

        static private readonly string[] bigCities = { "Москва","Санкт-Петербург", "Новосибирск",
            "Краснодар","Екатеринбург","Казань","Уфа","Нижний Новгород","Челябинск","Самара",
                "Ростов-на-Дону","Омск","Красноярск","Воронеж","Пермь","Волгоград","Владивосток"};

        public static string getRandomBigCity()
        {
            Random random = new Random();
            return bigCities[random.Next(bigCities.Length)];
        }

        public static string GenerateRandomNumber()
        {
            return Faker.RandomNumber.Next(30000).ToString();
        }

        public static string GetSpecialCharacters()
        {
            return "`~!@\"№#;$%:^?&*()_+={}[]/<>,.";
        }

        public static string GenerateBigCityWithSpacesBeforeAndAfter()
        {
            return "      " + getRandomBigCity() + "       ";
        }

    }
}
