using System;

namespace Brainfuck_Compiler
{
	internal class Program
	{
		// The array of cells that are modified by Brainfuck code
		private static char[] Cells = new char[30000];

		// Current index in the above array
		private static int index = 0;

		// Example:
		// ++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.
		// Hello World!\n

		private static void Main()
		{
			Console.WriteLine("Welcome! Please input Brainfuck code below. Press Enter when you're done.");
			Console.WriteLine("Be ready to input additional characters if you include , in your code.");

			string input = Console.ReadLine();

			try
			{
				Interpret(input);
			}
			catch (Exception e)
			{
				// We will throw an exception whenever something goes wrong
				Console.WriteLine(e.Message);
				Console.Read();
				return;
			}

			Console.WriteLine(Environment.NewLine + "Would you like to see cell values? Y/N");
			char z = Convert.ToChar(Console.Read());

			if (z == 'y' || z == 'Y')
			{
				for (int i = 0; i < Cells.Length; i++)
				{
					// \x0000 is the default char value
					if (Cells[i] != '\x0000')
					{
						Console.Write($"{i} - {Cells[i]}\t");
					}
				}
			}

			Console.WriteLine();
			Console.Read();
		}

		// Interpret the Brainfuck code that has been input, and show the output on screen
		private static void Interpret(string input)
		{
			Console.WriteLine();

			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];

				// Check the current character

				switch (c)
				{
					case '+':
						Cells[index]++;
						break;

					case '-':
						Cells[index]--;
						break;

					case '>':
						index++;
						break;

					case '<':
						index--;
						break;

					case '.':
						Console.Write(Cells[index]);
						break;

					case ',':
						Cells[index] = Convert.ToChar(Console.Read());
						break;

					case '[':

						if (Cells[index] == 0)
						{
							bool found = false;
							for (int j = i + 1; j < input.Length; j++)
							{
								if (input[j] == ']')
								{
									i = j;
									found = true;
									break;
								}
							}

							if (!found)
							{
								Console.WriteLine("Incorrect bracket matching!");
								throw new ArgumentException();
							}
						}

						break;

					case ']':

						if (Cells[index] != 0)
						{
							bool found = false;
							for (int j = i - 1; j >= 0; j--)
							{
								if (input[j] == '[')
								{
									i = j;
									found = true;
									break;
								}
							}

							if (!found)
							{
								Console.WriteLine("Incorrect bracket matching!");
								throw new ArgumentException();
							}
						}

						break;

					default:
						Console.WriteLine("An incorrect character was input!");
						throw new ArgumentException();
				}

				// Check that everything's right
				if (index < 0)
				{
					Console.WriteLine("Index is negative! Check your Brainfuck code.");
					throw new IndexOutOfRangeException();
				}
			}
		}
	}
}
