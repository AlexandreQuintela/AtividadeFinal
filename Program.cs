using System;
using System.Text;

namespace AtividadeFinal
{
    class Program
    {
        
        static void Linha(int col, string decor)
        {
            for (int i = 0; i < col; i++)
            {
                Console.Write(decor);
            }
            Console.Write("\n");
        }


        static void Titulo(int tamanhoColunas, string decor, string valor)
        {
            Linha(tamanhoColunas, decor);
            int espacos = (tamanhoColunas - 2 - valor.Length) / 2;

            StringBuilder branco = new StringBuilder();
            for (int i = 0; i < espacos; i++)
            {
                branco.Append(" ");
            }
            Console.Write("{0}{1}{2}{3}{4}\n", decor, branco, valor, branco, decor);
            Linha(tamanhoColunas, decor);
        }


        private static void Imprimir(Produto[] produtos, int indice)
        {
            Console.WriteLine("{0,-9}{1,25}{2,12}{3,12}{4,12}\n", "Código", "Descrição", "Preço", "Custo", "Lucro");
            for (int i = 0; i < indice; i++)
            {
                Console.WriteLine("{0,-9} {1,25} {2,12:C2} {3,12:C2} {4,12:C2}", produtos[i].Codigo, produtos[i].Descricao, produtos[i].Preco, produtos[i].Custo, (produtos[i].Preco - produtos[i].Custo));                
            }
        }


        private static void BuscaCodigo(Produto[] produto, int indice, int codigo )
        {
            bool achou = false;
            for (int i = 0; i < indice;  i++)
            {
                if (produto[i].Codigo == codigo)
                {
                    Console.WriteLine("\n\nVocê deseja conceder um (D)esconto ou fazaer um (A)créscimo de preço ao produto {0}, o preço atual: {1:C2} ", produto[i].Descricao, produto[i].Preco);
                    char opcao = ' ';
                    do
                    {
                        Console.Write("\nDigite (D)esconto ou (A)créscimo?");
                        try
                        {
                            opcao = char.ToUpper(Convert.ToChar(Console.ReadLine()));
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Escolha uma opção...");
                        }
                        
                        switch (opcao)
                        {
                            case 'A':
                                Console.Write("Indique o percentual de acréscimo que deseja aplicar? ");
                                double acrescimo = Convert.ToDouble(Console.ReadLine());
                                produto[i].Preco *= (1 + acrescimo/100);
                                Console.WriteLine("Novo preço do produto {0} é {1:C2}", produto[i].Descricao, produto[i].Preco);
                                break;

                            case 'D':
                                Console.Write("Indique o percentual de desconto que deseja aplicar? ");
                                double desconto = Convert.ToDouble(Console.ReadLine());
                                produto[i].Preco *= (1 - desconto / 100);
                                Console.WriteLine("Novo preço do produto {0} é {1:C2}", produto[i].Descricao, produto[i].Preco);
                                break;

                            default:
                                Console.WriteLine("Opção Inválida!!! :(");
                                break;
                        }

                        achou = true;
                    } while (opcao != 'A' && opcao != 'D');
                }
            }

            if (!achou)
            {
                Linha(80, "*");
                Console.WriteLine("Ainda não temos esse produto, ou código inválido.");
                Linha(80, "*");
            }
        }


        private static Produto CadastrarProduto()
        {
            Console.WriteLine("Digite os dados dos Produto:");
            Produto objProduto = new Produto();
            Console.Write("Código: ");
            objProduto.Codigo = Convert.ToInt32(Console.ReadLine());          
            Console.Write("Descrição: ");
            objProduto.Descricao = Console.ReadLine();
            Console.Write("Preco: ");
            objProduto.Preco = Convert.ToDouble(Console.ReadLine());
            Console.Write("Custo: ");
            objProduto.Custo = Convert.ToDouble(Console.ReadLine());
            return objProduto;
        }


        private static double PrecoMedio(Produto[] produto, int indice)
        {
            double soma = 0;
            for (int i = 0; i < indice; i++)
            {
                soma += produto[i].Preco;
            }
            return soma / indice;
        }


        static void Main(string[] args)
        {
            Produto[] produto = new Produto[100];
            int controle = 0;
            bool finaliza = false;
            while (!finaliza)
            {
                Titulo(80, "*", "Menu Principal");
                Console.WriteLine();
                Console.WriteLine("A - Cadastrar produto");
                Console.WriteLine("B - Atualizar o preço de um produto");
                Console.WriteLine("C - Imprimir o preço médio dos produtos");
                Console.WriteLine("D - Imprimir listagem de {0} produto(s)", controle);
                Console.WriteLine("S - Sair");
                Console.WriteLine();
                char opcao = ' ';
                do
                {
                    Linha(80, "*");
                    Console.Write("Escolha uma das opções acima digite (A, B, C, D OU S): ");
                    try
                    {
                        opcao = char.ToUpper(Convert.ToChar(Console.ReadLine()));

                    }
                    catch (Exception)
                    {
                        Console.Write("Escolha uma das opções acima.");
                    }
                    switch (opcao)
                    {
                        case 'A':
                            Console.Clear();
                            Titulo(80, "*", "Cadastrar produto");
                            if (controle < produto.Length)
                            {
                                produto[controle] = CadastrarProduto();
                                controle++;
                            }
                            else
                            {
                                Console.WriteLine("Não podemos cadastrar novos produtos Aperte qualquer tecla para retornar.");
                                Console.ReadKey();
                            }
                            Console.Clear();
                            break;
                     
                        case 'B':
                            Console.Clear();
                            Titulo(80, "*", "Atualizar o preço de um produto ");
                            Console.Write("Qual código do produto que deseja alterar o preço: ");
                            try
                            {
                                int codigo = Convert.ToInt32(Console.ReadLine());
                                BuscaCodigo(produto, controle, codigo);
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("ATENÇÃO: Digite somente números inteiros para o código");
                            }

                            Console.WriteLine("Aperte qualquer tecla para retornar ao Menu Principal");
                            Console.ReadKey();
                            Console.Clear();    
                            break;

                        case 'C':
                            Console.Clear();
                            Titulo(80, "*", "Preço médio dos produtos ");
                            Console.WriteLine("Preço médio dos produtos {0:C2}", PrecoMedio(produto, controle));
                            Linha(80, "*");
                            Console.WriteLine("Aperte qualquer tecla para retornar ao Menu Principal");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 'D':
                            Console.Clear();
                            Titulo(80, "*", "Imprimir listagem de produtos ");
                            Imprimir(produto, controle);
                            Console.WriteLine("Aperte qualquer tecla para retornar ao Menu Principal");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 'S':
                            Console.Clear();
                            Console.WriteLine("Obrigado por usar nosso sistema!");
                            Console.ReadKey();
                            finaliza = true;
                            break;

                        default:
                            Console.WriteLine("Opção Inválida!!! :(");
                            break;
                    }

                } while (opcao != 'A' && opcao != 'B' && opcao != 'C'
                        && opcao != 'D' && opcao != 'S');
            }
            
        }
    }
}
