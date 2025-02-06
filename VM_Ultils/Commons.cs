using System.ComponentModel;

namespace Cl.DataAccess.EF
{
	public enum ModerationStatus
	{
		[Description("Đã duyệt")]
		Approved = 0,

		[Description("Chờ duyệt")]
		Pending = 1,

		[Description("Từ chối")]
		Rejected = 2,

		[Description("Chờ xem xét")]
		Pending_Review = 3,

		[Description("Chờ phê duyệt")]
		Pending_Approval = 4,

		[Description("Nháp")]
		Draft = 5,

		[Description("Hủy hoặc Xóa")]
		Cancelled = 6
	}

	public class ResponeActionResult
	{
		public ResponeActionResult Message(string ex_message)
		{
			this.ex_message = ex_message;

			return this;
		}
		public ResponeActionResult(string ex_message = null)
		{
			this.ex_message = ex_message;
		}
		public ResponeActionResult(string ex_message, object data)
		{
			this.recordsTotal = recordsTotal;
			this.recordsFiltered = recordsFiltered;
		}

		public ResponeActionResult(object data, int draw, int recordsTotal, int recordsFiltered)
		{
			this.data = data;
			this.draw = draw;
			this.recordsTotal = recordsTotal;
			this.recordsFiltered = recordsFiltered;
		}

		public int? draw { get; set; }
		public int? recordsTotal { get; set; }
		public int? recordsFiltered { get; set; }
		public object? data { get; set; }
		public string? ex_message { get; set; }
		public string? querytext { get; set; }
	}
}
