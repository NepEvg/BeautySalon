using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Beauty_Saloon.Model;

namespace Beauty_Saloon
{
    /// <summary>
    /// Логика взаимодействия для ApointmentsWindow.xaml
    /// </summary>
    public partial class ApointmentsWindow : Window
    {
        public ObservableCollection<ClientService> clientServices { get; set; }
        public DateTime dt;
        DispatcherTimer timer = new DispatcherTimer();
        public ApointmentsWindow()
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 30);
            timer.Start();

            dt = DateTime.Now.AddDays(1);
            clientServices = new ObservableCollection<ClientService>(Connection.Database.ClientService.Where(x => x.StartTime >= DateTime.Now && x.StartTime <= dt));
            clientServices = new ObservableCollection<ClientService>(clientServices.OrderBy(x => x.StartTime));
            InitializeComponent();
            list.ItemsSource = clientServices;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            clientServices = new ObservableCollection<ClientService>(Connection.Database.ClientService.Where(x => x.StartTime >= DateTime.Now && x.StartTime <= dt));
            clientServices = new ObservableCollection<ClientService>(clientServices.OrderBy(x => x.StartTime));
            list.ItemsSource = clientServices;
            MessageBox.Show("список обновлен");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
        }

        private void cbDateTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDateTime.SelectedIndex == 1)
            {
                clientServices = new ObservableCollection<ClientService>(clientServices.OrderByDescending(x => x.StartTime));
                list.ItemsSource = clientServices;
            }
            else
            {
                clientServices = new ObservableCollection<ClientService>(clientServices.OrderBy(x => x.StartTime));
                list.ItemsSource = clientServices;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
