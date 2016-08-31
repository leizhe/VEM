namespace VEM.Business.Exceptions.CommodException
{
    public class CommodNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.商品已添加到货道中; }
        }
    }
}
