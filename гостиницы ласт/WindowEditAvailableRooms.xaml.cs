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
    /// Логика взаимодействия для WindowEditAvailableRooms.xaml
    /// </summary>
    public partial class WindowEditAvailableRooms : Window
    {
        public WindowEditAvailableRooms()
        {
            InitializeComponent();
            
            start();
        }

        public void start() // заполняет все поля выбранным айди
        {

            try
            {
                QQTB.Text = AvailableRooms.row.Row.ItemArray[5].ToString();
                
            }
            catch { System.Windows.Forms.MessageBox.Show("Выберите номер");
                Close(); }
        }
        private void AddBN_Click(object sender, RoutedEventArgs e) // сохранение изменений
        {
            try
            {
                string sql = "UPDATE [Room] SET [QQ] = '" + QQTB.Text + "' WHERE [RoomID] = " + AvailableRooms.index;

                WorkWithBD.Update(sql);
                
                Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
        }


        private void CancelBN_Click(object sender, RoutedEventArgs e) //отмена изменений
        {
            Close();
        }
    }
}
