namespace VEM.Business.Exceptions.PrivilegeException
{
    public class TelExistedException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.电话号已被注册; }
        }
    }
}
