using CampeonatoBrasileiroAPI.Entity;
using System.Collections.Generic;

namespace CampeonatoBrasileiroAPI.Repository
{
    public interface IRepository
    {
        IEnumerable<Campeonato> CarregarDados();
    }
}
