using ClubNauticoDBFirst.Business.SocioBusiness.Commands;
using ClubNauticoDBFirst.Business.SocioBusiness.Queries;
using ClubNauticoDBFirst.Resultado.Socios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static ClubNauticoDBFirst.Business.SocioBusiness.Commands.PostNuevoSocio; //tuve que agregar

namespace ClubNauticoDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioController : ControllerBase
    {
        //el mediador entre las clases
        private readonly IMediator _mediator;

        public SocioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ListadoSocios>> postSocio([FromBody] PostNuevoSocioCommand postSocioCommand)
        {
            //enviamos lo que recibimos en el cuerpo de la la request (en este caso el socio a crear)
            return await _mediator.Send(postSocioCommand);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<ListadoSocios>> getSocioById(int id)
        {
            return await _mediator.Send(new GetSocioByID.GetSocioBYIdQuery { IdSocio = id });
        }

        [HttpGet]
        [Route("getAllSocios")]
        public async Task<ActionResult<ListadoSocios>> getAllSocios()
        {
            //creo una instancia de la consulta
            var query = new GetSocios.GetSociosQuery();
            //guardo del resultado de la consulta
            var result = await _mediator.Send(query);
            //devuelvo el resultado
            return result;
        }
        
        [HttpPut]
        [Route("putSocio")]
        public async Task<ActionResult<ListadoSocios>> putSocio([FromBody] UpdateSocio.UpdateSocioCommand updateSocioCommand)
        {
            //enviamos lo que recibimos en el cuerpo de la la request (en este caso el socio a crear)
            return await _mediator.Send(updateSocioCommand);
        }
        
        [HttpDelete]
        [Route("deleteSocio/{id}")]
        public async Task<ActionResult<bool>> deleteSocioById(int id)
        {
            return await _mediator.Send(new DeleteSocio.DeleteSocioCommand() { IdSocio = id });
        }
    }
}