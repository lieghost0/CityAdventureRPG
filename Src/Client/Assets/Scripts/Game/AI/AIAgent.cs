using System.Collections;
using System.Collections.Generic;
using AI;
using Entity;
using UnityEngine;

public class AIAgent
{
    public AIBase AI;

    public Enemy Owner;

    public AIAgent(Enemy owner)
    {
        this.Owner = owner;
        string aiName = "AINormalEnemy";
        switch (aiName)
        {
            case AINormalEnemy.ID:
                this.AI = new AINormalEnemy(owner);
                break;
        }
    }
}
