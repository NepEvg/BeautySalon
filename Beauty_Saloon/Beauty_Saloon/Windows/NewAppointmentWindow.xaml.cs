using Beauty_Saloon.Model;
using System;
using System.Collections.Generic;
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

namespace Beauty_Saloon
{
    /// <summary>
    /// Логика взаимодействия для NewAppointmentWindow.xaml
    /// </summary>
    public partial class NewAppointmentWindow : Window
    {
        public List<Client> clients { get; set; }
        public Service _service { get; set; }
        public NewAppointmentWindow(Service service)
        {
            _service = service;
            clients = new List<Client>(Connection.Database.Client);
            InitializeComponent();
            client_cb.ItemsSource = clients;
            title.Text = _service.Title;
            duration.Text = _service.DurationInSeconds.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (time.Text != "" && client_cb.SelectedItem != null && date.Text != "")
            {
                string t = time.Text;
                int hour = Convert.ToInt32(t[0].ToString() + t[1]);
                int min = Convert.ToInt32(t[3].ToString() + t[4]);
                if (t.Length == 5 && t[2] == ':' && hour < 22 && hour > 7 && min < 60 && min >= 0)
                {
                    ClientService cs = new ClientService();
                    Client client = clients.Where(c => c.FirstName + " " + c.LastName + " " + c.Patronymic == clients[client_cb.SelectedIndex].FirstName + " " + clients[client_cb.SelectedIndex].LastName
                                + " " + clients[client_cb.SelectedIndex].Patronymic).FirstOrDefault();
                    cs.ClientID = client.ID;
                    cs.ServiceID = _service.ID;
                    cs.StartTime = Convert.ToDateTime(date.Text + " " + time.Text);
                    DateTime dt = Time();
                    cs.EndTime = dt;
                    Connection.Database.ClientService.Add(cs);
                    Connection.Database.SaveChanges();
                    MessageBox.Show("Запись добавлена.", "Успешно");
                    this.Close();
                }
                else { MessageBox.Show("Время начала указано некорректно.", "Неверные данные"); }
            }
            else { MessageBox.Show("Все поля должны быть заполнены.", "Неверные данные"); }
        }
        private DateTime Time()
        {
            DateTime dt = Convert.ToDateTime(date.Text + " " + time.Text);
            DateTime dt2 = dt.AddMinutes(_service.DurationInSeconds);
            return dt2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
