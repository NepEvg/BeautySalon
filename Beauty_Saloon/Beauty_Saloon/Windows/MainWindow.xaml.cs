using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Beauty_Saloon.Model;
using Beauty_Saloon.Windows;

namespace Beauty_Saloon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<ClientService> ClientServices { get; set; }
        public List<Service> _services { get; set; }
        int countOfServices;
        int cena = 2, skidka = 5;
        public MainWindow()
        {
            Connection.Database = new BeautySaloonEntities();
            Services = new ObservableCollection<Service>(Connection.Database.Service);
            ClientServices = new ObservableCollection<ClientService>(Connection.Database.ClientService);
            _services = Services.ToList();
            InitializeComponent();
            countOfServices = Services.Count;
            list.ItemsSource = Services;
            count.Content = "Количество доступных услуг: "+_services.Count()+" из "+countOfServices;
        }

        private void Cost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            _services = new ObservableCollection<Service>(Services).ToList();
            //поиск
            if (search.Text != "")
            {
                _services = _services.Where(x => x.Title.ToUpper().Contains(search.Text.ToUpper())).ToList();
            }

            //скидка
            if (skidka == 0)
            {
                _services = _services.Where(x => x.Discount < 5).ToList();
            }
            else if (skidka == 1)
            {
                _services = _services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
            }
            else if (skidka == 2)
            {
                _services = _services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
            }
            else if (skidka == 3)
            {
                _services = _services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
            }
            else if (skidka == 4)
            {
                _services = _services.Where(x => x.Discount >= 70).ToList();
            }

            //цена
            if (cb.SelectedIndex == 0)
            {
                cena = 0;
                _services = _services.OrderBy(x => x.Cost).ToList();
            }
            else if (cb.SelectedIndex == 1)
            {
                cena = 1;
                _services = _services.OrderByDescending(x => x.Cost).ToList();
            }
            else if (cb.SelectedIndex == 2)
            {
                cena = 2;
                _services = Services.ToList();
            }

            list.ItemsSource = _services;
            count.Content = "Количество доступных услуг: " + _services.Count() + " из " + countOfServices;
        }

        private void Discount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            _services = new ObservableCollection<Service>(Services).ToList();
            //поиск
            if (search.Text != "")
            {
                _services = _services.Where(x => x.Title.ToUpper().Contains(search.Text.ToUpper())).ToList();
            }

            //цена
            if (cena == 0)
            {
                _services = _services.OrderBy(x => x.Cost).ToList();
            }
            else if (cena == 1)
            {
                _services = _services.OrderByDescending(x => x.Cost).ToList();
            }
            else if (cena == 2)
            {
                _services = Services.ToList();
            }

            //скидка
            if (cb.SelectedIndex == 0)
            {
                skidka = 0;
                _services = _services.Where(x => x.Discount < 5).ToList();
            }
            else if (cb.SelectedIndex == 1)
            {
                skidka = 1;
                _services = _services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
            }
            else if (cb.SelectedIndex == 2)
            {
                skidka = 2;
                _services = _services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
            }
            else if (cb.SelectedIndex == 3)
            {
                skidka = 3;
                _services = _services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
            }
            else if (cb.SelectedIndex == 4)
            {
                skidka = 4;
                _services = _services.Where(x => x.Discount >= 70).ToList();
            }

            list.ItemsSource = _services;
            count.Content = "Количество доступных услуг: " + _services.Count() + " из " + countOfServices;
        }
        
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Service service = btn.DataContext as Service;
            EditServiceWindow edit = new EditServiceWindow(service);
            edit.ShowDialog();
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Service service = btn.DataContext as Service;
            if (ClientServices.Any(x => x.ServiceID == service.ID) == false)
            {
                //File.Delete(service.MainImagePath);
                Connection.Database.Service.Remove(service);
                Connection.Database.SaveChanges();
                Update();
            }
        }

        private void Make_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Service service = btn.DataContext as Service;
            NewAppointmentWindow newAppointment = new NewAppointmentWindow(service);
            newAppointment.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewServiceWindow newService = new NewServiceWindow();
            newService.ShowDialog();
            Update();
        }
        private void Update()
        {
            Services = new ObservableCollection<Service>(Connection.Database.Service);
            _services = Services.ToList();
            list.ItemsSource = _services;
            countOfServices = _services.Count();
            count.Content = "Количество доступных услуг: " + _services.Count() + " из " + countOfServices;
            search.Text = "";
            cb1.SelectedItem = null;
            cb2.SelectedItem = null;
        }

        private void search_KeyUp(object sender, KeyEventArgs e)
        {
            _services = new ObservableCollection<Service>(Services).ToList();
            //цена
            if (cena == 0)
            {
                _services = _services.OrderBy(x => x.Cost).ToList();
            }
            else if (cena == 1)
            {
                _services = _services.OrderByDescending(x => x.Cost).ToList();
            }

            //скидка
            if (skidka == 0)
            {
                _services = _services.Where(x => x.Discount < 5).ToList();
            }
            else if (skidka == 1)
            {
                _services = _services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
            }
            else if (skidka == 2)
            {
                _services = _services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
            }
            else if (skidka == 3)
            {
                _services = _services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
            }
            else if (skidka == 4)
            {
                _services = _services.Where(x => x.Discount >= 70).ToList();
            }

            _services = _services.Where(x => x.Title.ToUpper().Contains(search.Text.ToUpper())).ToList();
            list.ItemsSource = _services;
            count.Content = "Количество доступных услуг: " + _services.Count() + " из " + countOfServices;
        }

        private void near_Click(object sender, RoutedEventArgs e)
        {
            ApointmentsWindow apointmentsWindow = new ApointmentsWindow();
            apointmentsWindow.ShowDialog();
        }

        private void cencal_Click(object sender, RoutedEventArgs e)
        {
            cena = 2;
            skidka = 5;
            cb1.SelectedItem = null;
            cb2.SelectedItem = null;
            search.Text = "";
            _services = new ObservableCollection<Service>(Services).ToList();
            list.ItemsSource = _services;
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            if (kod.Password == "0000")
            {
                lab.Content = "Sign";
            }
        }
    }
}
