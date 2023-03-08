using Microsoft.AspNetCore.Mvc;
using TestWebApi.Business.Employees.Interfaces;
using TestWebApi.Models.Crud;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Enum;
using TestWebApi.Models.Query;
using TestWebApi.Models.Result;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRetriever Retriever { get; }
        private IEmployeeModifier Modifier { get; }
        private IEmployeeRemover Remover { get; }

        public EmployeesController(IEmployeeRetriever retriever, IEmployeeModifier modifier, IEmployeeRemover remover)
        {
            Retriever = retriever;
            Modifier = modifier;
            Remover = remover;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> FindEmployeesAsync([FromQuery] FindEmployeeQuery request)
        {
            ListResult<EmployeeDto> result = await Retriever.FindEmployeesAsync(request);

            return result.ResultType switch
            {
                ResultType.Found => new OkObjectResult(result.Result),
                ResultType.NotFound => new NotFoundResult(),
                _ => new BadRequestResult()
            };
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> SaveAsync(string code, [FromBody] SaveEmployeeIn request)
        {
            ItemResult<EmployeeDto> result = await Modifier.SaveEmployeeAsync(code, request);

            return result.ResultType switch
            {
                ResultType.Created => new CreatedResult((result.Result is not null ? $"api/employees/{result.Result.EmployeeCode}" : string.Empty), result.Result),
                ResultType.Updated => new OkObjectResult(result.Result),
                ResultType.BadRequest => new BadRequestObjectResult(result.Message),
                _ => new StatusCodeResult(304)
            };
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> RemoveAsync(string code)
        {
            ItemResult<EmployeeDto> result = await Remover.RemoveAsync(code);

            return result.ResultType switch
            {
                ResultType.NoContent => new NoContentResult(),
                ResultType.NotFound => new NotFoundResult(),
                ResultType.BadRequest => new BadRequestObjectResult(result.Message),
                _ => new BadRequestResult()
            };
        }
    }
}
