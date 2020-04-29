using System.Collections.Generic;

namespace CampeonatoBrasileiroAPI.Services
{
    public interface IService
    {
        object PorTime(string nomeTime);

        IEnumerable<object> PorEstado(string siglaEstado);

        object InformacoesComplementares();
    }
}
