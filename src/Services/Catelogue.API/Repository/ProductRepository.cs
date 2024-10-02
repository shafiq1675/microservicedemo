using Catelogue.API.Context;
using Catelogue.API.Interface.Repository;
using Catelogue.API.Models;
using MongoRepo.Context;
using MongoRepo.Repository;

namespace Catelogue.API.Repository
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(new CatelogueDBContext())
        {
        }
    }
}
