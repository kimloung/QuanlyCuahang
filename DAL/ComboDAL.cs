using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace QuanLyBanHang.DAL
{
    class ComboDAL
    {
        public List<ComboDTO> getAll()
        {
            List<ComboDTO> data = new List<ComboDTO>();
            string query = "SELECT * FROM Combo";
            SqlConnection con = new DatabaseConnection().connectDB();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    ComboDTO d = new ComboDTO();
                    d.id = reader.GetInt32(0);
                    d.name = reader.GetString(1);
                    d.deviceList = reader.GetString(2);
                    d.totalMoney = reader.GetDecimal(3);
                    d.discount = reader.GetInt32(4);
                    d.finalMoney = reader.GetDecimal(5);
                    d.amount = reader.GetInt32(6);
                    data.Add(d);
                }
                reader.NextResult();
            }
            con.Close();
            return data;
        }

        public string getDeviceListById(int id)
        {
            string kq = "";
            SqlConnection con = new DatabaseConnection().connectDB();
            string query = "select deviceList from Combo where id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    kq = reader.GetString(0);
                }
                reader.NextResult();
            }
            return kq;
        }
        public ComboDTO getComboById(int id)
        {
            ComboDTO kh = new ComboDTO();
            SqlConnection con = new DatabaseConnection().connectDB();
            try
            {
                string query = "Select * from Combo where id = " + id;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    kh.name = reader.GetString(1);
                    kh.deviceList = reader.GetString(2);
                    kh.totalMoney = reader.GetDecimal(3);
                    kh.discount = reader.GetInt32(4);
                    kh.finalMoney= reader.GetDecimal(5);
                    kh.amount = reader.GetInt32(6);
                }
            }
            catch (Exception ex) { throw ex; }
            return kh;
        }

        public void addCombo(ComboDTO combo)
        {
            SqlConnection con = new DatabaseConnection().connectDB();
            string query = "INSERT INTO Combo (name, deviceList, totalMoney, discount, finalMoney, amount) " +
                           "VALUES (@name, @deviceList, @totalMoney, @discount, @finalMoney, @amount)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", combo.name);
            cmd.Parameters.AddWithValue("@deviceList", combo.deviceList);
            cmd.Parameters.AddWithValue("@totalMoney", combo.totalMoney);
            cmd.Parameters.AddWithValue("@discount", combo.discount);
            cmd.Parameters.AddWithValue("@finalMoney", combo.finalMoney);
            cmd.Parameters.AddWithValue("@amount", combo.amount);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public int getCurrentComboId()
        {
            int a = 0;
            SqlConnection con = new DatabaseConnection().connectDB();
            try
            {
                string query = "select IDENT_CURRENT('dbo.Combo')";
                SqlCommand cmd = new SqlCommand(query, con);
                a = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                con.Close();
            }
            return a;
        }

        public void updateCombo(int id, ComboDTO cb)
        {
            SqlConnection con = new DatabaseConnection().connectDB();
            string query = "UPDATE Combo SET " +
                           "name = '" + cb.name +
                           "',deviceList = '" + cb.deviceList +
                           "',totalMoney = " + cb.totalMoney +
                           ",discount = " + cb.discount +
                           ",finalMoney = " + cb.finalMoney +
                           ",amount = " + cb.amount +
                          " WHERE id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void deleteCombo(int id)
        {
            SqlConnection con = new DatabaseConnection().connectDB();
            string query = "DELETE FROM Combo WHERE id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
