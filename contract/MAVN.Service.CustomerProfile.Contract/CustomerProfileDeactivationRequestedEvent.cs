namespace MAVN.Service.CustomerProfile.Contract
{
    public class CustomerProfileDeactivationRequestedEvent
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }
    }
}
