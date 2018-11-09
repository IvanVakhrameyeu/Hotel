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
using System.Windows.Shapes;

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для WindowEditClient.xaml
    /// </summary>
    public partial class WindowEditClient : Window
    {
        public WindowEditClient()
        {
            InitializeComponent();
            start();
        }

        public void start() // заполняет все поля выбранным айди
        {
            try {
                FullNameTB.Text = Clients.row.Row.ItemArray[1].ToString();
                SexMaleRB.IsChecked = (Clients.row.Row.ItemArray[2].ToString() == "М") ? true : false;
                SexFemaleRB.IsChecked = (Clients.row.Row.ItemArray[2].ToString() == "Ж") ? true : false;
                BirthdayDT.Text = Clients.row.Row.ItemArray[3].ToString().Split(' ')[0];
                AddresTB.Text = Clients.row.Row.ItemArray[4].ToString();
                TelTB.Text = Clients.row.Row.ItemArray[5].ToString();
                EmailTB.Text = Clients.row.Row.ItemArray[6].ToString();
                TypeTB.Text = Clients.row.Row.ItemArray[7].ToString();
                CodeTB.Text = Clients.row.Row.ItemArray[8].ToString();
                NumberTB.Text = Clients.row.Row.ItemArray[9].ToString();
                DayIDT.Text = (Clients.row.Row.ItemArray[10].ToString()).Split(' ')[0];
                IssuedByTB.Text = Clients.row.Row.ItemArray[11].ToString();
            }
            catch { MessageBox.Show("Не выбран клиент"); Close(); }
            }
        private void SaveBN_Click(object sender, RoutedEventArgs e) // сохранение изменений
        {
            try
            {
                if ((SexFemaleRB.IsChecked == false && SexMaleRB.IsChecked == false) || FullNameTB.Text == "" || NumberTB.Text == "" || IssuedByTB.Text == "" || AddresTB.Text == "" || CodeTB.Text == "" || TypeTB.Text == "")
                { System.Windows.Forms.MessageBox.Show("Заполните поля"); return; }

                string sql = "UPDATE [Document] SET [Type] = '" + TypeTB.Text + "', " +
          "[Code] = '" + CodeTB.Text + "', "+
          "[Number] = '" + NumberTB.Text + "', "+ 
          "[DI] = '" + Convert.ToString(Convert.ToDateTime(DayIDT.Text))  + "', " +
          "[IssuedBy] = '" + IssuedByTB.Text + "' " + "WHERE [DocumentID] = " + Clients.index;

            WorkWithBD.Update(sql);

            string sex = (SexMaleRB.IsChecked == true) ? "М" : "Ж";

            sql = "UPDATE [Client] SET [FullNameClient] = '" + FullNameTB.Text + "', " +
          "[Sex] = '" + sex + "', " +
          "[DOB] = '" + Convert.ToString(Convert.ToDateTime(BirthdayDT.Text)) + "', " +
          "[Address] = '" + AddresTB.Text + "', " +
          "[Tel] = '" + TelTB.Text + "', " +
          "[Email] = '" + EmailTB.Text + "' " + "WHERE [ClientID] = " + Clients.index;

            WorkWithBD.Update(sql);
            Close();
        }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
}

        private void CancelBN_Click(object sender, RoutedEventArgs e) //отмена изменений
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
