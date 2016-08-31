namespace VEM.Business.Exceptions.PrivilegeException
{
    public class EmailExistedException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.电子邮件已被注册; }
        }
    }
}
