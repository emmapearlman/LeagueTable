using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeagueTable.Models
{
    public class ProcessData
    {

        public ProcessData()
        {

        }

        /// <summary>
        /// these are hard coded for the purposes of this example
        /// </summary>
        public string uploadPath { get; set; }
        public string fileName { get; set; }
        /// <summary>
        /// Process the data into a League table format to be bound to a grid
        /// </summary>
        public LeagueTable ProcessLeagueData()
        {
            var data = ReadFile();
            var LeagueTable = new LeagueTable { Table = new List<FootballTeam>() };

            foreach (var line in data)
            {

                var lineData = line.Split(',');
                if (lineData[0].ToString() != "Div")
                {
                    // Parse teams
                    var homeTeamName = lineData[2];
                    var awayTeamName = lineData[3];

                    var homeTeam = LeagueTable.FindOrCreateTeam(homeTeamName);
                    var awayTeam = LeagueTable.FindOrCreateTeam(awayTeamName);

                    // Parse score

                    var homeTeamScore = int.Parse(lineData[4]);
                    var awayTeamScore = int.Parse(lineData[5]);

                    // Apply result to the league table
                    if (homeTeamScore > awayTeamScore)
                    {
                        // Home team win
                        homeTeam.Points += 3;
                        homeTeam.Wins += 1;
                        awayTeam.Defeats += 1;
                    }
                    else if (awayTeamScore > homeTeamScore)
                    {
                        // Away team win
                        awayTeam.Points += 3;
                        awayTeam.Wins += 1;
                        homeTeam.Defeats += 1;
                    }
                    else
                    {
                        // Draw
                        homeTeam.Points += 1;
                        awayTeam.Points += 1;
                        homeTeam.Draws += 1;
                        awayTeam.Draws += 1;
                    }

                    // Apply goal difference, this happens regardless of the score
                    homeTeam.GoalsFor += homeTeamScore;
                    homeTeam.GoalsAgainst += awayTeamScore;
                    awayTeam.GoalsFor += awayTeamScore;
                    awayTeam.GoalsAgainst += homeTeamScore;

                    // Increment number of games played, this happens regardless of score
                    homeTeam.Played++;
                    awayTeam.Played++;
                }
            }

            return LeagueTable;

        }

        /// <summary>
        /// reads file 
        /// </summary>
        /// <returns>returns an enumerable of strings based on file contents</returns>
        public IEnumerable<string> ReadFile()
        {
            return File.ReadLines(Path.Combine(uploadPath, fileName));
        }

        public IEnumerable<TeamResults> GetTeamResults(string Team)
        {
            var tResults = new List<TeamResults>();

            var data = ReadFile();

            foreach (var line in data)
            {

                var lineData = line.Split(',');
                if (lineData[0].ToString() != "Div")
                {
                    if (lineData[2] == Team
                        || lineData[3] == Team)
                    {
                        var res = new TeamResults();
                        res.MatchDate = DateTime.Parse(lineData[1]);
                        res.HomeTeam = lineData[2];
                        res.AwayTeam = lineData[3];
                        res.Score = lineData[4] + "-" + lineData[5];
                        tResults.Add(res);
                    }

                }
            }

            return tResults;
        }
    }
}