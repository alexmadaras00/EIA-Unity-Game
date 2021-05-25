using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInfo
{
    public string player_name;


    public UserInfo(string u)
    {
        player_name = u;
    }
}

[System.Serializable]
public class UserInfoWithID
{
    public int player_id;


    public UserInfoWithID(int id)
    {
        player_id = id;
    }
}

[System.Serializable]
public class UserList
{
    public UserInfo[] players;
}

[System.Serializable]
public class NewPlayerInfo
{
    public string player_name;
    public string player_password;

    public NewPlayerInfo(string u, string p)
    {
        player_name = u;
        player_password = p;
    }
}

[System.Serializable]
public class LoginUser
{
    public string player_name;
    public string player_password;

    public LoginUser(string u, string p)
    {
        player_name = u;
        player_password = p;
    }
}

[System.Serializable]
public class SetCharacterGold
{
    public int player_gold;
    public int player_id;

    public SetCharacterGold(int g, int id)
    {
        player_gold = g;
        player_id = id;
    }
}


[System.Serializable]
public class AddCharacterWeapons
{
    public int player_id;
    public string weapon_name;
    public int weapon_next_level_upgrade_cost;

    public AddCharacterWeapons(int id, string wn, int uc)
    {
        player_id = id;
        weapon_name = wn;
        weapon_next_level_upgrade_cost = uc;
    }
}

[System.Serializable]
public class WeaponLevel
{
    public int player_id;
    public string weapon_name;

    public WeaponLevel(int id, string wn)
    {
        player_id = id;
        weapon_name = wn;
    }
}

[System.Serializable]
public class SetWeaponLevel
{
    public int player_id;
    public string weapon_name;
    public int weapon_level;

    public SetWeaponLevel(int id, string wn, int wl)
    {
        player_id = id;
        weapon_name = wn;
        weapon_level = wl;
    }
}

