using ATG.Sandbox.Domain;
using ATG.Sandbox.Model;
using ATG.Sandbox.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ATG.Sandbox.Web.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        
        [HttpPost("[action]")]
        public IActionResult  Post([FromBody]OrderQueueModel orderModel)
        {
            List<string> errorList = new List<string>();

            try
            {
                if (ModelState.IsValid)
                {
                    var order = OrderTransformation.TransformOrderResultModelInDomain(orderModel);
                    orderService.AddInQueue(order);

                } else
                {
                   return StatusCode(403, GetModelErrors());
                }
                                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public IActionResult Validate([FromBody]OrderQueueModel orderModel)
        {
            List<string> errorList = new List<string>();

            try
            {
                if (ModelState.IsValid)
                    return Ok();
                else
                    return StatusCode(403, GetModelErrors());
                                                
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public IEnumerable<OrderResultModel> Get()
        {
            return OrderTransformation.TransformOrdersInModels(orderService.GetAll()); 
        }
    }
}
