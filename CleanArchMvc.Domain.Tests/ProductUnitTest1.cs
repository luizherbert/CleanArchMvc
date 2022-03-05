using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 9.99m, 99, "Product image");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id. Id must be greater than 0");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, "P", "Product description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name must be more than 3 characters");
        }

        [Fact]
        public void CreateProduct_MissingNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, "", "Product description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact]
        public void CreateProduct_NullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, null, "Product description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionInvalidDescription()
        {
            Action action = () => new Product(1, "Product Name", "d", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Description. Description must be more than 3 characters");
        }

        [Fact]
        public void CreateProduct_MissingDescriptionValue_DomainExceptionInvalidDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Description. Description is required");
        }

        [Fact]
        public void CreateProduct_NullDescriptionValue_DomainExceptionInvalidDescription()
        {
            Action action = () => new Product(1, "Product Name", null, 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Description. Description is required");
        }

        [Fact]
        public void CreateProduct_NegativePriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "Product Name", "Product description", -9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Price. Price must be greater than 0");
        }

        [Fact]
        public void CreateProduct_NegativeStockValue_DomainExceptionInvalidStock()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 9.99m, -99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Stock. Stock must be greater than 0");
        }

        [Fact]
        public void CreateProduct_MissingImageValue_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionInvalidImage()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 9.99m, 99, "Product image asdsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaasadasdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Image. Image must be less than 250 characters");
        }


    }
}
