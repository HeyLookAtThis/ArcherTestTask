using UnityEngine;

public class TrajectoryPoint : SpriteObject
{
    public void SetScale(float distance)
    {
        float scale = 1;
        float newScale = scale - distance;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
