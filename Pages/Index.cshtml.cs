using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orange_Tree.Models;
using Orange_Tree.services;

namespace Orange_Tree.Pages;

public class IndexModel(ILogger<IndexModel> logger, BlogService service) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;
    
    public List<Blog> Blogs { get; set; } = new List<Blog>();
    
    public async Task<IActionResult> OnGetAsync()
    {
        Blogs = await service.GetBlogsAsync();
        return Page();
    }
}