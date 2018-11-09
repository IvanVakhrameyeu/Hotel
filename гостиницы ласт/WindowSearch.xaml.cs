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

using System.Data.SqlClient;

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для WindowSearch.xaml
    /// </summary>
    public partial class WindowSearch : Window
    {
        public static string InfoSearch;
        public WindowSearch()
        {
            InitializeComponent();
            start();
        }
        private void start()
        {
            addChooseSearch();
        }
        private void addChooseSearch()
        {
            //SearchCB.Items.Add("Свободные номера в данный день");
            SearchCB.Items.Add("Услуги");
            SearchCB.Items.Add("Клиента");
        }
        private void DataGridSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void SearchCB_SelectionChanged(object sender, SelectionChangedEventArgs e) // комбо бокс критериев поиска 
        {
            
        }
        private void SearchBN_Click(object sender, RoutedEventArgs e) // кнопка поиска, при нажатии идет проверка с посл выводом инфы
        {
            GridSearch.Children.Clear();
            try
            {
                InfoSearch = Convert.ToString(InfoSearchTB.Text);
            }
            catch {}
            switch (SearchCB.Text)
            {
                //case "Свободные номера в данный день":
                //    freeRoom();
                //    break;
                case "Услуги":
                    service();
                    break;
                case "Клиента":
                    client();
                    break;
                default: break;
            }
        }
        private void client()
        {
            //try
            //{
                GridSearch.Children.Add(new SearchClient());
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка введенных данных");
            //}
        }
        private void service()
        {
            //try
            //{
            GridSearch.Children.Add(new SearchService());
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка введенных данных");
            //}
        }
        private void CloseBN_Click(object sender, RoutedEventArgs e) // кнопка закрыть
        {
            Close();
        }
    }
}
