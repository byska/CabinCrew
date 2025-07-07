using CabinCrew.Application.UseCases.CabinCrewUseCases.Commands;
using CabinCrew.Application.UseCases.CabinCrewUseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CabinCrew.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CabinCrewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CabinCrewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllCabinAttendantsQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetByIdCabinAttendantQueryRequest(id), cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCabinAttendantCommand command, CancellationToken cancellationToken = default)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCabinAttendantCommand command, CancellationToken cancellationToken = default)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<DeleteCabinAttendantCommandResponse> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new DeleteCabinAttendantCommand(id));
        }

        [HttpPost("{id}/recipes")]
        public async Task<AddRecipeCommandResponse> AddRecipe(Guid id, [FromBody] AddRecipeCommand command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command);

        }


        [HttpDelete("{id}/recipes/{recipeName}")]
        public async Task<RemoveRecipeCommandResponse> RemoveRecipe(Guid id, string recipeName, CancellationToken cancellationToken = default)
        {
           return await _mediator.Send(new RemoveRecipeCommand(id, recipeName));
        }


        [HttpPut("{id}/info")]
        public async Task<UpdateAttendantInfoCommandResponse> UpdateInfo(Guid id, [FromBody] UpdateAttendantInfoCommand command, CancellationToken cancellationToken = default)
        {
           return await _mediator.Send(command);
        }
    }
}
