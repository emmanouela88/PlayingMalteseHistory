using UnityEngine;

[CreateAssetMenu(fileName = "New Point", menuName = "POI")]
public class PointsToVisit : ScriptableObject
{
    [Tooltip("Name of the POI")]
    public string nameOfPoint;

    [Tooltip("Coordinates to place the POI")]
    public Vector2 coordinatesPoint;

    [Tooltip("1/2 Info of POI")]
    [TextArea(10, 20)]
    public string firstPartInfo;

    [Tooltip("2/2 Info of POI")]
    [TextArea(10, 20)]
    public string secondPartInfo;

    [Tooltip("Image for Marker of POI")]
    public Sprite imgMarker;

    [Tooltip("Image for POI")]
    public Sprite imgMain;

    [Tooltip("In which story is the POI assigned")]
    public string nameOfStory;

    [Tooltip("Hint of POI")]
    public string hintOfPOI;

    [Tooltip("Solution of POI")]
    public string solutionOfPOI;
}
