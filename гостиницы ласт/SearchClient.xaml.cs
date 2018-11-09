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
    /// Логика взаимодействия для SearchClient.xaml
    /// </summary>
    public partial class SearchClient : UserControl
    {
        public SearchClient()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {


            string sql = "SELECT Client.ClientID, Client.FullNameClient, Client.Sex, " +
            "Client.DOB, " +
            "Client.Address, " +
            "Client.Tel, " +
            "Client.Email, " +
            "Document.Type, " +
            "Document.Code, " +
            "Document.Number, " +
            "Document.DI, " +
            "Document.IssuedBy " +
            "FROM Client JOIN Document ON(Client.DocumentID=Document.DocumentID) Where  Client.FullNameClient  Like  '%" + WindowSearch.InfoSearch + "%'";                                                                                                                                                                                                                                                               // "UNION " + "SELECT Room.RoomID, Type.Class, " + "Type.DayCost " +


          
            for(int i=0;i < (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count;i++)
            {
                
                  
                    DataGridClient.Items.Add((WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i]));

            }
          
        }
        

      

        private void DataGridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
