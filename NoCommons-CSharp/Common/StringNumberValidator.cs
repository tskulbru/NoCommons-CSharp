using System;

namespace NoCommonsCSharp.Common
{
	public abstract class StringNumberValidator
	{
		static int[] BASE_MOD11_WEIGHTS = new int[]{2, 3, 4, 5, 6, 7};

		public const string ERROR_INVALID_CHECKSUM = "Invalid checksum : ";
		public const string ERROR_SYNTAX = "Only digits are allowed : ";

		/// <summary>
		/// Calculates the checksum for the given weights and number.
		/// </summary>
		/// <returns>The mod11 check sum.</returns>
		/// <param name="weights">Weights.</param>
		/// <param name="number">Number.</param>
		protected static int CalculateMod11CheckSum(int[] weights, StringNumber number) {
			int c = CalculateChecksum(weights, number, false) % 11;
			if (c == 1) {
				throw new ArgumentException(ERROR_INVALID_CHECKSUM + number);
			}
			return c == 0 ? 0 : 11 - c;
		}

		/// <summary>
		/// Calculates the check sum for the given weights and number.
		/// </summary>
		/// <returns>The mod10 check sum.</returns>
		/// <param name="weights">Weights.</param>
		/// <param name="number">Number.</param>
		protected static int CalculateMod10CheckSum(int[] weights, StringNumber number) {
			int c = CalculateChecksum(weights, number, true) % 10;
			return c == 0 ? 0 : 10 - c;
		}

		protected static void ValidateLengthAndAllDigits(String numberString, int length) {
			if (numberString == null || numberString.Length != length) {
				throw new ArgumentException(ERROR_SYNTAX + numberString);
			}

			ValidateAllDigits(numberString);
		}

		protected static void ValidateAllDigits(String numberString) {
			if (numberString == null || numberString.Length <= 0) {
				throw new ArgumentException(ERROR_SYNTAX + numberString);
			}
			for (int i = 0; i < numberString.Length; i++) {
				if (!Char.IsDigit(numberString[i])) {
					throw new ArgumentException(ERROR_SYNTAX + numberString);
				}
			}
		}
		
		protected static int[] GetMod10Weights(StringNumber k) {
			int[] weights = new int[k.GetLength() - 1];
			for (int i = 0; i < weights.Length; i++) {
				if ((i % 2) == 0) {
					weights[i] = 2;
				} else {
					weights[i] = 1;
				}
			}
			return weights;
		}
		
		protected static int[] GetMod11Weights(StringNumber k) {
			int[] weights = new int[k.GetLength() - 1];
			for (int i = 0; i < weights.Length; i++) {
				int j = i % BASE_MOD11_WEIGHTS.Length;
				weights[i] = BASE_MOD11_WEIGHTS[j];
			}
			return weights;
		}

		static int CalculateChecksum(int[] weights, StringNumber number, bool digitSum) {
			int checkSum = 0;
			for (int i = 0; i < weights.Length; i++) {
				int product = weights[i] * number.GetAt(weights.Length - 1 - i);
				if (digitSum) {
					checkSum += (product > 9 ? product - 9 : product);
				} else {
					checkSum += product;
				}
			}
			return checkSum;
		}
	}
}

