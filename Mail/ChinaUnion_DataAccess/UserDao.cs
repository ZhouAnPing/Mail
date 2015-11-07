
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class UserDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(User entity)
        {
            string sql = "INSERT INTO tb_user (userId, name, password) VALUE (@userId, @name, @password)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@name", entity.name);
                command.Parameters.AddWithValue("@password", entity.password);
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
        public int Update(User entity)
        {
            string sql = "UPDATE  tb_user SET userId=@userId ,name=@name,password=@password where userId=@userId ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@name", entity.name);
                command.Parameters.AddWithValue("@password", entity.password);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String userId)
        {
            string sql = "DELETE FROM tb_user WHERE userId=@userId";
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
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public User Get(String userId)
        {
            string sql = "SELECT userId, name, password FROM tb_user where  userId=@userId";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();

                User user = null;
                UserRightDao userRightDao = new UserRightDao();
                if (reader.Read())
                {
                    user = new User();
                    user.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    user.name = reader["name"] == DBNull.Value ? null : reader["name"].ToString();
                    user.password = reader["password"] == DBNull.Value ? null : reader["password"].ToString();                   
                }
                reader.Close();

                sql = "SELECT t1.userId,t1.menuId, t2.menu_text FROM tb_user_right t1, tb_menu t2 where  userId=@userId and t1.menuId = t2.ID ";

                command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader userRightReader = command.ExecuteReader();

                IList<UserRight> list = new List<UserRight>();
                UserRight userRight = null;
                while (userRightReader.Read())
                {
                    userRight = new UserRight();
                    userRight.userId = userRightReader["userId"] == DBNull.Value ? null : userRightReader["userId"].ToString();
                    userRight.menuId = userRightReader["menuId"] == DBNull.Value ? null : userRightReader["menuId"].ToString();
                    userRight.menuText = userRightReader["menu_text"] == DBNull.Value ? null : userRightReader["menu_text"].ToString();
                    list.Add(userRight);
                }
                if (user != null)
                {
                    user.userRightList = list;
                }

                mycn.Close();

                return user;
            }

        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<User> GetList(String userId)
        {
            string sql = "SELECT userId, name, password FROM tb_user ";
            if (!String.IsNullOrEmpty(userId))
            {
                sql = sql + " where userId like '" + userId + "%'";
            }
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<User> list = new List<User>();
                User user = null;
                // UserRightDao userRightDao = new UserRightDao();
                while (reader.Read())
                {
                    user = new User();

                    user.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    user.name = reader["name"] == DBNull.Value ? null : reader["name"].ToString();
                    user.password = reader["password"] == DBNull.Value ? null : reader["password"].ToString();
                    //user.userRightList = userRightDao.GetList(user.userId);
                    list.Add(user);
                }
                
                mycn.Close();
                return list;
            }
        }
    }
}

