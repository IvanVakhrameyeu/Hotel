using System;
using System.Collections.Generic;
using System.Data;
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

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для ServiceClients.xaml
    /// </summary>
    public partial class ServiceClients : UserControl
    {
        static public DataRowView row;
        static public int index;
        public DataRowView rows
        {
            get { return row; }
            set { row = value; }
        }
        public ServiceClients()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            string sql = "SELECT ServiceList.ServiceListID, Registration.RegistrationID,Service.ServiceID, Client.FullNameClient, Service.Service, " +
           "Service.Cost, ServiceList.Status " +
           "FROM ServiceList " +
           "JOIN Registration ON(ServiceList.RegistrationID=Registration.RegistrationID) " +
           "JOIN Client ON(Registration.ClientID=Client.ClientID) " +
           "JOIN Service ON(ServiceList.ServiceID=Service.ServiceID) WHERE (Registration.Booked = 'Заселился' OR Registration.Booked = 'Забронировал')";

            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;

        }
        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            row = DataGridTotal.SelectedItem as DataRowView;
            index = Convert.ToInt32(row.Row.ItemArray[0]);
        }
    }
}
