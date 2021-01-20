using System;

namespace Consulta_Endereco_Pelo_Cep
{
    public class Endereco_Cep
    {
        public string GerarUrlComCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                throw new Exception("CEP não pode ser vazio ou nulo");

            cep = cep.Replace("-", "").Replace(".","");
            return $"https://viacep.com.br/ws/{cep}/json/";
        }
        public string GerarUrlComLogradouro(string uf, string cidade, string logradouro)
        {
            if(string.IsNullOrEmpty(uf))
                throw new Exception("UF não pode ser vazio ou nulo");

            if (string.IsNullOrEmpty(cidade))
                throw new Exception("Cidade não pode ser vazio ou nulo");

            if (string.IsNullOrEmpty(logradouro))
                throw new Exception("Logradouro não pode ser vazio ou nulo");
            uf = uf.Replace(" ", "%20");

            cidade = cidade.Replace(" ", "%20");

            logradouro = logradouro.Replace(" ", "%20");

            return $"https://viacep.com.br/ws/{uf.ToString()}/{cidade}/{logradouro}/json/";
        }
    }
}
