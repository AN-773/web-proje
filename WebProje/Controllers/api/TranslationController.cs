using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Mvc;
using WebProje.Services;

namespace WebProje.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class TranslationController : Controller
{
    private readonly LanguageService _languageService;

    public TranslationController(LanguageService languageService)
    {
        _languageService = languageService;
    }

    [HttpGet]
    public JsonResult GetTranslations()
    {
        return Json(_languageService.LocalizerToJson());
    }
    
}