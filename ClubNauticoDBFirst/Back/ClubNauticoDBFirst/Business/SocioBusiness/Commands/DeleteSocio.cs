using ClubNauticoDBFirst.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClubNauticoDBFirst.Business.SocioBusiness.Commands;

public class DeleteSocio
{
    public class DeleteSocioCommand: IRequest<bool>
    {
        public int IdSocio { get; set; }
    }
    
    public class DeleteSocioCommandValidation : AbstractValidator<DeleteSocioCommand>
    {
        public DeleteSocioCommandValidation()
        {
            RuleFor(s => s.IdSocio).NotEmpty();
        }
    }

    public class DeleteSocioCommandHandler : IRequestHandler<DeleteSocioCommand, bool>
    {
        private readonly ClubNauticoDbfirstContext _context;
        private readonly IValidator<DeleteSocioCommand> _validator;

        public DeleteSocioCommandHandler(ClubNauticoDbfirstContext context,IValidator<DeleteSocioCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        public async Task<bool> Handle(DeleteSocioCommand request, CancellationToken cancellationToken)
        {
            bool ok = false; 
            try
            {
                var socio = await _context.Socios.FirstOrDefaultAsync(s => s.IdSocio.Equals(request.IdSocio)&& s.Activo == true);
               
                if (socio != null)
                {
                    socio.Activo = false;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }

}