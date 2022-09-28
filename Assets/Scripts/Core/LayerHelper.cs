using System;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
namespace Core
{
    public static class LayerHelper
    {
        private static readonly Dictionary<SteamId, LayerMask> PlayerLayerMasks = new();
        
        /// <summary>
        /// Returns player layer mask
        /// </summary>
        /// <param name="playerId">player steam id</param>
        /// <returns>player layer mask if </returns>
        public static LayerMask GetPlayerMask(SteamId playerId) 
            => PlayerLayerMasks.ContainsKey(playerId) 
                ? PlayerLayerMasks[playerId] 
                : -1;

        public static void AddPlayer(SteamId playerId)
        {
            if (PlayerLayerMasks.ContainsKey(playerId))
            {
                throw new ArgumentException("Same user already exists", nameof(playerId));
            }
            PlayerLayerMasks.Add(playerId, new LayerMask());
        }
        
        public static void RemovePlayer(SteamId playerId)
        {
            if (PlayerLayerMasks.ContainsKey(playerId))
                PlayerLayerMasks.Remove(playerId);
        }
    }
}