namespace VEM.Business.Exceptions.MachineException
{
    public class ContainerRoadNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.货道未清空; }
        }
    }
}
