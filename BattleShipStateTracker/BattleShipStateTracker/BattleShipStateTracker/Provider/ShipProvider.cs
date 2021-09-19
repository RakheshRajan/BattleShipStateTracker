using BattleShipStateTracker.Helpers;
using BattleShipStateTracker.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace BattleShipStateTracker.Provider
{
    public class ShipProvider : IShipProvider
    {
        public int ShipSize = 0;
        public readonly Direction direction = Direction.Horizontal;
        public int xMaxCordinate;
        public int yMaxCordinate;
        public IConfiguration Configuration { get; }

        public ShipProvider(IConfiguration configuration)
        {
            Configuration = configuration;
            ShipSize = int.Parse(Configuration["GameSettings:ShipSettings:ShipSize"].ToString());
            xMaxCordinate = int.Parse(Configuration["GameSettings:BoardSettings:xCordinate"].ToString());
            yMaxCordinate = int.Parse(Configuration["GameSettings:BoardSettings:yCordinate"].ToString());
        }        

        /// <summary>
        /// Allocates ships to the board.
        /// </summary>
        /// <param name="numberOfShips">Number of Ships required</param>
        /// <param name="board">The board object</param>
        /// <returns></returns>
        public Board CreateShips(int numberOfShips, Board board)
        {
            if (board == null || board.Units.Count == 0)
                return null;

            //Total units
            int MaxUnitCount = board.Units.Count;

            //Return empty if the number of ships requested is more than the total units in the board.
            if (numberOfShips >= MaxUnitCount)
                return board;

            //Return empty if the number of ships * ship size is more than the total units in the board.
            //For example, Shipsize is 10 and the number requested is 20. Then total units required will be 10*20=200.
            if (numberOfShips * ShipSize >= MaxUnitCount)
                return board;

            //Loop to create the required ships
            for (int i = 0; i <= numberOfShips; i++)
            {
                Ship ship = CreateShip(i + 1);
                board = AllocateShip(board, ship);
            }
            return board;
        }

        /// <summary>
        /// Allocates the ship to the board.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="ship"></param>
        /// <returns></returns>
        private Board AllocateShip(Board board, Ship ship)
        {
            //Loop through the board raw by raw to find the unallocated units               
            for (int y = 0; y <= yMaxCordinate; y++) //Y-Axis
            {
                for (int x = 0; x <= xMaxCordinate; x++) //X-Axis
                {
                    //unitIndex is the position of the unit from 0 to 100 in the list
                    int unitIndex = (y * 10) + x;
                    //Find the next unallocated unit.
                    if (board.Units[unitIndex].Ship.Id == 0)
                    {
                        //Check if the unit can be allocated based on the ship size and direction.
                        switch (direction)
                        {
                            case Direction.Horizontal:
                                int xCordinate = board.Units[unitIndex].XCordinate;
                                if (Helper.IsSpaceAvailableInRow(xCordinate, ShipSize))
                                {
                                    //Check if all the units in the series are unallocated
                                    if (!IsShipAllocated(board, unitIndex))
                                    {
                                        return AllocateShip(board, ship, unitIndex);
                                    }
                                }
                                break;
                            case Direction.Vertical:
                                //To be implemented
                                break;
                        }
                    }
                }
            }
            return board;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="ship"></param>
        /// <param name="unitIndex"></param>
        /// <returns></returns>
        private Board AllocateShip(Board board, Ship ship, int unitIndex)
        {
            int index = board.Units[unitIndex].XCordinate;
            int counter = 0;
            for (int k = index; k <= index + (ShipSize - 1); k++)
            {
                board.Units[unitIndex + counter].Ship = ship;
                counter++;
            }
            return board;
        }

        /// <summary>
        /// Check for any allocated units in the series.
        /// </summary>
        /// <param name="board">The Board object</param>
        /// <param name="unitIndex">Index of the unit in the list</param>
        /// <returns></returns>
        private bool IsShipAllocated(Board board, int unitIndex)
        {
            int index = board.Units[unitIndex].XCordinate;
            int counter = 0;
            for (int k = index + 1; k <= index + (ShipSize - 1); k++)
            {
                if (board.Units[unitIndex + counter].Ship.Id != 0)
                {
                    return true;
                }
                counter++;
            }
            return false;
        }

        /// <summary>
        /// Create ship object with the basic parameters
        /// </summary>
        /// <param name="j">Index</param>
        /// <returns>Ship Object</returns>
        private static Ship CreateShip(int j)
        {
            //Create the new ship object
            return new()
            {
                Id = j,
                Name = "Ship" + j
            };
        }
    }
}
