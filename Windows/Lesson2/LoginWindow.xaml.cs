using plc_demo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace plc_demo.Windows.Lesson2
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //获取用户名和密码
            string strLoginName = this.txtLoginName.Text.Trim();
            string strLoginPass = this.txtLoginPass.Password.Trim();

            //简单判断
            if (strLoginName.Length == 0) {
                MessageBox.Show("请先输入您的登录名称", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (strLoginPass.Length == 0)
            {
                MessageBox.Show("请先输入您的登录密码", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //密码MD5加密
            string strLoginPassEnc = SecurityHelper.GetMD5(strLoginPass);

            //判断，测试代码直接写死，正式应该从数据库读取（96e79218965eb72c92a549dd5a330112是111111的md5值）
            if (strLoginName != "admin") {
                MessageBox.Show("登录名称不存在，请重新输入", "提醒", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (strLoginPassEnc != "96e79218965eb72c92a549dd5a330112")
            {
                MessageBox.Show("登录密码不正确，请重新输入", "提醒", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //登录成功的话进行跳转
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
