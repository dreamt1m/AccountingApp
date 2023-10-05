namespace AccountingApp.Web.Positions
{
    public class CreatePositionResponse
    {
        public CreatePositionResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
