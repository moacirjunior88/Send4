using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Home : BaseTest
    {
        #region Elements
        #region Components
        private static By btnComecar = By.ClassName("introduction-actions");
 
        #endregion
        #endregion

        #region Methods
        #region Clicks

        public static void Comecar()
        {
            Clicks(btnComecar);
        }
        #endregion
        #endregion
    }
}
