using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Settings : MonoBehaviour
    {
        public static Settings Instance;

        public int ViewDistance;
        
        void Awake()
        {
            Instance = this;
        }
    }
}
