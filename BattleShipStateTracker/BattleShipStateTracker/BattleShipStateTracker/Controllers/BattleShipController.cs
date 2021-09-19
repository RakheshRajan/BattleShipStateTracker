using BattleShipStateTracker.Helpers;
using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BattleShipStateTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleShipController : ControllerBase
    {
        private readonly ILogger<BattleShipController> _logger;
        public IPlayerProvider _playerProvider { get; }

        public BattleShipController(ILogger<BattleShipController> logger, IPlayerProvider playerProvider)
        {
            _logger = logger;
            _playerProvider = playerProvider;
        }
        /// <summary>
        /// Fire api - Players can use this api to fire at enemy ships at any given cordinates within a 10x10 board.
        /// XCordinate values are from 0-9.
        /// YCordinate values are from 0-9.
        /// </summary>
        /// <param name="xCordinate">XCordinate of the Unit.</param>
        /// <param name="yCordinate">YCordinate of the Unit.</param>
        /// <param name="playerName">Default value is Player1</param>
        /// <returns>Success/Fail and the Return Message</returns>
        [HttpGet("{xCordinate}/{yCordinate}/{playerName}")]
        public IActionResult Fire(int xCordinate, int yCordinate, string playerName)
        {
            try
            {
                Player player = _playerProvider.GetPlayer(playerName);
                if (player == null)
                    return NotFound(Helper.CreateResponse(false, "Player not found"));                

                FireResult fireResult = _playerProvider.Fire(xCordinate, yCordinate, player);
                if (fireResult.Succeded)
                    return Ok(fireResult);

                return BadRequest(fireResult);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, Helper.CreateResponse(false, "Error while processing your request!!!"));
            }
            
        }
    }
}
