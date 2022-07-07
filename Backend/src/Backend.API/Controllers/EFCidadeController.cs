using Backend.Infra.Data.model;
using Backend.Service.EF.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCidadeController : ControllerBase
    {
        public ICidadeService cS { get; set; }
        public EFCidadeController(ICidadeService cs)
        {
            cS = cs;
        }
        [HttpGet("Pagina/{page}")]
        public ActionResult<IEnumerable<Cidade>> GetAll(int page)
        {
            try
            {
                return Ok(cS.GetAll(page));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Cidade> GetId(int id)
        {
            try
            {
                return Ok(cS.GetId(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Cidade p)
        {
            try
            {
                return Ok(cS.Post(p));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] Cidade p)
        {
            try
            {
                return Ok(cS.Put(p));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(cS.Delete(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

