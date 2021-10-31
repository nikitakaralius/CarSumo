namespace Shop
{
	public readonly struct Bargain
	{
		public string ExceptionMessage { get; }
		
		public bool IsValid { get; }

		public Bargain(string exceptionMessage)
		{
			ExceptionMessage = exceptionMessage;
			IsValid = false;
		}
		
		private Bargain(string exceptionMessage, bool isValid)
		{
			ExceptionMessage = exceptionMessage;
			IsValid = isValid;
		}

		public static Bargain Valid => new Bargain(string.Empty, true);
	}
}