using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeagueTable.Models
{
    public class FootballTeam
    {
        public string Name { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Defeats { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GoalDifference
        {
            get { return GoalsFor - GoalsAgainst; }
        }


    }
}