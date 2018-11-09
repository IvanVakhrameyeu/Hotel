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
    /// Логика взаимодействия для CheckFreeRoom.xaml
    /// </summary>
    public partial class CheckFreeRoom : UserControl
    {
        public CheckFreeRoom()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            string sql = "SELECT Registration.RegistrationID, Registration.RoomID, Type.Class, Type.DayCost, Registration.AD, Registration.DD " +
                  "FROM Registration JOIN Room ON(Registration.RoomID = Room.RoomID) JOIN Type ON(Room.TypeID = Type.TypeID) " +
                  "WHERE(Registration.RoomID = " + WindowAddRegistration.sRoomIDCB + ")";


            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;
        }

        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
