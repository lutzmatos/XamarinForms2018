using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BtnPesquisar.Clicked += BuscarCep;
		}

        private void BuscarCep(object sender,EventArgs args)
        {
            string resultado = "Cep não encontrado!";
            string cep = TxtCep.Text.Trim();

            // Validacao
            if (IsValidCep(cep))
            {
                try
                {
                    // Busca
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep);
                    // Resultado
                    if (end != null)
                    {
                        resultado = string.Format("Endereço do CEP {0}", end.cep) + "\n";
                        resultado += string.Format("Rua: {0}", end.logradouro) + "\n";
                        resultado += string.Format("Complemento: {0}", end.complemento) + "\n";
                        resultado += string.Format("Cidade/Uf: {0}/{1}", end.localidade, end.uf);
                    }
                } catch (Exception e)
                {
                    DisplayAlert("ERRO", e.Message, "OK");
                }
            }
            
            // Impressao do resultado
            LblResultado.Text = resultado;
        }

        private bool IsValidCep(string cep)
        {
            bool retorno = true;
            List<string> Erros = new List<string>();

            if (cep.Length < 8)
            {
                Erros.Add("O cep não pode conter menos de 8 caracteres");
                retorno = false;
            }

            int novoCep = 0;
            if (!int.TryParse(cep, out novoCep))
            {
                Erros.Add("O cep não é um número válido");
                retorno = false;
            }

            if (!retorno)
            {
                string mensagemErro = String.Join("\n", Erros.ToArray());
                DisplayAlert("ERRO", mensagemErro, "Ok");
            }

            return retorno;
        }
	}
}
