using System.Collections.Generic;
using FruitBowl;
using UnityEngine;

namespace FruitBowl {
	public class LemonPathHelper {
		public static List<Vector2> GetPathFromPolygonCollider2D(PolygonCollider2D collider) {
			List<Vector2> res = new List<Vector2>();
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

		public static int GetPathModulo(List<Vector2> path, int segment) {
			int tempSegment = segment;
			if (segment < 0) {
				int loops = Mathf.FloorToInt(Mathf.Abs(segment) / (float)path.Count);
				tempSegment = segment + (loops + 1) * path.Count;

			}
			return tempSegment % path.Count;
		}

		public static Pair<Vector2> GetPathSegment(List<Vector2> path, int segment) {
			return new Pair<Vector2>(path[GetPathModulo(path, segment)], path[GetPathModulo(path, segment + 1)]);
		}

		public static void DebugDrawPath(List<Vector2> path, Color c) {
			for (int i = 1; i < path.Count; i++) {
				Vector2 a = path[i - 1];
				Vector2 b = path[i];
				Debug.DrawLine(a, b, c);
			}
		}

		public static void DebugDrawPath(LemonPath path, Color c) {
			int segmentCount = path.GetSegmentCount();
			for (int i = 0; i < segmentCount; i++) {
				Pair<Vector2> s = GetPathSegment(path.path, i);
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
	}
}
