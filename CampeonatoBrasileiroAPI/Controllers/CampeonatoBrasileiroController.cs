using CampeonatoBrasileiroAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CampeonatoBrasileiroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampeonatoBrasileiroController : Controller
    {
        private readonly ILogger<CampeonatoBrasileiroController> logger;
        private readonly IService service;

        public CampeonatoBrasileiroController(
            IService _service,
            ILogger<CampeonatoBrasileiroController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/por-time/palmeiras
        [HttpGet("por-time/{nomeTime}")]
        public JsonResult PorTime(string nomeTime)
        {
            try
            {
                object listaTimes = service.PorTime(nomeTime);

                logger.LogInformation("Executou o método PorTime da API com sucesso.");

                return Json(listaTimes);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);

                return Json("Ops, ocorreu um erro. Verifique o arquivo de log.");
            }
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/por-estado/sp
        [HttpGet("por-estado/{siglaEstado}")]
        public JsonResult PorEstado(string siglaEstado)
        {
            try
            {
                IEnumerable<object> porEstado = service.PorEstado(siglaEstado);

                logger.LogInformation("Executou o método PorEstado da API com sucesso.");

                return Json(porEstado);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);

                return Json("Ops, ocorreu um erro. Verifique o arquivo de log.");
            }
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/informacoes-complementares
        [HttpGet("informacoes-complementares")]
        public JsonResult InformacoesComplementares()
        {
            try
            {
                object informacoesComplementares = service.InformacoesComplementares();

                logger.LogInformation("Executou o método PorEstado da API com sucesso.");

                return Json(informacoesComplementares);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);

                return Json("Ops, ocorreu um erro. Verifique o arquivo de log.");
            }
        }
    }
}
