using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab2
{


    public partial class Form1 : Form
    {
        private TextBox XBox1;
        private TextBox YBox1;
        private TextBox Xbox2;
        private TextBox YBox2;

        private Button Math1;

        private Button Cancel;

        public Form1()
        {

            InitializeComponent();
            CreateControls();
        }

        private void CreateControls()
        {
            XBox1 = new TextBox();
            XBox1.Location = new Point(20, 20);
            XBox1.Size = new Size(100, 20);
            XBox1.Text = "0";

            Xbox2 = new TextBox();
            Xbox2.Location = new Point(140, 20);
            Xbox2.Size = new Size(100, 20);
            Xbox2.Text = "0";

            YBox1 = new TextBox();
            YBox1.Location = new Point(20, 50);
            YBox1.Size = new Size(100, 20);
            YBox1.ReadOnly = true;
            

            YBox2 = new TextBox();
            YBox2.Location = new Point(140, 50);
            YBox2.Size = new Size(100, 20);
            YBox2.ReadOnly = true;

            Math1 = new Button();
            Math1.Location = new Point(85, 80);
            Math1.Text = "Нажми";
            Math1.Click += ClickBtnMath;

            Cancel = new Button();
            Cancel.Location = new Point(85, 120);
            Cancel.Text = "выйти";


            this.Controls.Add(XBox1);
            this.Controls.Add(Xbox2);
            this.Controls.Add(YBox1);
            this.Controls.Add(YBox2);
            this.Controls.Add(Math1);
            this.Controls.Add(Cancel);
        }

            private void ClickBtnCancel(object sender, EventArgs e)
        {
            this.Close();
        }
            private void ClickBtnMath(object sender, EventArgs e)
            {
            if (double.TryParse(XBox1.Text, out double x))
            {
                double y = ((Math.Sqrt(Math.Abs(x - 1)) - Math.Pow(Math.Abs(x), 1.0 / 3))
                / (1 + (x * x) / 2 + Math.Pow(x, 4) / 4)) * Math.Abs(Math.Pow(x, 3) - 4);
                YBox1.Text = $"{y:E5}";
            }
            else
            {
                YBox1.Text = "некоректное число";
            }
            if (double.TryParse(Xbox2.Text, out double x1))
            {
                double z = 2.0;

                double lnPart = Math.Log(x1 * x1 + 1);
                double numerator1 = (x1 / 3) + (z / 4);
                double denominator1 = (Math.Pow(Math.Cos(z), 2) + Math.Pow(Math.Cos(x1), 2)) * (Math.Pow(x1, 3) + Math.Pow(z, 3));
                double firstPart = lnPart * (numerator1 / denominator1);

                double numerator2 = x1 + z;
                double denominator2 = (z * z) * (x1 * x1 * x1);
                double expPart = Math.Exp(-x1 + z);
                double secondPart = (numerator2 / denominator2) * expPart;

                double result = firstPart + secondPart;
                YBox2.Text = $"{result}";
            }
            else
            {
                YBox2.Text = "некоректное число";
            }   

        }


    }
}
 
//            textBox1 = new TextBox();
//             textBox1.Location = new Point(20, 0);
//             textBox1.Size = new Size(100, 20);
//             textBox1.Text = "0";

//             // TextBox2 для вывода
//             textBox2 = new TextBox();
//             textBox2.Location = new Point(20, 50);
//             textBox2.Size = new Size(100, 20);
//             textBox2.ReadOnly = true; // Только для чтения

//             // Кнопка вычисления
//             button1 = new Button();
//             button1.Location = new Point(20, 80);
//             button1.Size = new Size(100, 30);
//             button1.Text = "Вычислить";
//             button1.Click += button1_Click;

//             // Кнопка выхода
//             button2 = new Button();
//             button2.Location = new Point(20, 120);
//             button2.Size = new Size(100, 30);
//             button2.Text = "Выход";
//             button2.Click += button2_Click;

//             // Добавляем контролы на форму
//             this.Controls.Add(textBox1);
//             this.Controls.Add(textBox2);
//             this.Controls.Add(button1);
//             this.Controls.Add(button2);

//             // Настройка формы
//             this.Text = "Lab2 - Вычисление синуса";
//             this.Size = new Size(400, 400);
//         }

//         private void button2_Click(object sender, EventArgs e)
//         {
//             this.Close();
//         }

//         private void button1_Click(object sender, EventArgs e)
//         {
//             if (double.TryParse(textBox1.Text, out double x))
//             {
//                 double y = Math.Sin(x);
//                 textBox2.Text = $"{y:0.####}";
//             }
//             else
//             {
                // MessageBox.Show("Введите корректное число!", "Ошибка", 
                //               MessageBoxButtons.OK, MessageBoxIcon.Error);
                // textBox1.Focus();
                // textBox1.SelectAll();
//             }
//         }
//     }
// }