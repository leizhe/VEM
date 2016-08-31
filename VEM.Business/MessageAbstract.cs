namespace VEM.Business
{
    public abstract class MessageAbstract
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }
}
