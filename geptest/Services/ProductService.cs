using geptest.Context;
using geptest.Models;
using AutoMapper;

namespace geptest.Services
{

    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Create(ProductRequest model);
        void Update(int id, ProductRequest model);
        void Delete(int id);
        void UpdateQuantity(int id, int quantity);
    }

    public class ProductService: IProductService
    {
        private SqlLiteContext _context;

        private readonly IMapper _mapper;

        public ProductService(SqlLiteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
        }

        public Product GetById(int id)
        {
            return getProduct(id);
        }

        public void Create(ProductRequest model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("O campo nome não pode ser vazio");
            var product = _mapper.Map<Product>(model);
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        public void Update(int id, ProductRequest model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("O campo nome não pode ser vazio");
            var product = getProduct(id);
            _mapper.Map(model, product);
            _context.Product.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = getProduct(id);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var product = getProduct(id);
            product.Quantity = quantity;
            _context.Product.Update(product);
            _context.SaveChanges();
        }

        private Product getProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null) throw new Exception("Produto não encontrado");
            return product;
        }
    }
}
