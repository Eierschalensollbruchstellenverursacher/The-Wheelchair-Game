using UnityEngine;

public sealed class LaserSpawn : MonoBehaviour
{
    public GameObject laserPrefab;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser =  Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

}
