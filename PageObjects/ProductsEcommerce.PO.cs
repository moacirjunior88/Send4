using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace Send4.PageObjects
{
    public class ProductsEcommerce : BaseTest
    {
        private static SelectElement selectElement;
        #region Elements
        #region Components
        private static By lblNomeProduto = By.ClassName("break-text");
        private static By cxbTodos = By.Id("product-ecommerce");
        private static By cxbPrimeiro = By.Id("product-ecommerce-bd63d56a404f45547320ebcb3e86421e");
        private static By cxbSegundo = By.Id("product-ecommerce-09392cecd4077744e4b323756c62f5b3");
        private static By cbxQuantidadePrimeiro = By.Id("product-ecommerce-qty-bd63d56a404f45547320ebcb3e86421e");
        private static By cbxAcaoPrimeiro = By.Id("product-ecommerce-action-bd63d56a404f45547320ebcb3e86421e");
        private static By cbxAcaoSegunda = By.Id("product-ecommerce-action-09392cecd4077744e4b323756c62f5b3");
        private static By cbxMotivoPrimeiro = By.Id("product-ecommerce-reason-bd63d56a404f45547320ebcb3e86421e");
        private static By cbxMotivoSegunda = By.Id("product-ecommerce-reason-09392cecd4077744e4b323756c62f5b3");
        private static By txaComoPodemosResolverPrimeiro = By.Id("product-ecommerce-comment-bd63d56a404f45547320ebcb3e86421e");
        private static By txaComoPodemosResolverSegunda = By.Id("product-ecommerce-comment-09392cecd4077744e4b323756c62f5b3");
        private static By btnContinuar = By.ClassName("buttons");
        #endregion
        #endregion

        #region Methods
        #region Checkbox
        public static void Todos()
        {
            ClicksCheckBoxAngular(cxbTodos);
        }

        public static void PrimeiroProduto()
        {
            ClicksCheckBoxAngular(cxbPrimeiro);
        }

        public static void SegundoProduto()
        {
            ClicksCheckBoxAngular(cxbSegundo);
        }
        #endregion

        #region Combobox
        public static void QuantidadePrimeiro(string quantidade)
        {
            selectElement = new SelectElement(driver.FindElement(cbxQuantidadePrimeiro));
            selectElement.SelectByText(quantidade);
        }

        public static void AcaoPrimeiro(string acao)
        {
            selectElement = new SelectElement(driver.FindElement(cbxAcaoPrimeiro));
            selectElement.SelectByText(acao);
        }

        public static void AcaoSegunda(string acao)
        {
            selectElement = new SelectElement(driver.FindElement(cbxAcaoSegunda));
            selectElement.SelectByText(acao);
        }

        public static void MotivosPrimeiro(string motivos)
        {
            selectElement = new SelectElement(driver.FindElement(cbxMotivoPrimeiro));
            selectElement.SelectByText(motivos);
        }

        public static void MotivosSegunda(string motivos)
        {
            selectElement = new SelectElement(driver.FindElement(cbxMotivoSegunda));
            selectElement.SelectByText(motivos);
        }
        #endregion

        #region SendKeys
        public static void ComoPodemosResolverPrimeiro(string comoPodemosResolverPrimeiro)
        {
            Sendkeys(txaComoPodemosResolverPrimeiro, comoPodemosResolverPrimeiro);
        }

        public static void ComoPodemosResolverSegunda(string comoPodemosResolverSegunda)
        {
            Sendkeys(txaComoPodemosResolverSegunda, comoPodemosResolverSegunda);
        }
        #endregion

        #region Clicks
        public static void Continuar()
        {
            ScroolElement(btnContinuar);
            Clicks(btnContinuar);
        }
        #endregion

        #region Text
        public static string NomePrimeiroProduto()
        {
            IReadOnlyList<IWebElement> listaProduto = ListElement(lblNomeProduto);

            return listaProduto[0].Text;
        }

        public static string NomeSegundoProduto()
        {
            IReadOnlyList<IWebElement> listaProduto = ListElement(lblNomeProduto);

            return listaProduto[1].Text;
        }
        #endregion
        #endregion
    }
}
