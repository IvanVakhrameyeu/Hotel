using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Word = Microsoft.Office.Interop.Word; // для работы с вордом
using System.Reflection;                    // для работы с вордом


namespace гостиницы_ласт
{
    class WorkWithWord
    {
             private static Word.Application wordApplication; 
        private static Word.Document wordDocument;// для создание документа
        public static void startReport(string NameEmployee,DateTime StartDay, DateTime LastDay, string Teplate) // запус отчета работника
        {
            string sql = "SELECT Registration.RegistrationID, Client.FullNameClient, Registration.RoomID, Registration.PaymentL, Registration.PaymentS," +
          "Registration.TotalSumD, Registration.AD, Registration.DD FROM Registration JOIN Client ON (Registration.ClientID=Client.ClientID)" +
          "JOIN Employee ON (Registration.EmployeeID=Employee.EmployeeID) WHERE (Employee.FullName='" + NameEmployee + "' AND (Registration.AD > '" + StartDay.Date + "' OR Registration.AD = '" + StartDay.Date + "') AND (Registration.DD < '" + LastDay + "' OR Registration.DD = '" + LastDay + "'))";

            CreateWord(Teplate);
             findString("FullName", NameEmployee);
            findString("StartDay", (StartDay.Date).ToString("d"));
            findString("LastDay", (LastDay.Date).ToString("d"));
            TableReport(sql,NameEmployee, (StartDay.Date), LastDay.Date);
      }
        public static void CreateWord(string Teplate)
        {
            wordApplication = new Word.Application();
            wordDocument = null;
            wordApplication.Visible = true;


            string pth = Assembly.GetExecutingAssembly().Location;
            int pthInt = pth.LastIndexOf('\\');
             pth = pth.Remove(pthInt);
            pth = pth.Insert(pthInt, "\\Template" + Teplate);

           var templatePathObj = pth + ".doc";

            try
            {
                wordDocument = wordApplication.Documents.Add(templatePathObj);
            }
            catch
            {
                try
                {
                    if (wordDocument != null)
                    {
                        wordDocument.Close(false);
                        wordDocument = null;
                    }
                    wordApplication.Quit();
                    wordApplication = null;
                    throw;
                }
                catch {}
            }
        }
        private static void TableReport(string sql,string NameEmployee, DateTime StartDay, DateTime LasDay)
        {
            wordApplication.Selection.Find.Execute("@Table");
            Word.Range wordRange = wordApplication.Selection.Range;

            var wordTable = wordDocument.Tables.Add(wordRange,
              1 + ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count), 8);

            wordTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            wordTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            
                wordTable.Cell(1, 1).Range.Text = "Номер регистрации";
                wordTable.Cell(1, 2).Range.Text = "ФИО клиента";
                wordTable.Cell(1, 3).Range.Text = "Номер комнаты";
                wordTable.Cell(1, 4).Range.Text = "Оплата за проживание";
                wordTable.Cell(1, 5).Range.Text = "Оплата за услуги";
                wordTable.Cell(1, 6).Range.Text = "Общая оплата";
                wordTable.Cell(1, 7).Range.Text = "Дата заселения";
                wordTable.Cell(1, 8).Range.Text = "Дата выселения";
            

            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (j == 6 || j == 7)
                    {
                        wordTable.Cell(i + 2, j + 1).Range.Text = Convert.ToDateTime((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[j]).Date.ToString("d");
                    }
                    else
                    {
                        wordTable.Cell(i + 2, j + 1).Range.Text = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[j].ToString();
                    }
                }
            }
        }
        public static void startStatment(string NameClient,string RegistrationClient) // запус 
        {
              CreateWord("Statement");
          
            string sql = "SELECT Registration.AD, Registration.DD, Registration.PaymentS,Registration.PaymentL, Registration.TotalSumD FROM Registration Where Registration.RegistrationID = " + RegistrationClient;
            findString("FullName", NameClient);
            findString("DayAD", Convert.ToDateTime((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[0]).Date.ToString("d"));
            findString("DayDD", Convert.ToDateTime((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[1]).Date.ToString("d"));
            findString("PaymentS", (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[2].ToString());
            findString("PaymentL", (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[3].ToString());
            findString("TotalSumD", (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[0].Row[4].ToString());
            sql = "SELECT[Service].[Service], [Service].Cost FROM Registration JOIN ServiceList ON(Registration.RegistrationID= ServiceList.RegistrationID) JOIN[Service] ON(ServiceList.[ServiceID]=[Service].ServiceID) WHERE(Registration.RegistrationID= " + RegistrationClient + " AND ServiceList.Status='Выполнено')";
            TableStatment(sql);
        }
        private static void TableStatment(string sql)
        {
            wordApplication.Selection.Find.Execute("@Table");
            Word.Range wordRange = wordApplication.Selection.Range;

            var wordTable = wordDocument.Tables.Add(wordRange,
              1 + ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count), 2);

            wordTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            wordTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;

            wordTable.Cell(1, 1).Range.Text = "Услуга";
            wordTable.Cell(1, 2).Range.Text = "Цена";


            for (int i = 0; i < ((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    wordTable.Cell(i + 2, j + 1).Range.Text = (WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[i].Row[j].ToString();
                }
            }
        }
        private static void findString(string Find, string inputWord)
        {
            object FindText = "@"+Find;
            object ReplaceText = inputWord;
            wordApplication.Selection.Find.Execute(FindText: ref FindText, ReplaceWith: ref ReplaceText);
            wordApplication.Selection.Collapse(0);
        }
        }
}
