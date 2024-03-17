using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 5f;
    [SerializeField] private GameObject GoalFx;
    private bool isDragging;
    private bool inHole;
    private void Update()
    {playerInput();}
    private bool isReady()
    {return rb.velocity.magnitude < 0.2f;}
    private void playerInput()
    {if (!isReady()) return;
       Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);
        if (Input.GetMouseButtonDown(0) && distance < 0.5f) dragStart();
        if (Input.GetMouseButton(0) && isDragging) dragChange(inputPos);
        if (Input.GetMouseButtonUp(0) && isDragging) dragRelease (inputPos);}
    private void dragStart()
    {isDragging = true;
    lr.positionCount = 2;}
    private void dragChange(Vector2 pos) 
    {Vector2 dir = (Vector2)transform.position - pos;
     lr.SetPosition(0,transform.position);
     lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude(dir * power / 2, maxPower / 2));}

    private void dragRelease(Vector2 pos) 
    {float distance = Vector2.Distance((Vector2)transform.position, pos);
     isDragging = false;
    lr.positionCount = 0;
     if (distance < 0.1f) 
        {return;}
    Vector2 dir = (Vector2)transform.position - pos;
    rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);}

    private Rigidbody2D rb2d;

    void Start()
    { rb2d = GetComponent<Rigidbody2D>();}
    
    public bool isFinish;
    public GameManager managerGame;
    public HoleDestroyer holeDestroyer2;
   
    private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.name == "MertHoles")
        { managerGame.updateScore();
            //holeDestroyer2.HoleDestroyer1();
        }
        if (other.tag == "Goal") CheckWinState();
    }
    private void CheckWinState()
    {
        if (inHole) return;
        
        if (rb.velocity.magnitude <= 20f)
        {
            inHole=true;
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            
                
            GameObject fx = Instantiate(GoalFx, transform.position, Quaternion.identity);
            Destroy(fx,1f) ;
            gameObject.transform.position = new Vector2(0, -3.741261f);
            gameObject.SetActive(true);
            
            StartCoroutine(twosecwait());

            
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Goal") CheckWinState();
    }

    
    IEnumerator twosecwait()
    {
        yield return new WaitForSecondsRealtime(2f);
        inHole = false;

    }

}
