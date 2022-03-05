using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        // Properties of the Category class
        public string Name { get; private set; }

        // Constructor of the Category class
        public Category(string name)
        {
            ValidateDomain(name);
        }
        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id. Id must be greater than 0");
            Id = id;
            ValidateDomain(name);
        }

        // Method to update and validate the Category class
        public void Update(string name)
        {
            ValidateDomain(name);
        }

        // validate the Category class

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name. Name is required");

            DomainExceptionValidation.When(name.Length > 50, "Invalid Name. Name must be less than 50 characters");

            DomainExceptionValidation.When(name.Length < 3, "Invalid Name. Name must be more than 3 characters");

            Name = name;
        }

        //relation between category and product
        public ICollection<Product> Products { get; set; }

    }
}
