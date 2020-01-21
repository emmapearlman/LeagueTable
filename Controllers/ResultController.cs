
using LeagueTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeagueTable.Controllers
{
    public class ResultController : Controller
    {

     [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ViewResult Submit()
        {
            //process data

            var pd = new ProcessData();
            pd.uploadPath = Server.MapPath("~/App_Data/uploads");
            pd.fileName = "PL-2015-2016.csv";
            var myLeague = pd.ProcessLeagueData();

           
            return View("ShowResults",myLeague);
        }

        public ActionResult ShowResults(Models.LeagueTable myLeague)
        {
            
            return View("ShowResults",myLeague);
        }

        public ActionResult ShowTeamResults(string team)
        {
            ViewBag.Message = "Results for " + team;
            var pd = new ProcessData();
            pd.uploadPath = Server.MapPath("~/App_Data/uploads");
            pd.fileName = "PL-2015-2016.csv";
            var myResults = pd.GetTeamResults(team);
            return View("ShowTeamResults",myResults);
        }

        public ViewResult GoBack()

        {
            return View("Index");
        }
    }
}