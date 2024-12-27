using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPenguins : Team
{
    public override int Direction { get { return 1; } }

    public override void OnDeath()
    {
        StatTracker.IncrementUnitsLost();
    }
}
