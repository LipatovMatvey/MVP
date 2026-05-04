using System;
using System.Collections.Generic;
using LABA7_OOP.Model;

namespace MVP.View
{
    public interface IView
    {
        /// <summary>
        /// Событие, возникающее при добавлении нового интернет-магазина
        /// </summary>
        public event Action? AddShop;

        /// <summary>
        /// Событие, возникающее при удалении интернет-магазин
        /// </summary>
        public event Action? RemoveShop;

        /// <summary>
        /// Событие, возникающее при отображении списка всех интернет-магазинов
        /// </summary>
        public event Action? ShowAllShops;

        /// <summary>
        /// Событие, возникающее при завершении работы приложения
        /// </summary>
        public event Action? Exit;

        /// <summary>
        /// Отображает сообщение пользователю
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="isError">true – сообщение об ошибке, false – информационное сообщение</param>
        public void ShowMessage(string message, bool isError = false);

        /// <summary>
        /// Отображает список интернет-магазинов
        /// </summary>
        /// <param name="shops">Коллекция магазинов для отображения</param>
        public void ShowShops(IEnumerable<InternetShop> shops);

        /// <summary>
        /// Запрашивает у пользователя данные для создания нового интернет-магазина
        /// </summary>
        /// <returns>Объект InternetShop, заполненный данными из пользовательского ввода</returns>
        public InternetShop GetNewShopData();

        /// <summary>
        /// Запрашивает у пользователя идентификатор магазина, который следует удалить
        /// </summary>
        /// <returns>Название магазина для удаления или null, если пользователь отменил операцию</returns>
        public string? GetShopNameToRemove();
    }
}