using Application.Abstracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/property-media")]
public class PropertyMediaController : ControllerBase
{
    private readonly IPropertyMediaService _service;

    public PropertyMediaController(IPropertyMediaService service)
    {
        _service = service;
    }

    // 🔹 Upload media to property
    // POST: api/property-media/1
    [HttpPost("{propertyAdId:int}")]
    public async Task<IActionResult> Upload(
        int propertyAdId,
        IFormFile file,
        [FromQuery] int? order = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File boşdur");

        var mediaId = await _service.UploadAsync(propertyAdId, file, order);

        return Ok(new { MediaId = mediaId });
    }

    // 🔹 Get all media of a property
    // GET: api/property-media/property/1
    [HttpGet("property/{propertyAdId:int}")]
    public async Task<IActionResult> GetAll(int propertyAdId)
    {
        var result = await _service.GetByPropertyIdAsync(propertyAdId);
        return Ok(result);
    }

    // 🔹 Delete media
    // DELETE: api/property-media/5
    [HttpDelete("{mediaId:int}")]
    public async Task<IActionResult> Delete(int mediaId)
    {
        await _service.DeleteAsync(mediaId);
        return Ok("Silindi");
    }
}
