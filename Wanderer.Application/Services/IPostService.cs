using Microsoft.AspNetCore.Http;

namespace Wanderer.Application.Services;

public interface IPostService
{
    Task<string> SaveImage(IFormFile image, string uploadsPath);
}
