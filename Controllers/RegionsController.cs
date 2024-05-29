using api8.Data;
using api8.Models;
using api8.Models.DTOs;
using api8.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly Api8DbContext DbContext;
        private readonly IRegionsRepository regionsRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public RegionsController(Api8DbContext DbContext, IRegionsRepository regionsRepository,
            IMapper mapper, ILogger<RegionsController> logger)
        {
            this.DbContext = DbContext;
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("Get All Was invoked");
                throw new Exception("New exception for testing");
                var regions = await this.regionsRepository.GetAllasync();
                if (regions == null)
                {
                    return NotFound();
                }
                //converting domain model into DTO
                var regionDTO = mapper.Map<List<RegionDTO>>(regions);
                /*
                foreach (var region in regions)
                {
                    regionDTO.Add(new RegionDTO() {
                        Id = region.Id,
                        Code = region.Code,
                        Name = region.Name,
                        RegionImageUrl = region.RegionImageUrl
                    });
                } */
                return Ok(regionDTO);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Getbyid([FromRoute] Guid id)
        {
            var region = await this.regionsRepository.Getbyidasync(id);
            if (region == null) { return NotFound(); }
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> create([FromBody] AddRegionDTO AddRegionDTO)
        {
            //DTO into Domain model
            var region = mapper.Map<Regions>(AddRegionDTO);
            region = await this.regionsRepository.create(region);

            //domain into DTO
            var regionDTO = mapper.Map<RegionDTO>(region);
            return CreatedAtAction(nameof(Getbyid), new { id = region.Id }, regionDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> putbyid([FromRoute] Guid id, [FromBody] AddRegionDTO AddRegionDTO)
        {
            var region = await this.regionsRepository.putbyid(id, AddRegionDTO);

            //Domainmodel into DTO
            var regionDTO = mapper.Map<RegionDTO> (region);
            return Ok(regionDTO);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> delete([FromRoute]Guid id)
        {
            var domainmodel = await this.regionsRepository.delete(id);
            if (domainmodel == null) { return NotFound(); }

            //Domain into DTO
            var regionDTO = mapper.Map<RegionDTO>(domainmodel);
            return Ok(regionDTO);
        }
    }
}
