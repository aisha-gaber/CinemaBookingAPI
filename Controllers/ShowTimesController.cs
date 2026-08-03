using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowTimesController : ControllerBase
    {
        private readonly IShowTimeService _service;

        public ShowTimesController(IShowTimeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShowTimeRequest request)
        {
            var item = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShowTimeRequest request)
        {
            var item = await _service.UpdateAsync(id, request);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}