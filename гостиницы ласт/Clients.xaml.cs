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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : UserControl
    {
        static public DataRowView row;
        static public int index;
        public DataRowView rows
        {
            get { return row; }
            set { row = value; }
        }
        public Clients()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            string sql = "SELECT Client.ClientID, Client.FullNameClient, Client.Sex, " +
            "Client.DOB, "+
            "Client.Address, " +
            "Client.Tel, " +
            "Client.Email, " +
            "Document.Type, " +
            "Document.Code, " +
            "Document.Number, " +
            "Document.DI, " +
            "Document.IssuedBy " +
            "FROM Client JOIN Document ON(Client.DocumentID=Document.DocumentID) ";

            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;
        }
        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            row = DataGridTotal.SelectedItem as DataRowView;
            index = Convert.ToInt32(row.Row.ItemArray[0]);
        }

    }
}
