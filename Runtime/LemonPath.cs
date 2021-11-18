using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl {
	public class LemonPath {
		public LemonBounds bounds { get; protected set; }
		public List<Vector2> path { get; protected set; }
		public float length { get; protected set; }
		public List<float> lengths { get; protected set; }

		public bool closed { get; protected set; }
		public bool valid { get { return path != null && path.Count > 1; } }

		void Initialize() {
			bounds = new LemonBounds();
			path = new List<Vector2>();
			lengths = new List<float>();
		}

		public LemonPath() {
			Initialize();
		}

		public void AddPoint(Vector2 p) {
			float distance = path.Count > 0 ? Vector2.Distance(path[path.Count - 1], p) : 0;
			length += distance;
			lengths.Add(length);
			path.Add(p);
			bounds.AddPoint(p);
			
		}

		public void AddPointRange(List<Vector2> points) {
			foreach (Vector2 p in points) {
				AddPoint(p);
			}
		}

		// TODO: use Oriented Point
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
	}
}