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
        private List<Rabbit> rabbits;
        private List<Fox> foxes;
        private bool doingSetup = true;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            rabbits = new List<Rabbit>();
            foxes = new List<Fox>();
            boardManager = GetComponent<BoardManager>();

            InitGameOfLife();
        }

        void InitGameOfLife()
        {
            Invoke("StartGameAfterSettingUp", gameStartDelay);

            rabbits.Clear();
            foxes.Clear();

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

            StartCoroutine(MoveRabbits());
            StartCoroutine(MoveFoxes());
        }

        public void AddRabbitToList(Rabbit rabbit)
        {
            rabbits.Add(rabbit);
        }

        public void AddFoxToList(Fox fox)
        {
            foxes.Add(fox);
        }

        public void GameOver()
        {
            enabled = false;
        }

        IEnumerator MoveRabbits()
        {
            yield return new WaitForSeconds(turnDelay);

            if (rabbits.Count == 0)
                yield return new WaitForSeconds(turnDelay);

            for (int i = 0; i < rabbits.Count; i++)
            {
                rabbits[i].Move();
                yield return new WaitForSeconds(rabbits[i].moveTime);
            }
        }

        IEnumerator MoveFoxes()
        {
            yield return new WaitForSeconds(turnDelay);

            if (foxes.Count == 0)
                yield return new WaitForSeconds(turnDelay);

            for (int i = 0; i < foxes.Count; i++)
            {
                foxes[i].Move();
                yield return new WaitForSeconds(foxes[i].moveTime);
            }
        }
    }
}

