using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public List<PointsToVisit> unlockedPoints;

    public SaveData()
	{
		unlockedPoints = new List<PointsToVisit>();
	}

}
