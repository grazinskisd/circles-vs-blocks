﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CvB
{
    public delegate void FormulaLoadedEvent();

    public class FormulaController : MonoBehaviour
    {
        [SerializeField]
        private string _url;

        [Header("Debug")]
        [SerializeField]
        private bool _isDebuging;

        [SerializeField]
        private FormulaSetup _debugSetup;

        public event FormulaLoadedEvent OnLoaded;

        private FormulaSetup _setup;

        private void Start()
        {
            if (_isDebuging)
            {
                _setup = _debugSetup;
                StartCoroutine(IssueDelayedOnLoaded());
            }
            else
            {
                StartCoroutine(DownloadFormulaSetup(_url));
            }
        }

        public float GetGoldIncrement(int level)
        {
            return Mathf.Round(_setup.goldMultiplier * Mathf.Pow(level, _setup.goldPower));
        }

        public float GetUpgradeCost(int level)
        {
            return Mathf.Round(_setup.upgradeMultiplier * Mathf.Pow(_setup.upgradePowerBase, level));
        }

        IEnumerator DownloadFormulaSetup(string url)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                try
                {
                    _setup = JsonUtility.FromJson<FormulaSetup>(www.downloadHandler.text);
                    IssueOnLoaded();
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        IEnumerator IssueDelayedOnLoaded()
        {
            yield return null;
            IssueOnLoaded();
        }

        private void IssueOnLoaded()
        {
            OnLoaded?.Invoke();
        }
    }

    [System.Serializable]
    public class FormulaSetup
    {
        public float goldMultiplier;
        public float goldPower;

        public float upgradeMultiplier;
        public float upgradePowerBase;
    }
}