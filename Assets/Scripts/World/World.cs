using System;
using System.Collections.Generic;
using Assets.Scripts.World.Block;
using UnityEngine;

namespace Assets.Scripts.World
{
    class World
    {
        private static World _instance;

        public const int Height = 256;
        public const int ChunkSize = 16;
        public const int Smoothness = 50;

        private Dictionary<Vector2, GameObject> _chunks = new Dictionary<Vector2, GameObject>();

        private static long _blockIndex;

        public static long BlockIndex
        {
            get { return _blockIndex++; }
        }

        public static World Instance()
        {
            if(_instance == null)
                _instance = new World();

            return _instance;
        }

        public GameObject GetBlock(int x, int y, int z)
        {
            GameObject chunk;

            Vector2 key = new Vector2(x/ChunkSize, z/ChunkSize);

            if (_chunks.TryGetValue(key, out chunk))
            {
                return chunk.GetComponent<Chunk>().GetBlock(new Vector3(x, y, z));
            }

            return null;
        }

        public void GenerateChunk(Vector2 location)
        {
            GameObject chunk = Resources.Load<GameObject>("Prefabs/chunk");

            GameObject newChunk = GameObject.Instantiate(chunk, new Vector3(location.x * ChunkSize, 0, location.y * ChunkSize), new Quaternion()) as GameObject;

            newChunk.transform.parent = GameObject.Find("Chunks").transform;
            newChunk.name = string.Format("Chunk {0}, {1}", location.x, location.y);

            newChunk.GetComponent<Chunk>().Location = location;

            _chunks.Add(location, chunk);
        }
    }
}
