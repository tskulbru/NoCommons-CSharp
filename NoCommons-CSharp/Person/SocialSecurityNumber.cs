using System;
using NoCommonsCSharp.Common;

namespace NoCommonsCSharp.Person
{
	public class SocialSecurityNumber : StringNumber
	{
		public SocialSecurityNumber (string socialSecurityNumber) : base(socialSecurityNumber)  { }

		/// <summary>
		/// Returns the first 4 digits of the Norwegian Social Security Number (Fødselsnummer)
		/// that contains the date (01-31) and month (01-12) of birth.
		/// </summary>
		/// <value>A string containing the date and month of birth.</value>
		public string DayAndMonth {
			get {
				return ParseDNumber (Value.Substring (0, 4));
			}
		}

		/// <summary>
		/// Returns the first 2 digits of the Norwegian Social Security Number (Fødselsnummer)
		/// that contains the date (01-31), stripped for eventual D-numbers.
		/// </summary>
		/// <value>A string containing the date of birth.</value>
		public string DayInMonth {
			get {
				return ParseDNumber (Value.Substring (0, 2));
			}
		}

		/// <summary>
		/// Returns the digits 3 and 4 of the Norwegian Social Security Number (Fødselsnummer)
		/// that contains the month (01-12), stripped for eventual D-numbers.
		/// </summary>
		/// <value>A string containing the month of birth.</value>
		public string Month {
			get {
				return ParseDNumber (Value.Substring (2, 2));
			}
		}

		/// <summary>
		/// Returns the birth year of the Norwegian Social Security Number (Fødselsnummer)
		/// </summary>
		/// <value>The year.</value>
		public string Year {
			get {
				return GetCentury() + DigitBirthYear;
			}
		}

		/// <summary>
		/// Returns the two digits of the Norwegian Social Security Number (Fødselsnummer)
		 /// that contains the year birth
		/// </summary>
		/// <value>A string containing the year of birth.</value>
		public string DigitBirthYear {
			get {
				return Value.Substring(4, 2);
			}
		}

		/// <summary>
		/// Returns the first 6 digits of the Norwegian Social Security Number (Fødselsnummer)
		/// that contains the date (01-31), month(01-12) and year(00-99) of birth.
		/// </summary>
		/// <value>A String containing the date and month of birth.</value>
		public string DateOfBirth {
			get {
				return ParseDNumber(Value.Substring(0, 6));
			}
		}

		/// <summary>
		/// Returns the last 5 digits of the Norwegian Social Security Number (Fødselsnummer),
		/// also referred to as the Person number. The Person number consists of the Individual number (3 digits)
		/// and two checksum digits.
		/// </summary>
		/// <value>A string containing the person number.</value>
		public string PersonNumber {
			get {
				return Value.Substring(6);
			}
		}

		/// <summary>
		/// Returns the first three digits of the Person number, also known as the Individual number.
		/// </summary>
		/// <value>A string containing the individual number.</value>
		public string IndividualNumber {
			get {
				return Value.Substring(6, 3);
			}
		}

		/// <summary>
		/// Returns the digit that decides the gender - the 9th in the Norwegian Social Security Number (Fødselsnummer)
		/// </summary>
		/// <value>The gender digit.</value>
		public int GenderDigit {
			get {
				return GetAt(8);
			}
		}

		/// <summary>
		/// Returns the first checksum digit - the 10th in the Norwegian Social Security Number (Fødselsnummer)
		/// </summary>
		/// <value>The checksum digit.</value>
		public int ChecksumDigit1 {
			get {
				return GetAt(9);
			}
		}

		/// <summary>
		/// Returns the second checksum digit - the 11th in the Norwegian Social Security Number (Fødselsnummer)
		/// </summary>
		/// <value>The checksum digit.</value>
		public int ChecksumDigit2 {
			get {
				return GetAt(10);
			}
		}

		/// <summary>
		/// Returns true if the Norwegian Social Security Number (Fødselsnummer) represents a man.
		/// </summary>
		/// <value><c>true</c> if this instance is male; otherwise, <c>false</c>.</value>
		public bool IsMale {
			get {
				return GenderDigit % 2 != 0;
			}
		}
	
		/// <summary>
		/// Returns true if the Norwegian Social Security Number (Fødselsnummer) represents a woman.
		/// </summary>
		/// <value><c>true</c> if this instance is female; otherwise, <c>false</c>.</value>
		public bool IsFemale {
			get {
				return !IsMale;
			}
		}

		public Sex GetSex() {
			if (IsFemale) {
				return Sex.WOMAN;
			} else {
				return Sex.MAN;
			}
		}
		
		public override string ToString() {
			return base.Value;
		}
		
		internal static bool IsDNumber(string ssn) {
			try {
				int firstDigit = GetFirstDigit(ssn);
				if (firstDigit > 3 && firstDigit < 8) {
					return true;
				}
			} catch (ArgumentException) {
				// ignore
			}
			return false;
		}
		
		internal static String ParseDNumber(string ssn) {
			if (!IsDNumber(ssn)) {
				return ssn;
			} else {
				return (GetFirstDigit(ssn) - 4) + ssn.Substring(1);
			}
		}

		internal string GetCentury()
		{
			string result = string.Empty;
			int individualNumberInt = int.Parse(IndividualNumber);
			int birthYear = int.Parse(DigitBirthYear);
			if (individualNumberInt <= 499) {
				result = "19";
			} else if (individualNumberInt >= 500 && birthYear < 40) {
				result = "20";
			} else if (individualNumberInt >= 500 && individualNumberInt <= 749 && birthYear > 54) {
				result = "18";
			} else if (individualNumberInt >= 900 && birthYear > 39) {
				result = "19";
			}
			return result;
		}

		private static int GetFirstDigit(string ssn) {
			return int.Parse(ssn.Substring(0, 1));
		}
	}
}

