using UnityEngine;

namespace FruitBowl.Lemon
{   
    public class PathSegment
    {
        public Pair<int> indices;
        public LemonPath path;
        public Pair<Vector3> pos
        {
            get
            {
                return new Pair<Vector3>(path.path[indices.a], path.path[indices.b]);
            }
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
