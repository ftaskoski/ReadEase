namespace ReadEase_C_.Interface
{
    public interface IMail
    {
        void RecoverEmail(string recipientEmail, string pass);
        void SendEmail(string recipientEmail);
    }
}