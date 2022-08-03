using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Service.Redis.Interface;
using Backend.Service.model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class DapperPessoaController : ControllerBase
    {
        public IPessoaService CS { get; set; }
        public DapperPessoaController(IPessoaService cs)
        {
            CS = cs;
        }
        [HttpGet("Pagina/{page}")]
        public ActionResult<IEnumerable<Pessoa>> GetAll(int page)
        {
            string templateLog = "[Backend.Api] [DapperPessoaController] [GetAll]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetAll");
                var res = CS.GetAll(page);
                Log.Information($"{templateLog} GetAll Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em" + e.StackTrace); ;
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetId(int id)
        {
            string templateLog = "[Backend.Api] [DapperPessoaController] [GetId]";
            try
            {
                Log.Information($"{templateLog} Iniciando GetId");
                var res = CS.GetId(id);
                Log.Information($"{templateLog} GetId Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em" + e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Pessoa p)
        {
            string templateLog = "[Backend.Api] [DapperPessoaController] [Post]";
            try
            {
                Log.Information($"{templateLog} Iniciando Post");
                var res = CS.Post(p);
                Log.Information($"{templateLog} Post Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em" + e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] Pessoa p)
        {
            string templateLog = "[Backend.Api] [DapperPessoaController] [Put]";
            try
            {
                Log.Information($"{templateLog} Iniciando Put");
                var res = CS.Put(p);
                Log.Information($"{templateLog} Put Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em" + e.StackTrace);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            string templateLog = "[Backend.Api] [DapperPessoaController] [Delete]";
            try
            {
                Log.Information($"{templateLog} Iniciando Delete");
                var res = CS.Delete(id);
                Log.Information($"{templateLog} Delete Finalizado, retornando");
                return Ok(res);
            }
            catch (Exception e)
            {
                Log.Error(templateLog + " [ERROR] Excessao achada : " + e.Message + " em" + e.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
