namespace VEM.Business.Exceptions.SystemException
{
    public class ChildMenuNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.子菜单非空; }
        }
    }
}
