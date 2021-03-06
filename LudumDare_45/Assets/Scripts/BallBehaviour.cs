﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    Rigidbody2D rb;
    scoring scoring;
    PlayerStats pS;
    float normalG ;
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic=true;
        normalG = rb.gravityScale;
    }
    void Start()
    {
       
        
        pS = GameObject.FindObjectOfType<PlayerStats>();
        transform.position = pS.GetStartPosition();
        scoring = GameObject.FindObjectOfType<scoring>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver();
    }
    public void startGame(){
        if(rb.isKinematic){
            rb.isKinematic=false;
            rb.velocity = Vector2.right*pS.GetInitVel();
        }
        
    }

    public void AddSpeed(float speed)
    {
        rb.AddForce((rb.velocity.magnitude == 0 ? Vector2.right : rb.velocity.normalized) *speed,ForceMode2D.Impulse);
    }
    public void SetDirection(Vector2 direction){
        
        rb.velocity = direction.normalized*rb.velocity.magnitude;
    }

    public void AddForceImpulse(Vector2 force)
    {
        rb.AddForce(force,ForceMode2D.Impulse);
    }

      public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    private void isGameOver(){
        if(transform.position.y < 0){
            scoring.Death();
        }
    }

    public void NoGravity(float time){
        rb.gravityScale = 0;
        StartCoroutine(ResetGravity(time));
        Debug.Log("no gravity");
    }

    IEnumerator ResetGravity(float time){
        yield return new WaitForSeconds(time);
        rb.gravityScale=normalG;
        Debug.Log("back on earth");
    }

}
