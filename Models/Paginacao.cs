using System.Collections.Generic;

namespace web_renderizacao_server_sidee.Models
{
    public record Paginacao<T> where T : class
    {
        public List<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}

// {
//   "results": [
//     {
//       "id": 1,
//       "nome": "Rafael Ferreira Carvalho Caffonso",
//       "email": "rafaelcaffonso@gmail.com"
//     }
//   ],
//   "currentPage": 1,
//   "pageCount": 1,
//   "pageSize": 3,
//   "recordCount": 1
// }