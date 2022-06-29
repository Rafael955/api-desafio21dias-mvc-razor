using System;

namespace web_renderizacao_server_sidee.Models
{
    public class Aluno : Entity<int>
    {
        public string Nome { get; set; }

        public string Matricula { get; set; }
        
        public string Notas { get; set; }
    }
}