using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Customer : BaseTest
    {
        #region Elements
        #region Components
        private static By txbNome = By.Id("customer-firstName");
        private static By txbSobreNome = By.Id("customer-lastName");
        private static By txbDocumento = By.Id("customer-document");
        private static By txbEmail = By.Id("customer-email");
        private static By txbTelefone = By.Id("customer-phone");
        private static By txbCep = By.Id("address-zip_code");
        private static By txbNumero = By.Id("address-number");
        private static By txbComplemento = By.Id("address-complement");
        private static By btnVoltar = By.ClassName("btn");
        private static By btnSalvar = By.ClassName("btn btn-primary");
        #endregion
        #endregion

        #region Methods
        #region Clicks

        public static void Voltar()
        {
            Clicks(btnVoltar);
        }

        public static void Salvar()
        {
            Clicks(btnSalvar);
        }
        #endregion

        #region SendKeys
        public static void Nome(string nome)
        {
            Sendkeys(txbNome, nome);
        }

        public static void SobreNome(string sobreNome)
        {
            Sendkeys(txbSobreNome, sobreNome);
        }

        public static void Documento(string documento)
        {
            Sendkeys(txbDocumento, documento);
        }

        public static void Email(string email)
        {
            Sendkeys(txbEmail, email);
        }

        public static void Telefone(string telefone)
        {
            Sendkeys(txbTelefone, telefone);
        }

        public static void Cep(string cep)
        {
            Sendkeys(txbCep, cep);
        }

        public static void Numero(string numero)
        {
            Sendkeys(txbNumero, numero);
        }

        public static void Complemento(string complemento)
        {
            Sendkeys(txbComplemento, complemento);
        }
        #endregion
        #endregion
    }
}
