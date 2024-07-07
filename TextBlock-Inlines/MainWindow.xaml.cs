using S7.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextBlock_Inlines
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Plc plc = null;

        ObservableCollection<string> dataFormat = new ObservableCollection<string>
                {
                    "Bit",
                    "Byte",
                    "Word",
                    "DWord"
                };

                ObservableCollection<string> addType_Bit = new ObservableCollection<string>
                {
                    "I",
                    "Q",
                    "M",
                    "DB.DB"
                };



        ObservableCollection<string> addType_Byte = new ObservableCollection<string>
                {
                    "MB",
                    "DB.DBB"
                };



        ObservableCollection<string> addType_Word = new ObservableCollection<string>
                {
                    "IW",
                    "QW",
                    "MW",
                    "DB.DBW"
                };


        ObservableCollection<string> addType_DWord = new ObservableCollection<string>
                {
                    "MD",
                    "DB.DBD"
                };


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            btn_Connect.Click += Btn_Connect_Click;
            btn_Disconnect.Click += Btn_Disconnect_Click;
            btn_Read.Click += Btn_Read_Click;
            btn_Write.Click += Btn_Write_Click;
            cbBox_dataFormat.SelectionChanged += CbBox_dataFormat_SelectionChanged;
            
        }

        private void CbBox_dataFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string dataFormatcheck = cbBox_dataFormat.SelectedItem as string;

            if (dataFormatcheck == "Bit")
            {
                cbBox_addressType.ItemsSource = addType_Bit;
                cbBox_addressType.SelectedIndex = 0;
            }
            else if (dataFormatcheck == "Byte")
            {
                cbBox_addressType.ItemsSource = addType_Byte;
                cbBox_addressType.SelectedIndex = 0;
            }
            else if (dataFormatcheck == "Word")
            {
                cbBox_addressType.ItemsSource = addType_Word;
                cbBox_addressType.SelectedIndex = 0;
            }
            else if (dataFormatcheck == "DWord")
            {
                cbBox_addressType.ItemsSource = addType_DWord;
                cbBox_addressType.SelectedIndex = 0;
            }
        }

        private void Btn_Write_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(plc != null)
                {
                    string addType = cbBox_addressType.SelectedItem as string;
                    string mainNo = txt_mainNo.Text;
                    string subNo = txt_subNo.Text;
                    int val = int.Parse(txt_value.Text);
                    string a = null;
                    if (addType == "I" || addType == "Q" || addType == "M")
                    {
                        a = addType + mainNo +"." + subNo;                      
                    }
                    else if(addType == "")
                    {

                    }
                    plc.Write(a, val);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Btn_Read_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if(plc != null)
                {
                    plc.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbBox_CpuType.ItemsSource = Enum.GetNames(typeof(CpuType));
                cbBox_CpuType.SelectedIndex = 5;

                cbBox_dataFormat.ItemsSource = dataFormat;
                cbBox_dataFormat.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Btn_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (plc != null)
            {
                plc.Close();
            }
            if (!plc.IsConnected)
            {
                MessageBox.Show("Plc is Disconnected!");
            }
        }

        private void Btn_Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CpuType cpu = (CpuType)Enum.Parse(typeof(CpuType), (string)cbBox_CpuType.SelectedItem);
                string ip = txtb_ipAdress.Text;
                short rack = short.Parse(txtb_rack.Text);
                short slot = short.Parse(txtb_slot.Text);
                plc = new Plc(cpu, ip, rack, slot);
                plc.Open();
                if (plc.IsConnected)
                {
                    MessageBox.Show("Plc is Connected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }





    }
}
