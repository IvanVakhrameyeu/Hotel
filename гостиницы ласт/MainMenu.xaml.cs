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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            //try
            //{
                InitializeComponent();
                start();
            //}
            //catch { }
        }
        static public DataRowView row;
        static public int index;
        public DataRowView rows
        {
            get { return row; }
            set { row = value; }
        }
        private void start()
        {
            string sql = "SELECT Registration.RegistrationID, " + 
           "Registration.PaymentL, " +
           "Registration.PaymentS, " +
           "Registration.TotalSumD, " +
           "Registration.Booked, " +
           "Registration.AD, " +
           "Registration.DD, " +
           "Employee.FullName, " +
           "Client.FullNameClient, " +
           "Room.RoomID " +
           "FROM Registration JOIN Employee ON(Registration.EmployeeID=Employee.EmployeeID) " +
           "JOIN Client ON(Registration.ClientID=Client.ClientID)" +
           "JOIN Room ON(Registration.RoomID=Room.RoomID) WHERE (Registration.Booked='Заселился' OR Registration.Booked='Забронировал')";

            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;
        }

        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            row = DataGridTotal.SelectedItem as DataRowView;
            index = Convert.ToInt32(row.Row.ItemArray[0]);
        }
    }
}
