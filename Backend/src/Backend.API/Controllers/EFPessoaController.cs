using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Service.EF;
using Backend.Service.EF.Interface;
using Backend.Service.model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFPessoaController : ControllerBase
    {
        public IPessoaService PS { get; set; }
        public EFPessoaController(IPessoaService ps)
        {
            PS = ps;
        }
        [HttpGet("Pagina/{page}")]
        public ActionResult<IEnumerable<Pessoa>> GetAll(int page)
        {
            string templateLog = "[Backend.Api] [EFPessoaController] [GetAll]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetAll");
                var res = PS.GetAll(page);
                Log.Information($"{templateLog} GetAll Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em :" + e.StackTrace);
                return StatusCode(500);
                
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetId(int id)
        {
            string templateLog = "[Backend.Api] [EFPessoaController] [GetId]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetId");
                var res = PS.GetId(id);
                Log.Information($"{templateLog} GetId Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {

                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em :" + e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Pessoa p)
        {
            string templateLog = "[Backend.Api] [EFPessoaController] [Post]";
            try
            {
                Log.Information($"{templateLog} Iniciando Post");
                var res = PS.Post(p);
                Log.Information($"{templateLog} Post Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em :" + e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] Pessoa p)
        {
            string templateLog = "[Backend.Api] [EFPessoaController] [Post]";
            try
            {
                Log.Information($"{templateLog} Iniciando Put");
                var res = PS.Put(p);
                Log.Information($"{templateLog} Put Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em :" + e.StackTrace);
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            string templateLog = "[Backend.Api] [EFPessoaController] [Delete]";
            try
            {
                Log.Information($"{templateLog} Iniciando Delete");
                var res =PS.Delete(id);
                Log.Information($"{templateLog} Delete Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em :" + e.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
