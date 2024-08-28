using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.MVC.Controllers;

[Authorize]
public class HomeController(ITopicService topicService, ICoursesService coursesService) : Controller
{
    public async Task<IActionResult> Index(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? searchTerm,
        [FromQuery] Guid? topicId,
        [FromQuery] CourseOrderingOptions? orderingOptions)
    {
        ViewData["Topics"] = await topicService.GetTopics();
        return View(await coursesService.GetPaginatedCourses(page, pageSize, topicId, searchTerm, orderingOptions));
    }
}
