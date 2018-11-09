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
using Xceed.Wpf.Toolkit;
namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для WindowAddClient.xaml
    /// </summary>
    public partial class WindowAddClient : Window
    {
        public WindowAddClient()
        {
            InitializeComponent();

            //BirthdayDT.Maximum = (DateTime.Today).AddYears(-18);
        }

        private void AddBN_Click(object sender, RoutedEventArgs e) // кнопка добавление клиента
        {
            try
            {
                if ((SexFemaleRB.IsChecked==false && SexMaleRB.IsChecked==false) || FullNameTB.Text=="" || NumberTB.Text == "" || IssuedByTB.Text == "" || AddresTB.Text == "" || CodeTB.Text == "" || TypeTB.Text == "")
                { System.Windows.Forms.MessageBox.Show("Заполните поля"); return; }
                string sql = "INSERT INTO Document (Type,Code,Number,DI,IssuedBy) " +
             "VALUES (@Type, @Code,@Number,@DI,@IssuedBy)";

                string Sex = (SexFemaleRB.IsChecked == true) ? "М" : "Ж";
                string sqlForClient = "INSERT INTO Client (DocumentID,FullNameClient, Sex,DOB,Address,Tel,Email) " +
           "VALUES (@DocumentID,@FullNameClient, @Sex,@DOB,@Address,@Tel,@Email)";
                WorkWithBD.inPutClientdb(sql, TypeTB.Text, CodeTB.Text, NumberTB.Text, Convert.ToDateTime(DayIDT.Text), IssuedByTB.Text,
                    sqlForClient, FullNameTB.Text, Sex, Convert.ToDateTime(BirthdayDT.Text), AddresTB.Text, TelTB.Text, EmailTB.Text);

                Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
        }

        private void CancelBN_Click(object sender, RoutedEventArgs e) // кнопка закрытия
        {
            Close();
        }

        private void SexMaleRB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SexFemaleRB_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
