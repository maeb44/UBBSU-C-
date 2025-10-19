using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection.Emit;

namespace lab3;

public partial class Form1 : Form
{
	private TextBox Xbox1;

	private TextBox Xbox2;

	private Button Cube;

	private Button Column;

	private Button Row;

	

	public Form1()
	{
		InitializeComponent();
		CreateControls();
	}

	private void CreateControls()
	{
		Xbox1 = new TextBox();
		Xbox1.Location = new Point(20, 20);
		Xbox1.Size = new Size(200, 220);
		Xbox1.Multiline = true;
		Xbox1.ScrollBars = ScrollBars.Vertical;
		Xbox1.Text = "0";

		Xbox2 = new TextBox();
		Xbox2.Location = new Point(240, 20);
		Xbox2.Size = new Size(200, 220);
		Xbox2.Multiline = true;
		Xbox2.ScrollBars = ScrollBars.Vertical;
		Xbox2.Text = "0";
		Xbox2.ReadOnly = true;

		Column = new Button();
		Column.Location = new Point(460, 20);
		Column.Size = new Size(150, 60);
		Column.Text = "одномМассивВCol";
		Column.Click += ClickBtnCol;

		Cube = new Button();
		Cube.Location = new Point(460, 90);
		Cube.Size = new Size(150, 60);
		Cube.Text = "Куб";
		Cube.Click += ClickBtnCube;

		Row = new Button();
		Row.Location = new Point(460, 160);
		Row.Size = new Size(150, 60);
		Row.Text = "одномМассивВRow";
		Row.Click += ClickBtnRow;




		this.Controls.Add(Column);
		this.Controls.Add(Xbox1);
		this.Controls.Add(Xbox2);
		this.Controls.Add(Row);
		this.Controls.Add(Cube);



	}
	private void ClickBtnCube(object sender, EventArgs e)
	{
		Xbox2.Text = "";

		string input = Xbox1.Text;
		String[] StrArr = input.Split(' ');
		for (int i = 0; i < StrArr.Length; i++)
		{
			if (int.TryParse(StrArr[i], out int number))
			{
				Xbox2.Text += number + "\t" + Math.Pow(number, 2) + "\t" + Math.Pow(number, 3) + "\r\n";
			}
			else
			{
				Xbox2.Text += "Некоректное число\r\n";
			}
		}
	}
	private void ClickBtnCol(object sender, EventArgs e)
	{
		Xbox2.Text = "";

		string input = Xbox1.Text;
		String[] StrArr = input.Split(' ');
		for (int i = 0; i < StrArr.Length; i++)
		{
			if (int.TryParse(StrArr[i], out int number))
			{
				Xbox2.Text += number + "\r\n";
			}
			else
			{
				Xbox2.Text += "Некоректное число\r\n";
			}
		}
	}
		private void ClickBtnRow(object sender, EventArgs e)
	{
		Xbox2.Text = "";

		string input = Xbox1.Text;
		String[] StrArr = input.Split(' ');
		for (int i = 0; i < StrArr.Length; i++)
		{
			if (int.TryParse(StrArr[i], out int number))
			{
				Xbox2.Text += number + " ";
			}
			else
			{
				Xbox2.Text += "Некоректное число\r\n";
			}
		}
	}
	
	
}
