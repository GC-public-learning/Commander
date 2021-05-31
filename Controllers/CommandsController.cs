using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers{

    // api/commands
    [Route("api/[controller]")] 
    [ApiController]
    public class CommandsController : ControllerBase {

        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands() {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems)); // Ok -> code 200
        }

        // api/commands/{id} -> to get a JSON value
        [HttpGet("{id}")] 
        public ActionResult <CommandReadDto> GetCommandById(int id) {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound(); //404
        }
    }
}
