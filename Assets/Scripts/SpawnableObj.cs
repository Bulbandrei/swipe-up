using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObj : MonoBehaviour {

    #region Vars
    int points = 0;
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
        if (other.tag == "DeadZone")
        {
            SelfDestroy();
        }
    }

    void SelfDestroy()
    {
        Spawner.Instance.ObjDestroyed(Points);
        Destroy(gameObject);
    }
}
