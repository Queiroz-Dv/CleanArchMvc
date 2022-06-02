using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;
namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                "product image"
            );

            action
                .Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product
            (
               -1,
               "Product Name",
               "Product Description",
                9.99m,
                99,
               "product image"
            );

            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product
            (
                1,
                "Pn",
                "Product Description",
                9.99m,
                99,
                "product image"
            );

            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                "product image toooooooooooooooooooooooooooooo" +
                "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo " +
                "longgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
                "ggggggggggggggggggggggg"
            );

            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid imnage name, too long, maximum 250 characters.");
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                null
            );

            action
                .Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                null
            );

            action
                .Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                ""
            );

            action
                .Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }


        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product
            (
                1,
                "Product Name",
                "Product Description",
                -9.99m,
                99,
                "product image "
            );

            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value.");
        }

        [Theory] // Theory é usado quando há parâmetros no teste 
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product
            (
                1,
                "Pro",
                "Product Description",
                9.99m,
                value,
                "product image"
            );

            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value.");
        }
    }
}
