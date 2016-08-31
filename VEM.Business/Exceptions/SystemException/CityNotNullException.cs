namespace VEM.Business.Exceptions.SystemException
{
    public class CityNotNullException : VemException
    {
        public override int ExceptionCode
        {
            get { return (int)Util.StatusCode.此城市已被引用; }
        }
    }
}
