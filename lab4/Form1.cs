using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace lab3;

public partial class Form1 : Form
{
	Label label1 = new Label();
	private TextBox Xbox1;

	private TextBox Xbox2;

	private Button Cube;

	private Button Column;

	private Button Row;

	private Button SecArr;

	private GroupBox Check = new GroupBox();

	private RadioButton Check1 = new RadioButton();
	private RadioButton Check2 = new RadioButton();
	private RadioButton Check3 = new RadioButton();
	private RadioButton Check4 = new RadioButton();
	private DataTable Table = new DataTable();

	public Form1()
	{
		InitializeComponent();
		CreateControls();
		CreateCheckBox();
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

		SecArr = new Button();
		SecArr.Location = new Point(460, 240);
		SecArr.Size = new Size(150, 40);
		SecArr.Text = "ВводДвумМас";
		SecArr.Click += ClickBtnArr;

		this.Controls.Add(Column);
		this.Controls.Add(Xbox1);
		this.Controls.Add(Xbox2);
		this.Controls.Add(Row);
		this.Controls.Add(Cube);
		this.Controls.Add(SecArr);

	}

	public void EnterArr()
	{
		DataGridView ArrGrid = new DataGridView();
		Button Exit = new Button();
		Button Sum = new Button();
		NumericUpDown Row2Arr = new NumericUpDown();
		NumericUpDown Col2Arr = new NumericUpDown();

		Sum.Location = new Point(20, 100);
		Sum.Size = new Size(80, 30);
		Sum.Text = "Сумма";
		Sum.Click += (s, e) => RefreshResult(ArrGrid);

		Row2Arr.Location = new Point(20, 20);
		Row2Arr.Size = new Size(50, 30);
		Row2Arr.Minimum = 1;
		Row2Arr.Value = 3;
		Row2Arr.Click += (s, e) => InitGrid(ArrGrid, Row2Arr, Col2Arr);


		Col2Arr.Location = new Point(20, 60);
		Col2Arr.Size = new Size(50, 30);
		Col2Arr.Minimum = 1;
		Col2Arr.Value = 3;
		Col2Arr.Click += (s, e) => InitGrid(ArrGrid, Row2Arr, Col2Arr);

		ArrGrid.Location = new Point(100, 20);
		ArrGrid.Size = new Size(400, 200);
		ArrGrid.ScrollBars = ScrollBars.Both;

		Exit.Location = new Point(460, 240);
		Exit.Size = new Size(100, 60);
		Exit.Text = "Exit";


		label1.Text = $"Сумма Массива: {SumOfArr(ArrGrid)}";
		label1.Location = new Point(300, 240);
		label1.Size = new Size(150, 20);
		label1.ForeColor = Color.Blue;
		label1.Font = new Font("Arial", 10);



		this.Controls.Add(label1);
		this.Controls.Add(ArrGrid);
		this.Controls.Add(Exit);
		this.Controls.Add(Row2Arr);
		this.Controls.Add(Col2Arr);
		this.Controls.Add(Sum);



	}
	public void RefreshResult(DataGridView ArrGrid)
	{
		label1.Text = $"Сумма Массива: {SumOfArr(ArrGrid)}";
	}
	
	private void InitGrid(DataGridView arrGrid, NumericUpDown rowControl, NumericUpDown colControl)
{
    int n = (int)rowControl.Value;
    int m = (int)colControl.Value; // Исправлено: используем colControl

    DataTable Tabel = new DataTable();
    DataColumn x1;

    // Создание столбцов
    for (int i = 0; i < m; i++)
    {
        x1 = Tabel.Columns.Add("Ст" + (i + 1), typeof(Int32));
        x1.DefaultValue = 0;
    }

    // Создание строк
    for (int i = 0; i < n; i++)
    {
        DataRow y = Tabel.NewRow();
        Tabel.Rows.Add(y);
    }

    arrGrid.DataSource = Tabel;
}



	private int SumOfArr(DataGridView arrGrid)
	{
		int RowCount = arrGrid.Rows.Count;
		int ColCount = arrGrid.Columns.Count;
		int[,] matrix = new int[RowCount, ColCount];
		int sum = 0;

		for (int i = 0; i < RowCount; i += 1)
		{
			for (int j = 0; j < ColCount; j += 1)
			{
				if (arrGrid.Rows[i].Cells[j].Value != null &&
		int.TryParse(arrGrid.Rows[i].Cells[j].Value.ToString(), out int value))
				{
					sum += value;
				}

			}
		}
		return sum;
	}


	


	

	public void CreateCheckBox()
	{
		Check = new GroupBox();
		Check.Text = "обрДвумМас";
		Check.Location = new Point(30, 250);
		Check.Size = new Size(300, 100);

    Check1.Text = "Умн2"; 
    Check1.Location = new Point(10, 20); 
    

    Check2.Text = "сумма"; 
    Check2.Location = new Point(200, 20); 

    Check3.Text = "ЧислоПолЭлем"; 
    Check3.Location = new Point(10, 60); 

    Check4.Text = "ПолЭлемСтр"; 
    Check4.Location = new Point(200, 60); 
   

		Check.Controls.Add(Check1);
		Check.Controls.Add(Check2);
		Check.Controls.Add(Check3);
		Check.Controls.Add(Check4);

		this.Controls.Add(Check);
		
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
	
	private void ClickBtnArr(object sender, EventArgs e)
	{
		this.Controls.Clear();
		EnterArr();
	}
}
