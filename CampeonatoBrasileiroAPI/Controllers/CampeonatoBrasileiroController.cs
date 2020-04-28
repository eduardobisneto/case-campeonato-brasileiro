using CampeonatoBrasileiroAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CampeonatoBrasileiroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampeonatoBrasileiroController : Controller
    {
        private readonly IService service;

        public CampeonatoBrasileiroController(IService _service)
        {
            service = _service;
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/por-time/palmeiras
        [HttpGet("por-time/{nomeTime}")]
        public JsonResult PorTime(string nomeTime)
        {
            try
            {
                return Json(service.PorTime(nomeTime));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/por-estado/sp
        [HttpGet("por-estado/{siglaEstado}")]
        public JsonResult PorEstado(string siglaEstado)
        {
            try
            {
                return Json(service.PorEstado(siglaEstado));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //endpoint: https://localhost:5001/api/campeonatobrasileiro/informacoes-complementares
        [HttpGet("informacoes-complementares")]
        public JsonResult InformacoesComplementares()
        {
            try
            {
                return Json(service.InformacoesComplementares());
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
