
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class UserRightDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(UserRight entity)
        {
            string sql = "INSERT INTO tb_user_right (userId, menuId) VALUE (@userId, @menuId)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@menuId", entity.menuId);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(UserRight entity)
        {
            string sql = "UPDATE  tb_user_right SET userId=@userId ,menuId=@menuId where userId=@userId ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@menuId", entity.menuId);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(UserRight entity)
        {
            string sql = "DELETE FROM tb_user_right WHERE userId=@userId and menuId =@menuId";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@menuId", entity.menuId);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int DeleteByUserId(String userId)
        {
            string sql = "DELETE FROM tb_user_right WHERE userId=@userId ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", userId);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
       
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<UserRight> GetList(String userId)
        {            
            string sql = "SELECT t1.userId,t1.menuId, t2.menu_text FROM tb_user_right t1, tb_menu t2 where  userId=@userId and t1.menuId = t2.ID ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
               
                IList<UserRight> list = new List<UserRight>();
                UserRight userRight = null;
                while (reader.Read())
                {
                    userRight = new UserRight();

                    userRight.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    userRight.menuId = reader["menuId"] == DBNull.Value ? null : reader["menuId"].ToString();
                    userRight.menuText = reader["menu_text"] == DBNull.Value ? null : reader["menu_text"].ToString();
                    list.Add(userRight);
                }
                mycn.Close();
                return list;
            }
        }
    }
}

