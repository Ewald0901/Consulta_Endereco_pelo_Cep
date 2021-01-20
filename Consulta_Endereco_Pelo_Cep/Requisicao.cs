using System;
using System.Net;
using System.Net.Http;


namespace Consulta_Endereco_Pelo_Cep
{
    public class Requisicao
    {

        public string RequisicaoCep(string url)
        {
            try
            {
                return ObterJson(url);                
            }
            catch (ArgumentException e)
            {
                throw new Exception($"Erro ao tentar fazer a requisição: {e.Message}");
            }
        }

        public string RequisicaoEndereco(string url)
        {
            try
            {
                return ObterJson(url);               
            }
            catch (ArgumentException e)
            {
                throw new Exception($"Erro ao tentar fazer a requisição: {e.Message}");
            }
        }


        internal static string ObterJson(string url)
        {
            try
            {
                HttpClient request = new HttpClient();

                HttpResponseMessage response = request.GetAsync(url).Result;

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new ArgumentException();                      

                return response.Content.ReadAsStringAsync().Result;               
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Erro ao tentar fazer a requisição: {ex.Message}");
            }
        }       
    }
}

