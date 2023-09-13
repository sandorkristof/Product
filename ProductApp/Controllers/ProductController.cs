using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ProductApp.Dtos;

namespace ProductApp.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private static readonly List<ProductDto> products = new()
        {
            new ProductDto(Guid.NewGuid(),"Termék1",3500,DateTimeOffset.UtcNow),
            new ProductDto(Guid.NewGuid(),"Termék2",2500,DateTimeOffset.UtcNow),
            new ProductDto(Guid.NewGuid(),"Termék3",1600,DateTimeOffset.UtcNow)
        };
        [HttpGet]

        public IEnumerable<ProductDto> GetAll()
        {
            return products;
        }
        [HttpGet("{id}")]
        public ProductDto GetById(Guid id) 
        {
            var product = products.Where(x => x.Id == id).FirstOrDefault();

            return product;
        }
        [HttpPost]

        public ProductDto PostProduct(CreateProductDto createProduct)
        {
            var product = new ProductDto(
                Guid.NewGuid(),
                createProduct.ProductName,
                createProduct.ProductPrice,
                DateTimeOffset.UtcNow
                );
            products.Add(product);
            return CreatedAtAction(nameof(GetAll),new ProductDto { Id=id},product);
        }
        [HttpPut("{id}")]
        public ProductDto PullProduct(Guid id,UpdateProductDto updtaProduct)
        {
            var existingProduct=products.Where(x=>x.Id == id).FirstOrDefault();
            var product = existingProduct with
            {
                ProductName = updtaProduct.ProductName,
                ProductPrice = updtaProduct.ProductPrice,
            };
            var index=products.FindIndex(x=> x.Id == id);
            products[index] = product;

            return products[index];
        }
    }
}
