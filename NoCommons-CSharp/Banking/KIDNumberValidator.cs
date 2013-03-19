using System;
using NoCommonsCSharp.Common;

namespace NoCommonsCSharp.Banking
{
	public class KIDNumberValidator : StringNumberValidator
	{
		public const string ERROR_LENGTH = "A KID number is between 2 and 25 digits";

		public KIDNumberValidator () : base() { }

		/// <summary>
		/// Return true if the provided string is a valid KID-number
		/// </summary>
		/// <returns><c>true</c> if it is a valid KID-number; otherwise, <c>false</c>.</returns>
		/// <param name="kidNumber">KID number.</param>
		public static bool IsValid(string kidNumber) {
			try {
				KIDNumberValidator.GetKIDNumber(kidNumber);
				return true;
			} catch (ArgumentException) {
				return false;
			}
		}

		/// <summary>
		/// Returns an object that represents a KID-number
		/// </summary>
		/// <returns>A KIDNumber instance.</returns>
		/// <param name="kidNumber">A string containing a KID-number.</param>
		public static KIDNumber GetKIDNumber(String kidNumber) {
			ValidateSyntax(kidNumber);
			ValidateChecksum(kidNumber);
			return new KIDNumber(kidNumber);
		}
		
		internal static void ValidateSyntax(String kidNumber) {
			ValidateAllDigits(kidNumber);
			ValidateLengthInRange(kidNumber, 2, 25);
		}

		
		internal static void ValidateChecksum(String kidNumber) {
			StringNumber k = new KIDNumber(kidNumber);
			int kMod10 = CalculateMod10CheckSum(GetMod10Weights(k), k);
			int kMod11 = CalculateMod11CheckSum(GetMod11Weights(k), k);
			if (kMod10 != k.GetChecksumDigit() && kMod11 != k.GetChecksumDigit()) {
				throw new ArgumentException(ERROR_INVALID_CHECKSUM + kidNumber);
			}
		}

		internal static void ValidateLengthInRange(String kidNumber, int i, int j) {
			if (String.IsNullOrEmpty(kidNumber) || kidNumber.Length < i || kidNumber.Length > j) {
				throw new ArgumentException(ERROR_LENGTH);
			}
		}

	}
}

