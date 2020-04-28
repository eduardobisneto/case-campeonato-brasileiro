using CampeonatoBrasileiroAPI.Dominio.Entity;
using System.Collections.Generic;

namespace CampeonatoBrasileiroAPI.Dominio.Repository
{
    public interface IRepository
    {
        IEnumerable<Campeonato> CarregarDados(string caminhoArquivo);
    }
}
