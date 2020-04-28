using CampeonatoBrasileiroAPI.Dominio.Entity;
using CampeonatoBrasileiroAPI.Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.Dominio.Services
{
    public class Service : IService
    {
        private readonly IRepository repository;

        public Service(IRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<object> PorEstado(string siglaEstado)
        {
            try
            {
                IEnumerable<Campeonato> dados = repository.CarregarDados();

                var listaPorEstado = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Aplica o filtro por estado
                    .Where(x => x.Estado.ToUpper().Equals(siglaEstado.ToUpper()))
                    //Agrupa os dados por nome do clube
                    .GroupBy(x => x.Nome)
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Nome = g.Key,
                        PontuacaoTotal = g.Sum(x => x.Pontos),
                        QuantidadeCampeonatos = g.Count(),
                        TotaJogos = g.Sum(x => x.Jogos),
                        TotalVitorias = g.Sum(x => x.Vitorias),
                        Empates = g.Sum(x => x.Empates),
                        Derrotas = g.Sum(x => x.Derrotas),
                        GolsPro = g.Sum(x => x.GolsAFavor),
                        GolsContra = g.Sum(x => x.GolsContra),
                        SaldoGols = g.Sum(x => x.GolsAFavor - x.GolsContra)
                    })
                    //Ordena os pontos do maior para o menor
                    .OrderByDescending(p => p.PontuacaoTotal)
                    .ToArray();

                return listaPorEstado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<object> PorTime(string nomeTime)
        {
            try
            {
                nomeTime = CampeonatoBrasileiroAPI.Helpers.Util.RemoverAcentos(nomeTime).ToUpper();

                IEnumerable<Campeonato> dados = repository.CarregarDados();

                var listaPorTime = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Aplica o filtro por time
                    .Where(x => x.Nome.ToUpper().Equals(nomeTime.ToUpper()))
                    //Agrupa os dados por nome do clube
                    .GroupBy(x => x.Nome)
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Nome = g.Key,
                        PontuacaoTotal = g.Sum(x => x.Pontos),
                        QuantidadeCampeonatos = g.Count(),
                        TotaJogos = g.Sum(x => x.Jogos),
                        TotalVitorias = g.Sum(x => x.Vitorias),
                        Empates = g.Sum(x => x.Empates),
                        Derrotas = g.Sum(x => x.Derrotas),
                        GolsPro = g.Sum(x => x.GolsAFavor),
                        GolsContra = g.Sum(x => x.GolsContra),
                        SaldoGols = g.Sum(x => x.GolsAFavor - x.GolsContra)
                    })
                    //Ordena os pontos do maior para o menor
                    .OrderByDescending(p => p.PontuacaoTotal)
                    .ToArray();

                return listaPorTime;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object InformacoesComplementares()
        {
            try
            {
                IEnumerable<Campeonato> dados = repository.CarregarDados();

                var quantidadeCampeonatos = dados
                    .ToList()
                    .GroupBy(x => x.Ano)
                    .Select(g => new
                    {
                        Ano = g.Key
                    })
                    .Count();

                var listainformacoesComplementares = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Agrupa os dados por nome do clube
                    .GroupBy(x => x.Nome)
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Nome = g.Key,
                        PontuacaoTotal = g.Sum(x => x.Pontos),
                        QuantidadeCampeonatos = g.Count(),
                        TotaJogos = g.Sum(x => x.Jogos),
                        TotalVitorias = g.Sum(x => x.Vitorias),
                        Empates = g.Sum(x => x.Empates),
                        Derrotas = g.Sum(x => x.Derrotas),
                        GolsPro = g.Sum(x => x.GolsAFavor),
                        GolsContra = g.Sum(x => x.GolsContra),
                        SaldoGols = g.Sum(x => x.GolsAFavor - x.GolsContra)
                    });

                var timeMelhorMediaGolsAFavor = listainformacoesComplementares
                    .OrderByDescending(x => x.GolsPro / quantidadeCampeonatos)
                    .FirstOrDefault();

                var timeMelhorMediaGolsContra = listainformacoesComplementares
                    .OrderBy(x => x.GolsContra / quantidadeCampeonatos)
                    .FirstOrDefault();

                var timeMaiorNumeroVitorias = listainformacoesComplementares
                    .OrderByDescending(x => x.TotalVitorias / quantidadeCampeonatos)
                    .FirstOrDefault();

                var timeMenorNumeroVitorias = listainformacoesComplementares
                    .OrderBy(x => x.TotalVitorias / quantidadeCampeonatos)
                    .FirstOrDefault();

                var listainformacoesComplementaresAno = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Agrupa os dados por campeonato
                    .GroupBy(x => x.Ano)
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Ano = g.Key,
                        Nome = g.Select(x => x.Nome).FirstOrDefault(),
                        PontuacaoTotal = g.Sum(x => x.Pontos),
                        QuantidadeCampeonatos = g.Count(),
                        TotaJogos = g.Sum(x => x.Jogos),
                        TotalVitorias = g.Sum(x => x.Vitorias),
                        Empates = g.Sum(x => x.Empates),
                        Derrotas = g.Sum(x => x.Derrotas),
                        GolsPro = g.Sum(x => x.GolsAFavor),
                        GolsContra = g.Sum(x => x.GolsContra),
                        SaldoGols = g.Sum(x => x.GolsAFavor - x.GolsContra)
                    });


                var timeMelhorMediaVitoriasPorCampeonato = listainformacoesComplementaresAno
                    .OrderByDescending(x => x.TotalVitorias / x.TotaJogos)
                    .FirstOrDefault();

                //Ordena pelo Time com Menor Media de Vitorias Por Campeonato
                var timeMenorMediaVitoriasPorCampeonato = listainformacoesComplementaresAno
                    .OrderBy(x => x.TotalVitorias / x.TotaJogos)
                    .FirstOrDefault();

                //Retorna o objeto calculado
                var informacoesComplementares = new
                {
                    TimeMelhorMediaGolsAFavor = timeMelhorMediaGolsAFavor,
                    TimeMelhorMediaGolsContra = timeMelhorMediaGolsContra,
                    TimeMaiorNumeroVitorias = timeMaiorNumeroVitorias,
                    TimeMenorNumeroVitorias = timeMenorNumeroVitorias,
                    TimeMelhorMediaVitoriasPorCampeonato = timeMelhorMediaVitoriasPorCampeonato,
                    TimeMenorMediaVitoriasPorCampeonato = timeMenorMediaVitoriasPorCampeonato
                };

                return informacoesComplementares;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
