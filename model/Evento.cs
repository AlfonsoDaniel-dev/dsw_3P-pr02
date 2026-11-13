namespace dsw_pr02.Models;

public class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Categoria { get; set; } = "";
    public string Lugar { get; set; } = "";
    public int NumeroMaximo { get; set; }
    public bool Activo { get; set; }

}