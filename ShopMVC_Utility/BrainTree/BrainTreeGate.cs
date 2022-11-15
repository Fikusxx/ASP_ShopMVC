using Braintree;
using Microsoft.Extensions.Options;

namespace ShopMVC_Utility
{
    public class BrainTreeGate : IBrainTreeGate
    {
        public BrainTreeSettings Settings { get; }
        private IBraintreeGateway brainTreeGateway { get; set; }

        public BrainTreeGate(IOptions<BrainTreeSettings> settings)
        {
            Settings = settings.Value;
        }

        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway(Settings.Environment, Settings.MerchantId, Settings.PublicKey, Settings.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (brainTreeGateway == null)
            {
                brainTreeGateway = CreateGateway();
            }

            return brainTreeGateway;
        }
    }
}
