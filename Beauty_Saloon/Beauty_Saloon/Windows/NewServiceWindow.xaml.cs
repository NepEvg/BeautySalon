using Beauty_Saloon.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
using System.Windows.Shapes;

namespace Beauty_Saloon
{
    /// <summary>
    /// Логика взаимодействия для NewServiceWindow.xaml
    /// </summary>
    public partial class NewServiceWindow : Window
    {
        public List<Service> services { get; set; }
        List<Uri> list_f = new List<Uri>();
        string selectedFileName = "";

        Service service = new Service();
        public NewServiceWindow()
        {
            services = new List<Service>(Connection.Database.Service);
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (title.Text != "" && cost.Text != "" && duration.Text != "")
            {
                if (services.Any(x => x.Title == title.Text) == false)
                {
                    if (Convert.ToInt32(duration.Text) > 0 && Convert.ToInt32(duration.Text) <= 240)
                    {
                        if (Convert.ToInt32(discount.Text) < 95 && Convert.ToInt32(discount.Text) > -1)
                        {
                            service.Title = title.Text;
                            service.Description = descript.Text;
                            service.Cost = Convert.ToDecimal(cost.Text);
                            if (discount.Text != "")
                            {
                                service.Discount = Convert.ToDouble(discount.Text);
                            }
                            service.DurationInSeconds = Convert.ToInt32(duration.Text);
                            service.MainImagePath = selectedFileName;
                            Connection.Database.Service.Add(service);
                            Connection.Database.SaveChanges();
                            this.Close();
                        }
                        else { MessageBox.Show("Такая скидка невозможна.", "Неверные данные"); }
                    }
                    else { MessageBox.Show("Продолжительность сеанса может быть не дольше 4 часов.", "Неверные данные"); }
                }
                else { MessageBox.Show("Такая услуга уже существует.", "Неверные данные"); }
            }
            else { MessageBox.Show("Поля названия, цены и длительности должны быть заполнены.", "Неверные данные"); }
        }

        private void AddImg_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog file_d = new OpenFileDialog();
            file_d.Multiselect = false;
            file_d.Title = "Выбор изображения";
            file_d.Filter = "Image files (*.jpg)|*.jpg|Image files (*.png)|*.png|All Files (*.*)|*.*";
            bool flag = false;
            if (file_d.ShowDialog() == true)
            {
                string pathOfImg = "Услуги\\" + file_d.SafeFileName;
                selectedFileName = pathOfImg;
                if (File.Exists(pathOfImg))
                {
                    flag = true;
                }
                if (flag != true)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    FileStream fileStream = new FileStream(pathOfImg, FileMode.Create);
                    var bitmap = new BitmapImage(new Uri(file_d.FileName));
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(fileStream);
                    fileStream.Close();
                }
                img.Source = new BitmapImage(new Uri(file_d.FileName));
            }
        }
        int cnt = 1;
        private void AddImg2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file_d = new OpenFileDialog();
            file_d.Multiselect = true;
            file_d.Title = "Выбор изображений";
            file_d.Filter = "Image files (*.jpg)|*.jpg|Image files (*.png)|*.png|All Files (*.*)|*.*";
            bool flag = false;
            if (file_d.ShowDialog() == true)
            {
                string pathOfImg = "Услуги\\" + file_d.SafeFileName;
                selectedFileName = pathOfImg;
                if (File.Exists(pathOfImg))
                {
                    flag = true;
                }
                if (flag != true)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    FileStream fileStream = new FileStream(pathOfImg, FileMode.Create);
                    var bitmap = new BitmapImage(new Uri(file_d.FileName));
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(fileStream);
                    fileStream.Close();
                }
                if (cnt == 1)
                {
                    AddImg2(file_d, selectedFileName);
                }
                if (cnt == 2)
                {
                    AddImg3(file_d, selectedFileName);
                }
            }
        }
        private void AddImg2(OpenFileDialog file_d, string selectedFileName)
        {
            img2.Source = new BitmapImage(new Uri(file_d.FileName));
            service.DopImagesPath1 = selectedFileName;
            cnt++;
        }
        private void AddImg3(OpenFileDialog file_d, string selectedFileName)
        {
            img3.Source = new BitmapImage(new Uri(file_d.FileName));
            service.DopImagesPath2 = selectedFileName;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
