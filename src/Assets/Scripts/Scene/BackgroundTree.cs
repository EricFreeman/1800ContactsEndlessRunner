using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class BackgroundTree : MonoBehaviour
    {
        public List<Sprite> TreeSprites;
        private float _speed;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = TreeSprites.Random();

            var layer = Random.Range(0, 3);
            var scale = Random.Range(.9f, 1.5f);
            transform.localScale = new Vector3(scale, scale, 1);

            if (layer == 0)
            {
                _spriteRenderer.color = Constants.TreeClosest;
                _spriteRenderer.sortingOrder = (int)SpriteLayers.TreeForeground;
                _speed = 2f;
            }
            else if (layer == 1)
            {
                _spriteRenderer.color = Constants.TreeMiddle;
                _spriteRenderer.sortingOrder = (int)SpriteLayers.TreeBackground2;
                _speed = 1.25f;
            }
            else if (layer == 2)
            {
                _spriteRenderer.color = Constants.TreeFurthestAway;
                _spriteRenderer.sortingOrder = (int)SpriteLayers.TreeBackground1;
                _speed = .5f;
            }
        }

        void Update()
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);

            if (transform.position.x < -2)
            {
                Destroy(gameObject);
            }
        }
    }
}