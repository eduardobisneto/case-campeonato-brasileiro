using CampeonatoBrasileiroAPI.Entity;
using CampeonatoBrasileiroAPI.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.TestesUnitarios
{
    [TestClass]
    public class TestesRepository
    {

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarCarregarRegistros()
        {
            IRepository repository = new Mock<BaseRepository>().Object;

            IEnumerable<Campeonato> campeonatos = repository.CarregarDados();
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "O arquivo não foi carregado corretamente.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarQuantidadeRegistros()
        {
            IRepository repository = new Mock<BaseRepository>().Object;

            IEnumerable<Campeonato> campeonatos = repository.CarregarDados();

            int quantidadeEsperada = 100;
            int quantidadeAtual = campeonatos.Count();

            Assert.AreEqual(quantidadeEsperada, quantidadeAtual, "Não foi carregado corretamente a quantidade de dados esperados.");
        }
    }
}
