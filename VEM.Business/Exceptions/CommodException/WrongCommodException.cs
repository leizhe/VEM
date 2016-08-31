namespace VEM.Business.Exceptions.CommodException
{
    public class WrongCommodException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.没有选择商品; }
        }
    }
}
