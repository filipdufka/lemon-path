using UnityEngine;

namespace FruitBowl.Lemon {
	public class ClosablePath : BasicPath {
		public bool closed { get; set; }
		public int segmentCount { get { return closed ? path.Count : path.Count - 1; } }

		protected virtual void Init(bool closed) {
			Init();
			this.closed = closed;
		}

		public int GetPathModulo(int segment) { // FIXME: maybe move somewhere more appropriate
			int tempSegment = segment;
			if (segment < 0) {
				int loops = Mathf.FloorToInt(Mathf.Abs(segment) / (float)path.Count);
				tempSegment = segment + (loops + 1) * path.Count;

			}
			return tempSegment % path.Count;
		}

		public (int, int) GetPathSegmentIndices(int segment) {
			return (GetPathModulo(segment), GetPathModulo(segment + 1));
		}

		public (Vector3, Vector3) GetPathSegment(int segment) {
			(int, int) indexPair = GetPathSegmentIndices(segment);
			return (path[indexPair.Item1], path[indexPair.Item2]);
		}
	}
}
