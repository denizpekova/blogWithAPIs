namespace blogWithAPI.Entity.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; set; }
    }
}
    