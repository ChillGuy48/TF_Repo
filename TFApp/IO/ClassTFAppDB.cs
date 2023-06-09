﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using Repo;

namespace IO
{
    public class ClassTFAppDB : ClassDBCon
    {
        public ClassTFAppDB()
        {
            //SetCon(@"Server = (localdb)\MSSQLLocalDB;Database=TFApp;Trusted_Connection=True;Trusted_Connection=True");
            SetCon(@"SERVER=math027r.aspitcloud.dk;PORT=3306;DATABASE=math027r_TF_RepoDB;UID=math027r;PASSWORD=sThV5etSOizHvB8BnD7f");
        }

        public int CreateUser(ClassUser inUser)
        {
            int res = 0;

            string sqlQuery = @"INSERT INTO users (userid, navn, username, password) VALUES
                                (@userid, @navn, @username, @password)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@userid", MySqlDbType.UInt32).Value = inUser.id;
                    cmd.Parameters.Add("@navn", MySqlDbType.VarChar).Value = inUser.navn;
                    cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = inUser.username;
                    cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = inUser.password;

                    OpenDB();
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                CloseDB();
            }

            return res;
        }


        public ClassUser GetUserData(string username, string password)
        {
            ClassUser res = new ClassUser();

            string sqlQuery = "SELECT userid, navn, username, password FROM users WHERE username = @username AND password = @password";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                    OpenDB();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        res.id = reader.GetInt32(0);
                        res.navn = reader.GetString(1);
                        res.username = reader.GetString(2);
                        res.password = reader.GetString(3);
                        //MessageBox.Show($"Id: {res.id}\nNavn: {res.navn}\nUsername: {res.username}\nPassword: {res.password}", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        res.status = "logincorrect";
                    }
                    else
                    {
                        MessageBox.Show($"Dit Login er forkert", "Login fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                        res.status = "logindeclied";
                    }
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Der gik noget galt, Error Message:\n{ex.Message}", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }



        public int CreateEnergibarometer(int energilevel, DateTime time, ClassUser userid)
        {
            int res = 0;

            string sqlQuery = @"INSERT INTO input (energi,time,userid) VALUES (@energi,@time,@userid)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@energi", MySqlDbType.Int32).Value = energilevel;
                    cmd.Parameters.Add("@time", MySqlDbType.DateTime).Value = time.Date;
                    cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userid.id;

                    OpenDB();
                    res = cmd.ExecuteNonQuery();            }
                if (res == 0 || res == -1)
                {
                    MessageBox.Show($"Der opstod en fejl under processen", "Energibarometer fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Der gik noget galt, Error Message:\n{ex.Message}", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                CloseDB();
            }

            return res;
        }

