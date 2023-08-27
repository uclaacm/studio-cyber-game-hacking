using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Draws the envelopes onto the screen to keep track of player lives. 
 */

public class HealthSystem : MonoBehaviour
{
    //Change hearts image when gameObject (enemy) exits trigger border
    [SerializeField] private GameObject LiveHeartPrefab;
    [SerializeField] private GameObject DeadHeartPrefab;

    public void DrawHearts(int hearts, int maxHearts)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHearts; i++)
        {
            if (i + 1 <= hearts)
            {
                GameObject heart = Instantiate(LiveHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            } else {
                GameObject heart = Instantiate(DeadHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
        }
    }
}
