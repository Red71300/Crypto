using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Jeu
    {
        private List<Carte> cartes;
        public List<Carte> Cartes
        {
            get
            {
                return this.cartes;
            }
        }

        private Cryptage cryptage;
        public Cryptage Cryptage
        {
            get
            {
                return this.cryptage;
            }
        }

        public Jeu()
        {
            this.cartes = new List<Carte>();
            this.cryptage = new Cryptage();
            Init();
        }

        //initialise le paquet de cartes
        public void Init()
        {
            string texte = File.ReadAllText("cartes.txt");
            string[] separateur = texte.Split('\n');

            for (int i=0; i<separateur.Length; i++)
            {
                if( i != separateur.Length-1 )
                separateur[i] = separateur[i].Remove(separateur[i].Length-1);

                if ( i == separateur.Length-1 )
                    cartes.Add(new Carte(separateur[i], i));
                else
                    cartes.Add(new Carte(separateur[i], i+1));
            }
        }

        //mélange complet afin de déterminer la clé complète à l'aide du paquet de cartes
        public void Melanger()
        {
            //recul du joker noir
            //Move("Joker-noir", -1);
            
        }

        //déplace une carte dans le paquet
        public void Move(string nom, int nb)
        {
            int position = FindPosition(nom);
            Carte carte = this.cartes[position];
            this.cartes[position] = this.cartes[position - nb];
            this.cartes[position - nb] = carte;
        }

        //récupère la position d'une carte dans le paquet
        int FindPosition(string nom)
        {
            int position = 0;
            int i = 0;
            foreach(Carte carte in this.cartes)
            {
                if (carte.Nom == nom)
                {
                    position = i;
                }
                i++;
            }
            return position;
        }
    }
}