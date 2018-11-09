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
    /// Логика взаимодействия для WindowEditServiceClient.xaml
    /// </summary>
    public partial class WindowEditServiceClient : Window
    {
        public WindowEditServiceClient()
        {
            InitializeComponent();
            start();
        }
        private void addStatus()
        {
            StatusCB.Items.Add("Выполнено");
            StatusCB.Items.Add("Не выполнено");
        }
        private void addRegistration()
        {
            string sql = "SELECT Registration.RegistrationID, Client.FullNameClient " +
         "FROM Registration JOIN Client ON(Registration.ClientID=Client.ClientID) WHERE (Registration.Booked = 'Заселился' OR Registration.Booked = 'Забронировал')";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                RegistrationIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0] + " " + (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[1].ToString());
            }
        }
        private void addService()
        {
            string sql = "SELECT Service.Service " +
         "FROM Service";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                ServiceIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
            }
        }
        private void start()
        {
            addStatus();
            addRegistration();
            addService();
            try
            {
                RegistrationIDCB.Text = ServiceClients.row.Row.ItemArray[1].ToString();
                ServiceIDCB.Text = ServiceClients.row.Row.ItemArray[4].ToString();
                StatusCB.Text = ServiceClients.row.Row.ItemArray[6].ToString();

            }
            catch { MessageBox.Show("Не выбран клиент"); Close(); }
            if (StatusCB.Text == "Выполнено")
            {
                StatusCB.IsEnabled = false;
            }

        }

        private void StatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ServiceIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RegistrationIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ServiceIDCB.Text == "") { MessageBox.Show("Заполните название услуги"); return; }
                string sql = "UPDATE [ServiceList] SET [RegistrationID] = " + RegistrationIDCB.Text.ToString().Split(' ')[0] + ", " +
                "[ServiceID] = (SELECT [Service].ServiceID From [Service] Where Service.Service='" +ServiceIDCB.Text + "') , " +
                "[Status] = '" + StatusCB.Text + "' " +
                "WHERE [ServiceListID] = " + ServiceClients.index;
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
