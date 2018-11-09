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
    /// Логика взаимодействия для WindowReport.xaml
    /// </summary>
    public partial class WindowReport : Window
    {
        public WindowReport()
        {
            InitializeComponent();
            start();
        }
        public void start()
        {
            addEmployees();
        }
        private void addEmployees() // добавление тех сотрудников, которые есть в комбо бокс
        {
            string sql = "SELECT Employee.FullName " +
           "FROM Employee WHERE (Employee.Post = 'Управляющий' OR Employee.Post = 'Администратор' OR Employee.Post = 'Менеджер по бронированию')";

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
                EmloyeeIDCB.Items.Add((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[0]);
        }

        private void CreateBN_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDateTime(LastDay.Text)<Convert.ToDateTime(StartDay.Text)) { MessageBox.Show("Ошибка даты"); return; }
            try
            {
                if (EmloyeeIDCB.Text == null || StartDay.Text == null || LastDay.Text == null || EmloyeeIDCB.Text == "")
                {
                    MessageBox.Show("Проверте введенные данные");
                    return;
                }
                WorkWithWord.startReport(EmloyeeIDCB.Text, Convert.ToDateTime(StartDay.Text), Convert.ToDateTime(LastDay.Text), "Report");
                Close();
            }
            catch { MessageBox.Show("Проверте введенные данные или закройте документ с отчетом"); }
        }

        private void CancelBN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmloyeeIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
