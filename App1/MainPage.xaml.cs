using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Text.RegularExpressions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// Colaboradores: Luiz Paulo, Matheus Jose, Carlos Eduardo
//alteração para o carlos
//teste de compatibilidade

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public enum ForcaDaSenha
    {
        Inaceitavel,
        Fraca,
        Aceitavel,
        Forte,
        Segura
    }

    /*
        GetPontoPorTamanho -Seis pontos serão atribuídos para cada caractere na senha, até um máximo de sessenta pontos.
        GetPontoPorMinusculas - Cinco pontos serão concedidos se a senha inclui uma letra minúscula. Dez pontos serão atribuídos se mais de uma letra minúscula estiver presente.
        GetPontoPorMaiusculas - Cinco pontos serão concedidos se a senha incluir uma letra maiúscula. Dez pontos serão atribuídos se mais de uma letra maiúscula estiver presente.
        GetPontoPorDigitos - Cinco pontos serão concedidos se a senha incluir um dígito numérico. Dez pontos serão atribuídos se mais de um dígito numérico estiver presente.
        GetPontoPorSimbolos - Cinco pontos serão concedidos se a senha incluir qualquer caractere diferente de uma letra ou um dígito. Isto inclui símbolos e espaços em branco. Dez pontos serão concedidos se houver dois ou mais de tais caracteres.
        GetPontoPorRepeticao - Se houver caracteres repetidos na senha será atribuido 30 pontos que será subtraida da fórmula do cálculo do total dos pontos;
    */

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void buttonGerarSenha_Click(object sender, RoutedEventArgs e)
        {
            var tamanho = Convert.ToInt32(sliderTamanho.Value);
            var chars = "";
            var random = new Random();

            if (!checkBoxLetraMaiuscula.IsChecked.Value & !checkBoxLetrasMinusculas.IsChecked.Value & !checkBoxNumeros.IsChecked.Value & !checkBoxCaracterEspecial.IsChecked.Value)
            {
                textBoxSenhaGerada.Text = "Marque pelo menos uma opção para gerar a senha";
            }
            else
            {
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


                var result = new string(
                    Enumerable.Repeat(chars, tamanho)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                textBoxSenhaGerada.Text = result;
                textBlockForcaSenha.Text = Convert.ToString(GetForcaDaSenha(result));
//                textBoxSenhaCriptografada.Text = Criptografar("",result);
  //              textBoxSenhaDescriptografada.Text = Descriptografar("",result);
            }
          }

        //Checar Força Senha
        public int geraPontosSenha(string senha)
        {
            if (senha == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            int pontosPorRepeticao = GetPontoPorRepeticao(senha);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;
        }

        private int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 7;
        }

        private int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 6;
        }

        private int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }


        private int GetPontoPorRepeticao(string senha)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }


        public ForcaDaSenha GetForcaDaSenha(string senha)
        {
            int placar = geraPontosSenha(senha);

            if (placar < 50)
                return ForcaDaSenha.Inaceitavel;
                
            else if (placar < 60)
                return ForcaDaSenha.Fraca;
            else if (placar < 80)
                return ForcaDaSenha.Aceitavel;
            else if (placar < 100)
                return ForcaDaSenha.Forte;
            else
                return ForcaDaSenha.Segura;
        }


        // ####### CRIPTOGRAFIA


    }

}
