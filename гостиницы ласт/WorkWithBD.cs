using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace гостиницы_ласт
{
    public class WorkWithBD
    {
         static string connectionString = //@"Data Source=.\SQLEXPRESS;Initial Catalog=hoteldb;Integrated Security=True";
        @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|hoteldb.mdf;Integrated Security=True;Connect Timeout=30;";

        static DataSet ds = new DataSet();
        public static DataSet outPutdb(string sql) // вывод из бд
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter (отправляет запрос на бд)
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset (представление о таблице)
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                return ds;
            }
        }
        //------------------------ДОБАВЛЕНИЕ-------------------------------
        public static void inPutClientdb(string sqlForDocument,string Type,string Code, string Number, DateTime DI, string IssuedBy, 
            string sqlForClient, string FullName, string Sex, DateTime DOB, string Address, string Tel,string Email) // добавление в бд
        {
            Int64 id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sqlForDocument, connection))
                {
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Number", Number);
                    cmd.Parameters.AddWithValue("@DI", DI);
                    cmd.Parameters.AddWithValue("@IssuedBy", IssuedBy);


                    SqlParameter idParam = new SqlParameter //получение автосозданного id 
                    {
                        ParameterName = "@DocumentID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                    };

                    cmd.Parameters.Add(idParam);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    id = Convert.ToInt64(cmd.ExecuteScalar());
                }
                using (var cmd = new SqlCommand(sqlForClient, connection))
                {
                    cmd.Parameters.AddWithValue("@DocumentID", id);
                    cmd.Parameters.AddWithValue("@FullNameClient", FullName);
                    cmd.Parameters.AddWithValue("@Sex", Sex);
                    cmd.Parameters.AddWithValue("@DOB", DOB);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Tel", Tel);
                    cmd.Parameters.AddWithValue("@Email", Email);
          
                    
                    cmd.ExecuteNonQuery();
                }

            }

    }
        public static void inPutServicedb(string sqlForService, string Service, double Cost) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sqlForService, connection))
                {
                    cmd.Parameters.AddWithValue("@Service", Service);
                    cmd.Parameters.AddWithValue("@Cost", Cost);
                    
                    cmd.ExecuteNonQuery();
                }               
            }

        }
        public static void inPutServiceClientdb(string sql, int RegistrationID, int ServiceID, string Status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@RegistrationID", RegistrationID);
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    cmd.Parameters.AddWithValue("@Status", "Не выполнено");
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void inPutRegistrationdb(string sql,DateTime AD,DateTime DD, string Client, int Room, string Employee,string Booked) // добавление в бд 
        {
            Int64 id;
            string sqlClient = "SELECT ClientID FROM Client WHERE FullNameClient='" + Client + "'";
            int ClientID = Convert.ToInt32((WorkWithBD.outPutdb(sqlClient)).Tables[0].DefaultView[0].Row[0]);
            string sqlEmployee = "SELECT EmployeeID FROM Employee WHERE FullName='" + Employee + "'";
            int EmployeeID = Convert.ToInt32((WorkWithBD.outPutdb(sqlEmployee)).Tables[0].DefaultView[0].Row[0]);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ClientID", ClientID);
                    cmd.Parameters.AddWithValue("@RoomID", Room);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@Booked", Booked);
                    cmd.Parameters.AddWithValue("@AD", AD);
                    cmd.Parameters.AddWithValue("@DD", DD);


                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@RegistrationID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                    };

                    cmd.Parameters.Add(idParam);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT @@IDENTITY";
                    id = Convert.ToInt64(cmd.ExecuteScalar());
                }
            }
        }

        //-------------------УДАЛЕНИЕ-----------------------
        public static void removeRegistrationdb(int index) // удаление из бд
        {
            removeServiceListdb(index);
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [Registration] WHERE [RegistrationID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void removeClientdb(int index) // удаление из бд клиента
        {
            string sql ="SELECT RegistrationID FROM Registration WHERE Registration.ClientID="+ index +"";
           
            List<int> list = new List<int>();
            for (int i = 0; i <Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].Rows.Count); i++)
            {
                list.Add(Convert.ToInt32(WorkWithBD.outPutdb(sql).Tables[0].DefaultView[i].Row[0].ToString()));
            }
            for(int i = 0; i < list.Count; i++)
            {
                removeServiceListdb(list[i]);
                removeRegistrationdb(list[i]);
            }
            
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [Client] WHERE [ClientID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [Document] WHERE [DocumentID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void removeServicedb(int index)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [Service] WHERE [ServiceID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void removeServiceListdb(int index)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [ServiceList] WHERE [RegistrationID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void removeServiceListGooddb(int index)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [ServiceList] WHERE [ServiceListID] = " + index, sqlConn))
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //------------------------ИЗМЕНЕНИЕ----------------
        public static void Update(string sql) // обновление изменений 
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand com = new SqlCommand(sql, sqlConn))
                {
                    sqlConn.Open();
                    SqlDataReader read = com.ExecuteReader();
                    read.Close();

                 
                }
            }
        }
  
        //-------------------ПОДСЧЕТ СУММ------------
          
        public static void sumForRoom(int index) // сумма за проживание в номере PaymentL
        {
            
                string sql = "SELECT Registration.RegistrationID, Registration.AD, Registration.DD, " +
                 "Room.RoomID, " +
                 "Room.TypeID, " +
                 "Type.DayCost " +
                 "FROM Registration JOIN Room ON(Registration.RoomID=Room.RoomID) " +
                 "JOIN Type ON(Room.TypeID=Type.TypeID) " +
                 "WHERE (Registration.RegistrationID=" + index + ")";
           //TimeSpan выводит необходимое кол-во дней между датами
            TimeSpan ts = Convert.ToDateTime((outPutdb(sql)).Tables[0].DefaultView[0].Row[2]) - Convert.ToDateTime((outPutdb(sql)).Tables[0].DefaultView[0].Row[1]);
            int result = ts.Days + 1;
            double PaymentL = result * Convert.ToDouble((outPutdb(sql)).Tables[0].DefaultView[0].Row[5].ToString()); //sumDay((outPutdb(sql)).Tables[0].DefaultView[0].Row[1].ToString().Split(' ')[0], (outPutdb(sql)).Tables[0].DefaultView[0].Row[2].ToString().Split(' ')[0], Convert.ToDouble((outPutdb(sql)).Tables[0].DefaultView[0].Row[5]));
                sql = "UPDATE [Registration] SET [PaymentL] = '" + PaymentL + "' " +
                 "WHERE [RegistrationID] = " + index;

                Update(sql);
        }
        public static void sumForService(int index,double PaymentS) // считается сумма за услуги PaymentS
        {
            string sql = "SELECT ServiceList.ServiceListID, " +
                       "ServiceList.ServiceID, " +
                       "Registration.RegistrationID, " +
                       "Service.Cost " +
                       "FROM ServiceList JOIN Registration ON(ServiceList.RegistrationID=Registration.RegistrationID) " +
                       "JOIN Service ON(ServiceList.ServiceID=Service.ServiceID) " +
                       "WHERE (ServiceList.RegistrationID=" + index + " AND ServiceList.Status='Выполнено')";
            for (int j = 0; j < Convert.ToInt32((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView.Count.ToString()); j++) // кол-во услуг у регистрации
            {
                PaymentS += Convert.ToDouble((WorkWithBD.outPutdb(sql)).Tables[0].DefaultView[j].Row[3].ToString());
            }
            sql = "UPDATE [Registration] SET [PaymentS] = '" + PaymentS + "' " +
             "WHERE [RegistrationID] = " + index;

            Update(sql);
        }
        public static void TotalSum(int index) // общая сумма за всё TotalSumD
        {
            string sql = "SELECT Registration.PaymentL, " +
                                "Registration.PaymentS " +
                                "FROM Registration " +
                                "WHERE (Registration.RegistrationID=" + index + ")";
            double sum = Convert.ToDouble(outPutdb(sql).Tables[0].DefaultView[0].Row[0].ToString()) + Convert.ToDouble(outPutdb(sql).Tables[0].DefaultView[0].Row[1].ToString());
            sql = "UPDATE [Registration] SET [TotalSumD] = '" + sum + "' " +
             "WHERE [RegistrationID] = " + index;

            Update(sql);
        }
    }
}
