
namespace RestaurantReservation.Db.Utilities
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        private OperationResult(bool isSuccess, string message, T data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static OperationResult<T> SuccessResult(string message)
        {
            return new OperationResult<T>(true, message);
        }

        public static OperationResult<T> FailureResult(string message)
        {
            return new OperationResult<T>(false, message);
        }

        public static OperationResult<T> SuccessDataMessage(string message, T data)
        {
            return new OperationResult<T>(true, message, data);
        }

        public static OperationResult<T> FailureDataMessage(string message, T data)
        {
            return new OperationResult<T>(false, message, data);
        }

        public static OperationResult<T> Result(bool isSuccess, string message)
        {
            return new OperationResult<T>(isSuccess, message);
        }
    }
}
