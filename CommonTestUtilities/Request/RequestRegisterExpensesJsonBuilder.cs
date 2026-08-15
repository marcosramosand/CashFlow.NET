using Bogus;
using CashFlow.communication.Enums;
using CashFlow.communication.Request;

namespace CommonTestUtilities.Request;

public class RequestRegisterExpensesJsonBuilder
{
    public  static RequestExpensesJson Build()
    {

        return  new Faker<RequestExpensesJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description , faker => faker .Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal());
      

    }

}
