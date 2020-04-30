using CampeonatoBrasileiroAPI.Entity;
using CampeonatoBrasileiroAPI.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.Services
{
    public class Service : IService
    {
        private readonly IRepository repository;
        private readonly ILogger<Service> logger;

        public Service(
            IRepository _repository,
            ILogger<Service> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        public IEnumerable<object> PorEstado(string siglaEstado)
        {
            try
            {
                IEnumerable<Campeonato> dados = repository.CarregarDados();

                logger.LogInformation("PorEstado - Dados do arquivo carregados com sucesso");

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

                logger.LogInformation("PorEstado - Dados consolidados  com sucesso");

                return listaPorEstado;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public object PorTime(string nomeTime)
        {
            try
            {
                IEnumerable<Campeonato> dados = repository.CarregarDados();

                logger.LogInformation("PorTime - Dados do arquivo carregados com sucesso");

                string nomePadronizado = Helpers.Util.PadronizarNomeTime(nomeTime).ToUpper();

                logger.LogInformation("PorEstado - Nome padronizado com sucesso");

                var time = dados
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
                    .SingleOrDefault();

                logger.LogInformation("PorEstado - Dados consolidados com sucesso");

                return time;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public object InformacoesComplementares()
        {
            try
            {
                IEnumerable<Campeonato> dados = repository.CarregarDados();

                logger.LogInformation("InformacoesComplementares - Dados do arquivo carregados com sucesso");

                //Retorna a Quantidade de Campeonatos
                var quantidadeCampeonatos = dados
                    .ToList()
                    .GroupBy(x => x.Ano)
                    .Select(g => new
                    {
                        Ano = g.Key
                    })
                    .Count();

                logger.LogInformation("InformacoesComplementares - Quantidade de campeonatos carregada");

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

                logger.LogInformation("InformacoesComplementares - Time de melhor média de gols a favor calculado");

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

                logger.LogInformation("InformacoesComplementares - Time de melhor média de gols contra calculado");

                //Ordena pelo Time com Maior Número de Vitorias no Período
                var timeMaiorNumeroVitorias = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        NumeroVitorias = t.TotalVitorias
                    })
                    .OrderByDescending(x => x.NumeroVitorias)
                    .FirstOrDefault();

                logger.LogInformation("InformacoesComplementares - Time com o maior número de vitórias calculado");

                //Ordena pelo Time com Menor Número de Vitorias no Período
                var timeMenorNumeroVitorias = listaInformacoesComplementares
                    .Select(t => new
                    {
                        Time = t,
                        NumeroVitorias = t.TotalVitorias
                    })
                    .OrderBy(x => x.NumeroVitorias)
                    .FirstOrDefault();

                logger.LogInformation("InformacoesComplementares - Time com o menor número de vitórias calculado");

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

                logger.LogInformation("InformacoesComplementares - Agrupou os dados por ano e time");

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

                logger.LogInformation("InformacoesComplementares - Time de melhor média de vitórias por campeonato calculado");

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

                logger.LogInformation("InformacoesComplementares - Time de menor média de vitórias por campeonato calculado");

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

                logger.LogInformation("InformacoesComplementares - Dados consolidados com sucesso");

                return informacoesComplementares;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
