using CampeonatoBrasileiroAPI.Entity;
using CampeonatoBrasileiroAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.Services
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
                    .GroupBy(x => x.Estado)
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Estado = g.Key,
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
                IEnumerable<Campeonato> dados = repository.CarregarDados();
                string nomePadronizado = Helpers.Util.PadronizarNomeTime(nomeTime).ToUpper();

                var listaPorTime = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Aplica o filtro por time
                    .Where(x => x.Nome.ToUpper().Equals(nomePadronizado.ToUpper()))
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

                //Retorna a Quantidade de Campeonatos
                var quantidadeCampeonatos = dados
                    .ToList()
                    .GroupBy(x => x.Ano)
                    .Select(g => new
                    {
                        Ano = g.Key
                    })
                    .Count();

                var listaInformacoesComplementares = dados
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
                        TotalJogos = g.Sum(x => x.Jogos),
                        TotalVitorias = g.Sum(x => x.Vitorias),
                        Empates = g.Sum(x => x.Empates),
                        Derrotas = g.Sum(x => x.Derrotas),
                        GolsPro = g.Sum(x => x.GolsAFavor),
                        GolsContra = g.Sum(x => x.GolsContra),
                        SaldoGols = g.Sum(x => x.GolsAFavor - x.GolsContra)
                    });

                //Ordena pelo Time com Melhor Média de Gols A Favor
                //Melhor Média de Gols A Favor
                //Quantidade de Gols A Favor / Quantidade de Campeonatos / Número e Jogos
                var timeMelhorMediaGolsAFavor = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        MediaGolsAFavor = ((float)t.GolsPro / quantidadeCampeonatos)
                    })
                    .OrderByDescending(x => x.MediaGolsAFavor)
                    .FirstOrDefault();

                //Ordena pelo Time com Melhor Média de Gols Contra
                //Melhor Média de Gols Contra
                //Quantidade de Gols Contra / Quantidade de Campeonatos / Número e Jogos
                var timeMelhorMediaGolsContra = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        MediaGolsContra = (float)t.GolsContra / (float)t.TotalJogos
                    })
                    .OrderBy(x => x.MediaGolsContra)
                    .FirstOrDefault();

                //Ordena pelo Time com Maior Número de Vitorias no Período
                var timeMaiorNumeroVitorias = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        NumeroVitorias = t.TotalVitorias
                    })
                    .OrderByDescending(x => x.NumeroVitorias)
                    .FirstOrDefault();

                //Ordena pelo Time com Menor Número de Vitorias no Período
                var timeMenorNumeroVitorias = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        NumeroVitorias = t.TotalVitorias
                    })
                    .OrderBy(x => x.NumeroVitorias)
                    .FirstOrDefault();

                var listainformacoesComplementaresAno = dados
                    //Converte o array para lista, para poder aplicar filtro, agrupar, selecionar e ordenar
                    .ToList()
                    //Agrupa os dados por campeonato
                    .GroupBy(x => new { x.Ano, x.Nome })
                    //Seleciona os dados, somando e contando os valores
                    .Select(g => new
                    {
                        Ano = g.Key.Ano,
                        Nome = g.Key.Nome,
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

                //Ordena pelo Time com Maior Media de Vitorias Por Campeonato
                //Maior Media de Vitorias Por Campeonato
                //Time com maior valor do Total de Vitorias no Campeonato / Total de Jogos no Campeonato
                var timeMelhorMediaVitoriasPorCampeonato = listainformacoesComplementaresAno
                    .Select(t => new
                    {
                        Time = t,
                        MediaVitoriasPorCampeonato = (float)t.TotalVitorias / (float)t.TotaJogos
                    })
                    .OrderByDescending(x => x.MediaVitoriasPorCampeonato)
                    .FirstOrDefault();

                //Ordena pelo Time com Menor Media de Vitorias Por Campeonato
                //Menor Media de Vitorias Por Campeonato
                //Time com menor valor do Total de Vitorias no Campeonato / Total de Jogos no Campeonato
                var timeMenorMediaVitoriasPorCampeonato = listainformacoesComplementaresAno
                    .Select(t => new
                    {
                        Time = t,
                        MediaVitoriasPorCampeonato = (float)t.TotalVitorias / (float)t.TotaJogos
                    })
                    .OrderBy(x => x.MediaVitoriasPorCampeonato)
                    .FirstOrDefault();

                //Retorna o objeto calculado
                var informacoesComplementares = new
                {
                    MelhorMediaGolsAFavor = timeMelhorMediaGolsAFavor,
                    MelhorMediaGolsContra = timeMelhorMediaGolsContra,
                    MaiorNumeroVitorias = timeMaiorNumeroVitorias,
                    MenorNumeroVitorias = timeMenorNumeroVitorias,
                    MelhorMediaVitoriasPorCampeonato = timeMelhorMediaVitoriasPorCampeonato,
                    MenorMediaVitoriasPorCampeonato = timeMenorMediaVitoriasPorCampeonato
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
