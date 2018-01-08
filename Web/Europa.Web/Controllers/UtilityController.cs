using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Messages;

namespace Europa.Web.Controllers
{
    public class UtilityController : Controller
    {
        private readonly ICommandDispatcher _commands;

        public UtilityController(ICommandDispatcher commands)
        {
            _commands = commands;
        }

        public async Task<IActionResult> Populate()
        {
            await _commands.Send(new PopulateCommand());
            return Content("ok");
        }
    }
}
