using BattleShipStateTracker.Models;
using Microsoft.Extensions.Configuration;

namespace BattleShipStateTracker.Provider
{
    /// <summary>
    /// Class responsible for creating the board.
    /// </summary>
    public class BoardProvider : IBoardProvider
    {
        public IUnitProvider UnitProvider { get; }

        public BoardProvider(IUnitProvider unitProvider)
        {
            UnitProvider = unitProvider;
        }

        /// <summary>
        /// This method creates the board, creats the units and assigns it to the board.
        /// </summary>
        /// <param name="id">An integer Id for the board.</param>
        /// <param name="boardName">Name for the board.</param>
        /// <returns></returns>
        public Board CreateBoard(int id, string boardName)
        {
            if (id == 0 || boardName == string.Empty)
                return null;

            return new Board()
            {
                Id = id,
                Name = boardName,
                Units = UnitProvider.CreateUnits()
            };
        }

    }
}
