using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace autoverseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Random rnd;
        private double celvonal = 650;

        private List<String> listaopciok = new List<String>() {"Ferrari","Lamborghini","Porsche","Csak Nézem"};

        private List<Button> celbaert = new List<Button>();

        string select;

        private List<Button> kocsik = new List<Button>();
        
        public MainWindow()
        {
            InitializeComponent();
            lbx_valasztas.ItemsSource = listaopciok;
            timer = new DispatcherTimer();
            rnd = new Random();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick;
            kocsik.Add(btn_ferrari);
            kocsik.Add(btn_lambo);
            kocsik.Add(btn_porsche);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            foreach (Button b in kocsik)
            {
                Mozog(b);
            }
        }

        private void Mozog(Button btn)
        {
            if (!celbaert.Contains(btn) && btn.Content.ToString() != select)
            {

            double pozicio = Canvas.GetLeft(btn);
            double sebesseg = rnd.Next(1,6);
            double ujpozicio = pozicio + sebesseg;
            Canvas.SetLeft(btn, ujpozicio);
            }
            if (Canvas.GetLeft(btn) >= celvonal)
            {
                MessageBox.Show($"A(z) {btn.Content} célba ért {celbaert.Count + 1}. helyezetben", "Nyertes");
                celbaert.Add(btn);
                btn.Click -= btn_kocsi_Click;

                if (celbaert.Count == 3)
                    {
                        MessageBox.Show("Verseny vége", "Vége");
                        timer.Stop();
                    }

                }
            
        }



        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            select = lbx_valasztas.SelectedItem.ToString();

            celbaert.Clear();

            foreach (Button b in kocsik)
            {
                Canvas.SetLeft(b, 20);
                if(b.Content.ToString() == select)
                {
                    b.Click += btn_kocsi_Click;

                }
            }
            timer.Start();
        }

        private void btn_kocsi_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            Canvas.SetLeft(clicked, Canvas.GetLeft(clicked) + rnd.Next(18,26));
  

        }
    }
}