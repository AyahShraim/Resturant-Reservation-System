namespace RestaurantReservation.API.Models.ErrorsHandling
{
    public class Error
    {
        private Error(string code, int statusCode, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
            StatusCode = statusCode;
        }
        public string Code { get; }
        public int StatusCode { get; }
        public string Description { get; }
        public ErrorType Type { get; }

        public static Error NotFound(string code, string description) =>
            new Error(code, GetStatusCode(ErrorType.NotFound), description, ErrorType.NotFound);

        public static Error Conflict(string code, string description) =>
          new Error(code, GetStatusCode(ErrorType.Conflict), description, ErrorType.Conflict);

        public static Error Failure(string code, string description) =>
            new Error(code, GetStatusCode(ErrorType.Failure), description, ErrorType.Failure);

        public static Error Validate(string code, string description) =>
         new Error(code, GetStatusCode(ErrorType.Validation), description, ErrorType.Validation);


        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,
            };
        }

    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3
    }
}
