using Microsoft.AspNetCore.Mvc;

namespace adminpage.Controllers
{
    public class AuthorizationController : Controller
    {
        public const string SessionKeyStr = "_strconn";
        private readonly ILogger<AuthorizationController> _logger;
        private string dbconn = "";
        public string GetDbConn() {
            return dbconn;
        }
        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("host")] string host, [Bind("port")] string port, [Bind("database")] string database, [Bind("user")] string user, [Bind("pass")] string pass)
        {
            GlobalVar.database = database;

            GlobalVar.host = host;

            GlobalVar.port = port;

            GlobalVar.user = user;

            GlobalVar.password = pass;

            return RedirectToAction("Index", "WorldBoundaries");

        }

    }
}
