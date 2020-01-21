using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeagueTable.Models
{
    public class TeamResults
    {
        public DateTime MatchDate { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }

    }
}