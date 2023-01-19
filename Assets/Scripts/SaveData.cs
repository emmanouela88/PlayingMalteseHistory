using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public List<PointsToVisit> unlockedPointsPoverty;
    public List<PointsToVisit> unlockedPointsQuarantine;
    public List<PointsToVisit> unlockedPointsJewish;

	public string storyPart;
    public SaveData()
	{
        unlockedPointsPoverty = new List<PointsToVisit>();
        unlockedPointsQuarantine = new List<PointsToVisit>();
        unlockedPointsJewish = new List<PointsToVisit>();
	}

}
