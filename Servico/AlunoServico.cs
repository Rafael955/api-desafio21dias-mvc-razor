using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_sidee.Models;

namespace web_renderizacao_server_sidee.Servico
{
    public static class AlunoServico
    {
        public static async Task<List<Aluno>> ObterTodos()
        {
            using var client = new HttpClient();
            
            using var response = await client.GetAsync($"{Program.AlunosProfessoresApi}/alunos/api/listar-alunos");

            if(!response.IsSuccessStatusCode) return new List<Aluno>();
            
            string json = await response.Content.ReadAsStringAsync();
            var paginacao = JsonConvert.DeserializeObject<Paginacao<Aluno>>(json);

            return paginacao.Results;
        }

        public static async Task<Paginacao<Aluno>> ObterTodosPaginado(int pagina = 1)
        {
            using var client = new HttpClient();
            
            using var response = await client.GetAsync($"{Program.AlunosProfessoresApi}/alunos/api/listar-alunos?page={pagina}");

            if(!response.IsSuccessStatusCode) return new Paginacao<Aluno>();
            
            string json = await response.Content.ReadAsStringAsync();
            var paginacao = JsonConvert.DeserializeObject<Paginacao<Aluno>>(json);

            return paginacao;
        }

        public static async Task<Aluno> BuscaPorId(int id)
        {
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync($"{Program.AlunosProfessoresApi}/alunos/api/detalhes-aluno/{id}");

            if(!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var aluno = JsonConvert.DeserializeObject<Aluno>(json);

            return aluno;
        }

        public static async Task<Aluno> Salvar(Aluno aluno)
        {
            using var client = new HttpClient();

            if(aluno.Id == 0)
            {
                //var json = JsonConvert.SerializeObject(administrador);
                //StringContent content = new StringContent(json, Encoding.UTF8);

                using var response = await client.PostAsJsonAsync($"{Program.AlunosProfessoresApi}/alunos/api/cadastrar-aluno", aluno);
                /*PostAsJsonAsync já covnerte o objeto da classe em um JSON e o submete via POST*/

                if(!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine("=========== Error ===========");
                    Console.WriteLine(error);
                    Console.WriteLine("=============================");

                    throw new Exception("Erro ao incluir novo aluno!");
                }

                return JsonConvert.DeserializeObject<Aluno>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                //var json = JsonConvert.SerializeObject(administrador);
                //StringContent content = new StringContent(json, Encoding.UTF8);

                using var response = await client.PutAsJsonAsync($"{Program.AdministradoresApi}/alunos/api/atualizar-aluno/{aluno.Id}", aluno);
                 /*PutAsJsonAsync já converte o objeto da classe em um JSON e o submete via PUT*/

                  if(!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao atualizar dados do aluno!");

                  return JsonConvert.DeserializeObject<Aluno>(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task ExcluirPorId(int id)
        {
            using var client = new HttpClient();
            
            using var response = await client.DeleteAsync($"{Program.AlunosProfessoresApi}/api/alunos/remover-aluno/{id}");

            if(!response.IsSuccessStatusCode)
                throw new Exception("Erro ao deletar dados do aluno!");
        }
    }
}