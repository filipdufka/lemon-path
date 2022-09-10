using UnityEngine;

namespace FruitBowl.Lemon
{   
    public class PathSegment
    {
        public (int, int) indices;
        public LemonPath path;
        public (Vector3, Vector3) pos
        {
            get
            {
                return (path.path[indices.Item1], path.path[indices.Item2]);
            }
        }
        public (Vector3, Vector3) bounds
        {
            get { return (path.path[indices.Item1], path.path[indices.Item2]); }
        }
    }

    /// <summary>
    /// TODO: Can we transform this into Path Segment?
    /// </summary>
    public class LineEvent2
    {
        public int thisIndex;
        public int nextIndex;
        public Vector2 pos;
        public int pathId;
        public LineEvent2(int thisIndex, int nextIndex, Vector2 pos, int pathId = 0)
        {
            this.thisIndex = thisIndex;
            this.nextIndex = nextIndex;
            this.pos = pos;
            this.pathId = pathId;
        }
    }
}
