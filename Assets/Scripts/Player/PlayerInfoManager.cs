using System;
using System.Collections;
using JetBrains.Annotations;
using Networking.Steam;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(TextMeshPro))]
    public class PlayerInfoManager : MonoBehaviour
    {
        private string _nickname;

        [Inject]
        private PlayerManager _playerManager;

        private TextMeshPro _nickMesh;

        private PlayerSteamID _playerSteamID;

        private void Start()
        {
            _nickMesh = transform.Find("Nickname").GetComponent<TextMeshPro>();
            _playerSteamID = GetComponentInParent<PlayerSteamID>();
             
            StartCoroutine(SetNicknameCoroutine());
        }

        private IEnumerator SetNicknameCoroutine()
        {
            while (true)
            {
                bool isNicknameSet;
                try
                {
                    _nickname = _playerManager.GetPlayer(_playerSteamID.SteamID).Name;
                    _nickMesh.text = _nickname;
                    isNicknameSet = true;
                }
                catch (Exception)
                {
                    // ignored
                    
                    isNicknameSet = false;
                }

                if (isNicknameSet)
                    yield break;
                
                yield return null;
            }
        }
    }
}