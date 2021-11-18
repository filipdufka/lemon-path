using UnityEngine;

namespace FruitBowl {
	public class LemonBounds {
		public float top;
		public float bottom;
		public float left;
		public float right;
		public Vector2 topLeft { get { return new Vector2(left, top); } }
		public Vector2 topRight { get { return new Vector2(right, top); } }
		public Vector2 bottomLeft { get { return new Vector2(left, bottom); } }
		public Vector2 bottomRight { get { return new Vector2(right, bottom); } }

		public void AddPoint(Vector2 p) {
			top = Mathf.Max(top, p.y);
			bottom = Mathf.Min(bottom, p.y);
			left = Mathf.Min(left, p.x);
			right = Mathf.Max(right, p.x);
		}

		public bool Contains(Vector2 p) {
			if (p.y >= bottom && p.y <= top && p.x >= left && p.x <= right) {
				return true;
			}
			return false;
		}

		public bool Intersects(LemonBounds ob) {
			if ((ob.bottom >= bottom && ob.bottom <= top) || (ob.top >= bottom && ob.top <= top) ||
				(bottom >= ob.bottom && bottom <= ob.top) || (top >= ob.bottom && top <= ob.top)) {
				if ((ob.left >= left && ob.left <= right) || (ob.right >= left && ob.right <= right) ||
					(left >= ob.left && left <= ob.right) || (right >= ob.left && right <= ob.right)) {
					return true;
				}
			}
			return false;
		}
	}
}
