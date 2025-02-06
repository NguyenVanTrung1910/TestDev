using Cl.DataAccess.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cl.DataAccess.EF.Entity
{
	public class CoreEntity
	{
		/// <summary>
		/// ID cho bản ghi
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Thời gian tạo cho bản ghi
		/// </summary>
		/// 
		public DateTime Created { get; set; } = DateTime.Now;

		/// <summary>
		/// Người tạo đầu tiên
		/// </summary>
		/// 

	}




}
