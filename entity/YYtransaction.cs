namespace BankPlus.entity
{
    public class YYtransaction
    {
        private string _id;
        private decimal _amount;
        private string _createdAt;
        private string _content;
        private string _senderAccountnumber;
        private string _receiverAccountnumber;
        private int _type;// 1.withdraw, 2.deposit, 3.transter.
        private string _createAt;
        private int _status;

        public YYtransaction()
        {
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public string CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public string Content
        {
            get => _content;
            set => _content = value;
        }

        public string SenderAccountnumber
        {
            get => _senderAccountnumber;
            set => _senderAccountnumber = value;
        }

        public string ReceiverAccountnumber
        {
            get => _receiverAccountnumber;
            set => _receiverAccountnumber = value;
        }

        public int Type
        {
            get => _type;
            set => _type = value;
        }

        public string CreateAt
        {
            get => _createAt;
            set => _createAt = value;
        }

        public int Status
        {
            get => _status;
            set => _status = value;
        }
    }
}