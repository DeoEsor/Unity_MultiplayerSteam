using UnityEngine;

namespace Infrastructure.Player
{
    public interface IGetPosition
    {
        Transform Position { get; set; }
    }
}