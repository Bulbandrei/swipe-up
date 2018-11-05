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
                isAlive = false;
                Spawner.Instance.ObjOnDeadZone(gameObject);
            }
            else if (other.tag == "PointZone")
            {
                OnPointingZone(true);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isAlive)
    //    {
    //        if (other.tag == "PointZone")
    //        {
    //            pointTimer -= Time.deltaTime;
    //            if (pointTimer <= 0.0f)
    //                SelfDestroy(true);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (isAlive)
        {
            if (other.tag == "PointZone")
            {
                OnPointingZone(false);
            }
        }
    }

    void OnPointingZone(bool gotIn)
    {
        Spawner.Instance.ObjOnPointZone(gotIn, points);
    }
}
