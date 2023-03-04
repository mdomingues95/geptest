using geptest.Models;
using geptest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace geptest.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductService _productServer;

        public ProductController(IProductService productService)
        {
            _productServer = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productServer.GetAll();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productServer.GetById(id);
                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest(new MessageResponse() { Message = ex.Message });
            }
           
        }

        [HttpPost]
        public IActionResult Create(ProductRequest model)
        {
            try
            {
                _productServer.Create(model);
                return Ok(new { message = "Produto cadastrado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse() { Message = ex.Message });
            }
           
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductRequest model)
        {
            try
            {
                _productServer.Update(id, model);
                return Ok(new { message = "Produto atualizado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse() { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productServer.Delete(id);
                return Ok(new { message = "Produto deletado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse() { Message = ex.Message });
            }
        }


        [HttpGet("{id}/{quantity}")]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            try
            {
                _productServer.UpdateQuantity(id, quantity);
                return Ok(new { message = "Quantidade atualizada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse() { Message = ex.Message });
            }
        }

    }
}
