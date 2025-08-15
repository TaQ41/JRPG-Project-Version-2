using System;
using EntityData;
using UnityEngine;
using WorldMapData;

namespace MapNavigationSystem
{
    public class MapNavigator : MonoBehaviour
    {
        [SerializeField]
        private ActiveProjectFile activeProjectFile;

        public int RemMoveCount;
        private Vector3Int currPos;
        private WorldMap worldMap;

        // TryMoveToTile Return codes (Numbers are nonsensical, but unique)
        public const int NO_ISSUES = 0;
        public const int TILE_DOES_NOT_EXIST = 14;
        public const int INSUFFICIENT_MOVECOUNT = 25;
        public const int CUSTOM_CONDITION_FAIL = 91;
        public const int OUT_OF_RANGE = 142;

        public void ExtractPlayerInfo()
        {
            Player currPlayer = activeProjectFile.Data.PlayerData.GetCurrentPlayer();
            currPos = currPlayer.WorldTileCoords;

            worldMap = activeProjectFile.Data.WorldMapData.SearchForMap(currPlayer.livingMapName);
        }

        /// <summary>
        /// Fetches the current entity from the container and the map set in the container.
        /// Note: Without the container DS, this will take a raw Entity and worldMap.
        /// </summary>
        public void ExtractEntityInfo(Entity entity, WorldMap worldMap2)
        {
            currPos = entity.BattleTileCoords;
            worldMap = worldMap2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <returns>
        /// Return codes that indicate what happened, so the game reacts as it should
        /// These return codes are constant and set in this class.
        /// </returns>
        public int TryMoveToTile(Vector3Int destCoords, out Tile destTile)
        {
            destTile = default;
            Debug.Log("currPos: " + currPos);
            Tile currTile = worldMap[currPos.x, currPos.z];

            if (RemMoveCount < currTile.MoveDecrementAmount)
            {
                return INSUFFICIENT_MOVECOUNT;
            }

            Vector3Int destPos = currPos + destCoords;
            Debug.Log("destPos: " + destPos);
            if (!worldMap.TryGetTile(destPos.x, destPos.z, out Tile destTile2))
            {
                return TILE_DOES_NOT_EXIST;
            }

            if (DidAllCustomConditionsPass(destTile2) == false)
            {
                return CUSTOM_CONDITION_FAIL;
            }

            if (IsEntityInRangeOfTile(destTile2) == false)
            {
                return OUT_OF_RANGE;
            }

              // TODO: Fix the map access issue. This appears to be reading incorrect data from somewhere that should have tiles or believes tiles are in
              // locations in which they aren't. Likely, a tile access method fault or a storage fault. |Hidden error| Replicate: Move around with a single player
              // that only calls the a returns the TryMoveToTile method on the map 'Tutorial'. This is setup as of Aug/13/25 at about 11:52pm


            destTile = destTile2;
            return NO_ISSUES;
        }

        private bool DidAllCustomConditionsPass(Tile destTile)
        {
            return true;
        }

        private bool IsEntityInRangeOfTile(Tile tile, int range = 1, int maxYDiff = 1)
        {
            int gridDistDiff = GetDiffOfTwoInts(currPos.x, (int)tile.MapCoords.x) + GetDiffOfTwoInts(currPos.z, (int)tile.MapCoords.z);
            int yDiff = GetDiffOfTwoInts(currPos.y, (int)tile.MapCoords.y);

            if (gridDistDiff > range)
            {
                return false;
            }

            if (yDiff > maxYDiff)
            {
                return false;
            }

            return true;
        }

        private int GetDiffOfTwoInts(int a, int b)
        {
            return Math.Abs(b - a);
        }
    }
}