//-----------------------------------------------------------------------
// <copyright file="Mainwindow.xaml.cs" company="Expro North Sea Ltd">
//     Copyright (c) Expro North Sea Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Wpf_Calculator
{
    using System;
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow extended application markup language
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This counts the number of digits in a operand
        /// </summary>
        private byte operandCount = 0;

        /// <summary>
        /// This variables keeps track of if operator is already used or not
        /// </summary>
        private bool operatorUsed = false;

        /// <summary>
        /// This variables helps to decide if it is safe to clear display on next key input
        /// </summary>
        private bool nextTxtinputClear = false;

        /// <summary>
        /// Initializes a new instance of the MainWindow class
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// This function handles clearing and displaying digit on display.
        /// Also checks and limits the digits in operand to four digits
        /// </summary>
        /// <param name="sender">All digit buttons plus clear button</param>
        /// <param name="e">button click event</param>
        private void Button_Calculator_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            switch (content)
            {
                /*Clear the display and operand count, insert 0*/
                case "C":
                    TxtBox_Display.Text = "0";
                    this.operandCount = 0;
                    this.operatorUsed = false;
                    break;

                /*Display operanad, increment digit count and store it for later.*/
                default:
                    if (this.nextTxtinputClear)
                    {
                        this.nextTxtinputClear = false;
                        TxtBox_Display.Text = string.Empty;
                    }

                    if (TxtBox_Display.Text != "0" && this.operandCount < 4)
                    {
                        TxtBox_Display.Text += content;
                        this.operandCount++;
                    }
                    else if (TxtBox_Display.Text == "0")
                    {
                        TxtBox_Display.Text = content;
                        this.operandCount++;
                    }

                    break;
            }
        }

        /// <summary>
        /// This function handles the operator keys and adds them to text string
        /// </summary>
        /// <param name="sender">math operations buttons</param>
        /// <param name="e">button click event</param>
        private void MathsOperations(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            if (false == this.operatorUsed && (this.operandCount > 0 || this.nextTxtinputClear))
            {
                /*set operater already used as true*/
                this.operatorUsed = true;
                /*clear digit count and add operator to string*/
                this.operandCount = 0;
                TxtBox_Display.Text += content;
                this.nextTxtinputClear = false;
            }
        }

        /// <summary>
        /// This function implements the memory functions of the calculator
        /// </summary>
        /// <param name="sender">memory function buttons</param>
        /// <param name="e">button click event</param>
        private void Memory_Functions(object sender, RoutedEventArgs e)
        {
            /*to be implemented later*/
        }

        /// <summary>
        /// This function calculates the result of expression in text box
        /// </summary>
        /// <param name="sender">button equal</param>
        /// <param name="e">equal to button click event</param>
        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            if (content == "=")
            {
                DataTable dt = new DataTable();

                try
                {
                    var result = dt.Compute(TxtBox_Display.Text, string.Empty);
                    TxtBox_Display.Text = System.Convert.ToString(result);
                    this.operatorUsed = false;
                    this.nextTxtinputClear = true;
                    this.operandCount = 0;
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

        /// <summary>
        /// This function displays information about calculator in a message box.
        /// </summary>
        /// <param name="sender">Help menu item</param>
        /// <param name="e">About item click</param>
        private void Help_About_Calculator(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WPF Practice App-1", "About");
        }

        /// <summary>
        /// This function is part of Edit Menu - does copy operation
        /// </summary>
        /// <param name="sender">Edit Menu item</param>
        /// <param name="e">edit item click</param>
        private void Edit_Copy_Calculator_Click(object sender, RoutedEventArgs e)
        {
            /*Feature will be implemented later*/
        }

        /// <summary>
        /// This function is part of Edit Menu - does Paste Operation
        /// </summary>
        /// <param name="sender">Edit menu item</param>
        /// <param name="e">Paste item click</param>
        private void Edit_Paste_Calculator_Click(object sender, RoutedEventArgs e)
        {
            /*Feature will be implemented later*/
        }

        /// <summary>
        /// This function is part of Edit Menu - does Cut operation
        /// </summary>
        /// <param name="sender">Edit menu item</param>
        /// <param name="e">cut item click</param>
        private void Edit_Cut_Calculator_Click(object sender, RoutedEventArgs e)
        {
            /*Feature will be implemented later*/
        }
    }
}