using Catelogue.API.Interface.Manager;
using Catelogue.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult PostProducts([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);
                return CustomResult("Succeed", isSaved, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProducts([FromBody] Product product)
        {
            try
            {
                bool idUpdate = _productManager.Update(product.Id, product);
                return CustomResult("Succeed", idUpdate, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult DeleteProducts(string id)
        {
            try
            {
                bool isDelete = _productManager.Delete(id);
                return CustomResult("Succeed", isDelete, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
