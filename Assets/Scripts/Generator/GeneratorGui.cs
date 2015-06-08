using UnityEngine;

namespace Assets.Scripts.Generator
{
    public class GeneratorGui : MonoBehaviour {

        void Start()
        {
            Generator.Instance.Seed = 1234;
        }
    }
}
