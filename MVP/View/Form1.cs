using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LABA7_OOP.Model;
using MVP.View;

namespace MVP
{
    public partial class Form1 : Form, IView
    {
        public event Action? AddShop;
        public event Action? RemoveShop;
        public event Action? ShowAllShops;
        public event Action? Exit;

        /// <summary>
        /// Список текущих магазинов для привязки к ComboBox
        /// </summary>
        private List<InternetShop> currentShops = new List<InternetShop>();
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            BindEvents();
        }

        /// <summary>
        /// Подписывает обработчики на события элементов управления
        /// </summary>
        private void BindEvents()
        {
            button1.Click += (s, e) => AddShop?.Invoke();
            btnDeleteObject.Click += (s, e) => RemoveShop?.Invoke();
            cmbObjectsList.SelectedIndexChanged += (s, e) => ShowSelectedShopInfo();
        }

        /// <summary>
        /// Отображает информацию о выбранном магазине в текстовом поле
        /// </summary>
        private void ShowSelectedShopInfo()
        {
            if (cmbObjectsList.SelectedItem is InternetShop shop)
            {
                txtDisplayInfo.Text = shop.ToString();
                lblCurrentObject.Text = $"Текущий объект: {shop.Name}";
            }
            else
            {
                txtDisplayInfo.Text = "Выберите магазин из списка.";
                lblCurrentObject.Text = "Текущий объект: не выбран";
            }
        }

        /// <summary>
        /// Очищает дисплай
        /// </summary>
        private void ClearInputFields()
        {
            txtDisplayInfo.Clear();
        }

        /// <summary>
        /// Сбрасывает поля ввода к значениям по умолчанию
        /// </summary>
        private void ResetInputFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            comboBox1.SelectedIndex = 0;
        }

        public void ShowMessage(string message, bool isError = false)
        {
            MessageBox.Show(message,
                isError ? "Ошибка" : "Информация",
                MessageBoxButtons.OK,
                isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        public void ShowShops(IEnumerable<InternetShop> shops)
        {
            currentShops = shops.ToList();
            cmbObjectsList.DataSource = null;
            cmbObjectsList.DataSource = currentShops;
            cmbObjectsList.DisplayMember = "Name";
            cmbObjectsList.ValueMember = null;
            lblObjectCount.Text = $"Создано объектов: {currentShops.Count}";
            if (currentShops.Count > 0)
            {
                cmbObjectsList.SelectedIndex = 0;
            }
            else
            {
                ShowSelectedShopInfo();
            }
        }

        public InternetShop GetNewShopData()
        {
            string name = textBox1.Text;
            string address = textBox2.Text;
            int purchases = (int)numericUpDown1.Value;
            int products = (int)numericUpDown2.Value;
            double avgCheck = (double)numericUpDown3.Value;
            double rating = (double)numericUpDown4.Value;
            bool isActive = (comboBox1.SelectedIndex == 0);
            return new InternetShop(name, address, purchases, products, avgCheck, rating, isActive);
        }

        public string? GetShopNameToRemove()
        {
            if (cmbObjectsList.SelectedItem is InternetShop shop)
            {
                return shop.Name;
            }
            return null;
        }

        /// <summary>
        /// Обработчик кнопки "Выход"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик кнопки "Сбросить поля"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetInputFields();
        }

        /// <summary>
        /// Обработчик кнопки "Очистить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        /// <summary>
        /// Обработчик кнопки "Показать данные"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnShowInfo_Click(object sender, EventArgs e)
        {
            ShowSelectedShopInfo();
        }
    }
}