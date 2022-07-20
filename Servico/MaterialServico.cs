using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_sidee.Models;

namespace web_renderizacao_server_sidee.Servico
{
    public static class MaterialServico
    {
        public static async Task<List<Material>> ObterTodos(int pagina = 1)
        {
            using var client = new HttpClient();

            using var result = await client.GetAsync($"{Program.MateriaisApi}/materiais/api/listar-materiais");

            var json = await result.Content.ReadAsStringAsync();

            var listaDeMateriais = JsonConvert.DeserializeObject<Paginacao<Material>>(json);

            return listaDeMateriais.Results;
        }
    }
}