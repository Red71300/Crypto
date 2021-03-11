using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto
{
    class Cryptage
    {
        private List<int> mesInt;//tableau contenant les valeurs numériques du message

        //récupère les valeurs numériques du message sous forme de string
        public string MesInt
        {
            get
            {
                string message = "";
                foreach (int entier in mesInt)
                {
                    message += entier;
                }
                return message;
            }
        }

        private int taille = 0;//taille du message
        public int Taille
        {
            get
            {
                return this.taille;
            }
        }

        private List<int> cle;//tableau contenant les valeurs numériques de la clé
        public string Cle
        {
            get
            {
                string message = "";
                foreach (int entier in cle)
                {
                    message += entier;
                }
                return message;
            }
        }
        private List<int> mesCrypt;//tableau contenant les valeurs numérique du message crypté
        private List<int> mesDecrypt;//Variable contenant le message decrypter
        public Cryptage()
        {
            mesInt = new List<int>();
            cle = new List<int>();
            mesCrypt = new List<int>();
            mesDecrypt = new List<int>();
            /*cle.Add(1);
            cle.Add(2);
            cle.Add(18);
            cle.Add(7);
            cle.Add(9);*/
        }
        /// <summary>
        /// Permet de récupérer les valeurs numériques des lettres du message
        /// </summary>
        public void Message(String message)
        {
            mesInt.Clear();
            mesCrypt.Clear();
            //On récupère les valeur en byte de chaque lettre du message
            byte[] ascii = Encoding.ASCII.GetBytes(message);
            //Pour chaque lettre
            foreach (byte b in ascii) 
            {
                /**On convertit chaque byte en int et on enlève 97
                pour avoir une valeur conprise entre 1 et 26**/
                int val = (int)b - 96;
                //On ajoute la valeur obtenu dans le tableau
                mesInt.Add(val);
            }
            this.taille = mesInt.Count;
        }

        /// <summary>
        /// Methode qui permet de cryter le message saisi à partir de
        /// la valeur numérique des ses lettres ainsi que des valeurs 
        /// numériques des lettres de la clé
        /// </summary>
        public void CrypterMessage()
        {
            for (int i = 0; i < mesInt.Count; i++)
            {
                int valT = mesInt[i] + cle[i - cle.Count * (int)Math.Truncate((Decimal)i / cle.Count)];//Permet de boucler sur la longueur de la clé
                if (valT > 26)
                {
                    valT = valT - 26;
                }
                mesCrypt.Add(valT);
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
            mes = ChaineAsciiCrypt(mesCrypt);
            return mes;
        }

        /// <summary>
        /// Nous partons du message crypter sous forme numérique pour retrouver le message d'origine
        /// </summary>
        /// <returns> Retourne le message décrypté </returns>
        public String MessageDecrypterString()
        {
            mesDecrypt.Clear();
            String mesD = "";

            //On soustrait les valeurs du message crypté aux valeurs de la clé pour retrouvé les valeur
            //du message d'origine
            for (int i = 0; i < mesCrypt.Count; i++)
            {
                int valT = mesCrypt[i] - cle[i - cle.Count * (int)Math.Truncate((Decimal)i / cle.Count)];//Permet de boucler sur la longueur de la clé
                if (valT < 0)
                {
                    valT = valT + 26;
                }
                mesDecrypt.Add(valT);
            }

            //On récupère le message en String
            mesD = ChaineAsciiCrypt(mesDecrypt);
            return mesD;
        }

        /// <summary>
        /// Permet d'obtenir les valeur en string du message en partant de leur valeur numérique
        /// </summary>
        /// <param name="mes"> Variable contenant le message a crypter ou décrypter </param>
        /// <returns></returns>
        public String ChaineAsciiCrypt(List<int> mes)
        {
            String mesF;
            //On créé un tableau de byte qui va contenir les valeur en du message
            //Le tableau est de la longueur du message
            byte[] mesAscii = new byte[mes.Count];
            //Entier qui permet de parcourir la totalité des valeurs du message
            int i = 0;

            //Pour chaque valeur int du message on récupère sa valeur ASCII en byte
            foreach (int chr in mes)
            {
                byte val = (byte)(chr + 96);
                mesAscii[i] = val;
                i++;
            }
            //On récupère le message sous forme de string en partant des valeurs ASCII
            return mesF = Encoding.ASCII.GetString(mesAscii);
        }

        /// <summary>
        /// Permet de récupérer les valeurs numérique de la clé généré dans jeu.cs
        /// </summary>
        /// <param name="cleJ"> Liste de string de la clé généré </param>
        public void creationCleInt(List<String> cleJ)
        {
            String cleS = null;
            foreach (String lettre in cleJ)
            {
                cleS += lettre;
            }
            byte[] ascii = Encoding.ASCII.GetBytes(cleS);
            foreach(byte b in ascii)
            {
                int val = (int)b - 96;
                this.cle.Add(val);
            }
        }
    }
}