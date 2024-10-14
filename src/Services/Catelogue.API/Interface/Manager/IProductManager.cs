using Catelogue.API.Models;
using MongoRepo.Interfaces.Manager;

namespace Catelogue.API.Interface.Manager
{
    public interface IProductManager:ICommonManager<Product>
    {
        public List<Product> GetProductByCategory(string category);
    }
}
