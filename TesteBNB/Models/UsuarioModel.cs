using Newtonsoft.Json;

public class UsuarioModel
{
    //public int Id { get; set; }
    public string? Nome { get; set; }

    public int? CPF { get; set; }

    public int? CNPJ { get; set; }

    public int Telefone { get; set; }

    public string? Email { get; set; }

    public string? EndComercial { get; set; }

    public string? EndResidencial { get; set; }
}
