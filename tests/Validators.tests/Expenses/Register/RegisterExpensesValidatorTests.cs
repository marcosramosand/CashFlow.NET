using CashFlow.Application.UsesCases.Expenses;
using CashFlow.communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Request;
using FluentAssertions;

namespace Validators.tests.Expenses.Register;

public class RegisterExpensesValidatorTests
{
[Fact]
    public void Sucess()
    {
        // Arrange 
        var validator =  new ExpensesValidation();
        var request = RequestRegisterExpensesJsonBuilder.Build();


        //Act
        var result = validator.Validate (request);

        //Assert
      
        result.IsValid.Should().BeTrue();


    }
[Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void  Error_Title_empty(string title )


    {
        // Arrange 
        var validator = new ExpensesValidation();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Title = string.Empty;
       


        //Act
        var result = validator.Validate(request);

        //Assert

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));

    }

[Fact]
    public void Error_Date_Future()

    {
        // Arrange 
        var validator = new ExpensesValidation();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);



        //Act
        var result = validator.Validate(request);

        //Assert

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPEMSES_CANNOT_FOR_FUTURE));

    }
[Fact]
    public void Error_Payment_type_Invalid()

    {
        // Arrange 
        var validator = new ExpensesValidation();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.PaymentType = (PaymentType)600;



        //Act
        var result = validator.Validate(request);

        //Assert

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));

    }


 [Theory]
 [InlineData(0)]
 [InlineData(-6)]
 [InlineData(-8)]

    public void Error_Amount_Invalid(decimal amount )

    {
        // Arrange 
        var validator = new ExpensesValidation();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Amount = amount;



        //Act
        var result = validator.Validate(request);

        //Assert

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));

    }
}
