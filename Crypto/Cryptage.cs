using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Cryptage
    {
        private List<int> mesInt;//tableau contenant les valeurs numériques du message
        private List<int> cle;//tableau contenant les valeurs numériques de la clé
        private List<int> mesCryt;//tableau contenant les valeurs numérique du message crypté
        public Cryptage()
        {

        }
        /// <summary>
        /// Permet de récupérer les valeurs numériques des lettres du message
        /// </summary>
        public void Message(String message)
        {
            //On récupère les valeur en byte de chaque lettre du message
            byte[] ascii = Encoding.ASCII.GetBytes(message);
            //Pour chaque lettre
            foreach (byte b in ascii) 
            {
                /**On convertit chaque byte en int et on enlève 97
                pour avoir une valeur conprise entre 1 et 26**/
                int val = (int)b - 96;
                //ON ajoute la valeur obtenu dans le tableau
                mesInt.Add(val);
            }
        }

        /// <summary>
        /// Methode qui permet de cryter le message saisie à partir de
        /// la valeur numérique des ses lettres ainsi que des valeur 
        /// numérique des lettre de la clé
        /// </summary>
        public void CrypterMessage()
        {
            for(int i=0; i <= mesInt.Count; i++)
            {
                int valT = mesInt[i] + cle[i];
                if(valT > 26) 
                {
                    valT = valT - 26;
                }
                mesCryt.Add(valT);
            }
        }

        /// <summary>
        /// Fonction qui permet d'obtenir le message crytper sous forme
        /// de string
        /// </summary>
        /// <returns> Le string du message crypté </returns>
        public String MessageCrypterString()
        {
            String mes = "";
            byte[] mesAscii = null;
            foreach(int chr in mesCryt)
            {
                byte val = (byte)(chr + 96);
                mesAscii.Append(val);
            }
            mes = Encoding.ASCII.GetString(mesAscii);
            return mes;
        }
    }
}
