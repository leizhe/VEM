namespace VEM.Business.Exceptions.MachineException
{
    public class MachineNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.已存在此型号的售货机; }
        }
    }
}
