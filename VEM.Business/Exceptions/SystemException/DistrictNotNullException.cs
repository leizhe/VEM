namespace VEM.Business.Exceptions.SystemException
{
    public class DistrictNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.此区县已被引用; }
        }
    }
}
