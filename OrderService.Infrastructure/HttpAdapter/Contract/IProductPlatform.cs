using OrderService.Domain.DTOs;
using OrderService.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.HttpAdapter.Contract
{
    public interface IProductPlatform
    {
        Task<ResponseWrapper<IEnumerable<ProductDTO>>> GetAllProduct();
        Task<ResponseWrapper<ProductDTO>> GetProduct(string id);
    }
}
