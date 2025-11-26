
using App.Enums;
using App.Error;

namespace Book.Error
{
    public class NotFound : BaseAppException
    {
        private const string _MSG = "Data not Founded";
        public NotFound() : base(_MSG, HttpStatus.NotFound) { }
    }
}
