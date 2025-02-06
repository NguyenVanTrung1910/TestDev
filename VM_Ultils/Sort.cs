namespace Cl.DataAccess.EF.Models
{
    [Serializable]
    public struct Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }
}
