using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class HooverAnimation : MonoBehaviour
    {
        [SerializeField]
        private float moventDistance = 0.5f;

        [SerializeField]
        private float animationDuration = 1;

        [SerializeField]
        private Ease animationEase;

        private void Start()
        {
            transform.DOMoveY(transform.position.y + moventDistance, animationDuration)
                .SetEase(animationEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}