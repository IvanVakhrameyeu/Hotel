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
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace гостиницы_ласт
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool autoruzade;
        public static string USER;
        public MainWindow()
        {
            



            WindowAutorization windowAutorization = new WindowAutorization();
            windowAutorization.ShowDialog();
            
            if (autoruzade == false) Close();
            else
            {
                MessageBox.Show("Добро пожаловать " + USER);
                InitializeComponent();
                start(); // стартовая функция подсчета сумм
            }
        }
        private void fillingPayment()
        {
            string sql = "SELECT Registration.RegistrationID " +
           "FROM Registration";

            for (int i = 0; i < Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].DefaultView.Count); i++)
            {
                WorkWithBD.sumForRoom(Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]));
                WorkWithBD.sumForService(Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]), 0);
                WorkWithBD.TotalSum(Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0]));
            }
        }
        public void start() // запуск функций подсчета сумм
        {
            ListViewMenu.SelectedIndex = 0;
            fillingPayment();
        }
        private void DataGridTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        
        private void addNewClient_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void invisible() // скрытие кнопок добавить изменить удалить
        {
            AddBN.Visibility = Visibility.Collapsed;
            EditBN.Visibility = Visibility.Collapsed;
            DelBN.Visibility = Visibility.Collapsed;
        }
        private void visible() // показать кнопки добавить изменить удалить
        {
                AddBN.Visibility = Visibility.Visible;
                EditBN.Visibility = Visibility.Visible;
                DelBN.Visibility = Visibility.Visible;
        }
        private void checkVisible()// проверка на были ли скрыты кнопки добавить ....
        {
            if (AddBN.Visibility is Visibility.Collapsed) { visible();}
        }
        private void ListViewMenu_SelectionChanged(object sender, RoutedEventArgs e) // нажатие на кнопку слева на меню
        {
            ContentGrid.Children.Clear();
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная
                    checkVisible();
                    SearchBN.Visibility = Visibility.Visible;
                    RefreshBN.Visibility = Visibility.Visible;
                    ContentGrid.Children.Add(new MainMenu());
                    break;
                case 1: //свободные номера
                    SearchBN.Visibility = Visibility.Collapsed;
                    RefreshBN.Visibility = Visibility.Visible;
                    invisible();
                    EditBN.Visibility = Visibility.Visible;
                    ContentGrid.Children.Add(new AvailableRooms());
                    break;
                case 2: // услуги
                    SearchBN.Visibility = Visibility.Collapsed;
                    RefreshBN.Visibility = Visibility.Visible;
                    checkVisible();
                    ContentGrid.Children.Add(new Service());
                    break;
                case 3: // услуги клиентов
                    SearchBN.Visibility = Visibility.Collapsed;
                    RefreshBN.Visibility = Visibility.Visible;
                    checkVisible();
                    ContentGrid.Children.Add(new ServiceClients());
                    break;
                case 4:  // информация о клиентах(вся инфа + кол-во посещений и мб сумма дней жизни)
                    RefreshBN.Visibility = Visibility.Visible;
                    SearchBN.Visibility = Visibility.Collapsed;
                    checkVisible();
                    ContentGrid.Children.Add(new Clients());
                    break;
                case 5:  //отчет
                    RefreshBN.Visibility= Visibility.Collapsed;
                    SearchBN.Visibility = Visibility.Collapsed;
                    Report.start();
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(Report.ReportButton);
                    ContentGrid.Children.Add(Report.StatementButton);
                    invisible();
                    break;
                case 6:  // справка
                    invisible();
                    ListViewMenu.SelectedIndex = 0;
                    try
                    {
                        Process.Start("Help.chm");
                    }
                    catch{ MessageBox.Show("Справка не найдена"); } break;
                default: break;
            }
        }
        private void AddBN_Click(object sender, RoutedEventArgs e) // нажатие кнопки добавить при различных менюшек слева
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная
                    WindowAddRegistration windowAddRegistration = new WindowAddRegistration();
                    windowAddRegistration.Show();

                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MainMenu());
                    break;
                case 1: //свободные номера
                    break;
                case 2: // услуги
                    WindowAddService windowAddService = new WindowAddService();
                    windowAddService.Show();
                    
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Service());
                    break;
                case 3: // услуги клиентов
                    WindowAddServiceClient windowAddServiceClient = new WindowAddServiceClient();
                    windowAddServiceClient.Show();

                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new ServiceClients());
                    break;
                case 4:  // информация о клиентах (при добавлении клиента не обновляется грид, при изменении удалении обновляется)
                    WindowAddClient windowAddClient = new WindowAddClient();
                    windowAddClient.Show();

                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Clients());
                    break;
                case 5:  //отчет
                    break;
                case 6:  // справка
                    break;
                default:
                    MessageBox.Show("Не выбран пункт слева"); break;
            }
        }
        private void EditBN_Click(object sender, RoutedEventArgs e) // нажатие кнопки изменить при различных менюшек слева
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная
                    WindowEditMainMenu windowEditMainMenu = new WindowEditMainMenu();
                    try
                    {
                        windowEditMainMenu.Show();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MainMenu());
                    break;
                case 1: //свободные номера
                    WindowEditAvailableRooms windowEditAvailableRooms = new WindowEditAvailableRooms();
                    try
                    {
                        windowEditAvailableRooms.Show();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new AvailableRooms());
                    break;
                case 2: // услуги
                    WindowEditService windowEditService = new WindowEditService();
                    try
                    {
                        windowEditService.Show();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Service());
                    break;
                case 3: // услуги клиентов
                    checkVisible();
                    WindowEditServiceClient windowEditServiceClient = new WindowEditServiceClient();
                    try
                    {
                        windowEditServiceClient.Show();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new ServiceClients());
                    break;
                case 4:  // информация о клиентах
                    WindowEditClient windowEditClient = new WindowEditClient();
                    try
                    {
                        windowEditClient.Show();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Clients());
                    break;
                case 5:  //отчет

                    break;
                case 6:  // справка
                    break;
                default:
                    MessageBox.Show("Не выбран пункт слева"); break;
            }
        }
        private void DelBN_Click(object sender, RoutedEventArgs e) // нажатие кнопки удалить при различных менюшек слева
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная
                    WorkWithBD.removeRegistrationdb(MainMenu.index);
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MainMenu());
                    break;
                case 1: //свободные номера
                    break;
                case 2: // услуги 
                    WorkWithBD.removeServicedb(Service.index);
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Service());
                    break;
                case 3: // услуги клиентов

                    try
                    {
                        string sql = "SELECT Status FROM ServiceList WHERE ServiceListID=" + ServiceClients.index;
                        if (WorkWithBD.outPutdb(sql).Tables[0].DefaultView[0].Row[0].ToString() == "Выполнено")

                        {
                            MessageBox.Show("Эта услуга оказана, нельзя удалить");
                        }


                        else
                        {
                            WorkWithBD.removeServiceListGooddb(ServiceClients.index);
                            ContentGrid.Children.Clear();
                            ContentGrid.Children.Add(new ServiceClients());
                        }
                    }
                    catch { }
                    break;
                case 4:  // информация о клиентах
                    WorkWithBD.removeClientdb(Clients.index);
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Clients());
                    break;
                case 5:  //отчет
                    break;
                case 6:  // справка
                    break;
                default:
                    MessageBox.Show("Не выбран пункт слева"); break;
            }
        }
        private void SearchBN_Click(object sender, RoutedEventArgs e) // нажатие кнопки поиск при различных менюшек слева
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная
                    WindowSearch windowSearch = new WindowSearch();
                    try
                    {
                        windowSearch.Show();
                    }
                    catch { }
                    break;
                case 1: //свободные номера
                    break;
                case 2: // услуги
                  
                    break;
                case 3: // услуги клиентов
                  
                    break;
                case 4:  // информация о клиентах
                    break;
                case 5:  //отчет
                    break;
                case 6:  // справка
                    break;
                default:
                    MessageBox.Show("Не выбран пункт слева"); break;
            }
        }
        private void RefreshBN_Click(object sender, RoutedEventArgs e) // нажатие кнопки обновить при различных менюшек слева
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0: // главная 
                    try
                    {
                        start();
                    }
                    catch { }
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MainMenu());
                    break;
                case 1: //свободные номера
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new AvailableRooms());
                    break;
                case 2: // услуги
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Service());
                    break;
                case 3: // услуги клиентов
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new ServiceClients());
                    break;
                case 4:  // информация о клиентах
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Clients());
                    break;
                case 5:  //отчет
                    break;
                case 6:  // справка
                    break;
                default:
                    MessageBox.Show("Не выбран пункт слева"); break;
            }
        }
    }
    public class Report
    {
        static public Button ReportButton; // отчет сотрудника
        static public Button StatementButton; // выписка клиенту 
        static public void start()
        {
            createButton();

        }
        private static void createButton()
        {
            ReportButton = new Button();
            ReportButton.Width = 220;
            ReportButton.Margin = new Thickness(300, 0, 0, 0);
            ReportButton.Content = "Форма-отчет сотрудника";
            ReportButton.Click += ReportButton_Click;
            ReportButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 80, 178, 226));
            ReportButton.Background = new SolidColorBrush(Color.FromArgb(255, 80, 178, 226));
            ReportButton.FontSize = 16;

            StatementButton = new Button();
            StatementButton.Width = 220;
            StatementButton.Margin = new Thickness(-300, 0, 0, 0);
            StatementButton.Content = "Форма-выписка клиента";
            StatementButton.Click += StatementButton_Click;
            StatementButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 80, 178, 226));
            StatementButton.Background = new SolidColorBrush(Color.FromArgb(255, 80, 178, 226));
            StatementButton.FontSize = 16;
        }
        private static void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            WindowReport windowReport = new WindowReport();
            try
            {
                windowReport.Show();
            }
            catch { }
        }
        private static void StatementButton_Click(object sender, RoutedEventArgs e)
        {
            WindowStatement windowStatement = new WindowStatement();
            try
            {
                windowStatement.Show();
            }
            catch { }
        }

    }
}
