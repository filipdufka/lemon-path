using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl {
	public class LemonPath {
		public LemonBounds bounds { get; protected set; }
		public List<Vector2> path { get; protected set; }
		public bool closed { get; protected set; }

		void Initialize() {
			bounds = new LemonBounds();
			path = new List<Vector2>();
		}

		public LemonPath() {
			Initialize();
		}

		void AddNewPoint(Vector2 p) {
			path.Add(p);
			bounds.AddPoint(p);
		}

		public void AddPointRange(List<Vector2> points) {
			foreach (Vector2 p in points) {
				AddNewPoint(p);
			}
		}
	}
}