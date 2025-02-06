namespace Cl.DataAccess.EF.Models
{
    public class Filter
    {
        public string field { get; set; }
        public string phuongthuc { get; set; }
        public string value { get; set; }
        public List<Filter> filters { get; set; }
        public string logic { get; set; }
        public Filter()
        {
            filters = new List<Filter>();
        }
    }
}
