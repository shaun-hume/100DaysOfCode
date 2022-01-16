using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _100DaysAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void Post([FromBody]Recipe recipe)
    {

        var x = recipe;
        return;
    }
}

public class Recipe
{
    public int Rating { get; set; }
    public string Title { get; set; }
    public Uri? WebUrl { get; set; }
    public string RecipeText { get; set; }
}