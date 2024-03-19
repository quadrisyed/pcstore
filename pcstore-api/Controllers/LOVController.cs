using Microsoft.AspNetCore.Mvc;
using pcstore_api.Models;

[ApiController]
[Route("api/[controller]")]
public class LOVController : ControllerBase
{
    private readonly StoredbContext _context;

    public LOVController(StoredbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllDropdownData()
    {

        var brands = _context.Brands.Select(c => new { c.BrandId, c.Name }).ToList();
        var gpus = _context.GraphicsCards.Select(c => new { c.GraphicCardId, Text= c.ToString() }).ToList();
        var storage = _context.Storages.Select(c => new { c.StorageId, Text= c.ToString() }).ToList();
        var processor = _context.Processors.Select(c => new { c.ProcessorId, Text= c.ToString() }).ToList();
        var psu = _context.Psus.Select(c => new { c.PsuId ,Text= c.ToString() }).ToList();
        var memory = _context.Memories.Select(c => new { c.MemoryId , Text=c.ToString() }).ToList();
     
        // Add more queries for other dropdowns as needed

        var dropdownData = new
        {
            GPUS = gpus,
            Storage= storage,
            Processor = processor,
            PSU = psu,
            Brands = brands,
            Memory = memory
            // Add more dropdowns as needed
        };

        return Ok(dropdownData);
    }
}
