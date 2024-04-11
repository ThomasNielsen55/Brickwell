namespace Brickwell.Data.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<string>? Categories { get; set; }
        public IEnumerable<string>? PrimaryColors { get; set; }
        public List<Product> Products { get; set; }
    }
}
