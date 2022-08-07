using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Service.Dapper;
using Backend.Service.EF;
using Backend.Service.model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class DapperCidadeController :ControllerBase
    {

            public ICidadeServiceEF CS { get; set; }
            public DapperCidadeController(ICidadeServiceEF cs)
            {
                CS = cs;
            }
            [HttpGet("Pagina/{page}")]
            public ActionResult<IEnumerable<Cidade>> GetAll(int page)
            {
                string templateLog = "[Backend.Api] [DapperCidadeController] [GetAll]";
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
            public ActionResult<Cidade> GetId(int id)
            {
                string templateLog = "[Backend.Api] [DapperCidadeController] [GetId]";
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
            public ActionResult<bool> Post([FromBody] Cidade p)
            {
                string templateLog = "[Backend.Api] [DapperCidadeController] [Post]";
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
            public ActionResult<int> Put([FromBody] Cidade p)
            {
                string templateLog = "[Backend.Api] [DapperCidadeController] [Put]";
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
                string templateLog = "[Backend.Api] [DapperCidadeController] [Delete]";
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
