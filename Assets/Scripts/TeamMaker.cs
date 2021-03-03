using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TeamMaker : MonoBehaviour
{
    public List<Player> participants;

    public List<Player> team1;
    public List<Player> team2;

    public int teamSize = 5;

    [Range(0, 100)]
    public float teamSkillDifference;

    public Text displayWindow;

    public void CreateTeams()
    {
        if (participants.Count < teamSize * 2)
        {
            return;
        }

        foreach (Player player in team1)
        {
            player.rollIsAssigned = false;
        }
        foreach (Player player in team2)
        {
            player.rollIsAssigned = false;
        }

        List<Player> shuffledList = ShuffleParticipants();
        team1 = new List<Player>();
        team2 = new List<Player>();

        for (int i = 0; i < shuffledList.Count; i++)
        {
            if (i % 2 == 0)
            {
                team1.Add(shuffledList[i]);
            }
            else
            {
                team2.Add(shuffledList[i]);
            }
        }
        if (!NoSkillGap())
        {
            Debug.Log("Unbalanced, trying again...");
            CreateTeams();
        }
        else
        {
            Debug.Log("Balanced teams created!");
            DisplayTeams();
        }
    }

    public List<Player> ShuffleParticipants()
    {
        System.Random rng = new System.Random();
        var result = participants.Select(x => new { value = x, order = rng.Next() }).OrderBy(x => x.order).Select(x => x.value).ToList();

        return result;
    }

    public bool NoSkillGap()
    {
        float team1skillScore = 0;
        float team2skillScore = 0;

        foreach (Player player in team1)
        {
            team1skillScore += player.skillLevel;
        }

        foreach (Player player in team2)
        {
            team2skillScore += player.skillLevel;
        }
        
        float skillgap = (team1skillScore / team2skillScore) * 100f;
        skillgap = 100 - skillgap;
        if (skillgap < 0)
        {
            skillgap *= -1f;
        }
        Debug.Log(skillgap);
        return skillgap < teamSkillDifference;
    }

    public void DisplayTeams()
    {
        string displayText = "Team 1 players: \n";
        foreach (Player player in team1)
        {
            displayText += player.playerName + "\n";
        }
        displayText += "\n\nTeam 2 players: \n";
        foreach (Player player in team2)
        {
            displayText += player.playerName + "\n";
        }

        displayWindow.text = displayText;
    }
}
