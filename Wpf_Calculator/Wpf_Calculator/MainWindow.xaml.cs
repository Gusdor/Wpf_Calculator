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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.JScript;


namespace Wpf_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    
    public partial class MainWindow : Window
    {    
        private byte OperandCount=0;
        private bool OperatorUsed = false;
        private bool NextTxtinputClear = false;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// This fucntion handles clearing and displaying digit on display.
        /// Also checks and limits the digits in operand to four digits
        /// </summary> 
        private void Button_Calculator_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            switch (content)
            {
                //Clear the display and operand count, insert 0
                case "C": 
                    TxtBox_Display.Text = "0";
                    OperandCount = 0;
                    OperatorUsed = false;
                    break;

                //Display operanad, increment digit count and store it for later.
                default:
                    if (NextTxtinputClear)
                    {
                        NextTxtinputClear = false;
                        TxtBox_Display.Text = "";
                    }
                    
                    if (TxtBox_Display.Text != "0" && OperandCount < 4)
                    {
                        TxtBox_Display.Text += content;
                        OperandCount++;
                    }
                    else if (TxtBox_Display.Text == "0")
                    {
                        TxtBox_Display.Text = content;
                        OperandCount++;
                    }

                    break;
            }         
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MathsOperations(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            if (false == OperatorUsed && (OperandCount >0 || NextTxtinputClear))
            {
                //set operater already used as true
                OperatorUsed = true;
                //clear digit count and add operator to string
                OperandCount = 0;
                TxtBox_Display.Text += content;
                NextTxtinputClear = false;
            }
        }

      

        /// <summary>
        /// This function implements the memory functions of the calculator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Memory_Functions(object sender, RoutedEventArgs e)
        {
            //to be implemented later

        }


        /// <summary>
        /// This function calculates the result of expression in text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            if (content == "=")
            {
                DataTable dt = new DataTable();

                try
                {
                    var result = dt.Compute(TxtBox_Display.Text, "");
                    TxtBox_Display.Text = System.Convert.ToString(result);
                    OperatorUsed = false;
                    NextTxtinputClear = true;
                    OperandCount = 0;

                }

                catch (OverflowException)
                {
                    TxtBox_Display.Text = "Overflow";
                }
                catch (Exception)
                {
                    TxtBox_Display.Text = "ERR";
                }

            }
        }



        
    }
}
