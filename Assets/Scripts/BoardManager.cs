using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GameOfLife
{
    public class BoardManager : MonoBehaviour
    {
        [Serializable]
        public class MinMaxBound
        {
            public int min;
            public int max;

            public MinMaxBound(int min, int max)
            {
                this.min = min;
                this.max = max;
            }
        }

        const float scaleRate = 20f;

        public int columns = 20;
        public int rows = 20;
        public MinMaxBound rabbitsCount = new MinMaxBound(40, 60);
        public MinMaxBound foxesCount = new MinMaxBound(7, 12);
        public GameObject[] floorModels;
        public GameObject[] rabbitModels;
        public GameObject[] foxModels;

        private Transform boardPosition;
        private List<Vector3> gridPositions = new List<Vector3>();
        private float floorSize;

        public void SetupScene()
        {
            BoardSetup();
            InitialiseList();

            GenerateActorAtRandomPosition(foxModels, foxesCount.min, foxesCount.max);
            GenerateActorAtRandomPosition(rabbitModels, rabbitsCount.min, rabbitsCount.max);
        }

        void BoardSetup()
        {
            boardPosition = new GameObject("Board").transform;

            GameObject floorPrefab = floorModels[Random.Range(0, floorModels.Length)];
            floorSize = floorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;

            GameObject floor = Instantiate(floorPrefab, new Vector3(floorSize, 0f, 0f), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardPosition);
        }

        void InitialiseList()
        {
            gridPositions.Clear();

            for (int x = 1; x < columns; x++)
            {
                for (int z = 1; z < rows; z++)
                {
                    float positionX = x * floorSize / columns;
                    float positionZ = z * floorSize / rows;
                    gridPositions.Add(new Vector3(positionX, 0f, positionZ));
                }
            }
        }

        void GenerateActorAtRandomPosition(GameObject[] modelsArray, int minimum, int maximum)
        {
            int actorsCount = Math.Min(Random.Range(minimum, maximum + 1), gridPositions.Count);
            for (int i = 0; i < actorsCount; i++)
            {
                Vector3 position = RandomPosition();
                Quaternion angle = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                GameObject actor = Instantiate(modelsArray[Random.Range(0, modelsArray.Length)], position, angle);

                // Adapt the size of the actor
                actor.transform.localScale *= scaleRate / Math.Max(columns, rows);
            }
        }

        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);

            return randomPosition;
        }
    }
}
