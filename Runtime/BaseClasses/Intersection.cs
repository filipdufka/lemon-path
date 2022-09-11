using FruitBowl.Orange;
using UnityEngine;

namespace FruitBowl.Lemon
{
	public class Intersection
	{
		// ends are in clockwise order
		public enum IntersectionEnd
		{
			Nan = -1,
			Aa = 0,
			Ba = 1,
			Ab = 2,
			Bb = 3,
		}
		protected int[] vertices;
		public int Aa { get { return vertices[0]; } }
		public int Ba { get { return vertices[1]; } }
		public int Ab { get { return vertices[2]; } }
		public int Bb { get { return vertices[3]; } }
		public Vector2 pos { get; protected set; }
		public bool isSelfIntersection { get; protected set; }


		public Intersection(Vector2 pos, PathSegment segmentA, PathSegment segmentB)
		{
			vertices = new int[4];
			vertices[(int)IntersectionEnd.Aa] = segmentA.indices.Item1;
			vertices[(int)IntersectionEnd.Ba] = segmentB.indices.Item1;
			vertices[(int)IntersectionEnd.Ab] = segmentA.indices.Item2;
			vertices[(int)IntersectionEnd.Bb] = segmentB.indices.Item2;
			this.pos = pos;
			
			// check clockwise-ness
			Vector3 aatoba = (segmentB.path.path[Ba] - segmentA.path.path[Aa]).normalized;
			Vector3 n = LemonPathHelper.GetSegmentNormal(segmentA);
			if (Vector3.Dot(aatoba, n) > 0)
			{ // flip Ba and Bb				
				int temp = Ba;
				vertices[(int)IntersectionEnd.Ba] = Bb;				
				vertices[(int)IntersectionEnd.Bb] = temp;
			}

			isSelfIntersection = segmentA.path == segmentB.path;
		}

		IntersectionEnd GetIntersectionIndex(int v)
		{
			if (v == Aa) { return IntersectionEnd.Aa; }
			if (v == Ab) { return IntersectionEnd.Ab; }
			if (v == Ba) { return IntersectionEnd.Ba; }
			if (v == Bb) { return IntersectionEnd.Bb; }
			return IntersectionEnd.Nan;
		}

		public int GetCWEnd(int v)
		{
			int arrayIndex = (int)GetIntersectionIndex(v);
			return vertices[(arrayIndex + 1) % 4];
		}

		public int GetCCWEnd(int v)
		{
			int arrayIndex = (int)GetIntersectionIndex(v);
			return vertices[(4 + arrayIndex - 1) % 4];
		}

		public int GetLineEnd(int v)
		{
			int arrayIndex = (int)GetIntersectionIndex(v);
			return vertices[(arrayIndex + 2) % 4];
		}

		public static Intersection GetIntersection(PathSegment segmentA, PathSegment segmentB)
		{
			float t0, s0;
			Vector3 intersection = MathUtils.LineLineIntersection((segmentA.pos), segmentB.pos, out t0, out s0);
			if (s0 > 0f && s0 < 1f && t0 > 0f && t0 < 1f)
            {
				return new Intersection(intersection, segmentA, segmentB);
            }

			return null;
		}
	}
}
