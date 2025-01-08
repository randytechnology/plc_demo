using plc_demo.Utils;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace plc_demo.Windows.Lesson3
{
    /// <summary>
    /// SqliteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SqliteWindow : Window
    {
        private static string dbPath = "lesson3.db";
        private static string connString = "Data Source=" + dbPath + ";Pooling=true;FailIfMissing=false;";

        public SqliteWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleInitDB(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(connString))
                {
                    connection.Open();

                    //判断是否存在某个表
                    int intTableCount = 0;
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "select count(*) from sqlite_master where type='table' AND name='TBUser'";
                        intTableCount = Convert.ToInt32(command.ExecuteScalar());
                    }

                    if (intTableCount > 0)
                    {
                        MessageBox.Show("数据库及表已存在，请勿重复创建", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else {
                        //创建表
                        
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = @"CREATE TABLE TBUser (
                                UserID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                UserName TEXT,
                                UserPass Text
                            )";
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("数据库创建完成！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化数据库失败，原因：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleQuery(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var connection = new SQLiteConnection(connString))
                {
                    connection.Open();

                    //查询数据
                    string sql = @"select * from TBUser";

                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, connection)) { 
                        da.Fill(dataTable);
                    }

                    dbGrid.ItemsSource = dataTable.DefaultView;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据失败，原因：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(connString))
                {
                    connection.Open();
                    // 生成随机数据
                    string randName = TextHelper.RandomChineseName();
                    string randPass = SecurityHelper.GetMD5((new Random().Next(100000,999999)).ToString());

                    //创建随机数据
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"Insert INTO TBUser (UserName, UserPass) VALUES ('"+ randName +"','"+ randPass +"')";
                        command.ExecuteNonQuery();
                    }
                    
                    //刷新数据
                    DataTable dataTable = new DataTable();
                    string sql = @"select * from TBUser";
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, connection))
                    {
                        da.Fill(dataTable);
                    }
                    dbGrid.ItemsSource = dataTable.DefaultView;

                    //弹出提示
                    MessageBox.Show("数据新增完成，并自动刷新！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增数失败，原因：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
