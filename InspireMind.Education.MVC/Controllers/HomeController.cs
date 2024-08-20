using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.MVC.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ITopicService _topicService;
    private readonly ICoursesService _coursesService;

    public HomeController(ITopicService topicService, ICoursesService coursesService)
    {
        _topicService = topicService;
        _coursesService = coursesService;
    }

    public async Task<IActionResult> Index(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? searchTerm,
        [FromQuery] Guid? topicId,
        [FromQuery] CourseOrderingOptions? orderingOptions)
    {
        ViewData["Topics"] = await _topicService.GetTopics();
        return View(await _coursesService.GetPaginatedCourses(page, pageSize, topicId, searchTerm, orderingOptions));
    }
}
