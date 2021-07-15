using System.Collections.Generic;

namespace Assessment.Negocio
{
    public interface IRepositorioDePessoa
    {
        void AlterarExistente(Pessoa pessoa);
        void Excluir(Pessoa pessoa);
    }
}
