using System;

namespace CleanArchMvc.Domain.Validation
{
        // Classe para tratar excessões que herda de Exception
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error)
            : base(error) { }

            // Método que verifica quando há um erro
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
