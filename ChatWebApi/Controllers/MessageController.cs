using AutoMapper;
using FluentValidation;
using ChatWebApi.Contracts.Message;
using ChatWebApi.Models.Message;
using ChatWebApi.Services.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddMessageRequest> _validatorAddMessageRequest;
        private readonly IValidator<GetMessageBySizeRequest> _validatorGetMessageBySizeRequest;

        public MessageController(IMessageService messageService, IMapper mapper, IValidator<AddMessageRequest> validatorAddMessageRequest, IValidator<GetMessageBySizeRequest> validatorGetMessageBySizeRequest)
        {
            _messageService = messageService;
            _mapper = mapper;
            _validatorAddMessageRequest = validatorAddMessageRequest;
            _validatorGetMessageBySizeRequest = validatorGetMessageBySizeRequest;
        }

        [Authorize(Roles = "User")]
        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessageAsync([FromBody] AddMessageRequest request)
        {
            var validationResult = await _validatorAddMessageRequest.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var guid = User.FindFirst("guid")?.Value;

            var messageDto = _mapper.Map<AddMessageRequest, MessageDto>(request);

            if (guid != messageDto.FromId.ToString())
                return Forbid();

            var resultMessageDto = await _messageService.AddMessageAsync(messageDto);
            
            if (resultMessageDto is null)
                return BadRequest("No such user!");

            return Ok();
        }

        [Authorize(Roles = "User")]
        [HttpPost("GetMessagesBySizeAsync")]
        public async Task<IActionResult> GetMessagesBySizeAsync([FromBody] GetMessageBySizeRequest request)
        {
            var validationResult = await _validatorGetMessageBySizeRequest.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var guid = User.FindFirst("guid")?.Value;

            if (guid != request.FromId.ToString())
                return Forbid();

            var resultMessageDto = await _messageService.GetMessagesBySizeAsync(request.FromId, request.ToId, request.Page, request.Size);

            if (resultMessageDto is null)
                return BadRequest("No such user!");

            var result = _mapper.Map<IEnumerable<MessageDto>, IEnumerable<GetMessageBySizeResponse>>(resultMessageDto);

            return Ok(result);
        }
    }
}
