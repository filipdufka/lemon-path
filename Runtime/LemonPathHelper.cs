using System.Collections.Generic;
using FruitBowl;
using UnityEngine;

namespace FruitBowl {
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

		public static int GetPathModulo(List<Vector3> path, int segment) {
			int tempSegment = segment;
			if (segment < 0) {
				int loops = Mathf.FloorToInt(Mathf.Abs(segment) / (float)path.Count);
				tempSegment = segment + (loops + 1) * path.Count;

			}
			return tempSegment % path.Count;
		}

		public static Pair<int> GetPathSegmentIndices(List<Vector3> path, int segment) {
			return new Pair<int>(GetPathModulo(path, segment), GetPathModulo(path, segment + 1));
		}

		public static Pair<Vector3> GetPathSegment(List<Vector3> path, int segment) {
			Pair<int> indexPair = GetPathSegmentIndices(path, segment);
			return new Pair<Vector3>(path[indexPair.a], path[indexPair.b]);
		}

		

		public static void DebugDrawPath(List<Vector3> path, Color c) {
			for (int i = 0; i < path.Count; i++) {
				Pair<Vector3> segment = GetPathSegment(path, i);
				Debug.DrawLine(segment.a, segment.b, c);
			}
		}

		public static void DebugDrawPath(LemonPath path, Color c) {
			for (int i = 0; i < path.segmentCount; i++) {
				Pair<Vector3> s = GetPathSegment(path.path, i);
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
