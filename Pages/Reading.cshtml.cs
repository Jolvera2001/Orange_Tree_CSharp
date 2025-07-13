using Ganss.Xss;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orange_Tree.Models;
using Orange_Tree.services;

namespace Orange_Tree.Pages;

public class Reading(ILogger<IndexModel> logger, BlogService service) : PageModel
{
    private readonly HtmlSanitizer  _htmlSanitizer = new HtmlSanitizer();
    public Blog? Blog { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var blog = await service.GetBlogBySlugAsync(slug);
        if (blog == null)
        {
            return NotFound();
        }
        blog.Content = SanitizeBlog(blog.Content);
        Blog = blog;
        return Page();
    }

    private string SanitizeBlog(string content)
    {
        var parsedContent = Markdown.ToHtml(content);
        return _htmlSanitizer.Sanitize(parsedContent);
    }
}