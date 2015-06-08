using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World.Block
{
    class BaseBlock : MonoBehaviour
    {
        public int X { get { return (int)transform.position.x; } }
        public int Y { get { return (int)transform.position.y; } }
        public int Z { get { return (int)transform.position.z; } }
        
        public long Index;

        private readonly List<GameObject> _neighbours = new List<GameObject>();

        private GameObject _player;

        public bool Surrounded { get { return _neighbours.Count() >= 26; } }

        //private BoxCollider _boxCollider;
        //private MeshRenderer _meshRenderer;

        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            //_boxCollider = GetComponent<BoxCollider>();
            //_meshRenderer = GetComponent<MeshRenderer>();
        }

        void Start()
        {
            World world = World.Instance();

            //for (int x = -1; x <= 1; x++)
            //{
            //    for (int y = -1; y <= 1; y++)
            //    {
            //        for (int z = -1; z <= 1; z++)
            //        {
            //            if (x == 0 && y == 0 && z == 0)
            //                continue;

            //            GameObject block = world.GetBlock(X + x, Y + y, Z + z);

            //            if (block != null)
            //            {
            //                block.GetComponent<BaseBlock>().AddNeighbour(gameObject);

            //                AddNeighbour(block);
            //            }
            //        }
            //    }
            //}
        }

        public void AddNeighbour(GameObject block)
        {
            if (!_neighbours.Contains(block))
            {
                _neighbours.Add(block);
            }
        }
    }
}
