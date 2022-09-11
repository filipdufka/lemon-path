using UnityEngine;

namespace FruitBowl.Lemon
{
    public class PathSegment
    {
        public (int, int) indices { get; protected set; }
        public MeasuredPath path { get; protected set; }

        public PathSegment(int index1, int index2, MeasuredPath path)
        {
            indices = (index1, index2);
            this.path = path;

            (Vector3, Vector3) positions = pos;
            indicesOrderedByX = positions.Item2.x > positions.Item1.x ? (index1, index2) : (index2, index1);
            indicesOrderedByY = positions.Item2.y > positions.Item1.y ? (index1, index2) : (index2, index1);
        }

        public (Vector3, Vector3) pos
        {
            get
            {
                return (path.path[indices.Item1], path.path[indices.Item2]);
            }
        }

        public (Vector3, Vector3) posOrderedByX
        {
            get
            {
                return (path.path[indicesOrderedByX.Item1], path.path[indicesOrderedByX.Item2]);
            }
        }

        public (Vector3, Vector3) posOrderedByY
        {
            get
            {
                return (path.path[indicesOrderedByY.Item1], path.path[indicesOrderedByY.Item2]);
            }
        }
        public (int, int) indicesOrderedByX { get; protected set; }
        public (int, int) indicesOrderedByY { get; protected set; }


        public static Intersection GetIntersection(PathSegment segmentA, PathSegment segmentB) => Intersection.GetIntersection(segmentA, segmentB);
    }
}
