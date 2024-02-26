using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadTextBoxValues();
        }

        private void LoadTextBoxValues()
        {
            textBoxN.Text = Properties.Settings.Default.textBoxN;
            textBoxListOfNumbers.Text = Properties.Settings.Default.textBoxListOfNumbers;
        }

        private void SaveTextBoxValues()
        {
            Properties.Settings.Default.textBoxN = textBoxN.Text;
            Properties.Settings.Default.textBoxListOfNumbers = textBoxListOfNumbers.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxN.Text, out int n))
            {
                string[] numbers = textBoxListOfNumbers.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                int[] sequence = new int[numbers.Length];
                bool validInput = true;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (!int.TryParse(numbers[i], out sequence[i]))
                    {
                        validInput = false;
                        break;
                    }
                }

                if (validInput)
                {
                    string result = Logic.Check(sequence, n);
                    MessageBox.Show(result);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите только числа в поле 'Последовательность чисел'.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение для n.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTextBoxValues();
        }
    }
    }

public class Logic
{
    public static string Check(int[] sequence, int n)
    {
        string msg = "";
        bool found = false;
        int position = -1;

        for (int i = 0; i < sequence.Length - n + 1; i++)
        {
            bool match = true;
            for (int j = 0; j < n - 1; j++)
            {
                if (sequence[i + j] != sequence[i + j + 1])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                found = true;
                position = i + 1;
                break;
            }
        }

        if (found)
        {
            if (n == 0)
            {
                msg = "Не может быть последовательности из 0 чисел.";
            }
            else
            {
                msg = $"В последовательности найдена {n} одинаковых соседних чисел, начиная с позиции {position}.";
            }
        }
        else
        {
            msg = $"В последовательности нет {n} одинаковых соседних чисел.";
        }
        return msg;
    }
}

