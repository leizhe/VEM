namespace VEM.Business.Exceptions.SystemException
{
    public class ButtonNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.按钮非空; }
        }
    }
}
