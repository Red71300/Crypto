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

        public Jeu()
        {
            this.cartes = new List<Carte>();
            init();
        }

        public void init()
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
    }
}