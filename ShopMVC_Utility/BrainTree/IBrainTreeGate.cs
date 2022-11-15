using Braintree;


namespace ShopMVC_Utility
{
	public interface IBrainTreeGate
	{
		public IBraintreeGateway CreateGateway();

		public IBraintreeGateway GetGateway();
	}
}
