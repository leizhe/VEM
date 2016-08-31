namespace VEM.Business.Exceptions.AccountException
{
    public class WrongTelOrPasswordException:VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.用户名或密码不正确; }
        }
    }
}
