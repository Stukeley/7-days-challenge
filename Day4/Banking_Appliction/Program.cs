using System;

namespace Banking_Appliction
{
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("Welcome!");
			Console.WriteLine("1 - Make a new account");
			Console.WriteLine("2 - Log in to an existing account");
			Console.WriteLine("3 - Forgot login");

			char c = Convert.ToChar(Console.Read());

			switch (c)
			{
				case '1':
					DatabaseOperations.AddNewUser();
					break;

				case '2':
					DatabaseOperations.LogIn();
					break;

				case '3':
					DatabaseOperations.ForgotLogin();
					break;

				default:
					Console.WriteLine("Please read instructions more carefully!");
					break;
			}

			if (CurrentAcc.CurrentAccount != null)
			{
				Console.Clear();

				while (true)
				{
					Console.WriteLine("1 - Change balance");
					Console.WriteLine("2 - See intrest rates");
					Console.WriteLine("3 - Log out");

					c = Convert.ToChar(Console.Read());

					switch (c)
					{
						case '1':
							DatabaseOperations.ChangeFunds();
							break;

						case '2':
							Console.WriteLine("Below 5000 -- 0.5%");
							Console.WriteLine("5000 - 15000 -- 0.7%");
							Console.WriteLine("15000 - 75000 -- 1.1%");
							Console.WriteLine("Above 75000 -- 2%");
							break;

						case '3':
							Console.WriteLine("Goodbye!");
							return;

						default:
							Console.WriteLine("Please read instructions more carefully!");
							break;
					}

					Console.Clear();
				}

			}



		}
	}
}
