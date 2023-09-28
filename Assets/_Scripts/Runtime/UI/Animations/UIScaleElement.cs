using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIScaleElement : MonoBehaviour
    {
        private Sequence sequence;

        [SerializeField]
        private RectTransform element;
        [SerializeField]
        private float animationEndScale;
        [SerializeField]
        private float animationTime;
        [SerializeField]
        private bool playConstantly = false;

        private float baseScaleValue;
        private Vector3 baseScale, endScale;

        private void Start()
        {
            baseScale = element.localScale;
            endScale = Vector3.one * animationEndScale;

            if (playConstantly)
            {
                sequence = DOTween.Sequence().Append(element.DOScale(baseScale, animationTime))
                    .Append(element.DOScale(endScale, animationTime));

                sequence.SetLoops(-1, LoopType.Yoyo);
                sequence.Play();
            }



        }
    }
}

