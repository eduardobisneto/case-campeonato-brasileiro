using System.Collections.Generic;

namespace CampeonatoBrasileiroAPI.Services
{
    public interface IService
    {
        IEnumerable<object> PorTime(string nomeTime);

        IEnumerable<object> PorEstado(string siglaEstado);

        object InformacoesComplementares();
    }
}
