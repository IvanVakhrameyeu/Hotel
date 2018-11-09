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
    /// Логика взаимодействия для WindowEditMainMenu.xaml
    /// </summary>
    public partial class WindowEditMainMenu : Window
    {
        public WindowEditMainMenu()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            addBooked();
            try
            {
                RegistationTB.Text = MainMenu.row.Row.ItemArray[0].ToString();
                RegistationTB.IsEnabled = false;
                BookedCB.Text = MainMenu.row.Row.ItemArray[4].ToString();
            }
            catch { MessageBox.Show("Не выбран клиент"); Close(); }
        }
        private void addBooked()
        {
            BookedCB.Items.Add("Заселился");
            BookedCB.Items.Add("Выселился");
            BookedCB.Items.Add("Забронировал");
        }
        private void BookedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            try { 
            string sql = "UPDATE [Registration] SET [Booked] = '" + BookedCB.Text + "' " +
                "WHERE [RegistrationID] = " + MainMenu.index;

            WorkWithBD.Update(sql);
            sql = "SELECT RoomID FROM Registration WHERE RegistrationID="+ MainMenu.index;
            sql = "UPDATE [Room] SET [Status] ='" + BookedCB.Text + "' WHERE RoomID=" + (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[0];
            WorkWithBD.Update(sql);

            Close();
        }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
}

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
