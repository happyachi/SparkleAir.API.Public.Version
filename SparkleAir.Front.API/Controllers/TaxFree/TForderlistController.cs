using ECPay.Payment.Integration;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkleAir.BLL.Service.TaxFree;
using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.Front.API.Models;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.TaxFree;
using Tforderlist = SparkleAir.Infa.EFModel.Models.Tforderlist;
using Tfreserf = SparkleAir.Infa.EFModel.Models.Tfreserf;
using Tfreservelist = SparkleAir.Infa.EFModel.Models.Tfreservelist;

namespace SparkleAir.Front.API.Controllers.TaxFree
{
    [Route("api/[controller]")]
    [ApiController]
    public class TForderlistController : ControllerBase
    {
        private AppDbContext _db;
        private readonly ITFOrderRepository _repository;
        private readonly TFOrderlistService _service;
        clearcartservice clearcart;



        public TForderlistController(AppDbContext context)
        {
            _db = context;
            _repository = new TFOrderlistEFRepository(context);
            _service = new TFOrderlistService(context,_repository);
            clearcart = new clearcartservice(_db);
        }

        [HttpGet]
        public async Task<IEnumerable<TFOrderlistsDto>> Get()
        {
            return _service.Get();
        }

        [HttpGet("getbyticketid/{id}")]
        public async Task<IEnumerable<TFOrderlistsDto>> GetItemsByTicketDetailsId(int id)
        {
            return _service.GetItemsByTicketDetailsId(id);
        }



        //儲存購物車購買商品
        [HttpPost("addtocart")]
        public async Task<IActionResult> Post([FromQuery] TFOrderlistsDto dto)
        {
            try
            {
                var existingItem = _db.Tforderlists.FirstOrDefault(item => item.TfitemsId == dto.TFItemsId && item.TicketDetailsId == dto.TicketDetailsId);

                if (existingItem != null)
                {
                    // 如果存在相同的商品，則更新數量
                    existingItem.Quantity += dto.Quantity;
                    _db.SaveChanges(); // 保存更改
                }
                else
                {
                    _service.AddToCart(dto);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"無此品項 {ex.Message}");
            }
        }

        



        [HttpDelete("{id}")]//刪除購物車訂單
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"無此品項 {ex.Message}");
            }
        }

        [HttpPut("update")]//更新購買數量
        public async Task<IActionResult> updatePut([FromQuery] int id, [FromQuery] int newQuantity)
        {
            try
            {
                _service.UpdateQuantity(id, newQuantity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"失敗 {ex.Message}");
            }
        }

        [HttpPut("updatepaid")] //更新付款狀態
        public async Task<IActionResult> UpdatePaid([FromQuery] int id, [FromQuery] int newstatusid)
        {
            try
            {
                _service.UpdatePaid(id, newstatusid);
                return Ok();
            }


            catch (Exception ex)
            {
                return StatusCode(500, $"失敗 {ex.Message}");
            }

        }

        [HttpGet("GetSelectedItems")]
        public async Task<IEnumerable<TFOrderlistsDto>> GetSelectedItems([FromQuery] int id, [FromQuery] List<int> selectedOrderListIds)
        {
            return _service.GetSelectedItems(id, selectedOrderListIds);
        }



        [HttpPost("Checkout")]

        public async Task<IActionResult> Checkout([FromQuery] int transferPaymentId, [FromQuery] int ticketDetailsId, [FromQuery] int memberId)
        {
            try
            {
                var reserveEntities = new List<Tfreserf>();
                var reservelistEntities = new List<Tfreservelist>();
                var itemsByTicketDetailsId = await GetItemsByTicketDetailsId(ticketDetailsId);
                var filteredItems = itemsByTicketDetailsId.Where(item => item.TFOrderStatusID == 1).ToList();


                int totalOrderPrice = 0;

                foreach (var orderItem in filteredItems)
                {

                    int totalPriceForItem = orderItem.TFItemsUnitPrice * orderItem.Quantity;
                    totalOrderPrice += totalPriceForItem;

                    var reserveEntity = new Tfreserf
                    {
                        MemberId = memberId,
                        TicketDetailsId = ticketDetailsId,
                        Discount = null,
                        TotalPrice = totalPriceForItem,
                        TransferPaymentId = transferPaymentId,
                        TforderStatusId = 1,
                    };
                    reserveEntities.Add(reserveEntity);
                }

                _db.Tfreserves.AddRange(reserveEntities);
                _db.SaveChanges();

                int tfreserfId = reserveEntities.First().Id;


                foreach (var orderItem in filteredItems)
                {
                    var reservelistEntity = new Tfreservelist
                    {
                        TfitemsId = orderItem.TFItemsId,
                        Quantity = orderItem.Quantity,
                        UnitPrice = orderItem.TFItemsUnitPrice,
                        Discount = null,
                        TotalPrice = orderItem.TFItemsUnitPrice * orderItem.Quantity,
                        TfreserveId = tfreserfId
                    };
                    _db.Tfreservelists.Add(reservelistEntity);
                }


                _db.Tfreservelists.AddRange(reservelistEntities);
                await _db.SaveChangesAsync();

                return Ok("成功你棒棒");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"失敗快除錯：{ex.Message}");
            }
        }

        [HttpGet("ClearCart")]
        public async Task<IActionResult> ClearCart([FromQuery] int ticketDetailsId)
        {
            try
            {
                var ordersToDelete = await _db.Tforderlists
                    .Where(order => order.TicketDetailsId == ticketDetailsId && order.TforderStatusId == 1)
                    .ToListAsync();

                _db.Tforderlists.RemoveRange(ordersToDelete);

                await _db.SaveChangesAsync();

                return Ok("空功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"問號欸：{ex.Message}");
            }
        }

        [HttpGet("ScheduleClearCart")]
        public IActionResult ScheduleClearCart()
        {
            RecurringJob.AddOrUpdate( () => clearcart.CheckAndClearCart(), Cron.MinuteInterval(1));

            return Ok("定期清除購物車");
        }

        





    }
}
