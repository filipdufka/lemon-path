using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl {
	public class BasicPath {
		public LemonBounds bounds { get; protected set; }
		public List<Vector3> path { get; protected set; }
		public bool valid { get { return path != null && path.Count > 1; } }

		protected void Initialize() {
			bounds = new LemonBounds();
			path = new List<Vector3>();
		}

		public virtual void AddPoint(Vector3 p) {
			path.Add(p);
			bounds.AddPoint(p);
		}

		public void AddPointRange(List<Vector3> points) {
			foreach (Vector3 p in points) {
				AddPoint(p);
			}
		}
	}
}
