using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDoga2023_09_20
{
    public class Fuvar
    {
        int taxiAzon;
        DateTime indulasIdopontja;
        int utazasIdotartama;
        double megtettTavolsag;
        double vilteldij;
        double borravalo;
        string fizetesModja;

        public Fuvar(string sor)
        {
            string[] felbontas = sor.Split(';');
            this.taxiAzon = int.Parse(felbontas[0]);
            this.indulasIdopontja = DateTime.Parse(felbontas[1]);
            this.utazasIdotartama = int.Parse(felbontas[2]);
            this.megtettTavolsag = double.Parse(felbontas[3]);
            this.vilteldij = double.Parse(felbontas[4]);
            this.borravalo = double.Parse(felbontas[5]);
            this.fizetesModja = felbontas[6];
        }

        public int TaxiAzon { get => taxiAzon; }
        public DateTime IndulasIdopontja { get => indulasIdopontja;}
        public int UtazasIdotartama { get => utazasIdotartama; }
        public double MegtettTavolsag { get => megtettTavolsag; }
        public double Vilteldij { get => vilteldij; }
        public double Borravalo { get => borravalo; }
        public string FizetesModja { get => fizetesModja; }
    }
}
