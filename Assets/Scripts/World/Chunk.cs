using System;
using System.Collections.Generic;
using Assets.Scripts.World.Block;
using UnityEngine;

namespace Assets.Scripts.World
{
    class Chunk : MonoBehaviour
    {
        private readonly Dictionary<Vector3, GameObject> _blockObjects = new Dictionary<Vector3, GameObject>();

        private readonly List<Vector3> _blockLocations = new List<Vector3>();

        private bool _blocksGenerated;

        public Vector2 Location;

        private GameObject _player;

        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            for (int x = 0; x < World.ChunkSize; x++)
            {
                for (int y = 0; y < World.ChunkSize; y++)
                {
                    float absX = Location.x * World.ChunkSize + x;
                    float absY = Location.y * World.ChunkSize + y;

                    int smoothness = (int)Math.Round(Mathf.PerlinNoise(absX / (float)World.Smoothness, absY / (float)World.Smoothness) * 50);

                    int height = (int)Math.Round(Mathf.PerlinNoise((float)absX / (float)World.ChunkSize / smoothness, (float)absY / (float)World.ChunkSize / smoothness) * 50.0f + 100f);

                    _blockLocations.Add(new Vector3(absX, height, absY));
                }
            }
        }

        void Update()
        {
            Vector3 localPos = _player.transform.InverseTransformPoint(transform.position);

            if (Math.Max(Math.Abs(_player.transform.position.x - transform.position.x), Math.Abs(_player.transform.position.z - transform.position.z)) > World.ChunkSize * Settings.Instance.ViewDistance || localPos.z < 0)
            {
                DestroyBlocks();
            }
            else
            {
                GenerateBlocks();
            }
        }
        
        //void OnBecameVisible()
        //{
        //    Debug.Log(string.Format("Visisble {0}, {1}", Location.x, Location.y));
        //}

        //void OnBecameInvisible()
        //{
        //    Debug.Log(string.Format("Invisisble {0}, {1}", Location.x, Location.y));
        //}

        private void GenerateBlocks()
        {
            if (_blocksGenerated)
                return;

            GameObject block = Resources.Load<GameObject>("Prefabs/grass");

            foreach (Vector3 location in _blockLocations)
            {
                GameObject newBlock = Instantiate(block, location, new Quaternion()) as GameObject;

                newBlock.transform.parent = gameObject.transform;

                AddBlock(newBlock);

                for (int i = (int)newBlock.transform.position.y - 1; i >= (int)newBlock.transform.position.y - 3; i--)
                {
                    GameObject bottomBlock = Instantiate(block, new Vector3(newBlock.transform.position.x, i, newBlock.transform.position.z), new Quaternion()) as GameObject;

                    bottomBlock.transform.parent = gameObject.transform;

                    AddBlock(bottomBlock);
                }
            }

            _blocksGenerated = true;
        }

        public void DestroyBlocks()
        {
            if (_blocksGenerated)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                _blocksGenerated = false;

                _blockObjects.Clear();
            }
        }

        public void AddBlock(GameObject block)
        {
            BaseBlock baseBlock = block.GetComponent<BaseBlock>();

            baseBlock.Index = World.BlockIndex;

            Vector3 key = new Vector3(baseBlock.X, baseBlock.Y, baseBlock.Z);

            _blockObjects.Add(key, block);
        }

        public GameObject GetBlock(Vector3 key)
        {
            GameObject value;

            if (_blockObjects.TryGetValue(key, out value))
                return value;

            return null;
        }
    }
}
