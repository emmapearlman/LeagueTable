using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeagueTable.Models
{
    public class LeagueTable
    {
        public List<FootballTeam> Table { get; set; }

        /// <summary>
        /// Order the league table.
        /// The team with the most points should be at the top.
        /// If teams have equal points, they are ordered by goal difference.
        /// If they have the same goal difference, they are ordered by name.
        /// ... and they will never have the same name so this guarantees the table is ordered.
        /// </summary>
        public IList<FootballTeam> OrderTable()
        {
            return Table.OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.GoalsFor)
                .ThenBy(t => t.GoalsAgainst)
                .ThenBy(t => t.Name)
                .ToList();
        }

        

        /// <summary>
        /// Get a football team by name. If the team doesn't exist, create it.
        /// </summary>
        /// <param name="Name">Team Name</param>
        /// <returns>FootballTeam</returns>
        public FootballTeam FindOrCreateTeam(string Name)
        {
            dynamic team = Table.SingleOrDefault(t =>
                    String.Equals(t.Name, Name, StringComparison.OrdinalIgnoreCase)
                );

            if (team == null)
            {
                team = new FootballTeam { Name = Name };
                Table.Add(team);
            }

            return team;
        }

    }
}