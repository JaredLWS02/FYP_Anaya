using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSwitch : MonoBehaviour
{
    [SerializeField] private MnKMovement p1;
    [SerializeField] private MnKMovement p2;
    public bool p1Active = true;

    // Start is called before the first frame update
    void Start()
    {
        p2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switchPlayer();
        }
    }

    public void switchPlayer()
    {
        if (p1Active)
        {
            p2.enabled = true;
            p1.enabled = false;
            p1Active = false;
        }
        else
        {
            p1.enabled = true;
            p2.enabled = false;
            p1Active = true;
        }
    }
}
