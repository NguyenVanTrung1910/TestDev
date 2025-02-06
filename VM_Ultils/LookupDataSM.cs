namespace Cl.DataAccess.EF.Models
{
    public class LookupDataSM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public LookupDataSM()
        {

        }
        public LookupDataSM(int id, string title)
        {
            ID = id;
            Title = title;
        }
        public LookupDataSM(string _txt)
        {
            string[] temp = _txt.Split('|');
            ID = Convert.ToInt32(temp[0]);
            Title = temp[1];
        }
    }
}
