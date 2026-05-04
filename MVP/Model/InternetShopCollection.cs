using System;
using System.Collections.Generic;
using LABA7_OOP.Model;

namespace MVC.Model
{
    public class InternetShopCollection
    {
        /// <summary>
        /// Внутренний список магазинов.
        /// </summary>
        private List<InternetShop> shops = new List<InternetShop>();

        /// <summary>
        /// Событие, возникающее при изменении коллекции
        /// </summary>
        public event Action? CollectionChanged;

        /// <summary>
        /// Добавляет магазин в коллекцию
        /// </summary>
        /// <param name="shop">Добавляемый магазин</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если shop равен null</exception>
        public void Add(InternetShop shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException(nameof(shop));
            }
            shops.Add(shop);
            CollectionChanged?.Invoke();
        }

        /// <summary>
        /// Удаляет магазин из коллекции
        /// </summary>
        /// <param name="shop">Удаляемый магазин</param>
        /// <returns>true, если удаление выполнено; false, если магазин не найден</returns>
        public bool Remove(InternetShop shop)
        {
            bool result = shops.Remove(shop);
            if (result)
            {
                CollectionChanged?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// Возвращает копию списка всех магазинов
        /// </summary>
        /// <returns>Новый список, содержащий все магазины</returns>
        public List<InternetShop> GetAll()
        {
            return new List<InternetShop>(shops);
        }
    }
}