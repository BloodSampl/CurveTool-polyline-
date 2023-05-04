using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField]
    List<Vector2> points;

    public Path(Vector2 center) // constructor for the class
    {
        points = new List<Vector2>   // here we initialize the ponts list to a set of four points around the center making the first cubic curve.
        {
            center + Vector2.left,
            center + (Vector2.left + Vector2.up) * 0.5f,
            center + (Vector2.right + Vector2.down) * 0.5f,
            center + Vector2.right
        };
    }

    public Vector2 this[int i]
    { get { return points[i]; } }
    public int NumSegments
    {
        get { return (points.Count - 4) / 3 + 1; }
    }

                                             
    public int NumPoints
    { get { return points.Count; } }
    public void AddSegment(Vector2 anchorPos)  // adding new anchor point here we adding 2 controll points
                                           // the first one needs to form a straight line with the previous anchor point
                                           // and its controll point and the second controll point where ever we want
                                           // but it will be half way between the last anchor point and the previous control point
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);
    }

    public Vector2[] GetPointsInSegments(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }
    public void MovePoint(int i , Vector2 pos)
    {
        Vector2 deltaMove = pos - points[i];
        points[i] = pos;

        if(i % 3 == 0)
        {
            if(i + 1 < points.Count)
            {
                points[i + 1] += deltaMove;
            }
            if(i - 1 >= 0)
            {
                points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControllIndex;
            if(nextPointIsAnchor)
            {
                correspondingControllIndex = i + 2;
            }
            else
            {
                correspondingControllIndex = i - 2;
            }
            int ancorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;
            if(correspondingControllIndex >= 0 && correspondingControllIndex < points.Count)
            {
                float dst = (points[ancorIndex] - points[correspondingControllIndex]).magnitude;
                Vector2 dir = (points[ancorIndex] - pos).normalized;
                points[correspondingControllIndex] = points[ancorIndex] + dir * dst;

            }
        }
    }
}
