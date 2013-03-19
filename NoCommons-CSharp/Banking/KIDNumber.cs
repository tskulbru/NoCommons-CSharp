using System;
using NoCommonsCSharp.Common;

namespace NoCommonsCSharp.Banking
{
	/// <summary>
	/// This class represent a Norwegian KID-nummer - a number used to identify 
	/// a customer on invoices. A Kidnummer consists of digits only, and the last
	/// digit is a checksum digit (either mod10 or mod11).
	/// </summary>
	public class KIDNumber : StringNumber
	{
		public KIDNumber (string bankAccountNumber) : base(bankAccountNumber) { }
	}
}

