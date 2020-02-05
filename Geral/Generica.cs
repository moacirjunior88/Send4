using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Send4.PageObjects;

namespace Send4.Geral
{
    public class Generica : BaseTest
    {
        private static readonly string pedido = ConfigurationManager.AppSettings["PEDIDO"];
        private static readonly string email = ConfigurationManager.AppSettings["EMAIL"];
        private static readonly string comentario1 = ConfigurationManager.AppSettings["COMENTARIO1"];
        private static readonly string comentario2 = ConfigurationManager.AppSettings["COMENTARIO2"];
        private static readonly string comentario3 = ConfigurationManager.AppSettings["COMENTARIO3"];
        public static void InicioCenarios()
        {
            Home.Comecar();
            SelectSource.LojaVirtual();
            Order.Pedido(pedido);
            Order.ConfirmeEmail(email);
            Order.BuscarContinuar();
            Wait(5000);
        }

        public static string SelecionarProdutos(int typeSelection, 
            string quantidade, string acao, string motivos, string comoPodemosResolver,
            string acao2, string motivos2, string comoPodemosResolver2)
        {
            string nomeProduto = "";

            switch(typeSelection)
            {
                case 0:
                    ProductsEcommerce.Todos();

                    nomeProduto = ProductsEcommerce.NomePrimeiroProduto();
                    ProductsEcommerce.QuantidadePrimeiro(quantidade);
                    ProductsEcommerce.AcaoPrimeiro(acao);
                    ProductsEcommerce.MotivosPrimeiro(motivos);
                    ProductsEcommerce.ComoPodemosResolverPrimeiro(comoPodemosResolver);

                    nomeProduto = nomeProduto + "," + ProductsEcommerce.NomeSegundoProduto();
                    ProductsEcommerce.AcaoSegunda(acao2);
                    ProductsEcommerce.MotivosSegunda(motivos2);
                    ProductsEcommerce.ComoPodemosResolverSegunda(comoPodemosResolver2);

                    break;

                case 1:
                    ProductsEcommerce.PrimeiroProduto();

                    nomeProduto = ProductsEcommerce.NomePrimeiroProduto();
                    ProductsEcommerce.QuantidadePrimeiro(quantidade);
                    ProductsEcommerce.AcaoPrimeiro(acao);
                    ProductsEcommerce.MotivosPrimeiro(motivos);
                    ProductsEcommerce.ComoPodemosResolverPrimeiro(comoPodemosResolver);
                    break;

                case 2:
                    ProductsEcommerce.SegundoProduto();

                    nomeProduto = ProductsEcommerce.NomeSegundoProduto();
                    ProductsEcommerce.AcaoSegunda(acao2);
                    ProductsEcommerce.MotivosSegunda(motivos2);
                    ProductsEcommerce.ComoPodemosResolverSegunda(comoPodemosResolver2);
                    break;

                default:
                    ProductsEcommerce.Todos();

                    nomeProduto = ProductsEcommerce.NomePrimeiroProduto();
                    ProductsEcommerce.QuantidadePrimeiro(quantidade);
                    ProductsEcommerce.AcaoPrimeiro(acao);
                    ProductsEcommerce.MotivosPrimeiro(motivos);
                    ProductsEcommerce.ComoPodemosResolverPrimeiro(comoPodemosResolver);

                    nomeProduto = nomeProduto + "," + ProductsEcommerce.NomeSegundoProduto();
                    ProductsEcommerce.AcaoSegunda(acao2);
                    ProductsEcommerce.MotivosSegunda(motivos2);
                    ProductsEcommerce.ComoPodemosResolverSegunda(comoPodemosResolver2);
                    break;
            }

            Wait(3000);

            ProductsEcommerce.Continuar();

            Wait(10000);

            return nomeProduto;
        }

        public static List<string> SelecionarLocalDevolucao(int typeSelection, 
            string novoCep, string novoNumero, string NovoComplemento)
        {
            List<string> listValoresValidacao = new List<string>();
            if(typeSelection <= 2) listValoresValidacao.Add(RepleceString(Shipping.EndereçoPrincipal(), "Opções exibidas baseadas no endereço:\r\n", ""));

            switch (typeSelection)
            {
                case 0:
                    listValoresValidacao.Add(RepleceString(Shipping.NomeLoja3(), "0.0 km", ""));
                    listValoresValidacao.Add(Shipping.EndereçoLoja3());
                    Shipping.Loja3();
                    break;

                case 1:
                    listValoresValidacao.Add(Shipping.NomeLoja1());
                    listValoresValidacao.Add(Shipping.EndereçoLoja1());
                    Shipping.Loja1();
                    break;

                case 2:
                    listValoresValidacao.Add(Shipping.NomeOutrosMetodos());
                    Shipping.OutrosMetodos();
                    break;

                case 3:
                    Shipping.AlterarEndereço();
                    Shipping.Cep(novoCep);
                    Shipping.Numero(novoNumero);
                    Shipping.Complemento(NovoComplemento);

                    Shipping.Salvar();

                    listValoresValidacao.Add(RepleceString(Shipping.EndereçoPrincipal(), "Opções exibidas baseadas no endereço:\r\n", ""));

                    listValoresValidacao.Add(Shipping.NomeOutrosMetodos());
                    Shipping.OutrosMetodos();
                    break;

                default:
                    listValoresValidacao.Add(Shipping.NomeOutrosMetodos());
                    Shipping.OutrosMetodos();
                    break;
            }

            Shipping.Continuar();

            return listValoresValidacao;
        }

        public static string Avaliacao(int avaliacao)
        {
            Exchange.DeZeroADez(avaliacao);

            if (avaliacao <= 6) Exchange.Comentario(comentario1);
            else if((avaliacao > 6) && (avaliacao <= 8)) Exchange.Comentario(comentario2);
            else if ((avaliacao > 8) && (avaliacao <= 10)) Exchange.Comentario(comentario3);

            Exchange.EnviarAvaliacao();

            Wait(3000);

            return Exchange.MensagemDeEnvioAvalicao();
        }
    }
}
