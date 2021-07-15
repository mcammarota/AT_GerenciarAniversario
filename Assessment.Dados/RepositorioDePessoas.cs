using System;
using System.Collections.Generic;
using System.Linq;
using Assessment.Negocio;

namespace Assessment.Dados
{
    public abstract class RepositorioDePessoas
    {
        public void Salvar(Pessoa pessoa)
        {
            if (FuncionarioJaEstaCadastrado(pessoa))
            {
                AlterarExistente(pessoa);
            }
            else
            {
                CriarNovo(pessoa);
            }
        }

        private bool FuncionarioJaEstaCadastrado(Pessoa pessoa)
        {
            int id = pessoa.Id;

            var pessoaEncontrada = BuscarPessoaPelo(id);

            if (pessoaEncontrada != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected abstract void CriarNovo(Pessoa pessoa);
        protected abstract void AlterarExistente(Pessoa pessoa);

        public abstract void Excluir(Pessoa pessoa);
        public abstract IEnumerable<Pessoa> ObterTodasAsPessoas();
        public abstract IEnumerable<Pessoa> ObterTodasAsPessoas(string nome);
        public abstract Pessoa BuscarPessoaPelo(int id);
        public abstract IEnumerable<Pessoa> NiverDataAtual(DateTime datanascimento);

    }
}
