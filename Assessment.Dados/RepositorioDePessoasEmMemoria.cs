using Assessment.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assessment.Dados
{
    public class RepositorioDePessoasEmMemoria : RepositorioDePessoas
    {
        private static List<Pessoa> PessoasCadastradas = new List<Pessoa>();

        protected override void CriarNovo(Pessoa pessoa)
        {
            PessoasCadastradas.Add(pessoa);
        }

        protected override void AlterarExistente(Pessoa pessoa)
        {
            PessoasCadastradas.Remove(pessoa);
            PessoasCadastradas.Add(pessoa);
        }

        public override IEnumerable<Pessoa> ObterTodasAsPessoas()
        {
            return PessoasCadastradas;
        }

        public override IEnumerable<Pessoa> ObterTodasAsPessoas(string nome)
        {
            //dica: nas consultas retornas ienumerable
            return PessoasCadastradas
                   .Where(pessoa => pessoa.NomeCompleto().Contains(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public override Pessoa BuscarPessoaPelo(int id)
        {
            return PessoasCadastradas.Find(pessoa => pessoa.Id == id);
        }

        public override void Excluir(Pessoa pessoa)
        {
            PessoasCadastradas.Remove(pessoa);
        }
        public override IEnumerable<Pessoa> NiverDataAtual(DateTime datanascimento)
        {
            return PessoasCadastradas.Where(pessoa => pessoa.DataDeNascimento.Day.Equals(DateTime.Now.Date.Day)
            && pessoa.DataDeNascimento.Month.Equals(DateTime.Now.Date.Month)).ToList();
        }
    }
}
