using System;

namespace Assessment.Negocio
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataDeNascimento { get; set; }

        public string NomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        public Pessoa(string nome, string sobrenome, DateTime datanascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeNascimento = datanascimento;
        }

    }
}
