namespace VEM.Business.Exceptions.MachineException
{
    public class SalesHistoryNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.销售记录非空; }
        }
    }
}
