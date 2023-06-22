using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        Double resultValue = 0;
        String operationPerformed = "";
        bool isOperationPerformed = false;

        string currentExpression = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == ",")
            {
                if (!currentExpression.Contains(",") && currentExpression.Length > 0 && !isOperationPerformed)
                {
                    currentExpression += button.Text;
                    textBox_Result.Text = FormatNumber(currentExpression);
                }
            }
            else
            {
                currentExpression += button.Text;
                textBox_Result.Text = FormatNumber(currentExpression);
            }
        }

        private string FormatNumber(string number)
        {
            string strippedNumber = number.Replace(",", "");
            double parsedNumber;

            if (double.TryParse(strippedNumber, out parsedNumber))
            {
                return parsedNumber.ToString("N0");
            }

            return number;
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(currentExpression))
            {
                EvaluateExpression();
            }

            operationPerformed = button.Text;
            currentExpression = ""; 
            isOperationPerformed = true;

            labelCurrentOperation.Text = resultValue + " " + operationPerformed;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            EvaluateExpression();
            operationPerformed = "";
            labelCurrentOperation.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox_Result.Text.Length > 0)
                textBox_Result.Text = textBox_Result.Text.Substring(0, textBox_Result.Text.Length - 1);

            if (textBox_Result.Text.Length <= 0)
                textBox_Result.Text = "0";
            currentExpression = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            currentExpression = "";
            resultValue = 0;
            labelCurrentOperation.Text = "";
        }

        private void EvaluateExpression()
        {
            if (isOperationPerformed && !string.IsNullOrEmpty(currentExpression))
            {
                double currentValue = Double.Parse(currentExpression);

                try
                {
                    switch (operationPerformed)
                    {
                        case "+":
                            resultValue += currentValue;
                            break;
                        case "-":
                            resultValue -= currentValue;
                            break;
                        case "*":
                            resultValue *= currentValue;
                            break;
                        case "/":
                            resultValue /= currentValue;
                            break;
                        default:
                            break;
                    }

                    if (double.IsInfinity(resultValue))
                    {
                        textBox_Result.Text = "Can't divide by 0";
                    }
                    else
                    {
                        textBox_Result.Text = resultValue.ToString("N0"); 
                    }
                }
                catch (Exception)
                {
                    textBox_Result.Text = "Error";
                }

                currentExpression = "";
            }
            else
            {
                double currentValue = Double.Parse(textBox_Result.Text);
                resultValue = currentValue;
            }

            isOperationPerformed = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox_Result_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

