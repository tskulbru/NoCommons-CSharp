using System;
using System.Text;
using System.Collections.Generic;

namespace NoCommonsCSharp.Banking
{
	public class BankAccountNumberCalculator
	{
		const int LENGTH = 11;
		const int REGISTERNUMMER_START_DIGIT = 0;
		const int ACCOUNTTYPE_START_DIGIT = 4;

		/// <summary>
		/// Returns a List with random but syntactically valid AccountNumber instances
		/// for a given AccountType.
		/// </summary>
		/// <returns>A List with AccountNumber instances</returns>
		/// <param name="accountType">A string representing the Account type to use for all AccountNumber instances</param>
		/// <param name="length">Length.</param>
		public static List<BankAccountNumber> GetAccountNumberListForAccountType(string accountType, int length) {
			BankAccountValidator.ValidateAccountTypeSyntax(accountType);
			return GetAccountNumberListUsingGenerator(new AccountTypeAccountNumberDigitGenerator(accountType), length);
		}

		/// <summary>
		/// Returns a List with random but syntactically valid BankAccountNumber instances
		/// for a given RegisterNumber.
		/// </summary>
		/// <returns>A List with BankAccountNumber instances.</returns>
		/// <param name="registerNumber">A String representing the RegisterNumber to use for all BankAccountNumber instances.</param>
		/// <param name="length">Specifies the number of BankAccountNumber instaces to create in the returned List.</param>
		public static List<BankAccountNumber> GetAccountNumberListForRegisternummer(string registerNumber, int length) {
			BankAccountValidator.ValidateRegisterNumberSyntax(registerNumber);
			return GetAccountNumberListUsingGenerator(new RegisterNumberAccountNumberDigitGenerator(registerNumber), length);
		}

		/// <summary>
		/// Returns a List with completely random but syntatically valid BankAccountNumber instances
		/// </summary>
		/// <returns>A List with random valid BankAccountNumber instances.</returns>
		/// <param name="length">Specifies the number of BankAccountNumber instances to create in the returned List</param>
		public static List<BankAccountNumber> GetAccountNumberList(int length) {
			return GetAccountNumberListUsingGenerator(new NormalAccountNumberDigitGenerator(), length);
		}

		static List<BankAccountNumber> GetAccountNumberListUsingGenerator(AccountNumberDigitGenerator generator, int length) {
			List<BankAccountNumber> result = new List<BankAccountNumber>();
			int numAddedToList = 0;
			while (numAddedToList < length) {
				BankAccountNumber accountNumber;
				try {
					accountNumber = BankAccountValidator.GetAndForceValidAccountNumber(generator.GenerateAccountNumber());
				} catch (ArgumentException) {
					// this number has no valid checksum
					continue;
				}
				result.Add(accountNumber);
				numAddedToList++;
			}
			return result;
		}

		internal abstract class AccountNumberDigitGenerator {
			internal abstract string GenerateAccountNumber();
		}

		internal class AccountTypeAccountNumberDigitGenerator : AccountNumberDigitGenerator {
			readonly string accountType;
			
			internal AccountTypeAccountNumberDigitGenerator(string accountType) {
				this.accountType = accountType;
			}
			
			internal override string GenerateAccountNumber() {
				StringBuilder accountNumberBuffer = new StringBuilder(LENGTH);
				for (int i = 0; i < LENGTH;) {
					if (i == ACCOUNTTYPE_START_DIGIT) {
						accountNumberBuffer.Append(accountType);
						i += accountType.Length;
					} else {
						accountNumberBuffer.Append((int) (new Random().Next() * 10));
						i++;
					}
				}
				return accountNumberBuffer.ToString();
			}
		}

		internal class RegisterNumberAccountNumberDigitGenerator : AccountNumberDigitGenerator {
			readonly string registerNr;
			
			internal RegisterNumberAccountNumberDigitGenerator(string registerNr) {
				this.registerNr = registerNr;
			}

			internal override string GenerateAccountNumber() {
				StringBuilder accountNumberBuffer = new StringBuilder(LENGTH);
				for (int i = 0; i < LENGTH;) {
					if (i == REGISTERNUMMER_START_DIGIT) {
						accountNumberBuffer.Append(registerNr);
						i += registerNr.Length;
					} else {
						accountNumberBuffer.Append((int) (new Random().Next() * 10));
						i++;
					}
				}
				return accountNumberBuffer.ToString();
			}
		}

		internal class NormalAccountNumberDigitGenerator : AccountNumberDigitGenerator {

			internal override string GenerateAccountNumber() {
				StringBuilder accountNumberBuffer = new StringBuilder(LENGTH);
				for (int i = 0; i < LENGTH; i++) {
					accountNumberBuffer.Append((int) (new Random().Next() * 10));
				}
				return accountNumberBuffer.ToString();
			}
		}
	}
}

