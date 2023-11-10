using EITracker.UI.Services;

namespace EITracker.UI
{
    public class Response
    {
        public bool succeeded { get; set; }
        IEnumerable<Errors> errors { get; set; }
    }
}
