using System.Collections.Generic;

namespace com.petronas.myevents.api.ViewModels
{
    public class ErrorsResponse
    {
        private readonly List<ErrorMessage> _errors;

        public ErrorsResponse()
        {
            _errors = new List<ErrorMessage>();
        }

        public ErrorsResponse(List<ErrorMessage> errors)
        {
            _errors = errors;
        }

        public IEnumerable<ErrorMessage> Errors
        {
            get
            {
                foreach (var error in _errors)
                    yield return error;
            }
        }

        public void Add(ErrorMessage error)
        {
            if (!_errors.Contains(error))
                _errors.Add(error);
        }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
        public int Code { get; set; }
    }
}