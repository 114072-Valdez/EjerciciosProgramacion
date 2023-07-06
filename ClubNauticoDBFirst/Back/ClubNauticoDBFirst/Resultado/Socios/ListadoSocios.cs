
namespace ClubNauticoDBFirst.Resultado.Socios;

public class ListadoSocios: ResultadoBase
{
    public List<ItemSocio> ListSocios { get; set; } = new List<ItemSocio>();
}

public class ItemSocio
{

    public int IdSocio { get; set; }

    public string Nombre { get; set; } 

    public string Apellido { get; set; } 

    public string Telefono { get; set; } 
    
}