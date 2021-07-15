using Assessment.Dados;
using Assessment.Negocio;
using Assessment_Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        private static void EscreverNaTela(string texto)
        {
            Console.WriteLine(texto);
        }

        public static void MenuPrincipal()
        {
            
            EscreverNaTela("Gerenciamento de Aniversários");
            EscreverNaTela("Selecione uma das opções abaixo:");
            EscreverNaTela("1 - Adicionar nova pessoa");
            EscreverNaTela("2 - Consultar pessoa");
            EscreverNaTela("3 - Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": AdicionarPessoa(); break;
                case "2": ConsultarPessoa(); break;;
                case "3": Sair(); break;
                default: break;
            }
        }


        private static void ConsultarPessoa()
        {
            Console.Clear();
            EscreverNaTela("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string nome = Console.ReadLine();

            var pessoasEncontradas = BancoDeDados.ObterTodasAsPessoas(nome);

            EscreverNaTela("Selecione uma das opções abaixo para visualizar os dados");

            int qtdPessoasEncontradas = pessoasEncontradas.Count();

            if (qtdPessoasEncontradas > 0)
            {
                foreach (var pessoa in pessoasEncontradas)
                {
                    EscreverNaTela(pessoa.Id + " - " + pessoa.NomeCompleto());
                }
            }
            else
            {
                EscreverNaTela("Nenhuma pessoa encontrada para o nome: " + nome);
                MenuPrincipal();
            }

            int id = Convert.ToInt32(Console.ReadLine());

            var pessoaSelecionada = pessoasEncontradas.FirstOrDefault(pessoa => pessoa.Id == id);

            EscreverNaTela("Nome completo: " + pessoaSelecionada.NomeCompleto());
            EscreverNaTela("Data de aniversário: " + pessoaSelecionada.DataDeNascimento);

            if (DateTime.Now.DayOfYear < pessoaSelecionada.DataDeNascimento.DayOfYear)
            {
                int diasRestantes = pessoaSelecionada.DataDeNascimento.DayOfYear - DateTime.Now.DayOfYear;
                Console.WriteLine($"Faltam {diasRestantes} dias para esse aniversário");
            }
            else if (DateTime.Now.DayOfYear > pessoaSelecionada.DataDeNascimento.DayOfYear)
            {
                int diasRestantes = pessoaSelecionada.DataDeNascimento.DayOfYear - DateTime.Now.DayOfYear + 365;
                Console.WriteLine($"Faltam {diasRestantes} dias para esse aniversário");
            }

            EscreverNaTela("Selecione uma das opções abaixo:");
            EscreverNaTela("1 - Voltar ao Menu Principal");
            EscreverNaTela("2 - Alterar data de aniversário");
            EscreverNaTela("3 - Excluir pessoa");
            EscreverNaTela("4 - Sair");

            string operacao = Console.ReadLine();

            if (operacao == "1")
            {
                MenuPrincipal();
            }
            else if (operacao == "2")
            {
                EscreverNaTela("Entre com a nova data de aniversário:");
                DateTime dataNova = Convert.ToDateTime(Console.ReadLine());
                pessoaSelecionada.DataDeNascimento = dataNova;
                BancoDeDados.Salvar(pessoaSelecionada);
                MenuPrincipal();
            }
            else if (operacao == "3")
            {
                BancoDeDados.Excluir(pessoaSelecionada);
                MenuPrincipal();
            }
            else if (operacao == "4")
            {
                Sair();
            }

        }

        private static void Sair()
        {
            EscreverNaTela("Operação encerrada. Tchau!");
            Console.ReadKey();
        }

        private static void AdicionarPessoa()
        {
            Console.Clear();
            EscreverNaTela("Entre com o nome:");
            string nome = Console.ReadLine();
            EscreverNaTela("Entre com o sobrenome:");
            string sobrenome = Console.ReadLine();
            EscreverNaTela("Entre com a data de nascimento no formato dd/mm/aaaa:");
            DateTime datanascimento = Convert.ToDateTime(Console.ReadLine());

            PessoaApp.CadastrarPessoa(nome, sobrenome, datanascimento);

            EscreverNaTela("Cadastrado com sucesso!");

            MenuPrincipal();

        }

        private static void AniversarioDataAtual(DateTime datanascimento)
        {
            var pessoas = BancoDeDados.NiverDataAtual(datanascimento);
            EscreverNaTela("Aniversariantes do dia:");
            foreach (var pessoa in pessoas)
            {
                EscreverNaTela(pessoa.NomeCompleto() + " - " + pessoa.DataDeNascimento);
            }
        }

        public static RepositorioDePessoas BancoDeDados
        {
            get
            {
                _bancoDeDados ??= new RepositorioDePessoasEmArquivo();
                BancoDeDados = _bancoDeDados;
                return _bancoDeDados;
            }
            set
            {
                _bancoDeDados = value;
            }
        }

        private static RepositorioDePessoas _bancoDeDados;


    }
}
