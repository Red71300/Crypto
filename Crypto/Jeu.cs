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
            Move("Joker-noir", -1); //recul du joker noir
            Move("Joker-rouge", -2); //recul du joker rouge
            DoubleCoupe();
        }

        //déplace une carte dans le paquet
        public void Move(string nom, int nb)
        {
            int position = FindPosition(nom);
            Carte carte = this.cartes[position];

            if ((position - nb) <= 53)
            {
                this.cartes[position] = this.cartes[position - nb];
                this.cartes[position - nb] = carte;
            }
            else
            {
                this.cartes[position] = this.cartes[-nb];
                this.cartes[-nb] = carte;
            }
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

        //double coupe des deux jokers
        void DoubleCoupe()
        {
            int posJokerR = FindPosition("Joker-rouge");
            int posJokerN = FindPosition("Joker-noir");
            List<Carte> listeDebut = new List<Carte>();
            List<Carte> listeFin = new List<Carte>();
            List<Carte> listeMilieu = new List<Carte>();
            List<Carte> listeFinale = new List<Carte>();

            //si le joker rouge est avant le joker noir
            if (posJokerN > posJokerR)
            {
                //init liste du début du paquet
                for (int i = 0; i < posJokerR; i++)
                {
                    listeDebut.Add(this.cartes[i]);
                }

                //init liste du milieu en incluant les deux jokers
                for (int i = posJokerR; i <= posJokerN; i++)
                {
                    listeMilieu.Add(this.cartes[i]);
                }

                //init liste de fin du paquet
                for (int i = posJokerN + 1; i < 54; i++)
                {
                    listeFin.Add(this.cartes[i]);
                }
            }
            //si le joker noir est avant le joker rouge
            else
            {
                //init liste du début du paquet
                for (int i = 0; i < posJokerN; i++)
                {
                    listeDebut.Add(this.cartes[i]);
                }

                //init liste du milieu en incluant les deux jokers
                for (int i = posJokerN; i <= posJokerR; i++)
                {
                    listeMilieu.Add(this.cartes[i]);
                }

                //init liste de fin du paquet
                for (int i = posJokerR + 1; i < 54; i++)
                {
                    listeFin.Add(this.cartes[i]);
                }
            }

            //init la liste finale
            foreach(Carte carte in listeFin)
            {
                listeFinale.Add(carte);
            }
            foreach (Carte carte in listeMilieu)
            {
                listeFinale.Add(carte);
            }
            foreach (Carte carte in listeDebut)
            {
                listeFinale.Add(carte);
            }

            //set nouvel ordre
            for (int i=0; i<54; i++)
            {
                this.cartes[i] = listeFinale[i];
            }
        }
    }
}