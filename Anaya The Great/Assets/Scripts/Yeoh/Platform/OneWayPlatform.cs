using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll=GetComponent<Collider2D>();
    }

    // Event Manager ============================================================================

    void OnEnable()
    {
        EventManager.Current.MoveYEvent += OnMoveY;
    }
    void OnDisable()
    {
        EventManager.Current.MoveYEvent -= OnMoveY;
    }

    // ============================================================================

    class Passenger
    {
        public GameObject gameObject;
        public Rigidbody2D rb;
        public Collider2D coll;
        public Coroutine timer;
    }

    List<Passenger> passengers = new();

    // ============================================================================

    void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.rigidbody;
        if(!rb) return;

        Passenger new_passenger = new();
        new_passenger.gameObject = rb.gameObject;
        new_passenger.rb = rb;
        new_passenger.coll = other.collider;

        passengers.Add(new_passenger);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Rigidbody2D rb = other.rigidbody;
        if(!rb) return;

        // reversed forloop
        for(int i=passengers.Count-1; i>=0; i--)
        {
            if(passengers[i].rb==rb)
            {
                passengers.RemoveAt(i);
            }
        }
    }

    // ============================================================================

    void OnMoveY(GameObject mover, float input_y)
    {
        if(input_y > -0.75f) return;

        foreach(var passenger in passengers)
        {
            if(mover == passenger.gameObject)
            {
                if(passenger.timer!=null) StopCoroutine(passenger.timer);
                passenger.timer = StartCoroutine(IgnoringColl(passenger.coll));
            }
        }
    }

    IEnumerator IgnoringColl(Collider2D targetColl)
    {
        if(targetColl)
        IgnoreColl(targetColl, true);
        
        yield return new WaitForSeconds(.5f);

        if(targetColl)
        IgnoreColl(targetColl, false);
    }

    void IgnoreColl(Collider2D targetColl, bool toggle)
    {
        Physics2D.IgnoreCollision(targetColl, coll, toggle);
    }
}
