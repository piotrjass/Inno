using InnoProducts.Models;
using MediatR;

public class GetProductsByNameQuery : IRequest<List<Product>>
{
    public string NameStartsWith { get; set; }

    public GetProductsByNameQuery(string nameStartsWith)
    {
        NameStartsWith = nameStartsWith;
    }
}