using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using App1.login;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// Colaboradores: Luiz Paulo, Matheus Jose, Carlos Eduardo

namespace App1
{
   
    public sealed partial class MainPage : Page
    {
        Senha senhaModel = new Senha();
        ConexaoDB conn = ConnectionFactory.conexao;
        SenhaDAO senhaDAO;
                 
        public MainPage()
        {
            this.InitializeComponent();
            conn = new ConexaoDB();
            senhaDAO = new SenhaDAO(conn);
            carregarLista();
        }

        SenhaViewModel SenhaViewModel { get; set; }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }



        public void carregarLista()
        {
            listViewSenha.ItemsSource = new SenhaViewModel().Senhas;
        }
        
        private void buttonGerarSenha_Click(object sender, RoutedEventArgs e)
        {
            var tamanho = Convert.ToInt32(sliderTamanho.Value);
            var chars = "";
            var random = new Random();

            if (!checkBoxLetraMaiuscula.IsChecked.Value & !checkBoxLetrasMinusculas.IsChecked.Value & !checkBoxNumeros.IsChecked.Value & !checkBoxCaracterEspecial.IsChecked.Value)
            {
                textBlockMensagem.Text = "Marque pelo menos uma opção para gerar a senha";
            }
            else
            {
                textBlockMensagem.Text = "";
                if (checkBoxLetraMaiuscula.IsChecked.Value)
                {
                    chars = chars + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                }

                if (checkBoxLetrasMinusculas.IsChecked.Value)
                {
                    chars = chars + "abcdefghijklmnopqrstuvwxyz";
                }

                if (checkBoxNumeros.IsChecked.Value)
                {
                    chars = chars + "0123456789";
                }

                if (checkBoxCaracterEspecial.IsChecked.Value)
                {
                    chars = chars + "!@#${}[]/<>=";
                }


                var senhaGerad = new string(
                    Enumerable.Repeat(chars, tamanho)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                textBoxSenhaGerada.Text = senhaGerad;
            }
          }

        
        private void limparCampos()
        {
            senhaModel = new Senha();
            textBoxSenhaGerada.Text="";
            textBoxTituloSenha.Text = "";
            textBlockMensagem.Text = "";
            checkBoxCaracterEspecial.IsChecked = false;
            checkBoxLetraMaiuscula.IsChecked = false;
            checkBoxLetrasMinusculas.IsChecked = false;
            checkBoxNumeros.IsChecked = false;
            sliderTamanho.Value = 4;
        }

       void preencherSenha()
        {
            textBoxTituloSenha.Text = senhaModel.descricao;
            textBoxSenhaGerada.Text = senhaModel.password;
        }
        
        private async void SalvarSenha(object sender, RoutedEventArgs e)
        {
            if (textBoxSenhaGerada.Text != "")
            {
                senhaModel.descricao = textBoxTituloSenha.Text;
                senhaModel.password = textBoxSenhaGerada.Text;

                if (senhaModel.senhaId == 0)
                {
                    await this.senhaDAO.InsertSenhaAsync(senhaModel);
                    limparCampos();
                }
                else
                {
                    await this.senhaDAO.UpdateSenhaAsync(senhaModel);
                    limparCampos();
                }
                carregarLista();
            }else
            {
                textBlockMensagem.Text = "Favor gerar a senha antes de Salvar";
            }
        }

        private async void deletarSenha(object sender, RoutedEventArgs e)
        {
            if (textBoxSenhaGerada.Text != "")
            {
                await this.senhaDAO.DeleteSenhaAsync(senhaModel);
                limparCampos();
                carregarLista();
            }else
            {
                textBlockMensagem.Text = "Favor selecione uma senha para ser exlcuida";
            }

        }

        private void selecionarSenha(object sender, SelectionChangedEventArgs e)
        {
            Senha senhaSelected = (Senha)listViewSenha.SelectedItem;
            if (senhaSelected != null)
            {
                senhaModel = senhaSelected;
                preencherSenha();
                carregarLista();
            }
        }

        private void novaSenha(object sender, RoutedEventArgs e)
        {
            limparCampos();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageLogin));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           this.Frame.Navigate(typeof(CadLogin));
        }

        private void editarLogin(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditarLogin));
        }
    }

}
