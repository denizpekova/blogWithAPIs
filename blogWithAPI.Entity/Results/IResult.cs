namespace blogWithAPI.Entity.Results 
{
    public interface IResult 
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
