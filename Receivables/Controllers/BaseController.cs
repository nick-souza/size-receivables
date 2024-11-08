using Microsoft.AspNetCore.Mvc;
using Receivables.DTO.Outgoing;

namespace Receivables.Controllers;

public class BaseController : ControllerBase
{
    protected ObjectResult OkResponse() => StatusCode(200, new Response(true));
    protected ObjectResult OkResponse<T>(T data) => StatusCode(200, new Response<T>(true, data));
}