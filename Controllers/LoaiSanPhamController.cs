using Cl.DataAccess.EF;
using Cl.DataAccess.EF.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestDev.Models;

namespace TestDev.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly LoaiSanPhamRepository _LoaiSanPhamRepository;
        private readonly ResponeActionResult _responeActionResult;
        public LoaiSanPhamController(LoaiSanPhamRepository LoaiSanPhamRepository, ResponeActionResult responeActionResult)
        {
            _LoaiSanPhamRepository = LoaiSanPhamRepository;
            _responeActionResult = responeActionResult;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPaged(LoaiSanPhamQuery query)
        {
            try
            {
                var lstTask = _LoaiSanPhamRepository.GetPaged(query);
                if (lstTask == null)
                {
                    return BadRequest(_responeActionResult.Message($"Không tìm thấy bản ghi"));
                }
                _responeActionResult.data = lstTask;
                _responeActionResult.draw = query.draw;
                _responeActionResult.recordsTotal = _LoaiSanPhamRepository.TotalRecord;
                _responeActionResult.recordsFiltered = _LoaiSanPhamRepository.TotalRecord;
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
        public IActionResult View(int Id)
        {
            LoaiSanPham LoaiSanPham = new LoaiSanPham();
            if (Id != 0)
            {
                var a = _LoaiSanPhamRepository.GetById(Id);
                if (a != null) LoaiSanPham = a;
            }
            return PartialView(LoaiSanPham);
        }
        public IActionResult Edit(int Id)
        {
            LoaiSanPham oTask = new LoaiSanPham();
            if (Id != 0)
            {
                oTask = _LoaiSanPhamRepository.GetById(Id);
                if (oTask == null) { oTask = new LoaiSanPham(); }
            }
            return View(oTask);
        }
        [HttpPost]
        public IActionResult Update(LoaiSanPham LoaiSanPhamInput)
        {
            try
            {
                if (LoaiSanPhamInput == null || LoaiSanPhamInput.Id == 0)
                {
                    return BadRequest(_responeActionResult.Message($"Bản ghi không hợp lệ"));
                }
                LoaiSanPham oLoaiSanPham = _LoaiSanPhamRepository.GetById(LoaiSanPhamInput.Id);
                oLoaiSanPham.Ten = LoaiSanPhamInput.Ten;
                oLoaiSanPham.NgayNhap = LoaiSanPhamInput.NgayNhap;


                _LoaiSanPhamRepository.Save();
                _responeActionResult.ex_message = "Sửa thành công";
                _responeActionResult.data = new LoaiSanPham() { Id = LoaiSanPhamInput.Id };
                return Ok(_responeActionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
        [HttpPost]
        public IActionResult Insert(LoaiSanPham LoaiSanPhamInput)
        {

            try
            {
                _LoaiSanPhamRepository.Add(LoaiSanPhamInput);
                _LoaiSanPhamRepository.Save();
                _responeActionResult.ex_message = "Thêm thành công";
                _responeActionResult.data = new LoaiSanPham() { Id = LoaiSanPhamInput.Id };
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
                _LoaiSanPhamRepository.DeleteById(Id);
                _LoaiSanPhamRepository.Save();
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
