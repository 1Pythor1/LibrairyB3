

using App.Enums;

namespace App.Error
{
    public class BaseAppException : Exception
    {
        public HttpStatus Status { get; init; }

        public BaseAppException(string message, HttpStatus status = HttpStatus.InternalServerError) 
            : base(message)
            => Status = status;

        private BaseAppException(Exception innerException, HttpStatus status = HttpStatus.InternalServerError)
            : base(innerException.Message, innerException)
            => Status = status;

        public static BaseAppException FromException(Exception ex) =>
            new BaseAppException(ex);

    }
}
