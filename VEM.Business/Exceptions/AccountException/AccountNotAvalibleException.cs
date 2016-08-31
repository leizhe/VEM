namespace VEM.Business.Exceptions.AccountException
{
    public class AccountNotAvalibleException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.账号已禁用; }
        }
    }
}
