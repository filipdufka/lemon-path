using System.Collections.Generic;
using FruitBowl;
using UnityEngine;

namespace FruitBowl.Lemon {
	public class LemonPathHelper {
		public static List<Vector3> GetPathFromPolygonCollider2D(PolygonCollider2D collider) {
			List<Vector3> res = new List<Vector3>();
			for (int p = 0; p < collider.points.Length; p++) {
				Vector2 p0 = collider.points[p];
				p0 = collider.transform.localToWorldMatrix * new Vector4(p0.x, p0.y, 0, 1);
				res.Add(p0);
			}
			return res;
		}

		public static List<Vector2> GetPathFromBoxCollider2D(BoxCollider2D collider) {
			List<Vector2> res = new List<Vector2>();
			Transform bt = collider.transform;

			res.Add(Matrix4x4.TRS(bt.position, bt.rotation, bt.localScale) * new Vector4(-0.5f, 0.5f, 0, 1));
			res.Add(Matrix4x4.TRS(bt.position, bt.rotation, bt.localScale) * new Vector4(-0.5f, -0.5f, 0, 1));
			res.Add(Matrix4x4.TRS(bt.position, bt.rotation, bt.localScale) * new Vector4(0.5f, -0.5f, 0, 1));
			res.Add(Matrix4x4.TRS(bt.position, bt.rotation, bt.localScale) * new Vector4(0.5f, 0.5f, 0, 1));
			return res;
		}

		public static void DebugDrawPath(LemonPath path, Color c) {
			for (int i = 0; i < path.segmentCount; i++) {
				Pair<Vector3> s = path.GetPathSegment(i);
				Debug.DrawLine(s.a, s.b, c);
			}
		}

		public static void DrawCircle(Vector2 center, float size, int resolution, Color color) {
			float angleStep = 2 * Mathf.PI / resolution;
			for (int a = 0; a < resolution; a++) {
				Vector2 A = center + size * new Vector2(Mathf.Sin(a * angleStep), Mathf.Cos(a * angleStep));
				Vector2 B = center + size * new Vector2(Mathf.Sin((a + 1) * angleStep), Mathf.Cos((a + 1) * angleStep));

				Debug.DrawLine(A, B, color);
			}
		}

		public static Vector2 GetSegmentNormal(PathSegment segment)
		{
			return GetSegmentNormal(segment.pos.a, segment.pos.b);
		}

		public static Vector2 GetSegmentNormal(Vector2 A, Vector2 B)
		{
			Vector2 lineDir = B - A;
			return (new Vector2(lineDir.y, -lineDir.x)).normalized;
		}

		// referencing http://geomalgorithms.com/a09-_intersect-3.html#Shamos-Hoey-Algorithm	

		//public static List<LineEvent2> PrepareLineEventQueue(params List<Vector2>[] paths)
		//{
		//	//List<LineEvent2> eventsQueue = new List<LineEvent2>();
		//	//for (int p = 0; p < paths.Length; p++)
		//	//{
		//	//	List<Vector2> path = paths[p];
		//	//	for (int i = 0; i < path.Count; i++)
		//	//	{
		//	//		// TODO: save reference to path
		//	//		Vector2 pos = path[i];
		//	//		int nextIndex = GetModuloIndex(path, i + 1);
		//	//		Vector2 nextPos = path[nextIndex];
		//	//		int prevIndex = GetModuloIndex(path, i - 1);
		//	//		Vector2 prevPos = path[prevIndex];

		//	//		if (nextPos.x > pos.x)
		//	//		{
		//	//			eventsQueue.Add(new LineEvent2(i, nextIndex, pos, p));
		//	//		}
		//	//		if (prevPos.x > pos.x)
		//	//		{
		//	//			eventsQueue.Add(new LineEvent2(i, prevIndex, pos, p));
		//	//		}

		//	//		if (nextPos.x == pos.x && nextPos.y > pos.y)
		//	//		{
		//	//			eventsQueue.Add(new LineEvent2(i, nextIndex, pos, p));
		//	//		}
		//	//		if (prevPos.x == pos.x && prevPos.y > pos.y)
		//	//		{
		//	//			eventsQueue.Add(new LineEvent2(i, prevIndex, pos, p));
		//	//		}
		//	//	}
		//	//}
		//	//List<LineEvent2> orderedByX = eventsQueue.OrderBy(o => o.pos.x).ToList();
		//	return orderedByX;
		//}

		//public static List<Intersection> GetIntersections(LemonPath pathA, LemonPath pathB) {
		//	List<LineEvent2> orderedByX = PrepareLineEventQueue(path);
		//	return result;
  //      }


	}
}
