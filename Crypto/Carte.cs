using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Carte
    {
        private string nom;
        public string Nom
        {
            get
            {
                return this.nom;
            }
        }

        private int num;
        public int Num
        {
            get
            {
                return this.num;
            }
        }

        public Carte(string nom, int num)
        {
            this.nom = nom;
            this.num = num;
        }
    }
}