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
    public static class AdministradorServico
    {
        public static async Task<List<Administrador>> ObterTodos()
        {
            using var client = new HttpClient();
            
            using var response = await client.GetAsync($"{Program.AdministradoresApi}/administradores/api/listar-administradores");

            if(!response.IsSuccessStatusCode) return new List<Administrador>();
            
            string json = await response.Content.ReadAsStringAsync();
            var paginacao = JsonConvert.DeserializeObject<Paginacao<Administrador>>(json);

            
            return paginacao.Results;
        }

        public static async Task<Paginacao<Administrador>> ObterTodosPaginado(int pagina = 1)
        {
            using var client = new HttpClient();
            
            using var response = await client.GetAsync($"{Program.AdministradoresApi}/administradores/api/listar-administradores?page={pagina}");

            if(!response.IsSuccessStatusCode) return new Paginacao<Administrador>();
            
            string json = await response.Content.ReadAsStringAsync();
            var paginacao = JsonConvert.DeserializeObject<Paginacao<Administrador>>(json);

            return paginacao;
        }

        public static async Task<Administrador> BuscaPorId(int id)
        {
            using var client = new HttpClient();
            
            using var response = await client.GetAsync($"{Program.AdministradoresApi}/administradores/api/detalhes-administrador/{id}");

            if(!response.IsSuccessStatusCode) return null;
            
            string json = await response.Content.ReadAsStringAsync();
            var adminitrador = JsonConvert.DeserializeObject<Administrador>(json);

            return adminitrador;
        }

        public static async Task<Administrador> Salvar(Administrador administrador)
        {
            using var client = new HttpClient();

            if(administrador.Id == 0)
            {
                //var json = JsonConvert.SerializeObject(administrador);
                //StringContent content = new StringContent(json, Encoding.UTF8);

                using var response = await client.PostAsJsonAsync($"{Program.AdministradoresApi}/administradores/api/cadastrar-administrador", administrador);
                /*PostAsJsonAsync já covnerte o objeto da classe em um JSON e o submete via POST*/

                if(!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine("=========== Error ===========");
                    Console.WriteLine(error);
                    Console.WriteLine("=============================");

                    throw new Exception("Erro ao incluir novo administrador!");
                }

                return JsonConvert.DeserializeObject<Administrador>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                //var json = JsonConvert.SerializeObject(administrador);
                //StringContent content = new StringContent(json, Encoding.UTF8);

                using var response = await client.PutAsJsonAsync($"{Program.AdministradoresApi}/administradores/api/atualizar-administrador/{administrador.Id}", administrador);
                 /*PutAsJsonAsync já converte o objeto da classe em um JSON e o submete via PUT*/

                  if(!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao atualizar dados do administrador!");

                  return JsonConvert.DeserializeObject<Administrador>(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task ExcluirPorId(int id)
        {
            using var client = new HttpClient();

            using var response = await client.DeleteAsync($"{Program.AdministradoresApi}/administradores/api/remover-administrador/{id}");

            if(!response.IsSuccessStatusCode)
                throw new Exception("Erro ao excluir administrador!");
        }
    }
}