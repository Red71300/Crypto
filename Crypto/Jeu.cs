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

        private string[] alphabet = { "a", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public string[] Alphabet
        {
            get
            {
                return alphabet;
            }
        }

        private List<String> cle = new List<String>();
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
            bool generation = false;
            string lettre; //lettre chiffrée
            this.cle.Clear();

            while(!generation)
            {
                Move("Joker-noir", -1); //recul du joker noir
                Move("Joker-rouge", -2); //recul du joker rouge
                DoubleCoupe();
                this.cartes[53].Num = 4;
                SimpleCoupe();
                lettre = LireLettre();
                if (lettre != "X")
                {
                    this.cle.Add(lettre);
                    if (this.cle.Count >= this.Cryptage.Taille)
                    {
                        generation = true;
                    }
                }
            }
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
        public int FindPosition(string nom)
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
        public void DoubleCoupe()
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

        //coupe simple de la dernière carte
        public void SimpleCoupe()
        {
            Carte carte = this.cartes[this.cartes.Count - 1];
            int nb = carte.Num;
            List<Carte> listeFinale = new List<Carte>();

            //on commence par remplir la liste avec les dernières cartes (sauf la dernière qui reste la dernière)
            for (int i=nb; i<this.cartes.Count-1; i++)
            {
                listeFinale.Add(this.cartes[i]);
            }

            //on termine avec la coupe avec les nb premières cartes (en laissant la dernière à la fin)
            for (int i=0; i<nb; i++)
            {
                listeFinale.Add(this.cartes[i]);
            }
            listeFinale.Add(carte); //on ajoute enfin la dernière

            //set nouvel ordre
            for (int i = 0; i < 54; i++)
            {
                this.cartes[i] = listeFinale[i];
            }
        }

        //dernière étape qui détermine la lettre
        public String LireLettre()
        {
            int n = this.cartes[0].Num;
            Carte carte = this.cartes[n];
            int m = carte.Num;
            string lettre = "X";

            //on vérifie qu'on n'est pas tombé sur un joker
            if (m != 53)
            {
                //on vérifie si m dépasse 26
                if (m > 26)
                {
                    m = m - 26;
                }
                lettre = alphabet[m];
            }
            return lettre;
        }
    }
}