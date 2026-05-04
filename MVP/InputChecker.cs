using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Laba3_oop
{
    public abstract class InputChecker
    {
        /// <summary>
        /// Регулярное выражение для проверки на корректность ввода имени магазина
        /// </summary>
        private static readonly string _shopNamePattern = @"^(?!\d+$)(?!.*\s{2})[A-Za-zА-Яа-яЁё0-9&""' -/]{2,40}$";

        /// <summary>
        /// Регулярное выражение для проверки на корректность ввода адреса
        /// </summary>
        private static readonly string _addressPattern = @"^(?!\d+$)(?!.*\s{2})[A-Za-zА-Яа-яЁё0-9&""'., -/]{2,40}$";

        /// <summary>
        /// Проверяет корректность имени магазина
        /// </summary>
        /// <param name="name">Имя для проверки</param>
        /// <returns>true если имя корректно, иначе false</returns>
        public static bool IsValidShopName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            return Regex.IsMatch(name, _shopNamePattern);
        }

        /// <summary>
        /// Проверяет корректность адреса склада магазина
        /// </summary>
        /// <param name="address">Адрес для проверки</param>
        /// <returns>true если адрес корректен, иначе false</returns>
        public static bool IsValidAddress(string? address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }
            return Regex.IsMatch(address, _addressPattern);
        }

        /// <summary>
        /// Проверяет, является ли строка целым числом в заданном диапазоне
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="min">Минимум</param>
        /// <param name="max">Максимум</param>
        /// <param name="result">Результат преобразования, если успешно, иначе 0</param>
        /// <returns>true если строка является целым числом в диапазоне, иначе false</returns>
        public static bool IsValidInt(string? input, int min, int max, out int result)
        {
            result = 0;
            if (int.TryParse(input, out int value))
            {
                if (value >= min && value <= max)
                {
                    result = value;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, является ли строка вещественным числом в заданном диапазоне
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="min">Минимум</param>
        /// <param name="max">Максимум</param>
        /// <param name="result">Результат преобразования, если успешно, иначе 0</param>
        /// <returns>true если строка является числом в диапазоне, иначе false</returns>
        public static bool IsValidDouble(string? input, double min, double max, out double result)
        {
            result = 0;
            if (double.TryParse(input, out double value))
            {
                if (value >= min && value <= max)
                {
                    result = value;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, является ли строка допустимым булевым значением
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="result">Результат: true для "да", false для "нет"</param>
        /// <returns>true если строка распознана, иначе false</returns>
        public static bool IsValidBool(string? input, out bool result)
        {
            result = false;
            if (string.IsNullOrWhiteSpace(input)) return false;
            string lower = input.Trim().ToLower();
            if (lower == "да")
            {
                result = true;
                return true;
            }
            if (lower == "нет")
            {
                result = false;
                return true;
            }
            return false;
        }
    }
}
