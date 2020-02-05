using OpenQA.Selenium;
using Send4.Geral;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.PageObjects
{
    public class Resume : BaseTest
    {
        private static string[] stringSeparators = new string[] { "\r\n" };
        private static IReadOnlyList<IWebElement> listNomeEmailTelefoneDocumento;

        #region Elements
        #region Components
        private static By lblNomeEmailTelefoneDocumento = By.ClassName("customer");
        private static By lblEndereco = By.ClassName("address");
        private static By lblInformacoesProduto = By.ClassName("is-title");
        private static By lblComentarios = By.ClassName("product-item-comment is-title");
        private static By lblLocalDevolucao = By.ClassName("shipping-informations");
        private static By lblLocalDevolucaoAgenciaDoCorreios = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div/div[3]/div/div/div[2]/h4");
        private static By btnVoltar = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div/div[6]/button[1]");
        private static By btnSalvar = By.XPath("//*[@id='root']/section/main/div[2]/div[1]/div/div[6]/button[2]");
        #endregion
        #endregion

        #region Methods
        #region Text
        public static string Nome()
        {
            listNomeEmailTelefoneDocumento = ListElement(lblNomeEmailTelefoneDocumento);
            string[] listaNomeEmailTelefoneDocumento = listNomeEmailTelefoneDocumento[0].Text.Split(stringSeparators, StringSplitOptions.None);
            return listaNomeEmailTelefoneDocumento[1];
        }

        public static string Email()
        {
            listNomeEmailTelefoneDocumento = ListElement(lblNomeEmailTelefoneDocumento);
            string[] listaNomeEmailTelefoneDocumento;
            listaNomeEmailTelefoneDocumento = listNomeEmailTelefoneDocumento[0].Text.Split(stringSeparators, StringSplitOptions.None);
            return listaNomeEmailTelefoneDocumento[3];
        }

        public static string Telefone()
        {
            listNomeEmailTelefoneDocumento = ListElement(lblNomeEmailTelefoneDocumento);
            string[] listaNomeEmailTelefoneDocumento = listNomeEmailTelefoneDocumento[0].Text.Split(stringSeparators, StringSplitOptions.None);
            return listaNomeEmailTelefoneDocumento[5];
        }

        public static string Documento()
        {
            listNomeEmailTelefoneDocumento = ListElement(lblNomeEmailTelefoneDocumento);
            string[] listaNomeEmailTelefoneDocumento = listNomeEmailTelefoneDocumento[0].Text.Split(stringSeparators, StringSplitOptions.None);
            return listaNomeEmailTelefoneDocumento[7];
        }

        public static string Endereco()
        {
            return RepleceString(Text(lblEndereco), "Endereço\r\n", "");
        }

        public static List<string> NomeProdutos()
        {
            IReadOnlyList<IWebElement> listInformacoesProduto = ListElement(lblInformacoesProduto);
            List<string> listaNomeProdutos = new List<string>();
            for(int i = 0; i <= (listInformacoesProduto.Count - 1); i++)
            {
                listaNomeProdutos.Add(listInformacoesProduto[i].Text);
            }
            return listaNomeProdutos;
        }

        public static string[] LocalDevolucao(int type)
        {
            if(type == 0)
            {
                string[] listaInformacoesLoja = Text(lblLocalDevolucaoAgenciaDoCorreios).Split(stringSeparators, StringSplitOptions.None);
                return listaInformacoesLoja;
            }
            else if(type == 1)
            {
                string[] listaInformacoesLoja = Text(lblLocalDevolucao).Split(stringSeparators, StringSplitOptions.None);
                return listaInformacoesLoja;
            }
            else
            {
                string[] listaInformacoesLoja = Text(lblLocalDevolucaoAgenciaDoCorreios).Split(stringSeparators, StringSplitOptions.None);
                return listaInformacoesLoja;
            }
        }
        #endregion

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
        #endregion
    }
}
