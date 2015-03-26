using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class BackgroundTree : MonoBehaviour
    {
        public List<Sprite> TreeSprites; 
        public float Speed = 3f;

        void Start()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = TreeSprites.Random();
        }

        void Update()
        {
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
    }
}