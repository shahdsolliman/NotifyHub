using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.DTOs;
using NotifyHub.Application.Commands;
using NotifyHub.Application.Common.Interfaces;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public NotificationsController(IMediator mediator, IMapper mapper, IApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNotificationRequest request)
        {
            var command = _mapper.Map<CreateNotificationCommand>(request);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NotificationResponseDto>> GetById(Guid id)
        {
            var outbox = await _dbContext.NotificationOutboxes.FindAsync(id);
            if (outbox == null)
                return NotFound();
            var dto = _mapper.Map<NotificationResponseDto>(outbox);
            return Ok(dto);
        }
    }
}
