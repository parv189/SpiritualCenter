using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spiritual_center.Code.Interfaces;
using spiritual_center.Models;

namespace spiritual_center.Controllers
{
            [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }
        //[EnableCors("Policy1")]
        //[HttpGet("{id}")]
        //public ActionResult<Payments> Getpayment(string id)
        //{
        //    List<Payments> c = paymentRepository.Find(x => x.User_Id == id).ToList();
        //    var y = "";
        //    if (c == null)
        //    {
        //        return NotFound();
        //    }
        //    foreach(Payments x in c)
        //    {
        //     y = y + x ;
        //    }
        //    return paymentRepository.Index().ToList();
        //}
        [EnableCors("Policy1")]
        [HttpGet]
        public ActionResult<IEnumerable<Payments>> Getpayment()
        {
            return paymentRepository.Index().ToList();
        }

        [EnableCors("Policy1")]
        [HttpPost]
        public ActionResult<Payments> NewPayment(Payments payments)
        {
            Payments p = new Payments();
            p.Year = payments.Year;
            p.Month = payments.Month;
            p.Amount = payments.Amount;
            p.User_Id = payments.User_Id;
            return paymentRepository.Add(p);
        }
    }
}
