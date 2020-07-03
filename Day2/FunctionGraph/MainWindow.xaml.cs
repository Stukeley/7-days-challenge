using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunctionGraph
{
	// Simple function graph - just polynomials or exponential
	// Example functions that will work: x^2;	3x^3 + 12x + e;	e^(3x)
	public partial class MainWindow : Window
	{
		private string Input;

		private Point MiddlePoint;
		public MainWindow()
		{
			InitializeComponent();

			// TODO: set width and height here
		}

		private void DrawFunction()
		{
			// Width of lines in pixels, increasing this will increase the graph's accuracy but increase the time it takes to draw as well
			double d = 5;

			// Draw the axes - just one vertical and one horizontal line

			double xMax = FunctionCanvas.MaxWidth;
			double yMax = FunctionCanvas.MaxHeight;

			MiddlePoint = new Point(xMax / 2, yMax / 2);

			var xAxis = new GeometryGroup();
			xAxis.Children.Add(new LineGeometry(new Point(0, MiddlePoint.Y), new Point(xMax, MiddlePoint.Y)));

			var xAxisPath = new Path
			{
				StrokeThickness = 1,
				Stroke = Brushes.Black,
				Data = xAxis
			};

			FunctionCanvas.Children.Add(xAxisPath);


			var yAxis = new GeometryGroup();
			yAxis.Children.Add(new LineGeometry(new Point(MiddlePoint.X, 0), new Point(MiddlePoint.X, yMax)));

			var yAxisPath = new Path
			{
				StrokeThickness = 1,
				Stroke = Brushes.Black,
				Data = yAxis
			};

			FunctionCanvas.Children.Add(yAxisPath);

			// End of drawing axes

			// Now calculate the function values
			// TODO: make it async - 2 processes, one from 0 to half, one from half to full !

			var points = new List<Point>();

			// The values are modified because here (0,0) means the lower left corner of the canvas, not the middle

			for (double i = d; i <= xMax; i += d)
			{
				double x = i;
				double y = FunctionValue(x - MiddlePoint.X);

				if (y < -MiddlePoint.Y)
				{
					// If Y is below the lower bound of the canvas, do not show it
					continue;
				}
				else if (y < 0)
				{
					// Lower half of the canvas
					y += MiddlePoint.Y;
				}
				else if (y > MiddlePoint.Y)
				{
					// If Y is above the upper bound of the canvas, do not show it
					continue;
				}
				else
				{
					// Upper half of the canvas
					y += MiddlePoint.Y;
				}

				y = FunctionCanvas.MaxHeight - y;

				points.Add(new Point(x, y));
			}

			// Draw a polyline - all points at once

			var polyline = new Polyline
			{
				StrokeThickness = 1,
				Stroke = Brushes.Red,
				Points = new PointCollection(points),
			};

			FunctionCanvas.Children.Add(polyline);
		}

		// Returns the fuction's value for a given x
		private double FunctionValue(double x)
		{
			if (Input == null)
			{
				return 0;
			}

			double totalValue = 0;

			for (int i = 0; i < Input.Length; i++)
			{
				double partialValue = 1;

				char c = Input[i];

				// + - * /
				// By default it's addition
				char sign = '+';

				// Get the next character
				// If it's a numer, it's a coefficient (ex. 2x^3 ; 2 - the coefficient)
				// If it's x (or e), first check if there's a ^ right after; if not, just multiply partialValue by it
				// If yes, get whatever is after the ^ (it's the exponent) and raise x (or e) to that power
				// If it's a sign + or -, note it and use it later to either add or subtract the value from totalValue

				switch (c)
				{
					case 'x':
						// Check if the next char is ^
						if (Input[i + 1] == '^')
						{
							string exponent = "";
							i += 2;

							for (; i < Input.Length; i++)
							{
								if (Input[i] >= 48 && Input[i] <= 57)
								{
									exponent += Input[i];
								}
								else if (Input[i] == '(')
								{
									continue;
								}
								else if (Input[i] == '-')
								{
									exponent = "-";
								}
								else if (Input[i] == 'e')
								{
									partialValue *= Math.Pow(x, Math.E);
									break;
								}
								else if (Input[i] == 'x')
								{
									partialValue *= Math.Pow(x, x);
									break;
								}
								else
								{
									break;
								}
							}
							bool czy = int.TryParse(exponent, out int exp);
							if (czy)
							{
								partialValue *= Math.Pow(x, exp);
							}
						}
						else
						{
							partialValue *= x;
						}
						break;

					case 'e':
						// Check if the next char is ^
						if (Input[i + 1] == '^')
						{
							string exponent = "";
							i += 2;

							for (; i < Input.Length; i++)
							{
								if (Input[i] >= 48 && Input[i] <= 57)
								{
									exponent += Input[i];
								}
								else if (Input[i] == '(')
								{
									continue;
								}
								else if (Input[i] == '-')
								{
									exponent = "-";
								}
								else if (Input[i] == 'e')
								{
									partialValue *= Math.Pow(Math.E, Math.E);
									break;
								}
								else if (Input[i] == 'x')
								{
									partialValue *= Math.Pow(Math.E, x);
									break;
								}
								else
								{
									break;
								}
							}
							bool czy = int.TryParse(exponent, out int exp);
							if (czy)
							{
								partialValue *= Math.Pow(Math.E, exp);
							}
						}
						else
						{
							partialValue *= Math.E;
						}
						break;

					case '+':
						sign = '+';
						break;

					case '-':
						sign = '-';
						break;

					default:
						if (c >= 48 && c <= 57)
						{
							string coefficient = "";
							for (; i < Input.Length; i++)
							{
								if (Input[i] >= 48 && Input[i] <= 57)
								{
									coefficient += Input[i];
								}
							}
							bool conv = int.TryParse(coefficient, out int partialV);
							if (conv)
							{
								partialValue = partialV;
							}
						}
						break;
				}

				switch (sign)
				{
					case '+':
						totalValue += partialValue;
						break;

					case '-':
						totalValue -= partialValue;
						break;
				}
			}

			return totalValue;
		}

		private void DrawButton_Click(object sender, RoutedEventArgs e)
		{
			// Clear previous graphs
			FunctionCanvas.Children.Clear();

			// Get input and call the drawing function
			// Remove spaces and make all letters lowercase (X->x, E->e)
			Input = InputBox.Text.Trim().ToLower();
			Input = Input.Replace(" ", string.Empty);

			DrawFunction();
		}
	}
}
