using System.Net;
using ClubNauticoDBFirst.Models;
using ClubNauticoDBFirst.Resultado.Socios;
using FluentValidation;
using MediatR;

namespace ClubNauticoDBFirst.Business.SocioBusiness.Commands;

public class PostNuevoSocio
{
    public class PostNuevoSocioCommand : IRequest<ListadoSocios>
    {
        public string Nombre { get; set; } 

        public string Apellido { get; set; } 

        public string Telefono { get; set; } 
        
        public bool Activo { get; set; }
        
    }

    public class PostNuevoSocioCommandValidation : AbstractValidator<PostNuevoSocioCommand>
    {
        public PostNuevoSocioCommandValidation()
        {
       
            RuleFor(s => s.Nombre).NotEmpty();
            RuleFor(s => s.Apellido).NotEmpty();
            RuleFor(s => s.Telefono).NotEmpty();
            
        }
    }
    
    public class PostNuevoSocioCommandHandler: IRequestHandler<PostNuevoSocioCommand,ListadoSocios>
    {
        private readonly ClubNauticoDbfirstContext _context;
        
        private readonly IValidator<PostNuevoSocioCommand> _validator;

        public PostNuevoSocioCommandHandler(ClubNauticoDbfirstContext context, IValidator<PostNuevoSocioCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        
        public async Task<ListadoSocios> Handle(PostNuevoSocioCommand request, CancellationToken cancellationToken)
        {
            var result = new ListadoSocios();
            
            try
            {   //valido la request
                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    var errors = string.Join(Environment.NewLine, validation.Errors);
                    result.SetMensajeError(errors, HttpStatusCode.InternalServerError);
                    return result;
                }
                
                //mapeo la request al nuevo socio
                var socio = new Socio()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Telefono = request.Telefono,
                    Activo = true
                };
                
                //agrego el nuevo socio a la tabla y guardo los cambion en la BD
                await _context.Socios.AddAsync(socio);
                await _context.SaveChangesAsync();

                var itemSocio = new ItemSocio
                {
                    IdSocio = socio.IdSocio,
                    Nombre = socio.Nombre,
                    Apellido = socio.Apellido,
                    Telefono = socio.Telefono
                };
                
                result.ListSocios.Add(itemSocio);
                //devuelvo una lista de Socios con una persona (la creada)
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