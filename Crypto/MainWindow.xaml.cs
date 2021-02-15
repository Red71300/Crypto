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
        }

        //évènement bouton chiffrement
        private void button_chiffrer_Click(object sender, RoutedEventArgs e)
        {
            jeu.Cryptage.Message(message.Text);
            message_chiffre.Text = jeu.Cryptage.MesInt;
        }
    }
}