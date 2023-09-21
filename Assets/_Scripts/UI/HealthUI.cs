using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private List <LifeElementUI> healthElements;
    [SerializeField]
    private Sprite fullHealth, emptyHealth;
    [SerializeField]
    private LifeElementUI healthPrefab;

    public void Initialize(int maxHealth)
    {
        healthElements = new List <LifeElementUI>();
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            LifeElementUI life = Instantiate(healthPrefab);
            life.transform.SetParent(transform,false);
            healthElements.Add(life);
        }
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0;i < healthElements.Count;i++)
        {
            healthElements[i].SetSprite(i < currentHealth ? fullHealth : emptyHealth);
        }
    }
}
