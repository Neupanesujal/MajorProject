using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovNShoot : MonoBehaviour
{
   public float minX = -18f; 
    public float maxX = 22f; 
    public float minZ = -4f; 
    public float maxZ = 12f; 
    public float moveSpeed = 12f; 

    void Start()
    {
        StartCoroutine(RandomXMoveCoroutine());
    }

    IEnumerator RandomXMoveCoroutine()
    {
        while (true)
        {
            // Generate random movement along the X-axis
            float randomX = Random.Range(-4f, 4f); 
            Vector3 newPosition = transform.localPosition + new Vector3(randomX, 0f, 0f) * moveSpeed * Time.deltaTime;

            // Clamp the new X position within the specified range
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            // Update the object's position
            transform.localPosition = newPosition;

            // Wait for a random interval before generating the next movement
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
    
}
