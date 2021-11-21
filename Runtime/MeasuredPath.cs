using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl {
	public class MeasuredPath : ClosablePath {
		public float nonClosedLength { get; protected set; }
		private float closingLength;
		public float length { get { return nonClosedLength + (closed ? closingLength : 0); } }
		public List<float> lengths { get; protected set; }

		protected override void Initialize(bool closed) {
			base.Initialize(closed);
			lengths = new List<float>();
		}

		public override void AddPoint(Vector3 p) {
			float distance = path.Count > 0 ? Vector3.Distance(path[path.Count - 1], p) : 0;
			closingLength = path.Count > 0 ? Vector3.Distance(path[0], p) : 0;
			nonClosedLength += distance;
			lengths.Add(nonClosedLength);

			base.AddPoint(p);
		}

		protected Pair<float> GetNormLengthSegmentPair(int segmentIndex) {
			Pair<int> indices = LemonPathHelper.GetPathSegmentIndices(path, segmentIndex);
			float max = indices.b == 0 ? 1 : lengths[indices.b] / length;
			return new Pair<float>(lengths[indices.a] / length, max);
		}
	}
}
