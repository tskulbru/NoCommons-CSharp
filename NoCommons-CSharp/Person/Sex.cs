using System;

namespace NoCommonsCSharp
{
	public enum Sex
	{
		MAN, 
		WOMAN, 
		BOTH
	}

	public static class SexExtensions {
		public static bool IsWoman(this Sex sex) {
			return sex == Sex.WOMAN;
		}

		public static bool IsBoth(this Sex sex) {
			return sex == Sex.BOTH;
		}

		public static Sex SexChange(this Sex sex) {
			return IsWoman (sex) ? Sex.MAN : Sex.WOMAN;
		}
	}
}

