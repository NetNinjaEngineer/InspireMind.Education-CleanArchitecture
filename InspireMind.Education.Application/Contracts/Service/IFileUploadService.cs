using Microsoft.AspNetCore.Http;

namespace InspireMind.Education.Application.Contracts.Service;
public interface IFileUploadService
{
    Task<bool> UploadFileAsync(IFormFile file);
}
