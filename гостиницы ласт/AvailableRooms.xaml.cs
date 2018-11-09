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
    /// Логика взаимодействия для AvailableRooms.xaml
    /// </summary>
    public partial class AvailableRooms : UserControl
    {

        static public DataRowView row;
        static public int index;
        public DataRowView rows
        {
            get { return row; }
            set { row = value; }
        }
        public AvailableRooms()
        {
            InitializeComponent();
            start();
        }

        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            row = DataGridTotal.SelectedItem as DataRowView;
            index = Convert.ToInt32(row.Row.ItemArray[0]);
        }
        private void start()
        {
            string sql = "SELECT Room.RoomID, " + "Type.Class, " +
            "Type.Berths, " +
            "Type.DayCost, " +
            "Type.Info, " +
            "Room.QQ " +
            "FROM Room JOIN Type ON(Room.TypeID=Type.TypeID)";
            DataGridTotal.ItemsSource = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView;
        }
    }
}
