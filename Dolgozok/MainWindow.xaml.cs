using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dolgozok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dolgozo> dolgozokLista = new List<Dolgozo>();
        public MainWindow()
        {
            InitializeComponent();
            FajlBeolvasas("dolgozok.txt");
            Nevek();
            Beosztások();
        }

        public void FajlBeolvasas(string fajlnev)
        {
            StreamReader sr = new StreamReader(fajlnev, Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(';');
                Trace.WriteLine(adatok[5]);
                dolgozokLista.Add(new Dolgozo(adatok[0], adatok[1], adatok[2], adatok[3], int.Parse(adatok[4]), adatok[5]));
            }
            sr.Close();
            foreach (var item in dolgozokLista)
            {
                Trace.WriteLine(item.Nev);
            }
        }

        public void Nevek()
        {
            List<string> nevek = new List<string>();
            foreach (var item in dolgozokLista)
            {
                nevek.Add(item.Nev);
            }
            lb_Nevek.ItemsSource = nevek;
            lb_Nevek.Items.Refresh();
		}

        public void Kiiras(object sender, SelectionChangedEventArgs e)
        {
            //ChatGpt-vel megnéztem, hogyan lehet adatot kiirini ha rákattintunk egy listbox elemre
            string kivalasztottNev = lb_Nevek.SelectedItem.ToString();
            bool megtalalva = false;
            while (megtalalva != true)
            {
                foreach (var item in dolgozokLista)
                {
                    if (item.Nev == kivalasztottNev)
                    {
                        lbl_kiiras.Content = $"Név: {item.Nev}\nBeosztás: {item.Beosztas}\nEmail: {item.Email}\nTelefonszám: {item.TelefonSzam}\nFizetés: {item.Fizetes} Ft\nNeme: {item.Nem}";
                        megtalalva = true;
                    }
                }
            }
        }

        public void Beosztások()
        {
            List<string> beosztasok = new List<string>();
            foreach (var item in dolgozokLista)
            {
                if (!beosztasok.Contains(item.Beosztas))
                {
                    beosztasok.Add(item.Beosztas);
                }
            }

            foreach (var item in beosztasok)
            {
                Trace.WriteLine(item);
            }

            cb_beosztasok.ItemsSource = beosztasok;
        }

        private void btn_mentes_Click(object sender, RoutedEventArgs e)
        {
            if (txtb_nev.Text == "" || cb_beosztasok.SelectedIndex == -1 || txtb_email.Text == "" || txtb_telefon.Text == "" || txtb_fizetes.Text == "" || (rb_ferfi.IsChecked == false && rb_no.IsChecked == false))
            {
                MessageBox.Show(this, "Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Mentes();
            }
        }

        private void Mentes()
        {
            string nev = txtb_nev.Text;
            string beosztas = cb_beosztasok.SelectedItem.ToString();
            string email = txtb_email.Text;
            string telefonszam = txtb_telefon.Text;
            int fizetes = int.Parse(txtb_fizetes.Text);
            string nem;
            if (rb_ferfi.IsChecked == true)
            {
                nem = "Férfi";
            }
            else
            {
                nem = "Nő";
            }
            Dolgozo ujDolgozo = new Dolgozo(nev, beosztas, email, telefonszam, fizetes, nem);
            dolgozokLista.Add(ujDolgozo);
			foreach (var item in dolgozokLista)
            {
                Trace.WriteLine(item.Nev);
			}
            lbl_mentes.Content = "Sikeres mentés!";
			Nevek();
            FormTorles();
            FajlIras("dolgozok.txt", ujDolgozo);
        }

        private void FormTorles()
        {
            txtb_nev.Clear();
            cb_beosztasok.SelectedIndex = -1;
            txtb_email.Clear();
            txtb_telefon.Clear();
            txtb_fizetes.Clear();
            rb_ferfi.IsChecked = false;
            rb_no.IsChecked = false;
		}

        private void FajlIras(string fajlnev,Dolgozo ujDolgozo)
        {
            StreamWriter sw = new StreamWriter(fajlnev,true);
			sw.WriteLine($"{ujDolgozo.Nev};{ujDolgozo.Beosztas};{ujDolgozo.Email};{ujDolgozo.TelefonSzam};{ujDolgozo.Fizetes};{ujDolgozo.Nem}");
            sw.Close();
		}
	}
}