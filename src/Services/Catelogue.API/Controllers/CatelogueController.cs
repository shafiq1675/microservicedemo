using Catelogue.API.Interface.Manager;
using Catelogue.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catelogue.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatelogueController : BaseController
    {
        IProductManager _productManager;
        public CatelogueController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Succeed", products, HttpStatusCode.OK);

            }
            catch (Exception e)
            {

                return CustomResult(e.Message, HttpStatusCode.OK);

            }
        }
    }
}
