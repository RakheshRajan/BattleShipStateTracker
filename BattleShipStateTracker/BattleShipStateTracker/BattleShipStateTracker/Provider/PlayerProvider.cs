using BattleShipStateTracker.Helpers;
using BattleShipStateTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;

namespace BattleShipStateTracker.Provider
{
    public class PlayerProvider : IPlayerProvider
    {
        public List<Player> Players = new();
        public IConfiguration Configuration { get; }
        public string Name = string.Empty;
        public Board Board { get; }

        public PlayerProvider(Board board, IConfiguration configuration)
        {
            Board = board;
            Configuration = configuration;
            //Initialize Primary player.
            //This is only since we are initializing on startup.
            Name = Configuration["GameSettings:DefaultPlayerName"].ToString();
            CreatePlayer(Name);
        }

        /// <summary>
        /// Responsible for creating a player object and adding it to the list.
        /// Inmemory implementation to be replaced later.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreatePlayer(string name)
        {
            if (Board == null || Board.Id == 0 || name == string.Empty)
                return false;

            try
            {
                if (GetPlayer(name) == null)
                {
                    Players.Add(new()
                    {
                        Id = 1,
                        board = Board,
                        Name = name
                    });
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Find the name of the player from the inmemory list.
        /// </summary>
        /// <param name="name">Name of the player to be searched</param>
        /// <returns></returns>
        public Player GetPlayer(string name)
        {
            return Players.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// The method responsible for hitting a cordinate based on user's input.
        /// </summary>
        /// <param name="xCordinate">XCordinate</param>
        /// <param name="yCordinate">YCordinate</param>
        /// <param name="player">Player Object</param>
        /// <returns></returns>
        public FireResult Fire(int xCordinate, int yCordinate, Player player)
        {
            Unit unit = null;
            if (player == null || player.board == null)
                return Helper.CreateResponse(false, "No board allocated for the player");

            if (!player.board.Units.Where(s => s.Ship.Id != 0).Any())
                return Helper.CreateResponse(false, "No ship allocated for the player");

            if (!player.board.Units.Where(s => s.IsActive).Any())
                return Helper.CreateResponse(false, "All ships hit already");

            if (!player.board.Units.Where(s => s.XCordinate == xCordinate && s.YCordinate == yCordinate).Any())
                return Helper.CreateResponse(false, "Cordinate does not exist");

            unit = player.board.Units.Where(s => s.XCordinate == xCordinate && s.YCordinate == yCordinate && s.Ship.Id != 0).FirstOrDefault();
            if (unit != null && !unit.IsActive)
                return Helper.CreateResponse(false, "This cordinate was already hit");

            unit = player.board.Units.Where(s => s.XCordinate == xCordinate && s.YCordinate == yCordinate).FirstOrDefault();
            if (unit != null && unit.IsActive && unit.Ship.Id != 0)
            {
                player.board.Units.Where(s => s.XCordinate == xCordinate && s.YCordinate == yCordinate).FirstOrDefault().IsActive = false;
                return Helper.CreateResponse(true, "Its a hit");
            }
            return Helper.CreateResponse(true, "It is a Miss");
        }
    }
}
