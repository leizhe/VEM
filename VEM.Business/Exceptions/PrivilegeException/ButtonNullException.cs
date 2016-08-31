namespace VEM.Business.Exceptions.PrivilegeException
{
    public class ButtonNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.此菜单下没有Button按钮; }
        }
    }
}
