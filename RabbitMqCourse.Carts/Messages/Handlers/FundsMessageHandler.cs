using Microsoft.EntityFrameworkCore;
using RabbitMqCourse.Carts.DAL.Context;
using RabbitMqCourse.Carts.DAL.Models;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Carts.Messages.Handlers;

internal sealed class FundsMessageHandler : IMessageHandler<FundMessage>
{
    private readonly CartsDbContext _dbContext;

    public FundsMessageHandler(CartsDbContext context)
        => _dbContext = context;

    public async Task HandleAsync(FundMessage message)
    {
        var funds = await _dbContext.CustomerFunds.SingleOrDefaultAsync(x => x.CustomerId == message.CustomerId);

        if (funds is null)
        {
            funds = new CustomerFundsModel
            {
                CustomerId = message.CustomerId,
                CurrentFunds = message.CurrentFunds
            };

            await _dbContext.CustomerFunds.AddAsync(funds);
            return;
        }

        funds.CurrentFunds = message.CurrentFunds;
        _dbContext.CustomerFunds.Update(funds);
    }
}
