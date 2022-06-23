namespace Console.ApplicationCore.Dtos
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResultDto(bool success)
        {
            Success = success;
        }

        public ResultDto(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static ResultDto Ok() => new(true);

        public static ResultDto Bad(string message) => new(false, message);
    }

    public class ResultDto<T> : ResultDto
    {
        public T Data { get; set; }

        public ResultDto(bool success) : base(success)
        {
        }

        public ResultDto(T data, bool success) : base(success)
        {
            Data = data;
        }

        public ResultDto(bool success, string message) : base(success, message)
        {
        }

        public static ResultDto<T> Ok(T data) => new(data, true);

        public new static ResultDto<T> Ok() => new(true);

        public new static ResultDto<T> Bad(string message) => new(false, message);  
    }
}
