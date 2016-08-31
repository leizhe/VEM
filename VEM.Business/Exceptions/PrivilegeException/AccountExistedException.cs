namespace VEM.Business.Exceptions.PrivilegeException
{
    public class AccountExistedException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.用户名已被注册; }
        }
    }
}
