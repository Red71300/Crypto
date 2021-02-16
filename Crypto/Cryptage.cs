﻿using System;
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
        private List<int> mesCryt;//tableau contenant les valeurs numérique du message crypté
        public Cryptage()
        {
            mesInt = new List<int>();
            cle = new List<int>();
            mesCryt = new List<int>();
            cle.Add(1);
            cle.Add(2);
            cle.Add(18);
            cle.Add(7);
            cle.Add(9);
        }
        /// <summary>
        /// Permet de récupérer les valeurs numériques des lettres du message
        /// </summary>
        public void Message(String message)
        {
            mesInt.Clear();
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
        }

        /// <summary>
        /// Methode qui permet de cryter le message saisie à partir de
        /// la valeur numérique des ses lettres ainsi que des valeur 
        /// numérique des lettre de la clé
        /// </summary>
        public void CrypterMessage()
        {
            for (int i = 0; i < mesInt.Count; i++)
            {
                int valT = mesInt[i] + cle[i - cle.Count*(int)Math.Truncate((Double)i/cle.Count)];//Permet de boucler sur la longueur de la clé
                if (valT > 26)
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
            byte[] mesAscii = new byte[mesCryt.Count];
            int i = 0;
            foreach(int chr in mesCryt)
            {
                byte val = (byte)(chr + 96);
                mesAscii[i] = val;
                i++;
            }
            mes = Encoding.ASCII.GetString(mesAscii);
            return mes;
        }
    }
}