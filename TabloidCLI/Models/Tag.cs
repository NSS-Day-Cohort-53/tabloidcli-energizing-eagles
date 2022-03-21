namespace TabloidCLI.Models
{
    // this class will need to inherit from another
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}