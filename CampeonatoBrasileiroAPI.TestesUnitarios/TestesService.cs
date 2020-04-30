using CampeonatoBrasileiroAPI.Helpers;
using CampeonatoBrasileiroAPI.Repository;
using CampeonatoBrasileiroAPI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.API.TestesUnitarios
{
    [TestClass]
    public class TestesService
    {
        public TestesService()
        {
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPMinusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("sp");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'sp'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPMaiusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("SP");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'SP'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPCaseVariado()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("sP");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'sP'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJMinusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("rj");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'rj'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJMaiusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("RJ");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'RJ'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJCaseVariado()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            IEnumerable<object> campeonatos = service.PorEstado("rJ");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "N�o foram retornados times para 'rJ'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeCorinthiansMinusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("corinthians");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'corinthians'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeCorinthiansCaseVariado()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("Corinthians");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'Corinthians'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRMinusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("atletico pr");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'atletico pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRComAcento()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("atl�tico pr");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'atl�tico pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRSemAcento()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("Atletico pr");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'Atletico pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPMinusculo()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("sao paulo");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'sao paulo'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPComAcento()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("s�o paulo");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 's�o paulo'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPMaiusculoComAcento()
        {
            IService service = new Service(new Mock<BaseRepository>().Object, new Mock<ILogger<Service>>().Object);

            object campeonato = service.PorTime("S�O PAULO");

            Assert.AreNotEqual(null, campeonato, "N�o retornou os dados por equipe para 'S�O PAULO'.");
        }
    }
}
