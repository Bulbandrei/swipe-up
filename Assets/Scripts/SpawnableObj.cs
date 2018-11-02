using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObj : MonoBehaviour {

    #region Vars
    int points = 0;
    bool isAlive = true;
    #endregion

    #region Properties
    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            if (other.tag == "DeadZone")
            {
                SelfDestroy();
            }
        }
    }

    void SelfDestroy()
    {
        isAlive = false;
        Destroy(transform.Find("Colliders").gameObject);
        Spawner.Instance.ObjDestroyed(Points);
        Destroy(gameObject);
    }
}
