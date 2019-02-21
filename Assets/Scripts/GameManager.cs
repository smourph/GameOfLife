using UnityEngine;
using System.Collections;

namespace GameOfLife
{
    using System.Collections.Generic;

    public class GameManager : MonoBehaviour
    {
        public float gameStartDelay = 2f;
        public float turnDelay = 0.1f;
        public static GameManager instance = null;

        [HideInInspector] public bool foxesTurn = true;

        private BoardManager boardManager;
        private bool doingSetup = true;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            boardManager = GetComponent<BoardManager>();

            InitGameOfLife();
        }

        void InitGameOfLife()
        {
            Invoke("StartGameAfterSettingUp", gameStartDelay);

            boardManager.SetupScene();

        }

        void StartGameAfterSettingUp()
        {
            doingSetup = false;
        }

        void Update()
        {
            if (doingSetup)
                return;
        }

        public void GameOver()
        {
            enabled = false;
        }
    }
}

