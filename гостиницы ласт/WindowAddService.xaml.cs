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
    /// Логика взаимодействия для WindowAddService.xaml
    /// </summary>
    public partial class WindowAddService : Window
    {
        public WindowAddService()
        {
            InitializeComponent();
        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO Service (Service,Cost) " + "VALUES (@Service, @Cost)";
            try
            {
                if (ServiceTB.Text == "") { MessageBox.Show("Заполните название услуги"); return; }
                WorkWithBD.inPutServicedb(sql, ServiceTB.Text, Convert.ToDouble(CostTB.Text));
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при заполнении, проверьте введённые данные");           
            }
        
        }

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
