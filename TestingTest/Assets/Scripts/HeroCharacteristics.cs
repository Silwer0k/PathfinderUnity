using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeroCharacteristics : NetworkBehaviour {

    [SyncVar]
    public int agility;
    [SyncVar]
    public int power;
    [SyncVar]
    public int inteintelligence;

    [SyncVar]
    private double damage;
    [SyncVar]
    private int health;
    [SyncVar]
    private float evasion;

    public void SetHealth(int new_health)
    {
        health = new_health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetDamage(double new_damage)
    {
        damage = new_damage;
    }

    public double GetDamage()
    {
        return damage;
    }

    public void SetEvasion(float new_evasion)
    {
        evasion = new_evasion;
    }

    public float GetEvasion()
    {
        return evasion;
    }

    public double CalcDamage(int i)
    {
        return 20 * i * 0.25;
    }

    public int CalcHealth(int p)
    {
        int h;
        if (p <= 5)
            h = p * 20;
        else
            h = p * 15;

        return h;
    }

    public float CalcEvasion(int a)
    {
        switch (a)
        {
            case 1:
                return 0;
            case 2:
                return 0.1f;
            case 3:
                return 0.1f;
            case 4:
                return 0.3f;
            case 5:
                return 0.3f;
            case 6:
                return 0.45f;
            case 7:
                return 0.5f;
            case 8:
                return 0.6f;
            case 9:
                return 0.7f;
            case 10:
                return 0.75f;
            default:
                return 0.8f;
        }
    }

    public void SetCharacteristics(int p, int a, int i, int h, double d, float e)
    {
        power = p;
        agility = a;
        inteintelligence = i;
        health = h;
        damage = d;
        evasion = e;
    }
}
