using System.Net;
using Asp.Versioning;
using AutoMapper;
using Core.Contracts.BLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Helpers;
using CDTO = Core.DTO.v1_0.AddressTables;
using Owns = Core.BLL.DTO.AddressTables.Owns;

namespace WebApp.APIControllers
{
    /// <summary>
    /// Owns management controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/v{version:apiVersion}/[controller]/[action]")]
    public class OwnsController : ControllerBase
    {
        private readonly ICoreBLL _bll;
        private readonly PublicDTOBllMapper<CDTO.Owns, Owns> _mapper;

        /// <summary>
        /// Controllers constructor
        /// </summary>
        /// <param name="mapper">Auto mapper</param>
        /// <param name="bll">Business Logic Layer instance</param>
        public OwnsController(IMapper mapper, ICoreBLL bll)
        {
            _mapper = new PublicDTOBllMapper<CDTO.Owns, Owns>(mapper);
            _bll = bll;
        }

        // GET: api/Owns
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<CDTO.Owns>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]        
        public async Task<ActionResult<IEnumerable<CDTO.Owns>>> GetAll()
        {
            var res = (await _bll.Owns.GetAllAsync()).Select(e => _mapper.Map(e));
            if (!res.Any())
            {
                return NotFound();
            }
            return Ok(res);
        }

        // GET: api/Owns/5
        /// <summary>
        /// Get Owns by its id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Owns entity</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<CDTO.Owns>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<ActionResult<CDTO.Owns>> GetById(Guid id)
        {
            var owns = _mapper.Map(await _bll.Owns.FirstOrDefaultAsync(id));

            if (owns == null)
            {
                return NotFound();
            }

            return owns;
        }

        /// <summary>
        /// Get by avatars id
        /// </summary>
        /// <param name="id">Avatars id</param>
        /// <returns>List of avatars owns</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<IEnumerable<CDTO.Owns>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<ActionResult<IEnumerable<CDTO.Owns>>> GetByAvatarsId(Guid id)
        {
            var owns = (await _bll.Owns.GetAllByAvatarIdAsync(id)).Select(e => _mapper.Map(e)).ToList();
            return owns.Count == 0 ? NotFound() : owns!;
        }

        /// <summary>
        /// Get owns by avatar and item ids
        /// </summary>
        /// <param name="avatarId">Avatars id</param>
        /// <param name="itemId">Items id</param>
        /// <returns>Owns instance</returns>
        [HttpGet("{avatarId}/{itemId}")]
        [ProducesResponseType<IEnumerable<CDTO.Owns>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<ActionResult<CDTO.Owns>> GetByAvatarAndItemIds(Guid avatarId, Guid itemId)
        {
            var owns = _mapper.Map(_bll.Owns.GetByAvatarAndItemIds(avatarId, itemId));
            return owns == null ? NotFound() : owns;
        }

        // PUT: api/Owns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update entity from its changed instance
        /// </summary>
        /// <param name="owns">Changed instance</param>
        /// <returns>Status 200</returns>
        [HttpPatch]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateOwns([FromBody] CDTO.Owns owns)
        {
            
            
            _bll.Owns.Update(_mapper.Map(owns));
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Owns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new Owns instance to database
        /// </summary>
        /// <param name="createInfo">New entity instance</param>
        /// <returns>Status 201</returns>
        [HttpPost]
        [ProducesResponseType<CDTO.Owns>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Owns>> AddOwns(OwnsCreateInfo createInfo)
        {
            var owns = new CDTO.Owns
            {
                Id = new Guid(),
                AvatarId = Guid.Parse(createInfo.AvatarId),
                ItemId = Guid.Parse(createInfo.ItemId),
                Amount = createInfo.Amount,
                IsEquipped = createInfo.IsEquiped
            };
            _bll.Owns.Add(_mapper.Map(owns)!);
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Owns/5
        /// <summary>
        /// Delete instance from database
        /// </summary>
        /// <param name="id">Owns id</param>
        /// <returns>No Content or NotFound</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteOwns(Guid id)
        {
            var owns = _mapper.Map(await _bll.Owns.FirstOrDefaultAsync(id));
            if (owns == null)
            {
                return NotFound();
            }

            await _bll.Owns.RemoveAsync(_mapper.Map(owns));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnsExists(Guid id)
        {
            return _bll.Owns.Exists(id);
        }
    }
}
