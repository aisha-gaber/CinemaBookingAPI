using Asp.Versioning;
using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllV1([FromQuery] string? search, [FromQuery] string? genre,
            [FromQuery] string? sortBy, [FromQuery] bool descending = false,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(search, genre, sortBy, descending, page, pageSize);

            var v1Items = result.Items.Select(m => new MovieDtoV1
            {
                Id = m.Id,
                Name = m.Name,
                AvailableInCinema = m.AvailableInCinema
            }).ToList();

            return Ok(new
            {
                items = v1Items,
                totalCount = result.TotalCount,
                page = result.Page,
                pageSize = result.PageSize
            });
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAllV2([FromQuery] string? search, [FromQuery] string? genre,
            [FromQuery] string? sortBy, [FromQuery] bool descending = false,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(search, genre, sortBy, descending, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _service.GetByIdAsync(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id, version = "1.0" }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieRequest request)
        {
            var movie = await _service.UpdateAsync(id, request);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}