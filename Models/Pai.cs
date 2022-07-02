using System;

namespace web_renderizacao_server_sidee.Models
{
    public class Pai : Entity<string>
    {
        public string Nome { get; set; }

        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }
    }
}