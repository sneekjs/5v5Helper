using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RollAssigner : MonoBehaviour
{
    public TeamMaker teamMaker;
    public Dictionary<Roles, Player> teamComp1 = new Dictionary<Roles, Player>();
    public Dictionary<Roles, Player> teamComp2 = new Dictionary<Roles, Player>();
    private List<Player> team1 = new List<Player>();
    private List<Player> team2 = new List<Player>();

    public Text rolesDisplay;

    private void ClearTeamComps()
    {
        teamComp1 = new Dictionary<Roles, Player>();
        teamComp1.Add(Roles.Top, null);
        teamComp1.Add(Roles.Jungle, null);
        teamComp1.Add(Roles.Mid, null);
        teamComp1.Add(Roles.Adc, null);
        teamComp1.Add(Roles.Support, null);
        teamComp2 = new Dictionary<Roles, Player>();
        teamComp2.Add(Roles.Top, null);
        teamComp2.Add(Roles.Jungle, null);
        teamComp2.Add(Roles.Mid, null);
        teamComp2.Add(Roles.Adc, null);
        teamComp2.Add(Roles.Support, null);
        for (int i = 0; i < team1.Count; i++)
        {
            team1[i].rollIsAssigned = false;
        }
        for (int i = 0; i < team2.Count; i++)
        {
            team2[i].rollIsAssigned = false;
        }
    }

    public void AssignRoles()
    {
        PrepareRolesAssignment();
        CheckFirstChoice();
        Autofill();
        DisplayRoles();
    }

    public void AssignRandomRoles()
    {
        PrepareRolesAssignment();
        for (int i = 0; i < team2.Count; i++)
        {
            teamComp1[(Roles)i] = team1[i];
        }
        for (int i = 0; i < team2.Count; i++)
        {
            teamComp2[(Roles)i] = team2[i];
        }
        DisplayRoles();
    }

    public void PrepareRolesAssignment()
    {
        ClearTeamComps();
        team1 = teamMaker.team1;
        team2 = teamMaker.team2;

        if (team1 == null || team2 == null)
        {
            rolesDisplay.text = "Roles not assigned propperly! Make new teams...";
            return;
        }
        ShuffleTeams();
    }

    public void ShuffleTeams()
    {
        System.Random rng = new System.Random();
        team1 = team1.Select(x => new { value = x, order = rng.Next() }).OrderBy(x => x.order).Select(x => x.value).ToList();
        team2 = team2.Select(x => new { value = x, order = rng.Next() }).OrderBy(x => x.order).Select(x => x.value).ToList();
    }

    public void CheckFirstChoice()
    {
        for (int x = 0; x < teamMaker.teamSize; x++)
        {
            for (int i = 0; i < team1.Count; i++)
            {
                if (team1[i].favoriteRolls.Count >= x + 1)
                {
                    if (teamComp1[team1[i].favoriteRolls[x]] == null && !team1[i].rollIsAssigned)
                    {
                        teamComp1[team1[i].favoriteRolls[x]] = team1[i];
                        team1[i].rollIsAssigned = true;
                    }
                }
            }
        }

        for (int x = 0; x < teamMaker.teamSize; x++)
        {
            for (int i = 0; i < team2.Count; i++)
            {
                if (team2[i].favoriteRolls.Count >= x + 1)
                {
                    if (teamComp2[team2[i].favoriteRolls[x]] == null && !team2[i].rollIsAssigned)
                    {
                        teamComp2[team2[i].favoriteRolls[x]] = team2[i];
                        team2[i].rollIsAssigned = true;
                    }
                }
            }
        }
    }

    public void Autofill()
    {
        for (int i = 0; i < team1.Count; i++)
        {
            if (!team1[i].rollIsAssigned)
            {
                for (int x = 0; x < teamComp1.Count; x++)
                {
                    foreach (KeyValuePair<Roles, Player> rol in teamComp1)
                    {
                        if (rol.Value == null && team1[i].rollIsAssigned == false)
                        {
                            teamComp1[rol.Key] = team1[i];
                            team1[i].rollIsAssigned = true;
                            break;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < team2.Count; i++)
        {
            if (!team2[i].rollIsAssigned)
            {
                for (int x = 0; x < teamComp2.Count; x++)
                {
                    foreach (KeyValuePair<Roles, Player> rol in teamComp2)
                    {
                        if (rol.Value == null && team2[i].rollIsAssigned == false)
                        {
                            teamComp2[rol.Key] = team2[i];
                            team2[i].rollIsAssigned = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void DisplayRoles()
    {
        string displayText = "Team 1 roles: \n";

        displayText += teamComp1[Roles.Top] != null ? "Top: " + teamComp1[Roles.Top].playerName + "\n" : "No Top... \n";
        displayText += teamComp1[Roles.Jungle] != null ? "Jungle: " + teamComp1[Roles.Jungle].playerName + "\n" : "No Jungle... \n";
        displayText += teamComp1[Roles.Mid] != null ? "Mid: " + teamComp1[Roles.Mid].playerName + "\n" : "No Mid... \n";
        displayText += teamComp1[Roles.Adc] != null ? "Adc: " + teamComp1[Roles.Adc].playerName + "\n" : "No Adc... \n";
        displayText += teamComp1[Roles.Support] != null ? "Support: " + teamComp1[Roles.Support].playerName + "\n" : "No Support... \n";
        displayText += "\n\nTeam 2 roles: \n";
        displayText += teamComp2[Roles.Top] != null ? "Top: " + teamComp2[Roles.Top].playerName + "\n" : "No Top... \n";
        displayText += teamComp2[Roles.Jungle] != null ? "Jungle: " + teamComp2[Roles.Jungle].playerName + "\n" : "No Jungle... \n";
        displayText += teamComp2[Roles.Mid] != null ? "Mid: " + teamComp2[Roles.Mid].playerName + "\n" : "No Mid... \n";
        displayText += teamComp2[Roles.Adc] != null ? "Adc: " + teamComp2[Roles.Adc].playerName + "\n" : "No Adc... \n";
        displayText += teamComp2[Roles.Support] != null ? "Support: " + teamComp2[Roles.Support].playerName + "\n" : "No Support... \n";

        //displayText += "Top: " + teamComp1[Roles.Top].playerName + "\n";
        //displayText += "Jungle: " + teamComp1[Roles.Jungle].playerName + "\n";
        //displayText += "Mid: " + teamComp1[Roles.Mid].playerName + "\n";
        //displayText += "Adc: " + teamComp1[Roles.Adc].playerName + "\n";
        //displayText += "Support: " + teamComp1[Roles.Support].playerName + "\n";
        //displayText += "\n\nTeam 2 roles: \n";
        //displayText += "Top: " + teamComp2[Roles.Top].playerName + "\n";
        //displayText += "Jungle: " + teamComp2[Roles.Jungle].playerName + "\n";
        //displayText += "Mid: " + teamComp2[Roles.Mid].playerName + "\n";
        //displayText += "Adc: " + teamComp2[Roles.Adc].playerName + "\n";
        //displayText += "Support: " + teamComp2[Roles.Support].playerName + "\n";

        rolesDisplay.text = displayText;
    }
}
