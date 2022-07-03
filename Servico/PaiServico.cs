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

        public static async Task<Pai> Salvar(Pai paiDoAluno)
        {
            using var client = new HttpClient();

            if(paiDoAluno.Id is null)
            {
                // var json = JsonConvert.SerializeObject(paiDoAluno);
                // var buffer = Encoding.UTF8.GetBytes(json);
                // var content = new ByteArrayContent(buffer);

                //using var response = await client.PostAsync($"{Program.PaisApi}/pais/api/cadastrar-pai", content);
                using var response = await client.PostAsJsonAsync($"{Program.PaisApi}/pais/api/cadastrar-pai", paiDoAluno);

                if(!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao incluir novo pai de aluno!");
                
                // json = await response.Content.ReadAsStringAsync();
                var _paiDoAluno = JsonConvert.DeserializeObject<Pai>(await response.Content.ReadAsStringAsync());

                return _paiDoAluno;
            }
            else
            {
                // var json = JsonConvert.SerializeObject(paiDoAluno);
                // var buffer = Encoding.UTF8.GetBytes(json);
                // var content = new ByteArrayContent(buffer);

                using var response = await client.PutAsJsonAsync($"{Program.PaisApi}/pais/api/atualizar-dados-pai/{paiDoAluno.Id}", paiDoAluno);

                if(!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao alterar dados do pai do aluno!");
                
                // json = await response.Content.ReadAsStringAsync();
               var _paiDoAluno = JsonConvert.DeserializeObject<Pai>(await response.Content.ReadAsStringAsync());

                return _paiDoAluno;
            }
        }

        public static async Task ExcluirPorId(string id)
        {
            using var client = new HttpClient();
            
            using var response = await client.DeleteAsync($"{Program.PaisApi}/pais/api/remover-pai/{id}");

            if(!response.IsSuccessStatusCode)
                throw new Exception("Erro ao deletar dados do pai do aluno!");
        }
    }
}