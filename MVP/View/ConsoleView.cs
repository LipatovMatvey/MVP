using System;
using System.Collections.Generic;
using System.Linq;
using LABA7_OOP.Model;
using Laba3_oop;

namespace MVP.View
{
    public class ConsoleView : IView
    {
        public event Action? AddShop;
        public event Action? RemoveShop;
        public event Action? ShowAllShops;
        public event Action? Exit;

        /// <summary>
        /// Запускает главный цикл консольного меню
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление интернет-магазинами ===");
                Console.WriteLine("1. Добавить магазин");
                Console.WriteLine("2. Удалить магазин");
                Console.WriteLine("3. Показать все магазины");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите пункт: ");
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddShop?.Invoke(); break;
                    case "2": RemoveShop?.Invoke(); break;
                    case "3": ShowAllShops?.Invoke(); break;
                    case "4": Exit?.Invoke(); return;
                    default: ShowMessage("Неверный ввод.", true); break;
                }
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        public void ShowMessage(string message, bool isError = false)
        {
            if (isError) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowShops(IEnumerable<InternetShop> shops)
        {
            Console.Clear();
            Console.WriteLine("=== Список магазинов ===");
            var list = shops.ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("Нет магазинов.");
            }
            else
            {
                foreach (var s in list)
                {
                    Console.WriteLine($"{s.Name} | {s.Address} " +
                        $"| Покупок: {s.PurchaseCount} | Товаров: {s.ProductCount} " +
                        $"| Чек: {s.AverageCheck:C} | Рейтинг: {s.Rating:F1} | {(s.IsActive ? "Активен" : "Неактивен")}");
                }
            }
        }

        public InternetShop GetNewShopData()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление магазина ===");
            string name = ReadValidString("Название: ", InputChecker.IsValidShopName, "Ошибка! Название должно быть 2-40 символов, без повторяющихся пробелов и не только из цифр.");
            string address = ReadValidString("Адрес склада: ", InputChecker.IsValidAddress, "Ошибка! Адрес должен быть 2-40 символов.");
            int purchases = ReadInt("Покупок в сутки: ", 0, int.MaxValue);
            int products = ReadInt("Количество товаров: ", 0, int.MaxValue);
            double avgCheck = ReadDouble("Средний чек: ", 0, double.MaxValue);
            double rating = ReadDouble("Рейтинг (0-5): ", 0, 5);
            bool active = ReadBool("Активен? (да/нет): ");
            return new InternetShop(name, address, purchases, products, avgCheck, rating, active);
        }

        public string? GetShopNameToRemove()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление магазина ===");
            Console.Write("Введите название: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Вспомогательный метод для ввода строки с проверкой через переданный валидатор
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="validator">Функция-предикат для проверки введённой строки</param>
        /// <param name="errorMessage">Сообщение об ошибке при невалидном вводе</param>
        /// <returns>Валидная строка</returns>
        private string ReadValidString(string prompt, Func<string, bool> validator, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (validator(input))
                {
                    return input;
                }
                ShowMessage(errorMessage, true);
            }
        }

        /// <summary>
        /// Вспомогательный метод для ввода целого числа в заданном диапазоне
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="min">Минимальное допустимое значение</param>
        /// <param name="max">Максимальное допустимое значение</param>
        /// <returns>Валидное целое число</returns>
        private int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (InputChecker.IsValidInt(input, min, max, out int result))
                {
                    return result;
                }
                ShowMessage($"Ошибка! Введите целое число от {min} до {max}.", true);
            }
        }

        /// <summary>
        /// Вспомогательный метод для ввода вещественного числа в заданном диапазоне
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="min">Минимальное допустимое значение</param>
        /// <param name="max">Максимальное допустимое значение</param>
        /// <returns>Валидное вещественное число</returns>
        private double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (InputChecker.IsValidDouble(input, min, max, out double result))
                {
                    return result;
                }
                ShowMessage($"Ошибка! Введите число от {min} до {max}.", true);
            }
        }

        /// <summary>
        /// Вспомогательный метод для ввода булева значения (да/нет)
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <returns>true – при вводе "да", false – при вводе "нет"</returns>
        private bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (InputChecker.IsValidBool(input, out bool result))
                {
                    return result;
                }
                ShowMessage("Ошибка! Введите 'да' или 'нет'.", true);
            }
        }
    }
}