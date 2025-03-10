namespace CropDealBackend.Models
{
    public class BankDetailVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string BankAccountNumber { get; set; } = null!;

        public string AccountHolderName { get; set; } = null!;

        public string Ifsccode { get; set; } = null!;

        public string BankName { get; set; } = null!;

    }
}
