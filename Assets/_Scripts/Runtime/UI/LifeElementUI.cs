using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlayerUI
{
    public class LifeElementUI : MonoBehaviour
    {
        private Image image;
        public UnityEvent OnSpriteChange;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetSprite(Sprite sprite)
        {
            if (image.sprite != sprite)
            {
                OnSpriteChange?.Invoke();
                image.sprite = sprite;
            }
        }
    }
}