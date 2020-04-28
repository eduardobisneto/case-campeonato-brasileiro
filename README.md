# case-campeonato-brasileiro

Projeto de API escrito em AspNetCore 2.2, com o objetivo de extrair dados de um  arquivo de texto com informações das tabelas de pontuação dos campeonatos brasileiros de futebol entre 2015 a 2019, consolidar esses dados, e expor em 3 endpoints, agrupados por time, por estado , e cálculo de indicadores estatísticos.

Endpoint Por Time
https://localhost:5001/api/campeonatobrasileiro/por-time/{nome do time}

Os passos são:

1. Carregar os dados do arquivo de texto, tratando o nome dos times do arquivo
2. Tratar o nome do time recebido como parâmetro
3. Filtrar os dados pelo time
4. Agrupar os dados pelo time
5. Contar a quantidade de participações do time, somar o total de jogos, total de vitórias, empates, derrotas, gols pró, gols contra e calcular o saldo de gols do time
7. Ordenar descendentemente pela Pontuação Total do time

Retorna:

Posição, Nome do Time, Pontuação total, Qtde de campeonatos disputados, Total de Jogos, Total de Vitorias, Total de Empates, Total de  Derrotas, Total de Gols Prós, Total de Gols Contras, Saldo de gols

Endpoint Por Estado
https://localhost:5001/api/campeonatobrasileiro/por-estado/{sigla do estado}

Os passos são:

1. Carregar os dados do arquivo de texto, tratando o nome dos times do arquivo
2. Tratar a sigla do estado recebido como parâmetro
3. Filtrar os dados pelos times do estado
4. Agrupar os dados pelos times do estado
5. Contar a quantidade de participações dos times do estado, somar o total de jogos, total de vitórias, empates, derrotas, gols pró, gols contra e calcular o saldo de gols dos times do estado
7. Ordenar descendentemente pela Pontuação Total do estado

Retorna:

Posição, Estado, Pontuação total, Qtde de campeonatos disputados, Total de Jogos, Total de Vitorias, Total de Empates, Total de Derrotas, Total de Gols Prós, Total de Gols Contra, Saldo de Gols

Endpoint de Informações Adicionais
https://localhost:5001/api/campeonatobrasileiro/informacoes-adicionais

Os passos são:

1. Carregar os dados do arquivo de texto, tratando o nome dos times do arquivo
2. Agrupar os dados pelo nome do time
3. Contar a quantidade de participações do time, somar o total de jogos, total de vitórias, empates, derrotas, gols pró, gols contra e calcular o saldo de gols do time
4. Calcular o time com a melhor média de gols a favor, o time que faz mais gols, em média (gols a favor / quantidade de campeonatos), no período.
5. Calcular o time com a melhor média de gols contra, o time que levou menos gols, em média (gols contra / quantidade de campeonatos), no período.
6. Calcular o time com o maior número de vitórias (total de vitórias) no período.
7. Calcular o time com o menor número de vitórias (total de vitórias) no período.
8. Calcular o time com o menor número de vitórias (total de vitórias) no período.
9. Agrupar os dados por ano e pelo nome do time
10. Contar a quantidade de participações do time, somar o total de jogos, total de vitórias, empates, derrotas, gols pró, gols contra e calcular o saldo de gols do time por ano
11. Calcular o time com a melhor media de vitorias (total de vitórias / total de jogos) por campeonato (ano)
12. Calcular o time com a menor media de vitorias (total de vitórias / total de jogos) por campeonato (ano)

Retorna

Time com melhor média de gols a favor, Time com melhor média de gols contra, Time com maior numero de vitórias, Time com menor numero de vitórias, Time com melhor média de vitorias por Campeonato, Time com menor média de vitórias por Campeonato
