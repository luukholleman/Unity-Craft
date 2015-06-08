using System;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Generator
{
    class Generator : MonoBehaviour
    {
        public static Generator Instance;

        private int _seed;

        private GameObject _player;

        public int Radius;

        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }
        
        private Dictionary<Vector2, bool> _done = new Dictionary<Vector2, bool>(); 

        void Awake()
        {
            Instance = this;

            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            int playerChunkX = (int) _player.transform.position.x/World.World.ChunkSize;
            int playerChunkY = (int) _player.transform.position.z/World.World.ChunkSize;

            //Debug.Log(string.Format("Player chunk {0}, {1}", playerChunkX, playerChunkY));

            for (int x = -Radius; x <= Radius; x++)
            {
                for (int y = -Radius; y <= Radius; y++)
                {
                    Vector2 location = new Vector2(playerChunkX + x, playerChunkY + y);

                    GenerateChunk(location);
                }
            }
        }
        
        public void GenerateChunk(Vector2 location)
        {
            int oldSeed = Random.seed;
            Random.seed = Seed;

            if (!_done.ContainsKey(location))
            {
                //Debug.Log(string.Format("Generate chunk {0}, {1}", location.x, location.y));

                World.World.Instance().GenerateChunk(location);

                _done[location] = true;
            }

            Random.seed = oldSeed;
        }
    }
}
