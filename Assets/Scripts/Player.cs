using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public List<Roles> favoriteRolls;

    [Range(1, 5)]
    public float skillLevel;

    public bool autofillProtected = false;

    public bool rollIsAssigned = false;
}


// 1    jarno	    4.64
// 2    jorrit	    4.55
// 3    jip	        4.42
// 4    olaf	    4.25
// 5    lars	    3.83
// 6    stef	    3.58
// 7    sven	    3.42
// 8    nico	    3.17
// 9    marnix	    3.08
// 10   ricky	    2.67
// 11   max	        2.42
// 12   martijn	    1.67