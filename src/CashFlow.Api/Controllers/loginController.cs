using CashFlow.Application.UsesCases.Login.DoLogin;
using CashFlow.communication.Request;
using CashFlow.communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class loginController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
     
    public async Task<IActionResult> Login(
        [FromServices]  IDoLoginUseCase usecase,
        [FromBody] RequestLoginJson request)
    {
         var resopnse = await usecase.Execute(request);

        return Ok (resopnse);

    }

}
