using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Appliction
{
	internal class Account
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		// Login - Auto generated; NameSur + Id^2
		public string Login { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
		public double Funds { get; set; }
		public double Interest { get; set; }
	}
}
