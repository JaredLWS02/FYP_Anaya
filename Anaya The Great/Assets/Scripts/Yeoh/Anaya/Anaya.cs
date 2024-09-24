using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Anaya,
    Wolf,
}

public enum Pilot
{
    None,
    Player,
    AI,
}

public class Anaya : MonoBehaviour
{
    public Pilot pilot = Pilot.Player;
}
