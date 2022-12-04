using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Commands.DeleteProduct;
using ThisRefactored.Domain.Entities;
using Xunit;

namespace ThisRefactored.Application.Tests;

public sealed class DeleteProductHandlerTests
{
    [Fact]
    public async Task DeleteProductHandler_ShouldDeleteProduct()
    {
        var dbName = Guid.NewGuid()
                         .ToString();
        var product = new Product("name",
                                  "description",
                                  1,
                                  1);
        var prepContext = DbContextHelper.GetContextWithFreshDb(dbName);
        prepContext.Products.Add(product);
        await prepContext.SaveChangesAsync();
        await prepContext.DisposeAsync();

        await using var context = DbContextHelper.GetContextWithExistingDb(dbName);
        var service = new DeleteProductHandler(context);
        await service.Handle(new DeleteProductCommand(product.Id), CancellationToken.None);
        var products = await context.Products.ToListAsync();
        products.Should()
                .BeEmpty();
    }
}