using Newtonsoft.Json;

namespace Cl.DataAccess.EF.Models
{
    public class JsonGrid
    {

        [JsonProperty("Data")] public Object Data { get; set; }
        [JsonProperty("Total")] public int Total { get; set; }
        [JsonProperty("Request")] public GridRequest Request { get; set; }
        [JsonProperty("Start")] public int Start { get; set; }
        [JsonProperty("qr")] public string qr { get; set; }
        public JsonGrid(object oData, int oRecordsTotal, int ostart = 0)
        {
            Total = oRecordsTotal;
            Data = oData;
            Start = ostart;
        }
        public JsonGrid(object oData, int oRecordsTotal, GridRequest _Request, string _qr = null)
        {
            Total = oRecordsTotal;
            Data = oData;
            Request = _Request;
            Start = (_Request.page - 1) * _Request.take + 1;
            qr = _qr;
        }
    }
}
