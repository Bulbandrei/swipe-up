using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObj : MonoBehaviour {

    #region Vars
    int points = 1;
    float pointTimer = 2.0f;
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
                SelfDestroy(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAlive)
        {
            if (other.tag == "PointZone")
            {
                pointTimer -= Time.deltaTime;
                if (pointTimer <= 0.0f)
                    SelfDestroy(true);
            }
        }
    }

    void SelfDestroy(bool gotPoints)
    {
        isAlive = false;
        Destroy(transform.Find("Colliders").gameObject);
        Spawner.Instance.ObjDestroyed(gotPoints ? Points : 0);
        Destroy(gameObject);
    }
}
