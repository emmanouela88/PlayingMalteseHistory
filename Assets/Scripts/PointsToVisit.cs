using UnityEngine;

[CreateAssetMenu(fileName = "New Point", menuName = "POI")]
public class PointsToVisit : ScriptableObject
{
    [Tooltip("Name of the POI")]
    public string nameOfPoint;

    [Tooltip("Coordinates to place the POI")]
    public Vector2 coordinatesPoint;

    [Tooltip("Main Info of POI")]
    public string mainInfo;

    [Tooltip("Image for Marker of POI")]
    public Sprite imgMarker;

    [Tooltip("Image for POI")]
    public Sprite imgMain;
}
