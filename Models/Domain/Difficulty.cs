namespace NZWalksAPI.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        //prop double tab creates this
        //public int MyProperty { get; set; }
    }
}