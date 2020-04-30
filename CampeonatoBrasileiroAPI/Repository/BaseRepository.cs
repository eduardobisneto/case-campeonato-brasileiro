using CampeonatoBrasileiroAPI.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace CampeonatoBrasileiroAPI.Repository
{
    public class BaseRepository : IRepository
    {
        public BaseRepository()
        {
        }

        /// <summary>
        /// Retorna as dados lidos e convertidos do arquivo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Campeonato> CarregarDados()
        {
            const short QUANTIDADE_CAMPEONATOS = 100;
            const short QUANTIDADE_CARACTERES_ANO = 4;
            Campeonato[] campeonatos = new Campeonato[QUANTIDADE_CAMPEONATOS];

            //Stream de leitura de arquivo, lendo linha a linha
            using (StreamReader streamReader = new StreamReader(@"repository\data.txt"))
            {
                short ano = default(short);
                short indice = default(short);

                //Enquanto n�o for o final do stream
                while (!streamReader.EndOfStream)
                {
                    //L� a linha atual do arquivo
                    char[] delimitador = new char[] { '\t' };
                    string[] linhaAtual = streamReader
                        .ReadLine()
                        .Split(delimitador, StringSplitOptions.RemoveEmptyEntries);

                    //Interpreta que tipo de linha est� lidando, se � uma linha de ano, de cabe�alho, de tra�os ou uma linha normal
                    //Linha com o ano da tabela de pontua��o
                    if (linhaAtual.Length > 0)
                    {
                        if (linhaAtual[0].Length == QUANTIDADE_CARACTERES_ANO)
                        {
                            ano = Convert.ToInt16(linhaAtual[0].Trim());
                        }

                        if (linhaAtual[0].Length == 1 || linhaAtual[0].Length == 2)
                        {
                            //Adiciona o campeonato
                            campeonatos.SetValue(new Campeonato()
                            {
                                Ano = ano
                            },
                            indice);
                            indice++;

                            Campeonato campeonato = (Campeonato)campeonatos.GetValue(indice - 1);

                            //Extrai os valores das colunas da linha do arquivo
                            short posicao = short.Parse(linhaAtual[0].Trim());
                            string nomeSemAcentos = Helpers.Util.RemoverAcentosNomeTime(linhaAtual[1].Trim());
                            string nomePadronizado = Helpers.Util.PadronizarNomeTime(nomeSemAcentos);
                            string nome = nomePadronizado;
                            string estado = linhaAtual[2].Trim();
                            short pontos = short.Parse(linhaAtual[3].Trim());
                            short jogos = short.Parse(linhaAtual[4].Trim());
                            short vitorias = short.Parse(linhaAtual[5].Trim());
                            short empates = short.Parse(linhaAtual[6].Trim());
                            short derrotas = short.Parse(linhaAtual[7].Trim());
                            short golsAFavor = short.Parse(linhaAtual[8].Trim());
                            short golsContra = short.Parse(linhaAtual[9].Trim());

                            //Adiciona a linha de pontuacao do time
                            campeonato.Posicao = posicao;
                            campeonato.Estado = estado;
                            campeonato.Nome = nome;
                            campeonato.Pontos = pontos;
                            campeonato.Jogos = jogos;
                            campeonato.Vitorias = vitorias;
                            campeonato.Empates = empates;
                            campeonato.Derrotas = derrotas;
                            campeonato.GolsAFavor = golsAFavor;
                            campeonato.GolsContra = golsContra;
                        }
                    }
                }
            }

            return campeonatos;
        }
    }
}
