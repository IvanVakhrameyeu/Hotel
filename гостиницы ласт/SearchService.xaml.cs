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
    /// Логика взаимодействия для SearchService.xaml
    /// </summary>
    public partial class SearchService : UserControl
    {
        public SearchService()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {


            string sql = "SELECT Client.FullNameClient, Service.Service  " +
            "FROM Registration JOIN ServiceList ON (ServiceList.RegistrationID=Registration.RegistrationID)" +
            " JOIN Client ON (Client.ClientID=Registration.ClientID)" +
            " JOIN Service ON (Service.ServiceID=ServiceList.ServiceID) Where  Service  Like  '%"+ WindowSearch.InfoSearch + "%'";                                                                                                                                                                                                                                                               // "UNION " + "SELECT Room.RoomID, Type.Class, " + "Type.DayCost " +


           
            for (int i = 0; i < (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count; i++)
            {
                DataGridService.Items.Add((WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i]));

            }

        }

        

        private void DataGridService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
