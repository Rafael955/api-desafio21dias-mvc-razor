using System;
using System.ComponentModel.DataAnnotations;

namespace web_renderizacao_server_sidee.Models
{
    public class Administrador : Entity<int>
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
    }
}
