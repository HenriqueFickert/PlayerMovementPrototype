using System.Collections;
using UnityEngine;

namespace Feedback
{
    public class HittableTempImmortality : MonoBehaviour, IHittable
    {
        [SerializeField]
        private Collider2D[] colliders = new Collider2D[0];
        [SerializeField]
        private float immortalityTime = 1;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private float flashDelay = 0.1f;

        [SerializeField]
        [Range(0, 1)]
        private float flashAlpha = 0.5f;

        [Header("Debug Purpose")]
        [SerializeField]
        private bool isImmortal = false;

        private void Awake()
        {
            if (colliders.Length == 0)
                colliders = GetComponents<Collider2D>();
        }

        public void GetHit(GameObject gameObject, int weaponDamage)
        {
            if (!this.enabled)
                return;

            ToggleColliders(false);
            StartCoroutine(ResetColliders());
            StartCoroutine(Flash(flashAlpha));

        }

        private void ToggleColliders(bool value)
        {
            isImmortal = !value;
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = value;
            }
        }

        private IEnumerator ResetColliders()
        {
            yield return new WaitForSeconds(immortalityTime);
            StopAllCoroutines();
            ToggleColliders(true);
            ChangeSpriteRenderColorAlpha(1);
        }

        private IEnumerator Flash(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            ChangeSpriteRenderColorAlpha(alpha);
            yield return new WaitForSeconds(flashDelay);
            StartCoroutine(Flash(alpha < 1 ? 1 : flashAlpha));
        }

        private void ChangeSpriteRenderColorAlpha(float alpha)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }
    }
}

