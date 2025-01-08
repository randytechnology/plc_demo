using plc_demo.Windows.Lesson3;
using plc_demo.Windows.Lesson4;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace plc_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击跳转到SQLiteWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showSqliteWindow(object sender, MouseButtonEventArgs e)
        {
            SqliteWindow sqliteWindow = new SqliteWindow();
            sqliteWindow.Show();
        }

        /// <summary>
        /// 点击跳转到SQLite2Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showSqlite2Window(object sender, MouseButtonEventArgs e)
        {
            Sqlite2Window sqlite2Window = new Sqlite2Window();
            sqlite2Window.Show();
        }
    }
}