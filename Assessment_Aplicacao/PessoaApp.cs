using System;
using Assessment.Negocio;
using Assessment.Dados;

namespace Assessment_Aplicacao
{
    public class PessoaApp
    {
        public static CadastrarPessoaResult CadastrarPessoa(string nome, string sobrenome, DateTime datanascimento)
        {
            var pessoa = new Pessoa(nome, sobrenome, datanascimento);

            try
            {
                BancoDeDados.Salvar(pessoa);
                return new CadastrarPessoaResult { CadastradoComSucesso = true };
            }
            catch (Exception)
            {
                return new CadastrarPessoaResult { CadastradoComSucesso = false };
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

    public class CadastrarPessoaResult
    {
        public bool CadastradoComSucesso { get; set; }
    }

}
