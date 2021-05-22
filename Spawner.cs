using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTimer = 5f;
    public float maxDistance = 20f;
    public float numberOfBullets = 5;
    public float timeToJumpApex = 0.5f;

    float elapsedTime;
    [SerializeField] GameObject bulletPrefab;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnTimer)
        {
            elapsedTime = 0;

            for (int i = 0; i < numberOfBullets; i++)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + new Vector3(0,2,0), Quaternion.identity) as GameObject;
                bulletInstance.transform.parent = this.transform;
                bulletInstance.transform.Rotate(new Vector3(0, 0, i* 30));

                bulletInstance.GetComponent<Javelin>().maxDistance = maxDistance;
                bulletInstance.GetComponent<Javelin>().timeToJumpApex = timeToJumpApex;
            }
        }
    }
}
