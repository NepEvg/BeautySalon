using Beauty_Saloon.Model;
using Microsoft.Win32;
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

namespace Beauty_Saloon.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        public Service _service { get; set; }
        public ObservableCollection<Service> Services { get; set; }
        string selectedFileName = "";
        Service serv = new Service();
        public EditServiceWindow(Service service)
        {
            _service = service;
            Services = new ObservableCollection<Service>(Connection.Database.Service);
            serv = Connection.Database.Service.Where(x => x.ID == _service.ID).FirstOrDefault();
            InitializeComponent();
            id.Text = _service.ID.ToString();
            title.Text = _service.Title;
            descript.Text = _service.Description;
            cost.Text = _service.Cost.ToString();
            discount.Text = _service.Discount.ToString();
            duration.Text = _service.DurationInSeconds.ToString();
            img.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + _service.MainImagePathSet.ToString()));
            if (_service.DopImagesPath1Set.Length > 24)
            {
                img2.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + _service.DopImagesPath1Set.ToString()));
            }
            if (_service.DopImagesPath2Set.Length > 24)
            {
                img3.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + _service.DopImagesPath2Set.ToString()));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (title.Text != null && cost.Text != null && duration.Text != null)
            {
                serv.Title = title.Text;
                serv.Description = descript.Text;
                serv.Cost = Convert.ToDecimal(cost.Text);
                if (discount.Text != "")
                {
                    serv.Discount = Convert.ToDouble(discount.Text);
                }
                serv.DurationInSeconds = Convert.ToInt32(duration.Text);
                if (selectedFileName != "")
                {
                    serv.MainImagePath = selectedFileName;
                }
                Connection.Database.SaveChanges();
                this.Close();
            }
            else { MessageBox.Show("Поля названия, цены и длительности должны быть заполнены.", "Неверные данные"); }
        }

        private void AddImg_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg)|*.jpg|Image files (*.png)|*.png|All Files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {
                selectedFileName = dialog.FileName;
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
            }
            img.Source = new BitmapImage(new Uri(dialog.FileName));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            serv.dopImagesPath1 = null;
            serv.dopImagesPath2 = null;
            Connection.Database.SaveChanges();
            img2.Source = null;
            img3.Source = null;
        }
    }
}
