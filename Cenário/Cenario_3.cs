using NUnit.Framework;
using Send4.Geral;
using Send4.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.Cenário
{
    public class Cenario_3 : BaseTest
    {
        readonly string acao = ConfigurationManager.AppSettings["ACAO"];
        readonly string motivos = ConfigurationManager.AppSettings["MOTIVOS"];
        readonly string comoPodemoResolver = ConfigurationManager.AppSettings["COMOPODEMOSRESOLVER"];
        readonly string novoCep = ConfigurationManager.AppSettings["CEPNOVO"];
        readonly string novoNumero = ConfigurationManager.AppSettings["NUMERONOVOCEP"];
        readonly string novoComplemento = ConfigurationManager.AppSettings["COMPLEMENTONOVOCEP"];
        readonly string mensgaemAvaliacao = ConfigurationManager.AppSettings["MENSAGEMENVIOAVALIACAO"];
        private string nomeProduto = "", quantidade = "3";
        private int typeSelectionProduto = 1, typeSelectionLocalDevolucao = 2;
        private List<string> listaInformacoesEndereco = new List<string>();
        private List<string> listaNomeInformaçõesProdutos = new List<string>();
        private string[] listaNomeLojasFisicas = ConfigurationManager.AppSettings["LOJASFISICAS"].Split(','), listaInformacoesLocalDevolucao;
        readonly private int avalicaoAtendimento = 9;

        [Test]
        public void Caso_de_Teste_1()
        {
            ExtentReport.StartTest(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
            nameScenario = ExtentReport.test.Description;

            Generica.InicioCenarios();

            nomeProduto = Generica.SelecionarProdutos(typeSelectionProduto, quantidade, acao, motivos, comoPodemoResolver,
                                              acao, motivos, comoPodemoResolver);

            listaInformacoesEndereco = Generica.SelecionarLocalDevolucao(typeSelectionLocalDevolucao,
                                        novoCep, novoNumero, novoComplemento);

            Assert.IsTrue(ConfigurationManager.AppSettings["NOMECLIENTE"] == Resume.Nome(), "Validação realizada com sucesso");
            Assert.IsTrue(ConfigurationManager.AppSettings["EMAIL"] == Resume.Email(), "Validação realizada com sucesso");
            Assert.IsTrue(listaInformacoesEndereco[0] == Resume.Endereco(), "Validação realizada com sucesso");
            Assert.IsTrue(ConfigurationManager.AppSettings["TELEFONECLIENTE"] == Resume.Telefone(), "Validação realizada com sucesso");
            Assert.IsTrue(ConfigurationManager.AppSettings["DOCUMENTOCLIENTE"] == Resume.Documento(), "Validação realizada com sucesso");

            listaNomeInformaçõesProdutos = Resume.NomeProdutos();

            for (int i = 0; i <= ((listaNomeInformaçõesProdutos.Count / 5) - 1); i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.IsTrue(ConfigurationManager.AppSettings["NOMEPRODUTOUM"] == listaNomeInformaçõesProdutos[0], "Validação realizada com sucesso");
                        Assert.IsTrue(quantidade == listaNomeInformaçõesProdutos[1], "Validação realizada com sucesso");
                        Assert.IsTrue(motivos == listaNomeInformaçõesProdutos[2], "Validação realizada com sucesso");
                        Assert.IsTrue(acao == listaNomeInformaçõesProdutos[3], "Validação realizada com sucesso");
                        Assert.IsTrue(comoPodemoResolver == RepleceString(listaNomeInformaçõesProdutos[4], "Comentário\r\n", ""), "Validação realizada com sucesso");
                        break;
                    case 1:
                        Assert.IsTrue(ConfigurationManager.AppSettings["NOMEPRODUTODOIS"] == listaNomeInformaçõesProdutos[5], "Validação realizada com sucesso");
                        Assert.IsTrue(motivos == listaNomeInformaçõesProdutos[7], "Validação realizada com sucesso");
                        Assert.IsTrue(acao == listaNomeInformaçõesProdutos[8], "Validação realizada com sucesso");
                        Assert.IsTrue(comoPodemoResolver == RepleceString(listaNomeInformaçõesProdutos[9], "Comentário\r\n", ""), "Validação realizada com sucesso");
                        break;
                }
            }

            listaInformacoesLocalDevolucao = Resume.LocalDevolucao(1);

            switch (typeSelectionLocalDevolucao)
            {
                case 0:
                    Assert.IsTrue(listaNomeLojasFisicas[0] == listaInformacoesLocalDevolucao[0].Substring(0, 6), "Validação realizada com sucesso");
                    Assert.IsTrue(ConfigurationManager.AppSettings["ENDERECOLOJA3"] == listaInformacoesLocalDevolucao[2], "Validação realizada com sucesso");
                    break;

                case 1:
                    Assert.IsTrue(listaNomeLojasFisicas[1] == listaInformacoesLocalDevolucao[0].Substring(0, 6), "Validação realizada com sucesso");
                    Assert.IsTrue(ConfigurationManager.AppSettings["ENDERECOLOJA1"] == listaInformacoesLocalDevolucao[2], "Validação realizada com sucesso");
                    break;

                case 2:
                    Assert.IsTrue(ConfigurationManager.AppSettings["OUTROSMETODOS"] == listaInformacoesLocalDevolucao[0], "Validação realizada com sucesso");
                    break;
            }

            Resume.Salvar();

            Assert.IsTrue(Generica.Avaliacao(avalicaoAtendimento) == mensgaemAvaliacao, "Validação realizada com sucesso");
        }
    }
}
