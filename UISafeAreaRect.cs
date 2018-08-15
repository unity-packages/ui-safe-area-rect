using UnityEngine;
using UnityEngine.UI;

namespace UnityPackages.UI {
	public class UISafeAreaRect : MonoBehaviour {

		public bool top;
		public bool bottom;
		public bool right;
		public bool left;

		private int lastCanvasSizeX;
		private int lastCanvasSizeY;

		private RectTransform canvasRect;
		private RectTransform selfRect;

		private void GetComponents () {
			this.canvasRect = this.GetComponentInParent<Canvas> ().GetComponent<RectTransform> ();
			this.selfRect = this.GetComponent<RectTransform> ();
		}

		private void Format () {
			var _left = 0f;
			var _right = 0f;
			var _top = 0f;
			var _bottom = 0f;
			switch (Mathf.RoundToInt ((this.canvasRect.sizeDelta.x / this.canvasRect.sizeDelta.y) * 100)) {
				// iPhone X
				case 46:
					if (this.top == true)
						_top = this.canvasRect.sizeDelta.x * 0.04f;
					if (this.bottom == true)
						_bottom = this.canvasRect.sizeDelta.y * 0.04f;
					break;
			}

			this.selfRect.hideFlags = HideFlags.NotEditable;
			this.selfRect.anchorMin = Vector2.zero;
			this.selfRect.anchorMax = Vector2.one;
			this.selfRect.sizeDelta = new Vector2 (-(_left + _right), -(_top + _bottom));
			this.selfRect.anchoredPosition = new Vector2 ((_left - _right) / 2f, -(_top - _bottom) / 2f);
		}

		private void FormatIfNeeded () {
			var _canvasX = Mathf.RoundToInt (this.canvasRect.sizeDelta.x);
			var _canvasY = Mathf.RoundToInt (this.canvasRect.sizeDelta.y);

			if (_canvasX == this.lastCanvasSizeX &&
				_canvasY == this.lastCanvasSizeY)
				return;

			this.lastCanvasSizeX = _canvasX;
			this.lastCanvasSizeY = _canvasY;

			this.Format ();
		}

		private void Awake () {
			this.GetComponents ();
			this.Format ();
		}

		private void Update () {
			this.FormatIfNeeded ();
		}

		private void OnDrawGizmos () {
			if (Application.isPlaying == false) {
				this.GetComponents ();
				this.Format ();
			}
		}
	}
}