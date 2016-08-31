namespace VEM.Business.Exceptions.MemberException
{
    public class MemberNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.已有会员使用此优惠券; }
        }
    }
}
