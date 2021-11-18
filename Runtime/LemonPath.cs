using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl {
	public class LemonPath {
		public LemonBounds bounds { get; protected set; }
		public List<Vector2> path { get; protected set; }
		public float length { get; protected set; }
		public List<float> lengths { get; protected set; }

		public bool closed { get; set; }
		public bool valid { get { return path != null && path.Count > 1; } }

		void Initialize(bool closed) {
			bounds = new LemonBounds();
			path = new List<Vector2>();
			lengths = new List<float>();
			this.closed = closed;
		}

		public LemonPath(bool closed = false) {
			Initialize(closed);
		}

		public LemonPath(List<Vector2> path, bool closed = false) {
			Initialize(closed);
			AddPointRange(path);
		}

		public void AddPoint(Vector2 p) {
			float distance = path.Count > 0 ? Vector2.Distance(path[path.Count - 1], p) : 0;
			length += distance;
			lengths.Add(length);
			path.Add(p);
			bounds.AddPoint(p);
			
		}

		public int GetSegmentCount() {
			return closed ? path.Count : path.Count - 1;
		}

		public void AddPointRange(List<Vector2> points) {
			foreach (Vector2 p in points) {
				AddPoint(p);
			}
		}

		// TODO: use Oriented Point
		// FIXME: take in consideration fact, that this could be closed
		public Vector2 GetPoint(float position) {
			float p = Mathf.Clamp01(position);
			for (int i = 1; i < lengths.Count; i++) {
				float min = lengths[i - 1] / length;
				float max = lengths[i] / length;
				if (p >= min && p <= max) {
					float f = Mathf.InverseLerp(min, max, p);
					Vector2 A = path[i - 1];
					Vector2 B = path[i];
					return Vector2.Lerp(A, B, f);
				}
			}
			Debug.LogError("Error with getting Point on path! Unbelievable!");
			return new Vector2(0,0);
		}

		// FIXME: take in consideration fact, that this could be closed
		public List<int> GetIndicesBetween(float a, float b) {
			float mn = Mathf.Min(a, b);
			float mx = Mathf.Max(a, b);

			List<int> points = new List<int>();

			for (int i = 0; i < lengths.Count; i++) {
				float l = lengths[i] / length;
				if (mn <= l && l <= mx) {
					points.Add(i);
				}
			}

			if (b < a) {
				points.Reverse();
			}
			return points;
		}
	}
}