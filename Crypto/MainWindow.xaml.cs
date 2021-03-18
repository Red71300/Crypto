using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crypto
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Jeu jeu;
        public MainWindow()
        {
            InitializeComponent();
            jeu = new Jeu();
            foreach (Carte carte in jeu.Cartes)
            {
                paquet.Text += carte.Nom + "\n";
            }
        }

        //évènement bouton chiffrement
        private void button_chiffrer_Click(object sender, RoutedEventArgs e)
        {
            if (message.Text.Length > 1 && message.Text.All(char.IsLower) && !message.Text.Any(char.IsSymbol))
            {
                jeu.Cryptage.Message(message.Text);
                message_chiffre.Text = jeu.Cryptage.MesInt;
                jeu.Melanger();
                cle.Text = jeu.Cle;
                paquet.Text = "";
                foreach (Carte carte in jeu.Cartes)
                {
                    paquet.Text += carte.Nom + "\n";
                }
            }
            else
            {
                String message = "Veuillez ne pas mettre de message vide ou entrer uniqument " +
                    "des lettres minuscules (Pas de maj, d'espace ou caractère spéciaux)";
                String caption = "Message vide / Caractère non accepté";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void btn_CryptageMessage_Click(object sender, RoutedEventArgs e)
        {
            jeu.Cryptage.CrypterMessage();
            message_crypter_final_txt.Text = jeu.Cryptage.MessageCrypterString();
        }

        private void btnDécrypté_Click(object sender, RoutedEventArgs e)
        {
            message_decrypter_txt.Text = jeu.Cryptage.MessageDecrypterString();
        }

        //évènement pour tester le recul du joker noir
        private void btnNoir_Click(object sender, RoutedEventArgs e)
        {
            jeu.Move("Joker-noir", -1);
            paquet.Text = "";
            foreach (Carte carte in jeu.Cartes)
            {
                paquet.Text += carte.Nom + "\n";
            }
        }

        //évènement pour tester le recul du joker rouge
        private void btnRouge_Click(object sender, RoutedEventArgs e)
        {
            jeu.Move("Joker-rouge", -2);
            paquet.Text = "";
            foreach (Carte carte in jeu.Cartes)
            {
                paquet.Text += carte.Nom + "\n";
            }
        }

        private void btnDoubleCoupe_Click(object sender, RoutedEventArgs e)
        {
            jeu.DoubleCoupe();
            paquet.Text = "";
            foreach (Carte carte in jeu.Cartes)
            {
                paquet.Text += carte.Nom + "\n";
            }
        }

        private void btnCoupe_Click(object sender, RoutedEventArgs e)
        {
            jeu.SimpleCoupe();
            paquet.Text = "";
            foreach (Carte carte in jeu.Cartes)
            {
                paquet.Text += carte.Nom + "\n";
            }
        }
    }
}