using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Exchange : BaseTest
    {
        #region Elements
        #region Components
        private static readonly string btn0A10 = "//*[@id='root']/section/main/div[2]/div[1]/div/div/div[1]/div/div[1]/ul/li[";
        private static readonly By txbComentario = By.Id("finish-comment");
        private static readonly By btnEnviarAvaliacao = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div/div/div[3]/button");
        private static readonly By msgEnvioAvaliacao = By.ClassName("mb-1");
        #endregion
        #endregion

        #region Methods
        #region Clicks
        public static void DeZeroADez(int numero)
        {
            By btnZeroADez = By.XPath(btn0A10 + (numero + 1).ToString() + "]");
            Clicks(btnZeroADez);
        }

        public static void EnviarAvaliacao()
        {
            Clicks(btnEnviarAvaliacao);
        }
        #endregion

        #region SendKeys
        public static void Comentario(string comentario)
        {
            Sendkeys(txbComentario, comentario);
        }
        #endregion

        #region Text
        public static string MensagemDeEnvioAvalicao()
        {
            return Text(msgEnvioAvaliacao);
        }
        #endregion
        #endregion
    }
}
