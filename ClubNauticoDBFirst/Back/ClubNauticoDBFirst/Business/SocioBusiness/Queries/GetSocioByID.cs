using System.Net;
using ClubNauticoDBFirst.Models;
using ClubNauticoDBFirst.Resultado.Socios;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClubNauticoDBFirst.Business.SocioBusiness.Queries;

//aca declaramos 3 clases
public class GetSocioByID
{
    //1ro misma nombre agregando Query o Command 
    //hereda de IRequest (MediatR), indica que esa es la
    //query y que se va a devolver un tipo de dato
    public class GetSocioBYIdQuery : IRequest<ListadoSocios>
    {
        public int IdSocio { get; set; } //porque es una consulta a traves de un Id
    }
    
    

    //2da mismo nombre que la anterior + Validation
    //Valida la clase anterior mediante FluentValidation del que hereda
    //indicamos que query se va a validar
    public class GetSocioBYIdQueryValidation : AbstractValidator<GetSocioBYIdQuery>
    {
        //en el contructor validamos que no este vacio la respuesta de la query
        public GetSocioBYIdQueryValidation()
        {
            RuleFor(s => s.IdSocio).NotEmpty().WithMessage("El id del socio es oblitario");
        }
    }

    
    //3ro la clase manejadora nombre de la clase + Handler
    //hereda de mediatR y le indicamos la consulta y el tipo de respuesta
    // implementamos el metodo
    public class GetSocioByIDHandler : IRequestHandler<GetSocioBYIdQuery,ListadoSocios>
    {
        //definimos la variable que nos permite interactuar con la base de datos
        private readonly ClubNauticoDbfirstContext _context;

        //definimos la variable que nos permite valdiar el modelo
        private readonly IValidator<GetSocioBYIdQuery> _validator;

        //inyectamos el context programado en el program
        public GetSocioByIDHandler(ClubNauticoDbfirstContext context, IValidator<GetSocioBYIdQuery> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        //metodo hace la query y devuelve la respuesta (le agregamos el async)
        public async Task<ListadoSocios> Handle(GetSocioBYIdQuery request, CancellationToken cancellationToken)
        {
            var result = new ListadoSocios();
            try
            {
                //valido la request
                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    var errors = string.Join(Environment.NewLine, validation.Errors);
                    result.SetMensajeError(errors, HttpStatusCode.InternalServerError);
                    return result;
                }
                
                var socio = await _context.Socios.FirstOrDefaultAsync(s => s.IdSocio.Equals(request.IdSocio) && s.Activo == true);

                if (socio != null)
                {
                    var itemSocio = new ItemSocio
                    {
                        IdSocio = socio.IdSocio,
                        Nombre = socio.Nombre,
                        Apellido = socio.Apellido,
                        Telefono = socio.Telefono
                    };

                    result.ListSocios.Add(itemSocio);
                    
                    return result;

                }
                
                //sino
                var mensajeError = "Socio con " + request.IdSocio.ToString() + " no encontrado";
                    result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);

                    return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
}