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
using Microsoft.IdentityModel.Tokens;
using WebApp.DTO;
using WebApp.Helpers;
using Avatar = Core.BLL.DTO.Entities.Avatar;
using CDTOE = Core.DTO.v1_0.AddressTables;
using CDTO = Core.DTO.v1_0.Entities;
using Item = Core.BLL.DTO.Entities.Item;
using Owns = Core.BLL.DTO.AddressTables.Owns;

namespace WebApp.APIControllers
{
    /// <summary>
    /// Item management controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/v{version:apiVersion}/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly ICoreBLL _bll;
        private readonly PublicDTOBllMapper<CDTO.Item, Item> _mapper;
        private readonly PublicDTOBllMapper<CDTOE.Owns, Owns> _ownsMapper;
        private readonly PublicDTOBllMapper<CDTO.Avatar, Avatar> _avatarMapper;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Controllers constructor
        /// </summary>
        /// <param name="bll">Business Logic Layer instance</param>
        /// <param name="mapper">Auto mapper</param>
        public ItemController(ICoreBLL bll, IMapper autoMapper, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<CDTO.Item, Item>(autoMapper);
            _ownsMapper = new PublicDTOBllMapper<CDTOE.Owns, Owns>(autoMapper);
            _avatarMapper = new PublicDTOBllMapper<CDTO.Avatar, Avatar>(autoMapper);
        }

