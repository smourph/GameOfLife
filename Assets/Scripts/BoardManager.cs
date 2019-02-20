using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GameOfLife
{
    public class BoardManager : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;
        public GameObject[] floorModels;

        private Transform boardPosition;
        private List<Vector3> gridPositions = new List<Vector3>();

        public void SetupScene()
        {
            BoardSetup();
        }

        void BoardSetup()
        {
            boardPosition = new GameObject("Board").transform;

            GameObject floorPrefab = floorModels[Random.Range(0, floorModels.Length)];
            Bounds floorPrefabBounds = floorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds;

            GameObject floor = Instantiate(floorPrefab, new Vector3(floorPrefabBounds.size.x, 0f, 0f), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardPosition);
        }
    }
}
