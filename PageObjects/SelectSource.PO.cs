using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class SelectSource : BaseTest
    {
        #region Elements
        #region Components
        private static By btnTipoLoja = By.ClassName("actions-icons");
        #endregion
        #endregion

        #region Methods
        #region Clicks
        public static void LojaFisica()
        {
            IReadOnlyList<IWebElement> listaLoja = ListElement(btnTipoLoja);
            listaLoja[0].Click();
        }

        public static void LojaVirtual()
        {
            IReadOnlyList<IWebElement> listaLoja = ListElement(btnTipoLoja);
            listaLoja[1].Click();
        }
        #endregion
        #endregion
    }
}
