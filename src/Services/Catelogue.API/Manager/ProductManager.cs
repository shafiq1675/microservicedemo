using Catelogue.API.Interface.Manager;
using Catelogue.API.Models;
using Catelogue.API.Repository;
using MongoRepo.Manager;
using MongoRepo.Repository;

namespace Catelogue.API.Manager
{
    public class ProductManager : CommonManager<Product>, IProductManager
    {
        public ProductManager() : base(new ProductRepository())
        {

        }
    }
}
