using Microsoft.AspNetCore.Mvc;
using SalesApi.Model;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountAllocationController : ControllerBase    
    {
    


        [HttpPost(Name = "calculateDiscountAllocation")]
        public IActionResult Calculate([FromBody] ProductList productList)
        {
            // เลือกเฉพาะรายการที่ไม่ได้เป็นประเภท "P"
            var productDiscount = productList.items
                .Where(i => !string.Equals(i.discountType, "P", StringComparison.OrdinalIgnoreCase))
                .ToList();

          
            decimal sum = productDiscount.Sum(i => (decimal)i.amount);

            if (sum <= 0m || productList.totalDiscount <= 0 || productDiscount.Count == 0)
            {
               
                return Ok(new AllocatedDiscount
                {
                    allocatedDiscounts = new List<ItemDiscount>(),
                    totalDiscount = 0
                });
            }

            var resultsItem = new List<ItemDiscount>(productDiscount.Count);

            decimal totalDiscountDec = (decimal)productList.totalDiscount;
            decimal assigned = 0m;

            for (int idx = 0; idx < productDiscount.Count; idx++)
            {
                var item = productDiscount[idx];

                decimal share;
              
                 
                    decimal ratio = ((decimal)item.amount) / sum;                   
                    share = Math.Round(totalDiscountDec * ratio, 2, MidpointRounding.AwayFromZero);
                    assigned += share;
                

                resultsItem.Add(new ItemDiscount
                {
                    recordNo = item.recordNo,
                    discount = (double)share
                });
            }

            var results = new AllocatedDiscount
            {
                allocatedDiscounts = resultsItem,               
                totalDiscount = (double)totalDiscountDec
            };

            return Ok(results);
        }

    }
}
