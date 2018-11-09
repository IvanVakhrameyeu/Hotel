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

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для WindowEditService.xaml
    /// </summary>
    public partial class WindowEditService : Window
    {
        public WindowEditService()
        {
            InitializeComponent();
            start();
        }

        public void start() // заполняет все поля выбранным айди
        {
            try
            {
                ServiceIDTB.Text = Service.row.Row.ItemArray[0].ToString();
                ServiceTB.Text = Service.row.Row.ItemArray[1].ToString();
                CostTB.Text = Service.row.Row.ItemArray[2].ToString();
            }
            catch { MessageBox.Show("Не выбрана услуга"); Close(); }
        }
        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if(ServiceTB.Text=="") { MessageBox.Show("Заполните название услуги"); return; }
            string sql = "UPDATE [Service] SET [Service] = '" + ServiceTB.Text + "', " +
            "[Cost] = '" + CostTB.Text + "' " +"WHERE [ServiceID] = " + Service.index;

            WorkWithBD.Update(sql);
            Close();
        }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
}

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
