using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    GameManager manager;
    BallManager Ball;
    public List<GameObject> barriers = new List<GameObject>();
    [SerializeField] GameObject Barrier, BarrierGroup, barrierPlasma;
    int barrierCount;
    private int barrierPos;
    int lastLane, sameLaneCount;
    int doubleBarrierPercent, barrierPlasmaPercent, tripleBarrierPercent, singleBarrierPercent;
    public void SelectLane()
    {
        
        int lane = Random.Range(0, 3);
        if (lane == lastLane)
        {
            sameLaneCount++;
        }
        lastLane = lane;

        if (sameLaneCount >= 2)
        {
            lane = Random.Range(0, 3);
            while (lane == lastLane)
            {
                lane = Random.Range(0, 3);
            }
            sameLaneCount = 0;
        }

        switch (lane)
        {
            case 0:
                BarrierInstantiate(-3.5f, Barrier, "barrier");
                break;
            case 1:
                if (manager.score > 100)
                {
                    int x = Random.Range(0, 100);
                    if(x<100- barrierPlasmaPercent)
                    {
                        BarrierInstantiate(0, Barrier, "barrier");
                    }
                    else
                    {
                        BarrierInstantiate(0, barrierPlasma,"plasma");
                    }
                }
                BarrierInstantiate(0,Barrier, "barrier");
                break;
            case 2:
                BarrierInstantiate(3.5f, Barrier, "barrier");
                break;
        }
        
    }
    private void BarrierInstantiate(float vectorX, GameObject Asset,string name)
    {
        if (name == "plasma")
        {
            barriers.Add(Instantiate(Asset, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
            Barrier.name = "Plasma";
            barrierCount++;
            barrierPos = barrierPos + 50;
        }
        else
        {
            int x = Random.Range(0, 100);

            if (x< tripleBarrierPercent)
            {
                float a, b, c;
                a = vectorX;
                barriers.Add(Instantiate(Asset, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;
                switch (vectorX)
                {
                    case 0:
                        vectorX = Random.Range(0, 2) == 0 ? -3.5f : 3.5f;
                        break;
                    case -3.5f:
                        vectorX = Random.Range(0, 2) == 0 ? 0 : 3.5f;
                        break;
                    case 3.5f:
                        vectorX = Random.Range(0, 2) == 0 ? -3.5f : 0;
                        break;
                }
                b = vectorX;
                barriers.Add(Instantiate(Barrier, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;

                c = a + b;
                switch (c)
                {
                    case 0:
                        vectorX = 0;
                        break;
                    case -3.5f:
                        vectorX = 3.5f;
                        break;
                    case 3.5f:
                        vectorX = -3.5f;
                        break;
                }
                barriers.Add(Instantiate(Asset, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;
                barrierPos = barrierPos + 60;
            }

            else if (x < tripleBarrierPercent+doubleBarrierPercent)
            {
                Debug.Log("double");
                barriers.Add(Instantiate(Asset, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;
                switch (vectorX)
                {
                    case 0:
                        vectorX = Random.Range(0, 2) == 0 ? -3.5f : 3.5f;
                        break;
                    case -3.5f:
                        vectorX = Random.Range(0, 2) == 0 ? 0 : 3.5f;
                        break;
                    case 3.5f:
                        vectorX = Random.Range(0, 2) == 0 ? -3.5f : 0;
                        break;
                }
                barriers.Add(Instantiate(Barrier, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;
                barrierPos = barrierPos + 25;
        }
            else
            {
                Debug.Log("single");
                barriers.Add(Instantiate(Asset, new Vector3(vectorX, 0.75f, barrierPos), Quaternion.identity, BarrierGroup.transform));
                Barrier.name = "Barrier" + barrierCount;
                barrierCount++;
                barrierPos = barrierPos + 25;
            }


        }
    }
    void Start()
    {
        barrierPlasmaPercent = 20;
        doubleBarrierPercent = 5;
        tripleBarrierPercent = 5;
        singleBarrierPercent = doubleBarrierPercent + tripleBarrierPercent;

        lastLane = 0;
        sameLaneCount = 0;
        barrierCount = 0;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Ball = GetComponent<BallManager>();
        barrierPos = 75;
        for (int j=0; j < 10;j++)
        {
            SelectLane();
        }
        //StartCoroutine(BarrierCreator());
    }
    // Update is called once per frame
    void Update()
    {
        singleBarrierPercent = 100-doubleBarrierPercent + tripleBarrierPercent;

        if (Ball.transform.position.z > barriers[0].transform.position.z)
        {
            Destroy(barriers[0]);
            barriers.RemoveAt(0);
        }
        if (barriers.Count < 20)
        {
            for (int j = 0; j < 10; j++)
            {
                SelectLane();
            }
        }

        if (manager.score%200==0)
        {
            barrierPlasmaPercent += 5;

            if(doubleBarrierPercent<=40)
                doubleBarrierPercent += 10;

            if (tripleBarrierPercent <= 40)
                tripleBarrierPercent += 5;
        }

    }
}
