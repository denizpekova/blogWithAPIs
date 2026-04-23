using blogWithAPI.Entity.Results;

namespace blogWithAPI.Entity.Results
{
    public class Result : IResult
    {
      public Result(bool isSuccess, string message) : this(isSuccess)
      {
        Message = message;
      }

      public Result(bool isSuccess)
      {
        IsSuccess = isSuccess;
      }

      public bool IsSuccess { get; set; }
      public string Message { get; set; } = string.Empty;

    }
}
      
    