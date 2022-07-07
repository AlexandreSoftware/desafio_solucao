using Backend.Infra.Data.model;
using Backend.Service.EF.Interface;
using Backend.Service.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCidadeController : ControllerBase
    {
        public ICidadeService CS { get; set; }
        public EFCidadeController(ICidadeService cs)
        {
            CS = cs;
        }
        [HttpGet("Pagina/{page}")]
        public ActionResult<IEnumerable<Cidade>> GetAll(int page)
        {
            string templateLog = "[Backend.Api] [EFCidadeController] [GetAll]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetAll");
                var res = CS.GetAll(page);
                Log.Information($"{templateLog} GetAll Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message);;
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Cidade> GetId(int id)
        {
            string templateLog = "[Backend.Api] [EFCidadeController] [GetId]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetId");
                var res =CS.GetId(id);
                Log.Information($"{templateLog} GetId Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Cidade p)
        {
            string templateLog = "[Backend.Api] [EFCidadeController] [Post]";
            try
            {
                Log.Information($"{templateLog} Iniciando Post");
                var res = CS.Post(p);
                Log.Information($"{templateLog} Post Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] Cidade p)
        {
            string templateLog = "[Backend.Api] [EFCidadeController] [Put]";
            try
            {
                Log.Information($"{templateLog} Iniciando Put");
                var res = CS.Put(p);
                Log.Information($"{templateLog} Put Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            string templateLog = "[Backend.Api] [EFCidadeController] [Delete]";
            try
            {
                Log.Information($"{templateLog} Iniciando Delete");
                var res = CS.Delete(id);
                Log.Information($"{templateLog} Delete Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message);
                return StatusCode(500);
            }
        }
    }
}

