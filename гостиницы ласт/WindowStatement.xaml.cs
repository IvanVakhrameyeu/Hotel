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
    /// Логика взаимодействия для WindowStatement.xaml
    /// </summary>
    public partial class WindowStatement : Window
    {
        public WindowStatement()
        {
            InitializeComponent();
            start();
        }
        public void start()
        {
            addClient();

        }
        private void addClient()
        {
            string sql = "SELECT Client.FullNameClient " +
         "FROM Client";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
                ClientCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }
        private void addRegistration(string FullNameClient)
        {
            string sql = "SELECT Registration.RegistrationID " +
         "FROM Registration JOIN Client ON (Registration.ClientID = Client.ClientID) WHERE (Client.FullNameClient = '" + FullNameClient + "')";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
                RegistrationClientCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }
        private void RegistrationClientCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClientCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RegistrationClientCB.Items.Clear();
                addRegistration(ClientCB.SelectedValue.ToString());
        }
        private void CreateBN_Click(object sender, RoutedEventArgs e)
        {
            WorkWithWord.startStatment(ClientCB.Text, RegistrationClientCB.Text);
            Close();
        }
        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
