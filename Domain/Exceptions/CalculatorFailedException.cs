namespace Domain.Exceptions
{
    public sealed class CalculatorFailedException : BadRequestException
    {
        public CalculatorFailedException(string error) :
            base($"Calculation unable to proceed({error})")
        {

        }
    }
}
