using System.Net;
using System.Text;
using Asp.Versioning;
using AutoMapper;
using Core.Contracts.BLL;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Helpers;
using Avatar = Core.BLL.DTO.Entities.Avatar;
using CDTO = Core.DTO.v1_0.Entities;

namespace WebApp.APIControllers
{
    /// <summary>
    /// Used to manage avatar entity.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/v{version:apiVersion}/[controller]/[action]")]
    public class AvatarController : ControllerBase
    {
        private readonly ICoreBLL _bll;
        private readonly PublicDTOBllMapper<CDTO.Avatar, Avatar> _mapper;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Avatars controller constructor
        /// </summary>
        /// <param name="bll">Business logic layer instance</param>
        /// <param name="autoMapper">Mapper for mapping DTOs</param>
        public AvatarController(ICoreBLL bll, IMapper autoMapper, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<CDTO.Avatar, Avatar>(autoMapper);
        }

        // GET: api/Avatar
        /// <summary>
        /// Get all avatars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<CDTO.Avatar>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<CDTO.Avatar>>> GetAll()
        {
            var res = (await _bll.Avatar.GetAllAsync())
                .Select(e => _mapper.Map(e)).ToList();
            return Ok(res);
        }

        // GET: api/Avatar/5
        /// <summary>
        /// Get avatar by his id
        /// </summary>
        /// <param name="id">Avatars id</param>
        /// <returns>Avatar</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<CDTO.Avatar>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Avatar>> GetById(Guid id)
        {
            var avatar = _mapper.Map(await _bll.Avatar.FirstOrDefaultAsync(id));

            if (avatar == null)
            {
                return NotFound();
            }

            return avatar;
        }

        /// <summary>
        /// Get avatar entities by their user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>List of avatar entities</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<IEnumerable<CDTO.Avatar>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<CDTO.Avatar>>> GetByUserId(Guid id)
        {
            var avatars = (await _bll.Avatar.GetByUserId(id)).Select(e => _mapper.Map(e)).ToList();
            return avatars.Count == 0 ? NotFound() : avatars!;
        }

        /// <summary>
        /// Get avatar bu user id with changed stats according to timespan
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Avatar</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<IEnumerable<CDTO.Avatar>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Avatar?>> GetAvatarUpdatedByUserId(Guid id)
        {
            var avatar = _mapper.Map(await _bll.Avatar.GetAvatarUpdatedByUserId(id));

            return avatar == null ? NotFound() : avatar;
        }

        // PUT: api/Avatar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update avatar data
        /// </summary>
        /// <param name="avatar">New avatar</param>
        /// <returns>Ok response</returns>
        [HttpPatch]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateAvatar(
            [FromBody]
            CDTO.Avatar avatar)
        {
            avatar.LastChanges = DateTime.UtcNow;
            _bll.Avatar.Update(_mapper.Map(avatar)!);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Avatar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new avatar
        /// </summary>
        /// <param name="createInfo">Avatars sex and image</param>
        /// <returns>Status 201</returns>
        [HttpPost]
        [ProducesResponseType<CDTO.Avatar>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Avatar>> AddAvatar(
            [FromBody]
            AvatarCreateInfo createInfo
            )
        {
            var user = await _userManager.GetUserAsync(User);
            
            var avatar = new CDTO.Avatar()
            {
                AppUserId = user!.Id,
                IsActive = true,
                Health = 100,
                Stamina = 100,
                Stress = 0,
                Hunger = 100,
                Strength = 10,
                Dexterity = 10,
                Intelligence = 10,
                Money = 100,
                Level = 1,
                Exp = 0,
                LastChanges = DateTime.Now.ToUniversalTime(),
                Sex = createInfo.Sex == "Male" ? ESex.Male : ESex.Female
            };
            
            if (createInfo.UploadedImage != null && createInfo.UploadedImage.Length > 0)
            {
                avatar.Image = Encoding.UTF8.GetBytes(createInfo.UploadedImage); 
            }
            
            _bll.Avatar.Add(_mapper.Map(avatar));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetById", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = avatar.Id
            }, avatar);
        }

        // DELETE: api/Avatar/5
        /// <summary>
        /// Delete avatar from database by id
        /// </summary>
        /// <param name="id">Avatars id</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> DeleteAvatar(Guid id)
        {
            var avatar = _mapper.Map(await _bll.Avatar.FirstOrDefaultAsync(id));
            if (avatar == null)
            {
                return NotFound();
            }

            await _bll.Avatar.RemoveAsync(_mapper.Map(avatar));
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        private bool AvatarExists(Guid id)
        {
            return _bll.Avatar.Exists(id);
        }
    }
}

public class StringedAvatar
{
    public string id { get; set; } = default!;
    public string appUserId { get; set; } = default!;
    public string isActve { get; set; } = default!;
    public string health { get; set; } = default!;
    public string stamina { get; set; } = default!;
    public string hunger { get; set; } = default!;
    public string stress { get; set; } = default!;
    public string strangth { get; set; } = default!;
    public string dexterity { get; set; } = default!;
    public string intelligence { get; set; } = default!;
    public string money { get; set; } = default!;
    public string sex { get; set; } = default!;
    public string level { get; set; } = default!;
    public string exp { get; set; } = default!;
    public string expToLevelUp { get; set; } = default!;
    public string image { get; set; } = default!;
    public string lastChanges { get; set; } = default!;
}
