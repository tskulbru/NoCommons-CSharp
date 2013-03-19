using System;
using NoCommonsCSharp.Common;

namespace NoCommonsCSharp.Banking
{
	/// <summary>
	/// Provides methods that validates if a accountnumber is valid with
	/// respect to syntax (length, and digits only) and that the checksum digit
	/// is correct.
	/// </summary>
	public class BankAccountValidator : StringNumberValidator
	{
		const int LENGTH = 11;
		const string NOT_ALLOWED_AS_LEADING = "0000";

		protected const int ACCOUNTTYPE_NUM_DIGITS = 2;
		protected const int REGISTERNUMBER_NUM_DIGITS = 4;

		public const string ERROR_LEADING_ZEROS = "Leading zeros too many : ";

		public BankAccountValidator () : base()
		{
		}

		public static bool IsValid(string accountNumber) {
			try {
				BankAccountValidator.GetAccountNumber(accountNumber);
				return true;
			} catch (ArgumentException) {
				return false;
			}
		}

		/// <summary>
		/// Returns an object that represents an accountnumber
		/// </summary>
		/// <returns>The account number.</returns>
		/// <param name="accountNumber">Account number.</param>
		public static BankAccountNumber GetAccountNumber(string accountNumber) {
			ValidateSyntax(accountNumber);
			ValidateChecksum(accountNumber);
			return new BankAccountNumber(accountNumber);
		}

		/// <summary>
		/// Returns an object that represents an accountnumber. The checksum of the
		/// supplied accountnumber is changed to a valid checksum if the original accountnumber
		/// has an invalid checksum.
		/// </summary>
		/// <returns>The and force valid account number.</returns>
		/// <param name="accountNumber">Account number.</param>
		public static BankAccountNumber GetAndForceValidAccountNumber(string accountNumber) {
			ValidateSyntax(accountNumber);
			try {
				ValidateChecksum(accountNumber);
			} catch (ArgumentException) {
				BankAccountNumber k = new BankAccountNumber(accountNumber);
				int checksum = CalculateMod11CheckSum(GetMod11Weights(k), k);
				accountNumber = accountNumber.Substring(0, LENGTH) + checksum;
			}
			return new BankAccountNumber(accountNumber);
		}
		
		internal static void ValidateSyntax(string accountNumber) {
			if (accountNumber.StartsWith (NOT_ALLOWED_AS_LEADING, StringComparison.Ordinal)) {
				throw new ArgumentException (ERROR_LEADING_ZEROS + accountNumber);
			}
			ValidateLengthAndAllDigits(accountNumber, LENGTH);
		}
		
		internal static void ValidateAccountTypeSyntax(string accountType) {
			ValidateLengthAndAllDigits(accountType, ACCOUNTTYPE_NUM_DIGITS);
		}

		internal static void ValidateRegisterNumberSyntax(string registerNumber) {
			ValidateLengthAndAllDigits(registerNumber, REGISTERNUMBER_NUM_DIGITS);
		}
		
		internal static void ValidateChecksum(string accountNumber) {
			BankAccountNumber k = new BankAccountNumber(accountNumber);
			int k1 = CalculateMod11CheckSum(GetMod11Weights(k), k);
			if (k1 != k.GetChecksumDigit()) {
				throw new ArgumentException(ERROR_INVALID_CHECKSUM + accountNumber);
			}
		}
	}
}

