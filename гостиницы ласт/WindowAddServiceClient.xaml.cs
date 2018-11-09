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
    /// Логика взаимодействия для WindowAddServiceClient.xaml
    /// </summary>
    public partial class WindowAddServiceClient : Window
    {
        public WindowAddServiceClient()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            addRegistration();
            addService();
        }
        private void addRegistration()
        {
            string sql = "SELECT Registration.RegistrationID, Client.FullNameClient " +
         "FROM Registration JOIN Client ON(Registration.ClientID=Client.ClientID) WHERE (Registration.Booked = 'Заселился' OR Registration.Booked = 'Забронировал')";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                RegistrationIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]+ " " + (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[1].ToString());
            }            
        }
        private void addService()
        {
            string sql = "SELECT Service.Service " +
         "FROM Service";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                ServiceIDCB.Items.Add((i+1).ToString() + " " +(WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
            }
        }
        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO ServiceList (RegistrationID,ServiceID, Status) " +
     "VALUES (@RegistrationID,@ServiceID,@Status)";
            try { 
            WorkWithBD.inPutServiceClientdb(sql, Convert.ToInt32(RegistrationIDCB.Text.ToString().Split(' ')[0]), Convert.ToInt32(ServiceIDCB.Text.ToString().Split(' ')[0]),"Не выполнено");
        
            Close();}
            catch
            {
                MessageBox.Show("Ошибка при заполнении, проверьте введённые данные");           
            }
        }
        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void ServiceIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        
        private void RegistrationIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
