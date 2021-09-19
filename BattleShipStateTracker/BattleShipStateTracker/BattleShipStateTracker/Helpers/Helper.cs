using BattleShipStateTracker.Models;

namespace BattleShipStateTracker.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FireResult CreateResponse(bool isSuccess, string message)
        {
            return new FireResult()
            {
                Succeded = isSuccess,
                Message = message
            };
        }       

        /// <summary>
        /// This method is responsible for checking if there is space in a row for allocating the ship
        /// </summary>
        /// <param name="cordinate">Starting Xcordinate</param>
        /// <param name="ShipSize">Size of the ship</param>
        /// <returns></returns>
        public static bool IsSpaceAvailableInRow(int cordinate, int ShipSize)
        {
            if (cordinate + (ShipSize - 1) <= 10)
                return true;
            return false;
        }
    }
}
