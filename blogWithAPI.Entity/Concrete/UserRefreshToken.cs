namespace blogWithAPI.Entity.Concrete
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
    }
}