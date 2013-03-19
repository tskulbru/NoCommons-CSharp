using System;

namespace NoCommonsCSharp.Common
{
	public abstract class StringNumber
	{
		readonly string _value;

		public string Value {
			get {
				return _value;
			}
		}
		
		protected StringNumber(String stringValue) {
			_value = stringValue;
		}
		
		public int GetAt(int i) {
			return int.Parse (Value[i].ToString());
		}
		
		public override string ToString() {
			return Value;
		}

		public override int GetHashCode() {
			const int prime = 31;
			int result = 1;
			result = prime * result + (String.IsNullOrEmpty(Value) ? 0 : Value.GetHashCode());

			return result;
		}
		
		public override bool Equals(Object obj) {
			if (this == obj) {
				return true;
			}
			if (obj == null) {
				return false;
			}
			if (GetType().Name != obj.GetType().Name) {
				return false;
			}
			StringNumber other = (StringNumber) obj;
			if (String.IsNullOrEmpty(Value)) {
				if (other.Value != null) {
					return false;
				}
			} else if (!Value.Equals(other.Value)) {
				return false;
			}
			return true;
		}
		
		public int GetLength() {
			return Value.Length;
		}

		/// <summary>
		/// The last digits of a StringNumber is the checksum digit.
		/// </summary>
		/// <returns>The checksum digit.</returns>
		public int GetChecksumDigit() {
			return GetAt(GetLength()-1);
		}
	}
}

