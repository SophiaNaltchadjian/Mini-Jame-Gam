using System;

using UnityEngine;
namespace HorseGame
{
    public class SceneTransitionAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public bool isAnimationFinished = false;

        private float gameStartTimer = 0;

        internal void CloseFlowerAnimation()
        {
            _animator.SetTrigger("FlowerClosed");
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}