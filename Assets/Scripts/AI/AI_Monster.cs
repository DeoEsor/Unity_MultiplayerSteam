using Infrastructure.Core;
using UnityEngine;
using Zenject;

namespace AI
{
    public partial class AIMonster : MonoBehaviour
    {
        private GameObject _player;
        
        [Inject]
        private IStateObject _monsterStateObject;
    }
}