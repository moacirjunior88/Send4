using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Shipping : BaseTest
    {
        private static IReadOnlyList<IWebElement> listNomeLojaFisica, listLoja3, listLoja1;

        #region Elements
        #region Components
        private static By lblEndereco = By.ClassName("shipping-change-address--info");
        private static By lblNomeOutrosMetodos = By.ClassName("couriers-title");
        private static By lblEnderecoLojas = By.ClassName("address");
        private static By lblNomeLojasFisicas = By.ClassName("title");
        private static By txbCep = By.Id("address-zip_code");
        private static By txbEndereco = By.Id("address-address");
        private static By txbNumero = By.Id("address-number");
        private static By txbBairro = By.Id("address-neighborhood");
        private static By txbCidade = By.Id("address-city");
        private static By txbEstado = By.Id("address-state");
        private static By txbComplemento = By.Id("address-complement");
        private static By btnAlterarEndereco = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div[1]/div[2]");
        private static By btnLojas = By.ClassName("stores-button");
        private static By btnOutrosMetodos = By.ClassName("couriers-button");
        private static By btnContinuar = By.ClassName("buttons");
        private static By btnSalvar = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div[3]/div/div/div/form/div[2]/button[2]");
        #endregion
        #endregion

        #region Methods
        #region Clicks
        public static void AlterarEndereço()
        {
            Clicks(btnAlterarEndereco);
        }

        public static void Loja3()
        {
            listLoja3 = ListElement(btnLojas);

            listLoja3[0].Click();
        }

        public static void Loja1()
        {
            listLoja1 = ListElement(btnLojas);

            listLoja1[1].Click();
        }

        public static void OutrosMetodos()
        {
            ScroolElement(btnOutrosMetodos);
            Clicks(btnOutrosMetodos);
        }

        public static void Continuar()
        {
            Clicks(btnContinuar);
        }

        public static void Salvar()
        {
            Clicks(btnSalvar);
            Wait(5000);
        }
        #endregion

        #region Text
        public static string NomeLoja3()
        {
            listNomeLojaFisica = ListElement(lblNomeLojasFisicas);
            return listNomeLojaFisica[0].Text;
        }

        public static string NomeLoja1()
        {
            listNomeLojaFisica = ListElement(lblNomeLojasFisicas);
            return listNomeLojaFisica[1].Text;
        }

        public static string NomeOutrosMetodos()
        {
            return Text(lblNomeOutrosMetodos);
        }

        public static string EndereçoPrincipal()
        {
            return Text(lblEndereco);
        }

        public static string EndereçoLoja3()
        {
            listLoja3 = ListElement(lblEnderecoLojas);
            return listLoja3[0].Text;
        }

        public static string EndereçoLoja1()
        {
            listLoja1 = ListElement(lblEnderecoLojas);
            return listLoja1[1].Text;
        }

        public static string Endereco()
        {
            return GetAttribute(txbEndereco);
        }

        public static string Bairro()
        {
            return GetAttribute(txbBairro);
        }

        public static string Cidade()
        {
            return GetAttribute(txbCidade);
        }

        public static string Estado()
        {
            return GetAttribute(txbEstado);
        }
        #endregion

        #region SendKeys
        public static void Cep(string cep)
        {
            Clear(txbCep);
            char[] listaCep = cep.ToCharArray();
            for (int i = 0; i <= (cep.Length -1); i++)
            {
                Sendkeys(txbCep, listaCep[i].ToString());
            }
            
            action.SendKeys("{TAB}").Perform();
        }

        public static void Numero(string numero)
        {
            Clear(txbNumero);
            Sendkeys(txbNumero, numero);
        }

        public static void Complemento(string complemento)
        {
            Clear(txbComplemento);
            Sendkeys(txbComplemento, complemento);
        }
        #endregion
        #endregion
    }
}
