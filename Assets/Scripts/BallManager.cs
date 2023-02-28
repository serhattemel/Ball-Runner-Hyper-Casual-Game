using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject Wall,Wall2, ball;
    public GameObject Road1, Road2,Road3;
    [SerializeField] public float playerSpeed;
    private Rigidbody rigid;
    public int planeNumber;
    public bool hitSomething = false;
    BallSpriteManager ballSpriteManager;
    Material m_Material;

    //public Renderer Object;


    public float gravityScale = 1f; //The gravity scale

    
    private void Start()
    {
        planeNumber = 2;
        rigid = gameObject.GetComponent<Rigidbody>();
        ballSpriteManager = GameObject.Find("BallSpriteManager").GetComponent<BallSpriteManager>();
        this.GetComponent<Renderer>().material = ballSpriteManager.ballMat;
        m_Material = ballSpriteManager.ballMat;


    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
        hitSomething = false;


    }
    void Update()
    {
        createRoad();

        Wall2.transform.position = new Vector3(0.1514f, 4.3324f, this.transform.position.z);
    }
    private void FixedUpdate()
    {
        rigid.AddForce(Vector3.forward * playerSpeed);
        //Add gravity to the rigidbody
        rigid.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }
    public void createRoad()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bck = transform.TransformDirection(Vector3.back);
        Vector3 up = transform.TransformDirection(Vector3.up);
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        
        Ray ray = new Ray(Wall2.transform.position, fwd);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.name == "Wall"&& hitSomething==false)
            {
                Debug.Log("Hit");
                hitSomething = true;
                StartCoroutine(Wait());
                SwtichPlane();
            }
        }
    }
    
    public void SwtichPlane()
    {
        Wall.transform.position = new Vector3(Wall.transform.position.x, Wall.transform.position.y, Wall.transform.position.z + 500);
        switch (planeNumber)
        {
            case 1:
                Road3.transform.position = new Vector3(Road3.transform.position.x, Road3.transform.position.y, Road3.transform.position.z + 1500);
                planeNumber = 2;
                break;
            case 2:
                Road1.transform.position = new Vector3(Road1.transform.position.x, Road1.transform.position.y, Road1.transform.position.z + 1500);
                planeNumber = 3;
                break;
            case 3:
                Road2.transform.position = new Vector3(Road2.transform.position.x, Road2.transform.position.y, Road2.transform.position.z + 1500);
                planeNumber = 1;
                break;
        }
        playerSpeed += 1;
    }

    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Block")
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public static implicit operator BallManager(BallSpriteManager v)
    {
        throw new NotImplementedException();
    }
}
