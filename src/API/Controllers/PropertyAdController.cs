using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.PropertyAd;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyAdController : ControllerBase
{
    private readonly IPropertyAdService _propertyAdService;
    public PropertyAdController(IPropertyAdService propertyAdService)
    {
        _propertyAdService = propertyAdService;
    }
    [HttpGet]
    public async  Task<IActionResult> GetAllAsync()
    {
        var propertyAds = await _propertyAdService.GetAllAsync();
        return Ok(propertyAds);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePropertyAdRequest request)
    {
        await _propertyAdService.CreateAsync(request);
        return Ok();
    }
    [HttpGet]
    [Route("{id}")]
    public async  Task<IActionResult> GetByIdAsync(int id)
    {
        var propertyAd = await _propertyAdService.GetByIdAsync(id);
        if (propertyAd == null)
            return NotFound();
        return Ok(propertyAd);
    }




}
