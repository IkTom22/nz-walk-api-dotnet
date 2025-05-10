using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    // Route: https://localhost:{port}/api/regions
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        // Constructor with corrected parameter name
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL regions
        // GET: https://localhost:{port}/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain models
            // var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Modeks to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            // Return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:{port}/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Find can only take primary key ( only one param) 
            // var region = dbContext.Regions.Find(id);
            // Get Region Domain Model from Database
            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/Convert Region Domain Model to Region DTO
            // var regionDto = new RegionDto
            // {
            //     Id = regionDomain.Id,
            //     Code = regionDomain.Code,
            //     Name = regionDomain.Name,
            //     RegionImageUrl = regionDomain.RegionImageUrl
            // };
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            //Return DTO back to client
            return Ok(regionDto);
        }

        // POST To Create New Region
        // POST: https://localhost:{port}/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            // var regionDomainModel = new Region
            // {
            //     Code = addRegionRequestDto.Code,
            //     Name = addRegionRequestDto.Name,
            //     RegionImageUrl = addRegionRequestDto.RegionImageUrl

            // };
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            //Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // await dbContext.Regions.AddAsync(regionDomainModel); // moved to Repository
            // await dbContext.SaveChangesAsync(); // moved to Repository
            //Map Domain Model back to DTO
            // var regionDto = new RegionDto
            // {
            //     Id = regionDomainModel.Id,
            //     Code = regionDomainModel.Code,
            //     Name = regionDomainModel.Name,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update region
        // Put: https://localhost:{port}/api/regions/{Id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model -- moved to Repository
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
        // Delete Region
        // DELETE: https://localhost:{port}/api/regions/{Id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(XmlConfigurationExtensions => XmlConfigurationExtensions.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
    }

}
