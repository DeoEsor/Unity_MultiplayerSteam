using System;
using System.Collections;
using Networking.Steam;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerInfoManager : MonoBehaviour
    {
        private string _nickname;

        private void Start()
        {
            StartCoroutine(SetNicknameCoroutine());
        }

        private IEnumerator SetNicknameCoroutine()
        {
            var isNicknameSet = false;
            while (true)
            {
                try
                {
                    _nickname = PlayerManager.Instance.GetPlayer(GetComponentInParent<PlayerSteamID>().SteamID).Name;
                    transform.Find("Nickname").GetComponent<TextMeshPro>().text = _nickname;
                    isNicknameSet = true;
                }
                catch (Exception e)
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