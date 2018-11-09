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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для BookingGried.xaml
    /// </summary>
    public partial class BookingGried : UserControl
    {
        public BookingGried()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            string sql = "SELECT Registration.RegistrationID, " +
           "Registration.PaymentL, " +
           "Registration.PaymentS, " +
           "Registration.TotalSumD, " +
            "Registration.EmployeeID, " +
            "Employee.FullName " +
            "FROM Registration " +
            "JOIN Employee ON(Registration.EmployeeID=Employee.EmployeeID) WHERE (Registration.Booked = 'Забронировал')";

            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;
        }
        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
