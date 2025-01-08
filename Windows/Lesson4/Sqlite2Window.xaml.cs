using plc_demo.Utils;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace plc_demo.Windows.Lesson4
{
    /// <summary>
    /// Sqlite2Window.xaml 的交互逻辑
    /// </summary>
    public partial class Sqlite2Window : Window
    {
        private static string dbPath = "lesson3.db";
        private static string connString = "Data Source=" + dbPath + ";Pooling=true;FailIfMissing=false;";

        public Sqlite2Window()
        {
            InitializeComponent();

            //初始化
            SqliteHelper.SetConnectionString(connString);
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void handleInitDB(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bolExist = await SqliteHelper.TableExist("TBUser");
                if (bolExist)
                {
                    MessageBox.Show("数据库及表已存在，请勿重复创建", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string strSql = @"CREATE TABLE TBUser (
                                UserID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                UserName TEXT,
                                UserPass Text
                            )";
                    await SqliteHelper.ExecuteNonQuery(strSql);
                    MessageBox.Show("数据库创建完成！");
                }
            }
            catch (Exception ex) { 
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
            try {
                string strSqlQuery = @"select * from TBUser";
                DataTable dt = SqliteHelper.GetDataTable(strSqlQuery);
                dbGrid.ItemsSource = dt.DefaultView;
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
        private async void handleAdd(object sender, RoutedEventArgs e)
        {
            try {
                //生成随机数据
                string randName = TextHelper.RandomChineseName();
                string randPass = SecurityHelper.GetMD5((new Random().Next(100000, 999999)).ToString());

                //插入数据
                string strSqlInsert = "Insert INTO TBUser (UserName, UserPass) VALUES (@UserName, @UserPass)";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("UserName", randName),
                    new SQLiteParameter("UserPass", randPass)
                };
                await SqliteHelper.ExecuteNonQuery(strSqlInsert, parameter);

                //查询数据
                string strSqlQuery = @"select * from TBUser";
                DataTable dt = SqliteHelper.GetDataTable(strSqlQuery);
                dbGrid.ItemsSource = dt.DefaultView;

                //弹出提示
                MessageBox.Show("数据新增完成，并自动刷新！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增数失败，原因：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 加载动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void handleLoading(object sender, RoutedEventArgs e)
        {
            loadingMask.Visibility = Visibility.Visible;
            await Task.Delay(5000);
            loadingMask.Visibility = Visibility.Collapsed;
        }
    }
}
