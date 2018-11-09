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
    /// Логика взаимодействия для WindowAutorization.xaml
    /// </summary>
    public partial class WindowAutorization : Window
    {
        public WindowAutorization()
        {
            InitializeComponent();
        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT [Login],[Password], EmployeeID FROM Users";
                for (int i = 0; i < WorkWithBD.outPutdb(sql).Tables[0].DefaultView.Count; i++)
                {
                    if (LoginTB.Text == WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0].ToString() && PasswordTB.Password.ToString()== WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[1].ToString())
                    {
                        string id = WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[2].ToString();
                        sql = "SELECT FullName FROM Employee WHERE EmployeeID=" + id;
                        MainWindow.autoruzade = true;
                        MainWindow.USER = WorkWithBD.outPutdb(sql).Tables[0].DefaultView[0].Row[0].ToString();
                        Close();
                    }
                }
                if(MainWindow.autoruzade == false)
                MessageBox.Show("Неверный логин или пароль.");
            }
            catch { MessageBox.Show("Заполните поля"); }
        }

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.autoruzade = false;
            Close();
        }
    }
}
