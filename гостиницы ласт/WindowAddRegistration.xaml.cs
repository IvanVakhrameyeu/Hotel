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
    /// Логика взаимодействия для WindowAddRegistration.xaml
    /// </summary>
    public partial class WindowAddRegistration : Window
    {
        public static DateTime dDayAD;
        public static DateTime dDayDD;
        public static string sRoomIDCB;
        public WindowAddRegistration()
        {
            InitializeComponent();
            start();
        }
        public void start()
        {
            if (MainWindow.autoruzade == true) { EmloyeeIDCB.Text = MainWindow.USER; EmloyeeIDCB.IsEnabled = false;  }
            else
            {
                addEmployees();
            }
            addRooms();
            addClient();
            addStatus();

            DayAD.Minimum = DateTime.Today;
            DayDD.Minimum = DateTime.Today;
            DayAD.Text=DateTime.Today.ToString();
            DayDD.Text = DateTime.Today.ToString();
        }
        private void addStatus()
        {
            BookedIDCB.Items.Add("Заселился");
            BookedIDCB.Items.Add("Забронировал");
        }
        private void checkRoom() // проверка, свободна ли комната в данный промежуток дней или нет
        {
            string sql = "SELECT Registration.AD, Registration.DD " +
                 "FROM Registration JOIN Room ON(Registration.RoomID = Room.RoomID) JOIN Type ON (Room.TypeID = Type.TypeID) " +
                 "WHERE " +     //(((Registration.AD < '" + dDayAD + "' AND Registration.DD < '" + dDayAD + "' AND Registration.AD < '" + dDayDD + "' AND Registration.DD < '" + dDayDD + "') OR (Registration.AD > '" + dDayAD + "' AND Registration.DD < '" + dDayAD + "' AND Registration.AD < '" + dDayDD + "' AND Registration.DD < '" + dDayDD + "')) AND " +
                 "Registration.RoomID = " + sRoomIDCB + "";
            //if (WorkWithBD.outPutdb(sql).Tables[0].DefaultView.Count == 0) MessageBox.Show("Можно бронировать");
            //else { MessageBox.Show("Выберите другие дни"); }
            ////   WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0];
            int buf = 0;
            for (int i = 0; i < WorkWithBD.outPutdb(sql).Tables[0].DefaultView.Count; i++)
            {
                if (((Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]) < dDayAD && Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[1]) < dDayAD) 
                    && ((Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]) < dDayDD && Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[1]) < dDayDD)))
                    || ((Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]) > dDayAD && Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[1]) > dDayAD)
                    && ((Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]) > dDayDD && Convert.ToDateTime(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[1]) > dDayDD))))
                {
                    buf++;
                }
                else {MessageBox.Show("Выберите другие дни"); return; }
            }
            if(buf== WorkWithBD.outPutdb(sql).Tables[0].DefaultView.Count) { MessageBox.Show("Можно бронировать"); }
        }
        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Convert.ToDateTime(DayDD.Text) < Convert.ToDateTime((DateTime.Now).ToString("d")) || Convert.ToDateTime(DayAD.Text) < Convert.ToDateTime((DateTime.Now).ToString("d"))) { MessageBox.Show("Введите корректные даты1"); return; }
            if (Convert.ToDateTime(DayDD.Text) < Convert.ToDateTime(DayAD.Text))  { MessageBox.Show("Введите корректные даты2"); return; }
                if (BookedIDCB.Text=="") { MessageBox.Show("Заполните поле: статус регистрации"); return; }
                string sql = "INSERT INTO Registration (ClientID,RoomID, EmployeeID,Booked,AD,DD) " +
           "VALUES (@ClientID,@RoomID, @EmployeeID,@Booked,@AD,@DD)";
                WorkWithBD.inPutRegistrationdb(sql, Convert.ToDateTime(DayAD.Text), Convert.ToDateTime(DayDD.Text), ClientIDCB.Text, Convert.ToInt32(RoomIDCB.Text), EmloyeeIDCB.Text, BookedIDCB.Text);


             //   string update = "UPDATE [Room] SET [Status] ='" + BookedIDCB.Text + "' WHERE RoomID=" + RoomIDCB.Text;
             //   WorkWithBD.Update(update);

                Close();
        }
            catch { System.Windows.Forms.MessageBox.Show("Введите корректные данные"); }
}

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void addEmployees() // добавление тех сотрудников, которые есть в комбо бокс
        {
            string sql = "SELECT Employee.FullName " +
           "FROM Employee WHERE (Employee.Post = 'Управляющий' OR Employee.Post = 'Администратор' OR Employee.Post = 'Менеджер по бронированию')";
            
            for(int i=0;i< ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++) 
                EmloyeeIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }
        private void addClient() // добавление тех сотрудников, которые есть в комбо бокс
        {
            ClientIDCB.Items.Clear();
            string sql = "SELECT Client.FullNameClient " +
           "FROM Client";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
                ClientIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }
        private void addRooms() // добавление свободных комнат
        {
            string sql = "SELECT Room.RoomID " +
          "FROM Room";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
                RoomIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }
        private void RoomIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)// комбо бокс для комнат
        {
            //try
            //{
            //    ComboBox comboBox = (ComboBox)sender;
            //    ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            //    MessageBox.Show(selectedItem.Content.ToString());
            //}
            //catch { }
        }
        private void EmloyeeIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)// комбо бокс для сотрудников
        {
        }

        private void ClientIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BookedIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OutBN_Click(object sender, RoutedEventArgs e) // вывод свободных дат,для регистрации
        {
            try
            {
                if (DayDD.Text=="" || DayAD.Text=="" || RoomIDCB.Text=="") { MessageBox.Show("Заполните поля");return; }
                DataGrid.Children.Clear();
                dDayAD = Convert.ToDateTime(DayAD.Text);
                dDayDD = Convert.ToDateTime(DayDD.Text);
                if(dDayAD> dDayDD) { MessageBox.Show("Дата заселения не должна быть больше даты выселения"); return; }
                sRoomIDCB = RoomIDCB.Text;

                checkRoom();
                DataGrid.Children.Add(new CheckFreeRoom());
        }
            catch { MessageBox.Show("Проверьте даты"); }
}
    }
}
