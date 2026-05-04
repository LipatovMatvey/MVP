using System;
using System.Linq;
using Laba3_oop;
using LABA7_OOP.Model;
using MVC.Model;
using MVP.View;

namespace MVP.Presenter
{
    public class InternetShopPresenter
    {
        /// <summary>
        /// Модель, содержащая коллекцию интернет-магазинов
        /// </summary>
        private readonly InternetShopCollection _model;

        /// <summary>
        /// Представление
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// Конструктор презентера
        /// </summary>
        /// <param name="model">Модель коллекции магазинов</param>
        /// <param name="view">Представление</param>
        public InternetShopPresenter(InternetShopCollection model, IView view)
        {
            _model = model;
            _view = view;
            _view.AddShop += OnAddShop;
            _view.RemoveShop += OnRemoveShop;
            _view.ShowAllShops += OnShowAllShops;
            _view.Exit += OnExit;
            _model.CollectionChanged += () => OnShowAllShops();
            OnShowAllShops();
        }

        /// <summary>
        /// Обработчик события добавления нового магазина
        /// </summary>
        private void OnAddShop()
        {
            InternetShop newShop = _view.GetNewShopData();
            if (newShop == null) return;
            if (!InputChecker.IsValidShopName(newShop.Name))
            {
                _view.ShowMessage("Некорректное название магазина! (2-40 символов, без повторяющихся пробелов)", true);
                return;
            }
            if (!InputChecker.IsValidAddress(newShop.Address))
            {
                _view.ShowMessage("Некорректный адрес склада! (2-40 символов)", true);
                return;
            }
            if (newShop.PurchaseCount < 0 || newShop.ProductCount < 0 || newShop.AverageCheck < 0 ||
                newShop.Rating < 0 || newShop.Rating > 5)
            {
                _view.ShowMessage("Числовые значения должны быть неотрицательными.", true);
                return;
            }
            if (newShop.Rating < 1 || newShop.Rating > 5)
            {
                _view.ShowMessage("Рейтинг должен быть от 1 до 5.", true);
                return;
            }
            _model.Add(newShop);
            _view.ShowMessage($"Магазин \"{newShop.Name}\" успешно добавлен!");
        }

        /// <summary>
        /// Обработчик события удаления магазина
        /// </summary>
        private void OnRemoveShop()
        {
            var allShops = _model.GetAll();
            if (allShops.Count == 0)
            {
                _view.ShowMessage("Нет магазинов для удаления.", true);
                return;
            }
            string? shopName = _view.GetShopNameToRemove();
            if (string.IsNullOrWhiteSpace(shopName))
            {
                _view.ShowMessage("Выберите магазин для удаления или введите название.", true);
                return;
            }
            var shopToRemove = _model.GetAll().FirstOrDefault(s => s.Name.Equals(shopName, StringComparison.OrdinalIgnoreCase));
            if (shopToRemove != null && _model.Remove(shopToRemove))
            {
                _view.ShowMessage($"Магазин \"{shopName}\" удалён.");
            }
            else
            {
                _view.ShowMessage($"Магазин с именем \"{shopName}\" не найден.", true);
            }
        }

        /// <summary>
        /// Обработчик события отображения всех магазинов
        /// </summary>
        private void OnShowAllShops()
        {
            var shops = _model.GetAll();
            _view.ShowShops(shops);
        }

        /// <summary>
        /// Обработчик события выхода из приложения.
        /// </summary>
        private void OnExit()
        {
            Environment.Exit(0);
        }
    }
}