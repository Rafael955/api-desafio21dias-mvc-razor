using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_sidee.Models;

namespace web_renderizacao_server_sidee.Servico
{
    public static class PaiServico
    {
        public static async Task<List<Pai>> ObterTodos()
        {
            using var client = new HttpClient(); 

            using var result = await client.GetAsync($"{Program.PaisApi}/pais/api/listar-pais-de-alunos");

            if(!result.IsSuccessStatusCode) return new List<Pai>();

            var json = await result.Content.ReadAsStringAsync();

            var listaDePais = JsonConvert.DeserializeObject<List<Pai>>(json);

            return listaDePais;
        }

        public static async Task<Pai> BuscaPorId(string id)
        {
            using var client = new HttpClient();

            using var result = await client.GetAsync($"{Program.PaisApi}/pais/api/detalhes-do-pai/{id}");

            if(!result.IsSuccessStatusCode) return null;

            var json = await result.Content.ReadAsStringAsync();

            var pai = JsonConvert.DeserializeObject<Pai>(json);

            return pai;
        }
    }
}