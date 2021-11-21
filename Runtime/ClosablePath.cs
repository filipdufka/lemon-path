using UnityEngine;

namespace FruitBowl {
	public class ClosablePath : BasicPath {
		public bool closed { get; set; }
		public int segmentCount { get { return closed ? path.Count : path.Count - 1; } }

		protected virtual void Initialize(bool closed) {
			Initialize();
			this.closed = closed;
		}

		public int GetPathModulo(int segment) { // FIXME: maybe move somewhere more accurate?
			int tempSegment = segment;
			if (segment < 0) {
				int loops = Mathf.FloorToInt(Mathf.Abs(segment) / (float)path.Count);
				tempSegment = segment + (loops + 1) * path.Count;

			}
			return tempSegment % path.Count;
		}

		public Pair<int> GetPathSegmentIndices(int segment) {
			return new Pair<int>(GetPathModulo(segment), GetPathModulo(segment + 1));
		}

		public Pair<Vector3> GetPathSegment(int segment) {
			Pair<int> indexPair = GetPathSegmentIndices(segment);
			return new Pair<Vector3>(path[indexPair.a], path[indexPair.b]);
		}
	}
}
