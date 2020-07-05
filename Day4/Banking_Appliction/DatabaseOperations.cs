using System;
using System.Linq;

namespace Banking_Appliction
{
	internal static class DatabaseOperations
	{
		// Checks if a date is valid (ex. not 30th of February)
		public static bool ValidateDate(DateTime date)
		{
			if (date.Day < 1 || date.Day > 31 || date.Month < 1 || date.Month > 13 || date.Year >= DateTime.Now.Year)
			{
				return false;
			}
			else if (date.Year % 4 == 0 && date.Month == 2 && date.Day > 29)
			{
				return false;
			}
			else if (date.Month == 2 && date.Day > 28)
			{
				return false;
			}
			else if ((date.Month == 4 || date.Month == 6 || date.Month == 9 || date.Month == 11) && date.Day > 30)
			{
				return false;
			}

			return true;
		}

		// Creates a bank account for a user
		public static void AddNewUser()
		{
			Console.WriteLine("You will now be asked about a few things to set up your accounts. Please enter the information you're asked for, then press Enter.");

			Console.WriteLine("Name: ");
			string name = Console.ReadLine();

			Console.WriteLine("Surname: ");
			string surname = Console.ReadLine();

			Console.WriteLine("Date of birth: day: ");
			string day = Console.ReadLine();
			Console.WriteLine("month: ");
			string month = Console.ReadLine();
			Console.WriteLine("year: ");
			string year = Console.ReadLine();

			// Parse Date of Birth, check if it's real
			bool czy1 = int.TryParse(day, out int d);
			bool czy2 = int.TryParse(month, out int m);
			bool czy3 = int.TryParse(year, out int y);

			DateTime date;

			if (czy1 && czy2 && czy3)
			{
				try
				{
					date = new DateTime(y, m, d);
				}
				catch (Exception e)
				{
					Console.WriteLine($"{e.Message}");
					return;
				}
			}
			else
			{
				Console.WriteLine("Incorrect date format! Please try again.");
				return;
			}

			if (!ValidateDate(date))
			{
				Console.WriteLine("Date doesn't exist! Please try again.");
			}

			// Generate the user's unique login - for that, we need the previous user's Id, so that we know the current one

			string login;

			using (var db = new AccountContext())
			{
				var previousId = (from a in db.Accounts orderby a.Id descending select a).FirstOrDefault().Id;

				previousId++;

				login = name + surname.Substring(0, 3) + Math.Pow((previousId), 2).ToString();

				// Save new user in the database

				var account = new Account()
				{
					Name = name,
					Surname = surname,
					DateOfBirth = date,
					Login = login
				};

				db.Accounts.Add(account);
				db.SaveChanges();

				// Confirm that everything's OK and display the login

				Console.WriteLine($"Account created! Your unique login is: {login} Please keep it around.");
			}

			// Make it so that the user is automatically logged in after making an account
			using (var db = new AccountContext())
			{
				CurrentAcc.CurrentAccount = (from a in db.Accounts where a.Login == login select a).FirstOrDefault();
			}
		}

		public static void LogIn()
		{
			Console.WriteLine("Please input your login: ");
			string login = Console.ReadLine();

			using (var db = new AccountContext())
			{
				var accounts = from a in db.Accounts where a.Login == login select a;
				if (accounts.Count() == 0)
				{
					Console.WriteLine("Account was not found!");
				}
				else
				{
					CurrentAcc.CurrentAccount = accounts.FirstOrDefault();
				}
			}
		}

		public static void ForgotLogin()
		{
			Console.WriteLine("Please input your name: ");
			string name = Console.ReadLine();

			Console.WriteLine("surname: ");
			string surname = Console.ReadLine();

			Console.WriteLine("Please input your date of birth: day: ");
			string day = Console.ReadLine();
			Console.WriteLine("month: ");
			string month = Console.ReadLine();
			Console.WriteLine("year: ");
			string year = Console.ReadLine();

			// Parse Date of Birth, check if it's real
			bool czy1 = int.TryParse(day, out int d);
			bool czy2 = int.TryParse(month, out int m);
			bool czy3 = int.TryParse(year, out int y);

			DateTime date;

			if (czy1 && czy2 && czy3)
			{
				try
				{
					date = new DateTime(y, m, d);
				}
				catch (Exception e)
				{
					Console.WriteLine($"{e.Message}");
					return;
				}
			}
			else
			{
				Console.WriteLine("Incorrect date format! Please try again.");
				return;
			}

			if (!ValidateDate(date))
			{
				Console.WriteLine("Date doesn't exist! Please try again.");
			}

			// Check if the input user exists, and if they do, 
			using (var db = new AccountContext())
			{
				var acc = (from a in db.Accounts where a.Name == name && a.Surname == surname && a.DateOfBirth == date select a).FirstOrDefault();

				Console.WriteLine($"Your login is: {acc.Login}. Please keep it around!");
				CurrentAcc.CurrentAccount = acc;
			}
		}

		public static void ChangeFunds()
		{
			if (CurrentAcc.CurrentAccount == null)
			{
				Console.WriteLine("User not logged in!");
				return;
			}

			Console.WriteLine("Would you like to add funds, or take them?");
			Console.WriteLine("1 - Add");
			Console.WriteLine("2 = Take");

			char c = Convert.ToChar(Console.Read());

			switch (c)
			{
				case '1':
					Console.WriteLine("How much would you like to add?");
					var input = Console.ReadLine();
					var czy = double.TryParse(input, out double value);
					if (czy)
					{
						// Open database
						using (var db = new AccountContext())
						{
							var dbUser = (from a in db.Accounts where a.Login == CurrentAcc.CurrentAccount.Login select a).FirstOrDefault();

							// Update funds
							dbUser.Funds += value;

							// Update interest
							if (dbUser.Funds > 75000d)
							{
								dbUser.Interest = 0.02d;
							}
							else if (dbUser.Funds > 15000d)
							{
								dbUser.Interest = 0.011d;
							}
							else if (dbUser.Funds > 5000d)
							{
								dbUser.Interest = 0.007d;
							}
							else
							{
								dbUser.Interest = 0.005d;
							}

							db.SaveChanges();
						}
					}

					break;

				case '2':
					Console.WriteLine("How much would you like to take?");
					input = Console.ReadLine();
					czy = double.TryParse(input, out value);
					if (czy)
					{
						// Open database
						using (var db = new AccountContext())
						{
							var dbUser = (from a in db.Accounts where a.Login == CurrentAcc.CurrentAccount.Login select a).FirstOrDefault();

							// Update funds
							dbUser.Funds -= value;

							// Update interest
							if (dbUser.Funds > 75000d)
							{
								dbUser.Interest = 0.02d;
							}
							else if (dbUser.Funds > 15000d)
							{
								dbUser.Interest = 0.011d;
							}
							else if (dbUser.Funds > 5000d)
							{
								dbUser.Interest = 0.007d;
							}
							else
							{
								dbUser.Interest = 0.005d;
							}

							db.SaveChanges();
						}
					}

					break;

				default:
					Console.WriteLine("Please read instructions more carefully!");
					break;
			}
		}
	}
}