        public ClassInput GetEnergibarometerData(int userid, DateTime time)
        {
            ClassInput res = new ClassInput();

            string sqlQuery = "SELECT userid, energi, time, id FROM input WHERE userid = @userid AND time = @time";
            //string sqlQuery = "SELECT userid, energi, time, id FROM input WHERE userid = @userid";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@userid", MySqlDbType.Int16).Value = userid;
                    cmd.Parameters.Add("@time", MySqlDbType.Date).Value = time;

                    OpenDB();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        res.userid = reader.GetInt16(0);
                        res.energi = reader.GetInt16(1);
                        res.time = reader.GetDateTime(2);
                        res.id = reader.GetInt16(3);

                        //MessageBox.Show($"userid: {res.userid}\nid: {res.id}\ntime: {res.time}\nenergi: {res.energi}\ndateStringColor: {res.dateStringColor}", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        res.energi = 0;
                        MessageBox.Show($"Denne dag indeholder ingen energibarometer", "Energibarometer", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Der gik noget galt, Error Message:\n{ex.Message}", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }
        //public List<ClassInput> GetEnergibarometerData(int userid)
        //{
        //    List<ClassInput> res = new List<ClassInput>();

        //    string sqlQuery = "SELECT id, energi, time, info, sleep, userid FROM input WHERE userid = @userid";

        //    try
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userid;

        //            OpenDB();
        //            MySqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                ClassInput ci = new ClassInput();

        //                ci.id = reader.GetInt32(0);
        //                ci.energi = reader.GetInt32(1);
        //                ci.time = reader.GetDateTime(2);
        //                ci.info = reader.GetString(3);
        //                ci.sleep = reader.GetInt32(4);
        //                ci.userid = reader.GetInt32(5);

        //                res.Add(ci);
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    return res;
        //}







        //public List<ClassCountry> GetAllCountryFromDB()
        //{
        //    List<ClassCountry> res = new List<ClassCountry>();

        //    string sqlQuery = @"SELECT id, CountryCode, CountryName, ValutaName, ValutaRate, UpdateTime FROM dbo.CountryAndRates";

        //    try
        //    {
        //        DataTable dt = DBReturnDataTable(sqlQuery);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            ClassCountry co = new ClassCountry();

        //            co.id = Convert.ToInt32(row["id"]);
        //            co.countryCode = row["CountryCode"].ToString();
        //            co.countryName = row["CountryName"].ToString();
        //            co.valutaName = row["ValutaName"].ToString();
        //            co.valutaRate = Convert.ToDouble(row["ValutaRate"]);
        //            co.updateTime = Convert.ToDateTime(row["UpdateTime"]);

        //            res.Add(co);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return res;
        //}

        //public List<ClassMeat> GetAllMeatFromDB()
        //{
        //    List<ClassMeat> res = new List<ClassMeat>();

        //    string sqlQuery = @"SELECT id, TypeOfMeat, Stock, Price, PriceTimeStamp FROM dbo.Meat";

        //    try
        //    {
        //        DataTable dt = DBReturnDataTable(sqlQuery);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            ClassMeat cm = new ClassMeat();

        //            cm.id = Convert.ToInt32(row["Id"]);
        //            cm.TypeOfMeat = row["TypeOfMeat"].ToString();
        //            cm.stock = Convert.ToInt32(row["Stock"]);
        //            cm.price = Convert.ToDouble(row["Price"]);
        //            cm.priceTimeStamp = Convert.ToDateTime(row["PriceTimeStamp"]);

        //            res.Add(cm);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return res;
        //}

        //public int SaveNewCustomerInDB(ClassCustomer inCustomer)
        //{
        //    int res = 0;

        //    string sqlQuery = @"INSERT INTO Customer (CampanyName, Address, ZipCity, Phone, Mail, ContactName, Country) VALUES
        //                        (@CompanyName, @Address, @ZipCity, @Phone, @Mail, @ContactName, @Country)
        //                        SELECT SCOPE_IDENTITY()";

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inCustomer.companyName;
        //            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inCustomer.address;
        //            cmd.Parameters.Add("@ZipCity", SqlDbType.NVarChar).Value = inCustomer.zipCity;
        //            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = inCustomer.phone;
        //            cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = inCustomer.mail;
        //            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = inCustomer.contactName;
        //            cmd.Parameters.Add("@Country", SqlDbType.Int).Value = inCustomer.country.id;

        //            OpenDB();
        //            res = Convert.ToInt32(cmd.ExecuteScalar());
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }

        //    return res;
        //}

        //public void UpdateCustomerInDB(ClassCustomer inCustomer)
        //{
        //    int res = 0;
        //    string sqlQuery = "UPDATE Customer " +
        //                      "SET CampanyName = @CompanyName, " +
        //                      "Address = @Address, " +
        //                      "ZipCity = @ZipCity, " +
        //                      "Phone = @Phone, " +
        //                      "Mail = @Mail, " +
        //                      "ContactName = @ContactName, " +
        //                      "Country = @Country " +
        //                      "WHERE id = @id";
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@id", SqlDbType.Int).Value = inCustomer.id;

        //            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inCustomer.companyName;
        //            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inCustomer.address;
        //            cmd.Parameters.Add("@ZipCity", SqlDbType.NVarChar).Value = inCustomer.zipCity;
        //            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = inCustomer.phone;
        //            cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = inCustomer.mail;
        //            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = inCustomer.contactName;
        //            cmd.Parameters.Add("@Country", SqlDbType.Int).Value = inCustomer.country.id;

        //            OpenDB();
        //            res = Convert.ToInt32(cmd.ExecuteScalar());
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //}

        //public void UpdateMeatVolme(ClassOrder inOrder)
        //{
        //    int res = 0;
        //    string sqlQuery = "UPDATE Meat " +
        //                      "SET Stock = @Stock";

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = inOrder.orderMeat.stock;

        //            OpenDB();
        //            res = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //}

        //public void UpdatePriceForMeatInDB(string InMeat, double inPrice, int inWeight)
        //{
        //    int res = 0;
        //    string sqlQuery = "UPDATE Meat " +
        //                      "SET TypeOfMeat = @TypeOfMeat, " +
        //                      "Price = @Price, " +
        //                      "Weight = @Weight";
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@TypeOfMeat", SqlDbType.NVarChar).Value = InMeat;
        //            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = inPrice;
        //            cmd.Parameters.Add("@Weight", SqlDbType.NVarChar).Value = inWeight;

        //            OpenDB();
        //            res = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //}

        //public void UpdateTimestampForMeat()
        //{
        //    int res = 0;
        //    string sqlQuery = "UPDATE Meat " +
        //                      "SET PriceTimeStamp = @PriceTimeStamp";

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@PriceTimeStamp", SqlDbType.DateTime2).Value = DateTime.Now;

        //            OpenDB();
        //            res = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //}

        //public int AddOrderToDB(ClassOrder inOrder)
        //{
        //    int res = 0;

        //    string sqlQuery = @"INSERT INTO Orders (Customer, Meat, Weight, OrderDate, OrderPriceDKK, OrderPriceValuta) VALUES
        //                        (@Customer, @Meat, @Weight, @OrderDate, @OrderPriceDKK, @OrderPriceValuta)
        //                        SELECT SCOPE_IDENTITY()";

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
        //        {
        //            cmd.Parameters.Add("@Customer", SqlDbType.Int).Value = inOrder.orderCustomer.id;
        //            cmd.Parameters.Add("@Meat", SqlDbType.Int).Value = inOrder.orderMeat;
        //            cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = inOrder.orderWeight;
        //            cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = DateTime.Now;
        //            cmd.Parameters.Add("@OrderPriceDKK", SqlDbType.Money).Value = inOrder.orderPriceDKK;
        //            cmd.Parameters.Add("@OrderPriceValuta", SqlDbType.Money).Value = inOrder.orderPriceValuta;

        //            OpenDB();
        //            res = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }

        //    return res;
        //}
    }
}