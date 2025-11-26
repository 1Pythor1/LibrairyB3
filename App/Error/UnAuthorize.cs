using App.Enums;

namespace App.Error
{
    public class UnAuthorize : BaseAppException
    {
        private const string _MSG = "You are not allowed";
        public UnAuthorize() : base(_MSG, HttpStatus.Unauthorized) { }
    }
}
