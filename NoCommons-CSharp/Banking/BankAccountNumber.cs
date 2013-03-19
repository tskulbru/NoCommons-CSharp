using System;
using System.Text;
using NoCommonsCSharp.Common;

namespace NoCommonsCSharp.Banking
{
	public class BankAccountNumber : StringNumber
	{
		public BankAccountNumber(string accountNumber) : base(accountNumber) {
		}

		/// <summary>
		/// The four first digits of the BankAccountNumber is known as the Registernumber
		/// </summary>
		/// <returns>The registernumber.</returns>
		public string GetRegisternummer() {
			return Value.Substring(0, 4);
		}

		/// <summary>
		/// The two digits after the Registernumber is often use to identify an account type.
		/// </summary>
		/// <returns>The account type.</returns>
		public string GetAccountType() {
			return Value.Substring(4, 2);
		}

		/// <summary>
		/// The 6 digits after the Registernumber are known as the account.
		/// </summary>
		/// <returns>The account.</returns>
		public string GetAccount() {
			return Value.Substring(4, 6);
		}
	
		/// <summary>
		/// Returns the Accountnumber as a string, formatted with '.''s seperating the 
		/// Registernumber, AccountType and end part
		/// </summary>
		/// <returns>The grouped value.</returns>
		public string GetGroupedValue() {
			StringBuilder sb = new StringBuilder();
			sb.Append(GetRegisternummer()).Append(".");
			sb.Append(GetAccountType()).Append(".");
			sb.Append(GetPartAfterAccountType());
			return sb.ToString();
		}
		
		private string GetPartAfterAccountType() {
			return Value.Substring(6);
		}
	}
}

