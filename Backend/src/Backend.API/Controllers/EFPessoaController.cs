using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Service.EF;
using Backend.Service.EF.Interface;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                return Ok(PS.GetAll(page));
            }
            catch (Exception e)
            {
                //log here
                return StatusCode(500);
                
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetId(int id)
        {
            try
            {
                return Ok(PS.GetId(id));
            }
            catch (Exception e)
            {
                
                //log here
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Pessoa p)
        {
            try
            {
                return Ok(PS.Post(p));
            }
            catch (Exception e)
            {
                //log here
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] Pessoa p)
        {
            try
            {
                return Ok(PS.Put(p));
            }
            catch (Exception e)
            {
                //log here
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(PS.Delete(id));
            }
            catch (Exception e)
            {
                //log here
                return StatusCode(500);
            }
        }
    }
}
