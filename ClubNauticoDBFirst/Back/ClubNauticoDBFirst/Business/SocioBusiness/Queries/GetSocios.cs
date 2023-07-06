using ClubNauticoDBFirst.Models;
using ClubNauticoDBFirst.Resultado.Socios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClubNauticoDBFirst.Business.SocioBusiness.Queries;

public class GetSocios
{
    public class GetSociosQuery : IRequest<ListadoSocios>
    {
    }


    public class GetSociosHandler : IRequestHandler<GetSociosQuery, ListadoSocios>
    {
        private readonly ClubNauticoDbfirstContext _context;

        public GetSociosHandler(ClubNauticoDbfirstContext context)
        {
            _context = context;
        }


        public async Task<ListadoSocios> Handle(GetSociosQuery request, CancellationToken cancellationToken)
        {
            var result = new ListadoSocios();

            try
            {
                var listadoSocios = await _context.Socios.Where(s => s.Activo == true).ToListAsync();

                if (listadoSocios != null)
                {
                    foreach (var socio in listadoSocios)
                    {
                        var itemSocio = new ItemSocio
                        {
                            IdSocio = socio.IdSocio,
                            Nombre = socio.Nombre,
                            Apellido = socio.Apellido,
                            Telefono = socio.Telefono
                        };

                        result.ListSocios.Add(itemSocio);
                    }

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