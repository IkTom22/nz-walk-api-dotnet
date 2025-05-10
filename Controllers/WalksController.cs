using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        // Create Walk
        //POST: /api/walks
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Get All Walks
        // Get: https://localhost:{port}/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true, pageNumber, pageSize);
            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Create Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //MapDto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);
            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // Get walk by Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update walk
        //PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        //Delete walk
        //DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}