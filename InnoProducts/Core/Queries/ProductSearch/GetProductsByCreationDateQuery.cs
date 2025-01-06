using InnoProducts.Models;
using MediatR;

public class GetProductsByCreationDateQuery : IRequest<List<Product>>
{
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }

    public GetProductsByCreationDateQuery(DateTime? minDate, DateTime? maxDate)
    {
        MinDate = minDate;
        MaxDate = maxDate;
    }
}