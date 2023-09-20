using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WPFDoga2023_09_20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// </summary>
    //balra: Barizs Márton Dániel, -
    //jobbra: -, Sinka József
    public partial class MainWindow : Window
    {
        List<Fuvar> fuvarok = File.ReadAllLines("fuvar.csv").Skip(1).Select(x => new Fuvar(x)).ToList();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHarmadikFeladat_Click(object sender, RoutedEventArgs e)
        {
            int fuvarokSzama = fuvarok.Count();
            MessageBox.Show($"3.Feladat: {fuvarokSzama} fuvar");
        }

        private void txtAzonositoEllenorzes(object sender, TextCompositionEventArgs e) {

            Regex csakSzamok = new Regex("[^0-9]+");
            e.Handled = csakSzamok.IsMatch(e.Text);
        }
        private void btnNegyedikFeladat_Click(object sender, RoutedEventArgs e)
        {
            int taxiAzon = int.Parse(txtTaxisAzonosito.Text);
            double osszFizetes = 0;
            int hanyFuvarbolAllt = 0;
            if (fuvarok.Any(x => x.TaxiAzon == taxiAzon) == true)
            {
                foreach (var sor in fuvarok)
                {
                    if (sor.TaxiAzon == taxiAzon)
                    {
                        osszFizetes += sor.Vilteldij + sor.Borravalo;
                        hanyFuvarbolAllt++;
                    }
                }
                MessageBox.Show($"{hanyFuvarbolAllt} alatt: {osszFizetes}$");

            }
            else
            {
                MessageBox.Show("Nincsen ilyen azonosítójú taxis");
            }
            
        }

        private void btnOtodikFeladat_Click(object sender, RoutedEventArgs e)
        {
            foreach (var sor in fuvarok.GroupBy(x => x.FizetesModja).ToList())
            {
                lbfizetesiModok.Items.Add($"{sor.Key}: {sor.Count()} fuvar");
            }
        }

        private void btnHatodikFeladat_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{Math.Round(fuvarok.Sum(x => x.MegtettTavolsag)*1.6, 2)} km");
        }

        private void btnHetedikFeladat_Click(object sender, RoutedEventArgs e)
        {
            lbLeghosszabbFuvar.Items.Add($"Fuvar hossza: {fuvarok.MaxBy(x => x.MegtettTavolsag).UtazasIdotartama} másodperc");
            lbLeghosszabbFuvar.Items.Add($"Taxi azonosítója: {fuvarok.MaxBy(x => x.MegtettTavolsag).TaxiAzon}");
            lbLeghosszabbFuvar.Items.Add($"Megtett távolság: {fuvarok.MaxBy(x => x.MegtettTavolsag).MegtettTavolsag*1.6} km");
            lbLeghosszabbFuvar.Items.Add($"Viteldíj: {fuvarok.MaxBy(x => x.MegtettTavolsag).Vilteldij}$");

        }

        private void btnNyolcadikFeladat_Click(object sender, RoutedEventArgs e)                                             
        {

            using (StreamWriter sw = File.CreateText("hibak.txt"))
            {
                sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
            }
            File.AppendAllLines("hibak.txt", fuvarok.Where(x => x.Vilteldij > 0 && x.MegtettTavolsag == 0 && x.UtazasIdotartama > 0).OrderBy(x => x.IndulasIdopontja).ToList().Select(x => $"{x.TaxiAzon};{x.IndulasIdopontja};{x.UtazasIdotartama};{x.MegtettTavolsag};{x.Vilteldij};{x.Borravalo};{x.FizetesModja}"));
            MessageBox.Show("8.Feladat: hibak.txt, sikeres mentés!");
            
        
        }
    }
}
