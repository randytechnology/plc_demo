using System.Windows;
using System.Configuration;

namespace plc_demo.Windows.Lesson5
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读App配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadApp(object sender, RoutedEventArgs e)
        {
            if (txtAppRead1.Text == "") { 
                MessageBox.Show("请先输入要读的键名称", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(txtAppRead1.Text) == false)
            {
                MessageBox.Show("没有找到该键", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else {
                string? strRead = ConfigurationManager.AppSettings[txtAppRead1.Text];
                MessageBox.Show("读取到的值：" + strRead);
            }
        }

        /// <summary>
        /// 写App配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteApp(object sender, RoutedEventArgs e)
        {
            if (txtAppWrite1.Text == "" || txtAppWrite2.Text == "")
            {
                MessageBox.Show("请先输入要写的键名和值", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Configuration _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (_configuration.AppSettings.Settings.AllKeys.Contains(txtAppWrite1.Text))
            {
                _configuration.AppSettings.Settings[txtAppWrite1.Text].Value = txtAppWrite2.Text;
            }
            else { 
                _configuration.AppSettings.Settings.Add(txtAppWrite1.Text, txtAppWrite2.Text);
            }
            _configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("数据写入并刷新成功。");

        }


        /// <summary>
        /// 读自定义配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCustomerConfig(object sender, RoutedEventArgs e)
        {
            if (txtIniRead1.Text == "")
            {
                MessageBox.Show("请先输入要读的键名称", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = "AppCustom.config"
                }, 
                ConfigurationUserLevel.None
            );

            if (ConfigurationInstance.AppSettings.Settings.AllKeys.Contains(txtIniRead1.Text) == false)
            {
                MessageBox.Show("没有找到该键", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string? strRead = ConfigurationInstance.AppSettings.Settings[txtIniRead1.Text].Value;
                MessageBox.Show("读取到的值：" + strRead);
            }
        }

        /// <summary>
        /// 写自定义配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteCustomerConfig(object sender, RoutedEventArgs e)
        {
            if (txtIniWrite1.Text == "" || txtIniWrite2.Text == "")
            {
                MessageBox.Show("请先输入要写的键名和值", "提醒", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = "AppCustom.config"
                }, 
                ConfigurationUserLevel.None
            );

            if (ConfigurationInstance.AppSettings.Settings.AllKeys.Contains(txtIniWrite1.Text))
            {
                ConfigurationInstance.AppSettings.Settings[txtIniWrite1.Text].Value = txtIniWrite2.Text;
            }
            else
            {
                ConfigurationInstance.AppSettings.Settings.Add(txtIniWrite1.Text, txtIniWrite2.Text);
            }
            ConfigurationInstance.Save();
            MessageBox.Show("数据写入并刷新成功。");

        }
    }
}
