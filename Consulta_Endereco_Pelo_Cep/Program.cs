using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Consulta_Endereco_Pelo_Cep
{
    public static class Program
    {
        public static string _cep, _uf, _cidade, _logradouro, _tipo_consulta, _url_Consulta;
        public static void Main(string[] args)
        {
            string[] estados = new string[]
            {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO"};

            Endereco_Cep url = new Endereco_Cep();
            Requisicao requisicao = new Requisicao();

            Console.Write("Digite 1 para Consultar CEP ou 2 para Endereço ");
            _tipo_consulta = Console.ReadLine().ToString();

            while (!_tipo_consulta.Equals("1") && !_tipo_consulta.Equals("2"))
            {
                Console.Clear();
                Console.Write("Opção inválida \nDigite 1 para Consultar CEP ou 2 para Endereço ");
                _tipo_consulta = Console.ReadLine().ToString();
            }

            if (_tipo_consulta.Equals("1"))
            {
                Console.Write("Digite o cep: ");
                _cep = Console.ReadLine().ToString();

                while (validarString(_cep))
                {
                    Console.Write("Cep digitado inválido \nDigite o cep: ");
                    _cep = Console.ReadLine().ToString();
                }
                _cep = _cep.Replace("-", "").Replace(".", "");
                while (_cep.Length != 8)
                {
                    Console.Write("Cep digitado inválido \nDigite o cep: ");
                    _cep = Console.ReadLine().ToString();
                }
                _url_Consulta = url.GerarUrlComCep(_cep);

                string retorno_requisicao = requisicao.RequisicaoCep(_url_Consulta);
                RetornoConsulta retorno =  JsonConvert.DeserializeObject<RetornoConsulta>(retorno_requisicao);

                Console.Write("\nCep: " + retorno.cep + "\n"
                    + "Logradouro: " + retorno.logradouro + "\n"
                    + "Complemento: " + retorno.complemento + "\n"
                    + "Bairro: " + retorno.bairro + "\n"
                    + "Localidade: " + retorno.localidade + "\n"
                    + "UF: " + retorno.uf + "\n"
                    + "Cod_IBGE: " + retorno.ibge + "\n"
                    + "Gia: " + retorno.gia + "\n"
                    + "DDD: " + retorno.ddd + "\n"
                    + "Siafi: " + retorno.siafi);

            }

            else if (_tipo_consulta.Equals("2"))
            {
                bool estado_encontrado = false;

                Console.Write("Digite o código do Estado Ex: ES para Espirito Santo ");
                _uf = Console.ReadLine().ToString();

                while (validarString(_uf) && _uf.Length !=2)
                {
                    Console.Clear();
                    Console.Write("Estado digitado inválido \nDigite o novamente: ");
                    _uf = Console.ReadLine().ToString();
                }

                while (!estado_encontrado)
                {
                    foreach (string estado in estados)
                    { 
                        if (estado.ToUpper().Equals(_uf.ToUpper()))
                        {
                            estado_encontrado = true;
                            break;
                        }
                    }

                    if (!estado_encontrado)
                    {
                        Console.Clear();
                        Console.Write("Estado digitado não encontrado \nDigite o novamente: ");
                        _uf = Console.ReadLine().ToString();
                    }
                }

                Console.Write("Digite a cidade: ");
                _cidade = Console.ReadLine().ToString();
                if (string.IsNullOrEmpty(_cidade))
                {
                    Console.Clear();
                    Console.Write("O nome da cidade não pode ser vazio \n Digite novamente: ");
                    _cidade = Console.ReadLine().ToString();
                }


                Console.Write("Digite o logradouro (Rua Avenida ...): ");
                _logradouro = Console.ReadLine().ToString();

                if (string.IsNullOrEmpty(_logradouro))
                {
                    Console.Clear();
                    Console.Write("O nome do Logradouro não pode ser vazio \n Digite novamente: ");
                    _logradouro = Console.ReadLine().ToString();
                }

                _url_Consulta = url.GerarUrlComLogradouro(_uf, _cidade, _logradouro);

                string retorno_requisicao = requisicao.RequisicaoCep(_url_Consulta);
                List<RetornoConsulta> retorno = JsonConvert.DeserializeObject< List<RetornoConsulta>>(retorno_requisicao);

                for (int i = 0; i < retorno.Count; i++)
                {
                    Console.Write("\n ==================================================" + (i + 1) + "º" + " Regsitro"
                   + "\nCep: " + retorno[i].cep + "\n"
                   + "Logradouro: " + retorno[i].logradouro + "\n"
                   + "Complemento: " + retorno[i].complemento + "\n"
                   + "Bairro: " + retorno[i].bairro + "\n"
                   + "Localidade: " + retorno[i].localidade + "\n"
                   + "UF: " + retorno[i].uf + "\n"
                   + "Cod_IBGE: " + retorno[i].ibge + "\n"
                   + "Gia: " + retorno[i].gia + "\n"
                   + "DDD: " + retorno[i].ddd + "\n"
                   + "Siafi: " + retorno[i].siafi
                   );
                }
                Console.ReadKey();
            }
        }

        public static bool validarString(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return true;
            else
                return false;
        }
        public static string RemoverAcentuacao(this string text)
        {
            if (validarString(text))
                return "";

            else
                return new string(text
                    .Normalize(NormalizationForm.FormD)
                    .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    .ToArray());
        }
    }
}
