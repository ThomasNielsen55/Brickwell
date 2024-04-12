namespace Brickwell.Data.ViewModels
{
	public class CheckoutViewModel
	{
		public int TransactionId;

		public int CustomerId;

		public string Date;

		public string? DayOfWeek;

		public int? Time;

		public string EntryMode;

		public int? Amount;

		public string TypeOfTransaction;

		public string? CountryOfTransaction;

		public string? ShippingAddress;

		public string Bank;

		public string TypeOfCard;

		public bool Fraud;
	}
}
