using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using App01_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App01_ConsultarCEP.Servico
{
    public class ViaCepServico
    {
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCep(string cep)
        {
            // Vamos substituir o parametro {0} pelo cep desejado
            string novoEnderecoURL = string.Format(EnderecoURL, cep);
            // Pesquisa online
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(novoEnderecoURL);
            // Conversao da consulta para o modelo (desserializar)
            Endereco endereco = JsonConvert.DeserializeObject<Endereco>(conteudo);
            // Verificar se os campos foram retornados
            if (endereco.cep == null)
            {
                return null;
            }
            // Retorno final
            return endereco;
        }
    }
}
