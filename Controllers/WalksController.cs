using api8.Data;
using api8.Models;
using api8.Models.DTOs;
using api8.Repository;
using api8.validation;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Api8DbContext dbContext;
        private readonly IWalks walksRepository;

        public WalksController(IMapper mapper, Api8DbContext dbContext, IWalks walksRepository)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.walksRepository = walksRepository;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] WalksDTO walksDTO)
        {
            Walks Walkss = mapper.Map<Walks>(walksDTO);
            var Walk = await this.walksRepository.CreateWalksAsync(Walkss);
            return Ok(mapper.Map<WalksDTO>(Walkss));
        }
        [HttpGet]
        public async Task<IActionResult> Getall([FromQuery] string? filterOn, [FromQuery] string? value, [FromQuery] string? orderBy,
            [FromQuery] bool ascending = true, [FromQuery] int pagenum = 1, [FromQuery] int size=1000 )
        {
            var walks = await this.walksRepository.GetAllWalks(filterOn, value, orderBy, ascending, pagenum, size);
            if (walks == null) { return NotFound(); }
            //var WalksDTO = mapper.Map<List<WalksDTO>>(walks);
            return Ok(walks);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Getbyid([FromRoute] Guid id)
        {
            var DomainWalks = await this.walksRepository.GetByID(id);
            if (DomainWalks == null) { return NotFound(id); }

            //Domainmodel into DTO
            return Ok(mapper.Map<WalksDTO>(DomainWalks));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> putbyid([FromRoute]Guid id, [FromBody] AddwalkDTO addwalkDTO)
        {
            var domain = mapper.Map<Walks>(addwalkDTO);
            var wals = await this.walksRepository.putById(id, domain);
            return Ok(mapper.Map<WalksDTO>(wals));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domain = await this.walksRepository.Delete(id);
            if(domain == null) { return NotFound(); }

            //Domain into Dto
            return Ok(mapper.Map<WalksDTO>(domain));
        }
    }
}
