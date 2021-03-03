using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    public TeamMaker teamMaker;
    public Player buttonOwner;

    public Image buttonColor;
    public Color availableColor;
    public Color activeColor;

    private bool isParticipating = false;

    public void ToggleParticipation()
    {
        if (isParticipating)
        {
            teamMaker.participants.Remove(buttonOwner);
            buttonColor.color = availableColor;
        }
        else
        {
            teamMaker.participants.Add(buttonOwner);
            buttonColor.color = activeColor;
        }
        isParticipating = !isParticipating;
    }

}
