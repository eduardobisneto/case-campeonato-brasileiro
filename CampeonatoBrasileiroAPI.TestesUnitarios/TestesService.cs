using CampeonatoBrasileiroAPI.Repository;
using CampeonatoBrasileiroAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CampeonatoBrasileiroAPI.API.TestesUnitarios
{
    [TestClass]
    public class TestesService
    {
        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPMinusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("sp");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'sp'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPMaiusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("SP");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'SP'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoSPCaseVariado()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("sP");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'sP'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJMinusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("rj");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'rj'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJMaiusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("RJ");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'RJ'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEstadoRJCaseVariado()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorEstado("rJ");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não foram retornados times para 'rJ'");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeCorinthiansMinusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("corinthians");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'corinthians'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeCorinthiansCaseVariado()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("Corinthians");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'Corinthians'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRMinusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("atletico pr");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'atletico pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRComAcento()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("atlético pr");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'atlético pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoPRSemAcento()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("Atletico pr");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'Atletico pr'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPMinusculo()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("sao paulo");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'sao paulo'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPComAcento()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("são paulo");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'são paulo'.");
        }

        [TestMethod]
        [DeploymentItem(@"repository\data.txt", "optionalOutFolder")]
        public void TestarPorEquipeAtleticoSPMaiusculoComAcento()
        {
            IService service = new Service(new BaseRepository());

            IEnumerable<object> campeonatos = service.PorTime("SÃO PAULO");
            int quantidadeAtual = campeonatos.Count();

            Assert.AreNotEqual(0, quantidadeAtual, "Não retornou os dados por equipe para 'SÃO PAULO'.");
        }
    }
}
