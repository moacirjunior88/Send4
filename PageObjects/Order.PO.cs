using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Order : BaseTest
    {
        #region Elements
        #region Components
        private static By txbPedido = By.Id("order-number");
        private static By txbConfirmeEmail = By.Id("order-confirmation");
        private static By btnBuscarContinuar = By.ClassName("buttons");
        #endregion
        #endregion

        #region Methods

        #region Clicks
        public static void BuscarContinuar()
        {
            Clicks(btnBuscarContinuar);
        }
        #endregion
        #region Sendkeys
        public static void Pedido(string pedido)
        {
            Sendkeys(txbPedido, pedido);
        }

        public static void ConfirmeEmail(string confirmeEmail)
        {
            Sendkeys(txbConfirmeEmail, confirmeEmail);
        }
        #endregion
        #endregion
    }
}
