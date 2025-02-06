using Cl.DataAccess.EF;
using Cl.DataAccess.EF.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestDev.Models;

namespace TestDev.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly SanPhamRepository _SanPhamRepository;
        private readonly ResponeActionResult _responeActionResult;
        public SanPhamController(SanPhamRepository SanPhamRepository, ResponeActionResult responeActionResult)
        {
            _SanPhamRepository = SanPhamRepository;
            _responeActionResult = responeActionResult;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPaged(SanPhamQuery query)
        {
            try
            {
                var lstTask = _SanPhamRepository.GetPaged(query);
                if (lstTask == null)
                {
                    return BadRequest(_responeActionResult.Message($"Không tìm thấy bản ghi"));
                }
                _responeActionResult.data = lstTask;
                _responeActionResult.draw = query.draw;
                _responeActionResult.recordsTotal = _SanPhamRepository.TotalRecord;
                _responeActionResult.recordsFiltered = _SanPhamRepository.TotalRecord;
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
        public IActionResult View(int Id)
        {
            SanPham SanPham = new SanPham();
            if (Id != 0)
            {
                var a = _SanPhamRepository.GetById(Id);
                if (a != null) SanPham = a;
            }
            return PartialView(SanPham);
        }
        public IActionResult Edit(int Id)
        {
            SanPham oTask = new SanPham();
            if (Id != 0)
            {
                oTask = _SanPhamRepository.GetById(Id);
                if (oTask == null) { oTask = new SanPham(); }
            }
            return View(oTask);
        }
        [HttpPost]
        public IActionResult Update(SanPham SanPhamInput)
        {
            try
            {
                if (SanPhamInput == null || SanPhamInput.Id == 0)
                {
                    return BadRequest(_responeActionResult.Message($"Bản ghi không hợp lệ"));
                }
                SanPham oSanPham = _SanPhamRepository.GetById(SanPhamInput.Id);
                oSanPham.Ten = SanPhamInput.Ten;
                oSanPham.Gia = SanPhamInput.Gia;
                oSanPham.NgayNhap = SanPhamInput.NgayNhap;


                _SanPhamRepository.Save();
                _responeActionResult.ex_message = "Sửa thành công";
                _responeActionResult.data = new SanPham() { Id = SanPhamInput.Id };
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
        [HttpPost]
        public IActionResult Insert(SanPham SanPhamInput, List<int> LoaiSP)
        {

            try
            {
                _SanPhamRepository.Add(SanPhamInput);
                _SanPhamRepository.Save();
                _SanPhamRepository.AddSP_Loai(SanPhamInput,LoaiSP);

                _responeActionResult.ex_message = "Thêm thành công";
                _responeActionResult.data = new SanPham() { Id = SanPhamInput.Id };
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                _SanPhamRepository.DeleteById(Id);
                _SanPhamRepository.Save();
                _responeActionResult.ex_message = "Xóa thành công";
                _responeActionResult.data = null;
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                Exception inner = ex.InnerException;
                if (inner is SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        if (sqlEx.Message.Contains("dbo.GiaoViecs"))
                        {
                            return BadRequest(_responeActionResult.Message($" Đã có Giao Việc sử dụng trong Task này"));
                        }

                    }
                }
                return BadRequest();
                //Log.Error(ex, "Lỗi khi xóa");
                //return StatusCode(500, "Đã có lỗi xảy ra khi xóa người dùng. Vui lòng thử lại sau.");
            }

        }
    }
}
