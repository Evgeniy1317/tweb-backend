using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(new BussinesLogic().GetTournamentBL().GetAll());
    }
}