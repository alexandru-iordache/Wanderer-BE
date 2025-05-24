using Microsoft.AspNetCore.Http;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Services;

public class PostService : IPostService
{
    public async Task<string> SaveImage(IFormFile image, string uploadsPath)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return fileName;
    }
}
