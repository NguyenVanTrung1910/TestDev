namespace Cl.DataAccess.EF.Models
{
    public class GridRequest
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

        public int ItemID { get; set; }
        public List<Sort> sort { get; set; }
        public string Field { get; set; }
        public bool FieldOption { get; set; }
        public Filter filter { get; set; }
        public GridRequest()
        {
            sort = new List<Sort>();
            filter = new Filter();
        }

    }
}
