using System.Data.Entity;

namespace Banking_Appliction
{
	internal class AccountContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
	}
}
