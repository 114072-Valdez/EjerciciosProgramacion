using System.Net;
using ClubNauticoDBFirst.Models;
using ClubNauticoDBFirst.Resultado.Socios;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClubNauticoDBFirst.Business.SocioBusiness.Commands;

public class UpdateSocio
{
    public class UpdateSocioCommand : IRequest<ListadoSocios>
    {
        public int IdSocio { get; set; }
        public string Nombre { get; set; } 

        public string Apellido { get; set; } 

        public string Telefono { get; set; } 

    }
    
    public class UpdateSocioCommandValidation : AbstractValidator<UpdateSocioCommand>
    {
        public UpdateSocioCommandValidation()
        {
            RuleFor(s => s.IdSocio).NotEmpty();
            RuleFor(s => s.Nombre).NotEmpty();
            RuleFor(s => s.Apellido).NotEmpty();
            RuleFor(s => s.Telefono).NotEmpty();
            
        }
    }
    
      public class UpdateSocioCommandHandler: IRequestHandler<UpdateSocioCommand,ListadoSocios>
    {
        private readonly ClubNauticoDbfirstContext _context;
        
        private readonly IValidator<UpdateSocioCommand> _validator;

        public UpdateSocioCommandHandler(ClubNauticoDbfirstContext context, IValidator<UpdateSocioCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        
        public async Task<ListadoSocios> Handle(UpdateSocioCommand request, CancellationToken cancellationToken)
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
                
                var socio = await _context.Socios.FirstOrDefaultAsync(s => s.IdSocio.Equals(request.IdSocio) && s.Activo == true);
               
                if (socio != null)
                {
                    socio.Nombre = request.Nombre;
                    socio.Apellido = request.Apellido;
                    socio.Telefono = request.Telefono;
                    
                    await _context.SaveChangesAsync();
                    
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