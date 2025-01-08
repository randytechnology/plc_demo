using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace plc_demo.Windows.Lesson6
{
    /// <summary>
    /// PortWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PortWindow : Window
    {
        private SerialPort serialPort = new SerialPort();

        public PortWindow()
        {
            InitializeComponent();

            //初始化
            GetAvailSerialPort();
            cmBaudRate.Text = "9600";
            cmDataBits.Text = "8";
            cmParity.Text = "None";
            cmStopBits.Text = "1";
        }

        /// <summary>
        /// 获取所有可用的串口
        /// </summary>
        private void GetAvailSerialPort() {
            cmPortName.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames(); //获取可用的串口
            for (int i = 0; i < ports.Length; i++)
            {
                cmPortName.Items.Add(ports[i]);
            }
        }

        /// <summary>
        /// 点击打开串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPort_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen == true) {
                try
                {
                    //关闭串口
                    serialPort.Close();

                    //设置按钮颜色文字
                    btnPort.Foreground = new SolidColorBrush(Colors.Black);
                    btnPort.Background = new SolidColorBrush(Color.FromRgb(221,221,221));
                    btnPort.Content = "打开串口";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("关闭端口失败：原因" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else {
                //检查输入
                string strPortName = cmPortName.Text;
                if (strPortName.Length == 0) { 
                    MessageBox.Show("请先选择端口", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    //打开串口
                    serialPort.PortName = strPortName;
                    serialPort.BaudRate = Convert.ToInt32(cmBaudRate.Text);
                    serialPort.DataBits = Convert.ToInt16(cmDataBits.Text);
                    switch (cmParity.Text) {
                        case "Odd":
                            serialPort.Parity = System.IO.Ports.Parity.Odd;
                            break;
                        case "Even":
                            serialPort.Parity = System.IO.Ports.Parity.Even;
                            break;
                        case "Mark":
                            serialPort.Parity = System.IO.Ports.Parity.Mark;
                            break;
                        case "Space":
                            serialPort.Parity = System.IO.Ports.Parity.Space;
                            break;
                        default:
                            serialPort.Parity = System.IO.Ports.Parity.None;
                            break;
                    }

                    switch (cmStopBits.Text)
                    {
                        case "1.5":
                            serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort.StopBits = System.IO.Ports.StopBits.Two;
                            break;
                        default:
                            serialPort.StopBits = System.IO.Ports.StopBits.One;
                            break;
                    }
                    serialPort.DataReceived += SerialPort_DataReceived;

                    serialPort.Open();

                    //成功后设置按钮颜色、文字
                    btnPort.Foreground = new SolidColorBrush(Colors.White);
                    btnPort.Background = new SolidColorBrush(Colors.Green);
                    btnPort.Content = "关闭串口";
                }
                catch(Exception ex) {
                    MessageBox.Show("打开端口失败：原因" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen == false) { 
                MessageBox.Show("请先连接端口才能进行发送操作", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtSend.Text.Length == 0)
            {
                MessageBox.Show("请先输入要发送的内容", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (rbSendString.IsChecked == true)
                {
                    serialPort.Write(txtSend.Text);
                }
                else
                {
                    serialPort.Write(strToHexbytes(txtSend.Text), 0, strToHexbytes(txtSend.Text).Length);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("数据发送时发生错误：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
        }

        /// <summary>
        /// 串口接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try {
                Dispatcher.Invoke(() => {
                    if (rbRecvString.IsChecked == true)
                    {
                        txtReceive.AppendText(" " + serialPort.ReadExisting());
                    }
                    else
                    {
                        int len = serialPort.BytesToRead;
                        byte[] buff = new byte[len];
                        serialPort.Read(buff, 0, len);

                        txtReceive.AppendText(" " + byteToHexstr(buff));
                    }
                });
            }catch (Exception ex) {
                MessageBox.Show("数据接收时发生错误：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
        }

        //16进制显示字符串
        private string byteToHexstr(byte[] buff)
        {
            string str = "";
            try
            {
                if (buff != null)
                {

                    for (int i = 0; i < buff.Length; i++)
                    {
                        str += buff[i].ToString("x2");//转化为小写的16进制
                        str += " ";//两个之间用空格
                    }
                    return str;
                }
            }
            catch
            {
                return str;
            }
            return str;
        }

        //字符串转为16进制
        private byte[] strToHexbytes(string str)
        {
            str = str.Replace(" ", "");//清除空格
            byte[] buff;
            if ((str.Length % 2) != 0)
            {
                buff = new byte[(str.Length + 1) / 2];
                try
                {
                    for (int i = 0; i < buff.Length; i++)
                    {
                        buff[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                    }
                    buff[buff.Length - 1] = Convert.ToByte(str.Substring(str.Length - 1, 1).PadLeft(2, '0'), 16);
                    return buff;
                }
                catch
                {
                    MessageBox.Show("含有非16进制的字符","提示");
                    return buff;
                }
            }
            else
            {
                buff = new byte[str.Length / 2];
                try
                {
                    for (int i = 0; i < buff.Length; i++)
                    {
                        buff[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                    }
                }
                catch
                {
                    {
                        MessageBox.Show("含有非16进制的字符","提示");
                        return buff;
                    }
                }
            }
            return buff;
        }
    }
}
