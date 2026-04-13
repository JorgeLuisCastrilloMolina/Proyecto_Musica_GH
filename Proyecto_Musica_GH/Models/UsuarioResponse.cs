namespace Proyecto_Musica_GH.Models
{
    public class UsuarioResponse
    {
        public int Usuario_ID { get; set; }   // 👉 Esto fue lo que se agregó

        public string Message { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
    }
}