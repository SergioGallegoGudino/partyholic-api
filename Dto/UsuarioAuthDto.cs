namespace partyholic_api.Dto
{
    public class UsuarioAuthDto
    {
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string? Passwd { get; set; }
        public string? FotoP { get; set; }
        public string? RolApp { get; set; }
    }
}
