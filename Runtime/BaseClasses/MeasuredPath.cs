using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl.Lemon {
	public class MeasuredPath : ClosablePath {
		public float nonClosedLength { get; protected set; }
		private float closingLength;
		public float length { get { return nonClosedLength + (closed ? closingLength : 0); } }
		public List<float> lengths { get; protected set; }

		private List<PathSegment> _segments;
		public List<PathSegment> segments {
			get {
				if (_segments == null){	_segments = GetPathSegmentList(this); }
				return _segments; 
			}
		}

		protected override void Init(bool closed) {
			base.Init(closed);
			lengths = new List<float>();
		}

		public override void AddPoint(Vector3 p) {
			float distance = path.Count > 0 ? Vector3.Distance(path[path.Count - 1], p) : 0;
			closingLength = path.Count > 0 ? Vector3.Distance(path[0], p) : 0;
			nonClosedLength += distance;
			lengths.Add(nonClosedLength);
			_segments = null; // FIXME: we can handle this better

			base.AddPoint(p);
		}

		protected (float, float) GetNormLengthSegmentPair(int segmentIndex) {
			(int, int) indices = GetPathSegmentIndices(segmentIndex);
			float max = indices.Item2 == 0 ? 1 : lengths[indices.Item2] / length;
			return (lengths[indices.Item1] / length, max);
		}

		static List<PathSegment> GetPathSegmentList(MeasuredPath path) {
			List<PathSegment> segmentList = new List<PathSegment>();
            for (int i = 0; i < path.segmentCount; i++)
            {
				(int, int) segment = path.GetPathSegmentIndices(i);
				PathSegment ps = new PathSegment(segment.Item1, segment.Item2, path);
				segmentList.Add(ps);
            }
			return segmentList;
		}
	}
}
