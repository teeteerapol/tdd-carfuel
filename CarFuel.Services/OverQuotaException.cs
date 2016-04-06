using System;

namespace CarFuel.Services
{
    public class OverQuotaException : BusinessException
    {
        public OverQuotaException()
        {

        }

        public OverQuotaException(String message) : base(message)
        {

        }
    }
}