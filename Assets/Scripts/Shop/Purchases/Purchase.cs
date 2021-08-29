namespace Shop
{
	public readonly struct Purchase
	{
		public string ExceptionMessage { get; }
		
		public bool IsValid { get; }

		public Purchase(string exceptionMessage)
		{
			ExceptionMessage = exceptionMessage;
			IsValid = false;
		}
		
		private Purchase(string exceptionMessage, bool isValid)
		{
			ExceptionMessage = exceptionMessage;
			IsValid = isValid;
		}

		public static Purchase Valid => new Purchase(string.Empty, true);
	}
}