using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        // Properties of the Product class
        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public string Image { get; private set; }

        // Constructor of the Product class
        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id. Id must be greater than 0");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        // Method to update and validate the Product class
        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            DomainExceptionValidation.When(categoryId < 0, "Category Id must be greater than 0");
            CategoryId = categoryId;
        }

        // validate the Product class
        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name. Name is required");

            DomainExceptionValidation.When(name.Length > 50, "Invalid Name. Name must be less than 50 characters");

            DomainExceptionValidation.When(name.Length < 3, "Invalid Name. Name must be more than 3 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid Description. Description is required");

            DomainExceptionValidation.When(description.Length > 500, "Invalid Description. Description must be less than 500 characters");

            DomainExceptionValidation.When(description.Length < 3, "Invalid Description. Description must be more than 3 characters");

            DomainExceptionValidation.When(price < 0, "Invalid Price. Price must be greater than 0");

            DomainExceptionValidation.When(stock < 0, "Invalid Stock. Stock must be greater than 0");

            DomainExceptionValidation.When(image?.Length > 250, "Invalid Image. Image must be less than 250 characters");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        //relation between product and category
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
