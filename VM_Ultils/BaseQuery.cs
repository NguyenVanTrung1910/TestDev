//using Newtonsoft.Json;
using Cl.DataAccess.EF.common;
using Cl.DataAccess.EF.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Cl.DataAccess.EF.Query
{
    public class BaseQuery
    {
        #region Tham số phục vụ hiện thị trên grid
        public string t_gridRequest { get; set; }
        public int draw { get; set; }
        public int? CurrentCongTyOfUserId { get; set; }
        public int? CurrentRoleOfUserId {  get; set; }
        public List<Sort> sort { get; set; }
        public GridRequest oGridRequest
        {
            //get;
            //set;
            get
            {
                GridRequest _oGridRequest = new GridRequest();

                if (!string.IsNullOrEmpty(t_gridRequest))
                    _oGridRequest = JsonConvert.DeserializeObject<GridRequest>(t_gridRequest);
                List<Filter> specialClassFilter = _oGridRequest.filter.filters.ToList();
                if (!string.IsNullOrEmpty(Keyword) && SearchIn.Count > 0)
                {
                    Filter oFilterSearchIn = new Filter()
                    {
                        logic = "or",
                        filters = SearchIn.Select(x => new Filter()
                        {
                            phuongthuc = "contains",
                            field = x,
                            value = Keyword
                        }).ToList()
                    };
                    _oGridRequest.filter = new Filter()
                    {
                        logic = "and",
                        filters = new List<Filter>() { oFilterSearchIn, new Filter() {
                            phuongthuc = "gt",
                            field = "ID",
                            value = "0"
                        } }
                    };
                    //trường hợp nếu _oGridRequest!= null thì gộp cả 2 trường hợp
                    if (specialClassFilter != null && specialClassFilter.Any())
                    {
                        _oGridRequest.filter.filters.AddRange(specialClassFilter);
                    }
                }
                //search theo ngày
                if (!string.IsNullOrEmpty(dateField))
                {
                    if (SearchTuNgay != null)
                    {
                        _oGridRequest.filter.filters.Add(new Filter()
                        {
                            phuongthuc = "gt",
                            field = dateField,
                            value = SearchTuNgay.Value.ToString("yyyy/MM/dd HH:mm:ss")
                        });
                    }
                    if (SearchDenNgay != null)
                    {
                        _oGridRequest.filter.filters.Add(new Filter()
                        {
                            phuongthuc = "lt",
                            field = dateField,
                            value = SearchDenNgay.Value.ToString("yyyy/MM/dd HH:mm:ss")
                        });
                    }

                }
                if (_oGridRequest.sort == null || _oGridRequest.sort.Count == 0)
                {
                    _oGridRequest.sort = new List<Sort>(){ new Sort()
                    {
                        field = "Id",
                        dir = "desc"

                    } };
                }

                return _oGridRequest;
            }
        }
        #endregion

        public bool isgetBylisID { get; set; }
        public string Keyword { get; set; }
        public List<string> SearchIn { get; set; }
        public string TuNgay { get; set; }
        public DateTime? SearchTuNgay
        {

            get
            {

                DateTimeFormatInfo dtfiParser;
                dtfiParser = new DateTimeFormatInfo();
                dtfiParser.ShortDatePattern = "dd/MM/yyyy";
                dtfiParser.DateSeparator = "/";
                DateTime? temp = null;
                if (!string.IsNullOrEmpty(TuNgay))
                {
                    temp = Convert.ToDateTime(TuNgay, dtfiParser).Date.AddTicks(-1);
                }
                return temp;
            }
            //set { this.SearchTuNgay = value; }

        }
        public string DenNgay { get; set; }
        public string dateField { get; set; }
        public DateTime? SearchDenNgay
        {
            get
            {
                DateTimeFormatInfo dtfiParser = new DateTimeFormatInfo
                {
                    ShortDatePattern = "dd/MM/yyyy",
                    DateSeparator = "/"
                };

                DateTime? temp = null;
                if (!string.IsNullOrEmpty(DenNgay))
                {
                    temp = Convert.ToDateTime(DenNgay, dtfiParser);
                    // Thiết lập thời gian thành cuối ngày (23:59:59.999)
                    temp = temp.Value.Date.AddDays(1).AddTicks(-1);
                }
                return temp;
            }
            //set { this.SearchDenNgay = value; }
        }

        /// <summary>
        /// Tìm theo listid
        /// </summary>
        public string lstID { get; set; }
        public List<int> lstIDGet
        {
            get
            {

                if (!string.IsNullOrEmpty(lstID))
                {
                    isgetBylisID = true;
                    return clsUtils.GetIDsFormString(lstID);
                }
                return new List<int>();


            }
            set { }
        }
        public BaseQuery()
        {
            TuNgay = string.Empty;
            DenNgay = string.Empty;
            sort = new List<Sort>();
            SearchIn = new List<string>();
            isgetBylisID = false;
            draw = 0;
            // lstIDGet = new List<int>();
        }
        public bool NotNullDichVu { get; set; }
    }
}