        // GET: api/Item
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>Enumerable of items</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<CDTO.Item>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<CDTO.Item>>> GetAll()
        {
            var res = (await _bll.Item.GetAllAsync()).Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Item/5
        /// <summary>
        /// Get item by its id
        /// </summary>
        /// <param name="id">Items id</param>
        /// <returns>Item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<IEnumerable<CDTO.Item>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Item>> GetById(Guid id)
        {
            var item =_mapper.Map(await _bll.Item.FirstOrDefaultAsync(id)) ;

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Get item by its id as string
        /// </summary>
        /// <param name="id">Items id as string</param>
        /// <returns>Item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<CDTO.Item>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public Task<ActionResult<CDTO.Item>> GetItemByIdValue(string id)
        {
            var item = _mapper.Map(_bll.Item.GetItemByIdValue(id));
            return Task.FromResult<ActionResult<CDTO.Item>>(item == null ? NotFound() : item);
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update item data
        /// </summary>
        /// <param name="item">Changed item instance</param>
        /// <returns>Status 200</returns>
        [HttpPatch]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateItem([FromBody] CDTO.Item item)
        {
            _bll.Item.Update(_mapper.Map(item));
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new Item entity to database
        /// </summary>
        /// <param name="item">New item instance</param>
        /// <returns>Status 201</returns>
        [HttpPost]
        [ProducesResponseType<Avatar>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CDTO.Item>> AddItem(
            [FromBody]
            ItemCreateInfo item)
        {
            var newItem = new CDTO.Item()
            {
                Id = new Guid(),
                Name = item.Name,
                Description = item.Description,
                IsConsumable = item.IsConsumable,
                StatToUpgrade = (EStats)(item.StatToUpgrade ?? 0),
                ItemRarity = (ERarity) item.ItemRarity,
                Slot = (ESlot)(item.Slot ?? 0),
                Price = item.Price,
            };
            
            if (item.Image != null && item.Image.Length > 0)
            {
                newItem.Image = Encoding.UTF8.GetBytes(item.Image);
            }

            if (item.Object != null && item.Object.Length > 0)
            {
                newItem.Object = Encoding.UTF8.GetBytes(item.Object);
            }
            
            _bll.Item.Add(_mapper.Map(newItem)!);
            await _bll.SaveChangesAsync();

            // return CreatedAtAction("GetById", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = item.Id
            // }, item);
            return NoContent();
        }

        // DELETE: api/Item/5
        /// <summary>
        /// Delete item from data base
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = _mapper.Map(await _bll.Item.FirstOrDefaultAsync(id));
            if (item == null)
            {
                return NotFound();
            }

            await _bll.Item.RemoveAsync(_mapper.Map(item)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        /// <summary>
        /// Buy Item by its Id
        /// </summary>
        /// <param name="id">Items id</param>
        /// <returns>No Content</returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> BuyItem(
            [FromBody]
            Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var avatar = _avatarMapper.Map((await _bll.Avatar.GetByUserId(user.Id)).First(e => e.IsActive));
            var item = (await _bll.Item.GetAllAsync()).Select(e => _mapper.Map(e)).First(i => i.Id.Equals(id));
            
            if (avatar.Money >= item.Price)
            {
                var avatarInventory = (await _bll.Owns.GetAllByAvatarIdAsync(avatar.Id)).Select(e => _ownsMapper.Map(e));
                avatar.Money -= item.Price;

                var owns = avatarInventory.FirstOrDefault(e => e.AvatarId.Equals(avatar.Id) && e.ItemId.Equals(item.Id));

                if (owns != null)
                {
                    owns.Amount += 1;
                    _bll.Owns.Update(_ownsMapper.Map(owns)!);
                }
                else
                {
                    owns = new CDTOE.Owns()
                    {
                        AvatarId = avatar.Id,
                        ItemId = item.Id,
                        Amount = 1,
                        IsEquipped = false
                    };
                    _bll.Owns.Add(_ownsMapper.Map(owns));
                }

                await _bll.SaveChangesAsync();
            }

            return NoContent();
        }

        /// <summary>
        /// Use item by its id
        /// </summary>
        /// <param name="id">Items id</param>
        /// <returns>No Content</returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UseItem(
            [FromBody]
            Guid id
            )
        {
            var owns = _ownsMapper.Map(await _bll.Owns.GetByIdWithData(id)); // Mapping destroy nested avatar(
            var avatar = _avatarMapper.Map(_bll.Avatar.GetById(owns.AvatarId));
            if (owns.Item!.IsConsumable)
            {
                if (owns.Amount > 1)
                {
                    owns.Amount--;
                    if (avatar!.Hunger + 10 > 100) avatar.Hunger = 100;
                    else avatar.Hunger += 10;
                    _bll.Owns.Update(_ownsMapper.Map(owns)!);
                    _bll.Avatar.Update(_avatarMapper.Map(avatar)!);
                }
                else
                {
                    if (avatar!.Hunger + 10 > 100) avatar.Hunger = 100;
                    else avatar.Hunger += 10;
                    _bll.Avatar.Update(_avatarMapper.Map(avatar)!);
                    await _bll.Owns.RemoveAsync(_ownsMapper.Map(owns)!);
                }
            }
            else
            {
                owns.IsEquipped = !owns.IsEquipped;
                _bll.Owns.Update(_ownsMapper.Map(owns)!);
            }

            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Sell an Item by its id as a string
        /// </summary>
        /// <param name="id">Items id as a string</param>
        /// <returns>No content or Not found</returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> SellItem(string id)
        {
            var avatar = _avatarMapper.Map((await _bll.Avatar.GetByUserId(
                    (await _userManager.GetUserAsync(User))!.Id)
                ).FirstOrDefault(e => e.IsActive));
            var owns = _ownsMapper.Map(_bll.Owns.GetByIdString(id));
            if (avatar == null || owns == null) return NotFound();

            var item = _mapper.Map(await _bll.Item.FirstOrDefaultAsync(owns.ItemId));
            avatar.Money += item!.Price / 2;
            _bll.Avatar.Update(_avatarMapper.Map(avatar));

            if (owns.Amount > 1)
            {
                owns.Amount -= 1;
                _bll.Owns.Update(_ownsMapper.Map(owns));
            }
            else
            {
                await _bll.Owns.RemoveAsync(_ownsMapper.Map(owns));
            }
            
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return _bll.Item.Exists(id);
        }
    }
}
