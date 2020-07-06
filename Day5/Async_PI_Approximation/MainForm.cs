using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async_PI_Approximation
{
	public partial class MainForm : Form
	{
		// Pen used to draw the circle and square
		private Pen MainPen = new Pen(Color.Black);

		// Brush used to draw the points inside
		private SolidBrush InsideBrush = new SolidBrush(Color.LimeGreen);

		// Brush used to draw the points outside
		private SolidBrush OutsideBrush = new SolidBrush(Color.Cyan);

		private int Amount;

		private int W, H;

		private bool WasClicked = false;

		public MainForm()
		{
			InitializeComponent();

			MainPen.Width = 1.5f;

			W = DrawPanel.Width;
			H = DrawPanel.Height;
		}

		private void RunButton_Click(object sender, EventArgs e)
		{
			bool czy = int.TryParse(InputBox.Text, out Amount);

			if (czy)
			{
				if (Amount < 1)
				{
					// Incorrect number!
					MessageBox.Show("Please input a correct number!", "Incorrect number", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Amount = 5000;
					InputBox.Text = "5000";
				}
			}
			else
			{
				MessageBox.Show("Please input a correct number!", "Incorrect number", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Amount = 5000;
				InputBox.Text = "5000";
			}

			WasClicked = true;
			DrawPanel.Refresh();
		}

		private async void DrawPanel_Paint(object sender, PaintEventArgs e)
		{
			// Make middle point the center for easier drawing
			e.Graphics.TranslateTransform(W / 2f, H / 2f);

			// Draw the circle and rectangle
			Rectangle rect = new Rectangle(-W / 2, -H / 2, W - 1, H - 5);
			e.Graphics.DrawRectangle(MainPen, rect);
			e.Graphics.DrawEllipse(MainPen, rect);

			// Only start drawing after the button was clicked
			if (WasClicked)
			{
				double PI;
				int inside = 0, allPoints = 0;

				for (int i = 0; i < Amount; i++)
				{
					var task = Task.Run(() => GetNextPoint());

					Application.DoEvents();

					allPoints++;

					var next = await task;

					// Calculate if the result is inside the circle or not (points ON the circle count as outside)
					if ((next.X * next.X) + (next.Y * next.Y) < (W / 2 * H / 2))
					{
						inside++;
						// Draw with a lime green colour
						e.Graphics.FillRectangle(InsideBrush, next.X, next.Y, 1, 1);
					}
					else
					{
						// Draw with a cyan colour
						e.Graphics.FillRectangle(OutsideBrush, next.X, next.Y, 1, 1);
					}

					// Sleep for 1 millisecond every 50 points for better visualization
					if (allPoints % 50 == 0)
					{
						Thread.Sleep(1);
					}
				}

				PI = 4 * (inside / (double)allPoints);

				MessageBox.Show($"Pi for {Amount} points is approximated to be: {PI}", "Approximation complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void richTextBox1_Enter(object sender, EventArgs e)
		{
			richTextBox1.Text = "";
		}

		private Point GetNextPoint()
		{
			// Randomize a point on the panel

			Point next = new Point();

			Random rng = new Random();

			// Remember that the middle point is (0,0)
			next.X = rng.Next(-(W / 2), (W / 2) + 1);
			next.Y = rng.Next(-(H / 2), (H / 2) + 1);

			return next;
		}
	}
}
