namespace SupplyChain.App.ViewModels
{
    public class MessageViewModel
    {
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
