using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
   public  class MyMenuItemDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(MyMenuItem entity)
        {


            string sql = "INSERT INTO tb_menu (ID,FATHER_ID, MENU_NAME, MENU_TEXT,IMAGE_KEY) VALUE (@Id,@Parent_Id, @Menu_Name, @Menu_Text,@image_key)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Parent_Id", entity.Parent_Id);
                command.Parameters.AddWithValue("@Menu_Name", entity.Menu_Name);
                command.Parameters.AddWithValue("@Menu_Text", entity.Menu_Text);
                command.Parameters.AddWithValue("@image_key", entity.image_key);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(MyMenuItem entity)
        {
            string sql = "DELETE FROM tb_menu WHERE ID=@Id";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@Id", entity.Id);
                return command.ExecuteNonQuery();
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetListDT()
        {

            DataTable tb = new DataTable();


            string sql = "SELECT ID,FATHER_ID, MENU_NAME, MENU_TEXT,IMAGE_KEY FROM tb_menu order by Id asc";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();

                tb.Load(reader);
                return tb;
            }
        }

      
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<MyMenuItem> GetList()
        {
            string sql = "SELECT ID,FATHER_ID, MENU_NAME, MENU_TEXT,IMAGE_KEY FROM tb_menu order by Id asc";
          
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
              
                MySqlDataReader reader = command.ExecuteReader();
                IList<MyMenuItem> list = new List<MyMenuItem>();
                MyMenuItem menu = null;

              
               
                while (reader.Read())
                {
                    menu = new MyMenuItem();

                    menu.Id = reader["ID"] == DBNull.Value ? null : reader["ID"].ToString();
                    menu.Parent_Id = reader["FATHER_ID"] == DBNull.Value ? null : reader["FATHER_ID"].ToString();
                    menu.Menu_Name = reader["MENU_NAME"] == DBNull.Value ? null : reader["MENU_NAME"].ToString();
                    menu.Menu_Text = reader["MENU_TEXT"] == DBNull.Value ? null : reader["MENU_TEXT"].ToString();
                    menu.image_key = reader["IMAGE_KEY"] == DBNull.Value ? null : reader["IMAGE_KEY"].ToString();

                    list.Add(menu);
                }
                return list;
            }
        }
    }
}
