using Assessment.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assessment.Dados
{
    public class RepositorioDePessoasEmArquivo : RepositorioDePessoas
    {
        public override IEnumerable<Pessoa> ObterTodasAsPessoas()
        {
            string nomeDoArquivo = ObterNomeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            string[] pessoas = resultado.Split(';');

            List<Pessoa> pessoasList = new List<Pessoa>();

            for (int i = 0; i < pessoas.Length - 1; i++)
            {
                string[] dadosDaPessoa = pessoas[i].Split(',');

                int id = Convert.ToInt32(dadosDaPessoa[0]);
                string nome = dadosDaPessoa[1];
                string sobrenome = dadosDaPessoa[2];
                DateTime dataDeNascimento = Convert.ToDateTime(dadosDaPessoa[3]);

                Pessoa pessoa = new Pessoa(nome, sobrenome, dataDeNascimento);
                pessoasList.Add(pessoa);

            }

            return pessoasList;

        }
        private static string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPasta = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDasPessoas.txt";

            return localDaPasta + nomeDoArquivo;
        }

        protected override void CriarNovo(Pessoa pessoa)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string formato = $"{pessoa.Id}, {pessoa.Nome}, {pessoa.Sobrenome}, {pessoa.DataDeNascimento};";

            File.AppendAllText(nomeDoArquivo, formato);
        }

        public override void Excluir(Pessoa pessoa)
        {
            string nomeDoArquivo = ObterNomeArquivo();
            List<string> pessoasList = File.ReadAllLines(nomeDoArquivo).ToList();
            pessoasList.RemoveAt(0);
            File.WriteAllLines(nomeDoArquivo, pessoasList.ToArray());
        }

        protected override void AlterarExistente(Pessoa pessoa)
        {
            //throw new NotImplementedException();
            string nomeDoArquivo = ObterNomeArquivo();
            string resultado = File.ReadAllText(nomeDoArquivo);
            string[] pessoas = resultado.Split(';');

            List<string> pessoasList = new List<string>();
            for (int i = 0; i < pessoasList.Count - 1; i++)
            {
                string[] dadosDaPessoa = pessoas[i].Split(',');
                string[] newdadosDaPessoa = pessoas[i].Split(',');
                pessoasList.Remove(dadosDaPessoa[3]);
                pessoasList.Add(newdadosDaPessoa[3]);

            }
        }

        public override IEnumerable<Pessoa> ObterTodasAsPessoas(string nome)
        {
            return from x in ObterTodasAsPessoas()
                    where x.NomeCompleto().Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.NomeCompleto()
                    select x;
        }

        public override Pessoa BuscarPessoaPelo(int id)
        {
            return (from x in ObterTodasAsPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        public override IEnumerable<Pessoa> NiverDataAtual(DateTime datanascimento)
        {
            return from x in ObterTodasAsPessoas()
                    where x.DataDeNascimento.Day.Equals(datanascimento.Day)
                    && x.DataDeNascimento.Month.Equals(datanascimento.Month)
                    select x;
        }

    }
}
