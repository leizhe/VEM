namespace VEM.Business.Exceptions.AccountException
{
    public class WrongOldPasswordException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.原密码不正确; }
        }
    }
}
