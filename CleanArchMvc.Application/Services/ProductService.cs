using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null) throw new Exception($"Entity could not be loaded");

            var products = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productQueryByID = new GetProductByIdQuery(id.Value);
            if (productQueryByID == null) throw new Exception($"Entity could not be loaded");

            var product = await _mediator.Send(productQueryByID);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task Add(ProductDTO product)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO product)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(product);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if (productRemoveCommand == null) throw new Exception($"Entity could not be loaded");

            await _mediator.Send(productRemoveCommand);
        }
    }
}
