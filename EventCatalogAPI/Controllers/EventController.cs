using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventContext _context;
        private readonly IConfiguration _config;
        public EventController(EventContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var items= await  _context.EventCategories.ToListAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventLocations()
        {
            var items = await _context.EventLocations.ToListAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("items/{id}")]
        public async Task<IActionResult> GetItemesById(int id)
        {
            if (id<=0)
            {
                return BadRequest("Invalid Id!");
            }
            var item = await _context.EventItems.FirstOrDefaultAsync(e => e.Id == id);
            if (item!=null)
            {
                item.EventImageUrl = item.EventImageUrl.Replace("http://externalcatalogbaseurltobereplaced"
                    , _config["ExternalEventBaseUrl"]);
                return Ok(item);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery]int pageSize=6,
            [FromQuery]int pageIndex=0)
        {
            var itemsCount=await _context.EventItems.LongCountAsync();
            var items = await _context.EventItems
                .OrderBy(e => e.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            items = ChangePictureUrl(items);
            var model=new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = itemsCount,
                Data = items
            };
            return Ok(model);
        }
        private List<EventItem> ChangePictureUrl(List<EventItem> items)
        {
            items.ForEach(
                e => e.EventImageUrl = e.EventImageUrl.Replace("http://externalcatalogbaseurltobereplaced"
                    , _config["ExternalEventBaseUrl"])
                );
            return items;
        }
    }
}