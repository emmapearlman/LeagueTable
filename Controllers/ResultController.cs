namespace LeagueTable.Controllers
{
    using LeagueTable.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    public class ResultController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            string resultsPath = Server.MapPath("~/App_Data/uploads");
            DirectoryInfo resultsDirectory = new DirectoryInfo(resultsPath);
            var fileList = resultsDirectory.GetFiles("*.csv");
            var files = new List<SelectListItem>();
            foreach (var f in fileList)
            {
                files.Add(new SelectListItem
                {
                    Value = f.FullName,
                    Text = f.Name
                });
            }

            Session["resultsfiles"] = files;
            return View();
        }

        [HttpPost]
        public ViewResult Submit()
        {
            //process data

            var pd = new ProcessData();
            FileInfo newFile = new FileInfo(Request.Form["MyFiles"].ToString());
            Session["FilePath"] = Request.Form["MyFiles"].ToString();
            pd.uploadPath = newFile.DirectoryName;
            pd.fileName = newFile.Name;
            var myLeague = pd.ProcessLeagueData();

            return View("ShowResults", myLeague);
        }

        public ActionResult ShowResults(Models.LeagueTable myLeague)
        {

            return View("ShowResults", myLeague);
        }

        public ActionResult ShowTeamResults(string team)
        {
            ViewBag.Message = "Results for " + team;
            var pd = new ProcessData();
            FileInfo newFile = new FileInfo(Session["FilePath"].ToString());
            pd.uploadPath = newFile.DirectoryName;
            pd.fileName = newFile.Name;
            var myResults = pd.GetTeamResults(team);
            return View("ShowTeamResults", myResults);
        }

        public ViewResult GoBack()
        {
            return View("Index");
        }
    }
}