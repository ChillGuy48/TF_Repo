﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Repo;

namespace IO
{
    public class ClassTFAppDB : ClassDBCon
    {
        public ClassTFAppDB()
        {
            //SetCon(@"Server = (localdb)\MSSQLLocalDB;Database=TFApp;Trusted_Connection=True;Trusted_Connection=True");
            SetCon(@"Server = https://cp03.resellerhotel.dk:2083/cpsess9474677846/3rdparty/phpMyAdmin/db_structure.php?server=1&db=math027r_TF_RepoDB;Database=TF_RepoDB;Trusted_Connection=True;Trusted_Connection=True");
        }

        public int CreateUser(ClassUser inUser)
        {
            int res = 0;

            string sqlQuery = @"INSERT INTO users (id, navn, username, password) VALUES
                                (@id, @navn, @username, @password)
                                SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inUser.id;
                    cmd.Parameters.Add("@navn", SqlDbType.VarChar).Value = inUser.navn;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = inUser.username;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = inUser.password;

                    OpenDB();
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                CloseDB();
            }

            return res;
        }

        
        public ClassUser GetUserData(int id)
        {
            ClassUser res = new ClassUser();

            string sqlQuery = "SELECT navn, username, password FROM users WHERE id = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = res.id;
                    cmd.Parameters.Add("@navn", SqlDbType.VarChar).Value = res.navn;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = res.username;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = res.password;

                    OpenDB();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        res.id = id;
                        res.navn = reader.GetString(0);
                        res.username = reader.GetString(1);
                        res.password = reader.GetString(2);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }



        public int CreateEnergibarometer(ClassInput inInput)
        {
            int res = 0;

            string sqlQuery = @"INSERT INTO input (id, energi, time, info, sleep, userid) VALUES
                                (@id, @energi, @time, @info, @sleep, @userid)
                                SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inInput.id;
                    cmd.Parameters.Add("@energi", SqlDbType.Int).Value = inInput.energi;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime).Value = inInput.time;
                    cmd.Parameters.Add("@info", SqlDbType.VarChar).Value = inInput.info;
                    cmd.Parameters.Add("@sleep", SqlDbType.Int).Value = inInput.sleep;
                    cmd.Parameters.Add("@userid", SqlDbType.Int).Value = inInput.userid;

                    OpenDB();
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                CloseDB();
            }

            return res;
        }


        public List<ClassInput> GetEnergibarometerData(int userid)
        {
            List<ClassInput> res = new List<ClassInput>();

            string sqlQuery = "SELECT id, energi, time, info, sleep, userid FROM input WHERE userid = @userid";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;

                    OpenDB();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ClassInput ci = new ClassInput();

                        ci.id = reader.GetInt32(0);
                        ci.energi = reader.GetInt32(1);
                        ci.time = reader.GetDateTime(2);
                        ci.info = reader.GetString(3);
                        ci.sleep = reader.GetInt32(4);
                        ci.userid = reader.GetInt32(5);

                        res.Add(ci);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ERROR Data Request did not return a Vaild Result! : {ex.Message}", "SQL-QUERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }







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