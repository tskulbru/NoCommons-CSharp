using System;
using NUnit.Framework;
using NoCommonsCSharp.Banking;

namespace NoCommonsCSharp.Test
{
	[TestFixture]
	public class BankAccountNumberTest
	{
		const int LIST_LENGTH = 100;
		const string TEST_ACCOUNT_TYPE = "45";
		const string TEST_REGISTERNUMMER = "9710";

		[Test]
		public void TestGetAccountNumberList ()
		{
			var options = BankAccountNumberCalculator.GetAccountNumberList (LIST_LENGTH);
			Assert.Equals (LIST_LENGTH, options.Count);
			foreach(var option in options) {
				Assert.IsTrue(BankAccountValidator.IsValid(option.ToString()));
			}
		}

		[Test]
		public void TestGetAccountNumberListForAccountType() {
			var options = BankAccountNumberCalculator.GetAccountNumberListForAccountType (TEST_ACCOUNT_TYPE, LIST_LENGTH);
			Assert.Equals (LIST_LENGTH, options.Count);
			foreach (var option in options) {
				Assert.IsTrue(BankAccountValidator.IsValid(option.ToString()));
				Assert.IsTrue(option.GetAccountType().Equals(TEST_ACCOUNT_TYPE));
			}
		}

		[Test]
		public void TestGetAccountNumberListForRegisterNumber() {
			var options = BankAccountNumberCalculator.GetAccountNumberListForAccountType (TEST_ACCOUNT_TYPE, LIST_LENGTH);
			Assert.Equals (LIST_LENGTH, options.Count);
			foreach (var option in options) {
				Assert.IsTrue(BankAccountValidator.IsValid(option.ToString()));
				Assert.IsTrue(option.GetRegisternummer().Equals(TEST_REGISTERNUMMER));
			}
		}
	}
}

