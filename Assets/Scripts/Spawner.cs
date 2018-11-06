using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    #region Constants
    const int SPAWN_AMOUNT = 5;
    #endregion

    public static Spawner Instance;

    [SerializeField]
    GameObject RingPrefab;

    [SerializeField]
    GameObject RingDestroyParticle;

    #region Vars
    int objsOnScreen = 0;
    int objsOnPointZone = 0;
    List<GameObject> spawned = new List<GameObject>();
    #endregion

    #region Properties
    public int ObjsOnScreen
    {
        get
        {
            return objsOnScreen;
        }
        set
        {
            objsOnScreen = value;
        }
    }

    public int ObjsOnPointZone
    {
        get
        {
            return objsOnPointZone;
        }

        set
        {
            objsOnPointZone = value;
        }
    }
    #endregion

    #region Initialization
    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        SpawnObjs();
	}

    void OnDisable()
    {
        DestroyObjs();
    }
    #endregion
	
	void SpawnObjs()
    {
        GravityController.NoGravity();

        float xStartPos = Random.Range(-4.0f, 4.0f);
        for (int i = 0; i < SPAWN_AMOUNT; i++)
        {
            // Here I spawn a ring with a Z distance from each other so they won't collide at game start
            var ring = Instantiate(RingPrefab, new Vector3(xStartPos + (.25f*i), transform.position.y, transform.position.z - (1*i)), Quaternion.Euler(Random.Range(0,360.0f),0,0));
            ring.transform.parent = transform;
            spawned.Add(ring);
        }
        ObjsOnScreen = spawned.Count;
        ObjsOnPointZone = 0;
    }

    void DestroyObjs()
    {
        foreach(var ring in spawned)
        {
            if(ring != null)
            {
                Destroy(ring);
            }
        }
        spawned.Clear();
    }

    #region Events
    public void ObjDestroyed(GameObject gObject)
    {
        ObjsOnScreen--;
        Instantiate(RingDestroyParticle, gObject.transform.position, Quaternion.identity);
        Destroy(gObject);
    }

    public void ObjOnDeadZone(GameObject gObject)
    {
        ObjDestroyed(gObject);
        CheckRoundEnd();
    }
    public void ObjOnPointZone(bool gotIn, int points)
    {
        objsOnPointZone = gotIn ? objsOnPointZone + 1 : objsOnPointZone - 1;
        GameManager.Instance.CurrentRoundScore = gotIn ? GameManager.Instance.CurrentRoundScore + points : GameManager.Instance.CurrentRoundScore - points;
        CheckRoundEnd();
    }
    #endregion

    void CheckRoundEnd()
    {
        if (ObjsOnScreen == ObjsOnPointZone)
        {
            GameManager.Instance.CurrentScore += GameManager.Instance.CurrentRoundScore;
            foreach (var item in spawned)
            {
                if (item != null)
                    ObjDestroyed(item);
            }
            spawned.Clear();
            SpawnObjs();
        }
    }
}
