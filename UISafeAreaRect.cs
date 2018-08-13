using UnityEngine;
using UnityEngine.UI;

namespace UnityPackages.UI {
	public class UISafeAreaRect : MonoBehaviour {

		public bool top;
		public bool bottom;
		public bool right;
		public bool left;

		private void Format () {
			var _canvas = this.GetComponentInParent<Canvas> ().GetComponent<RectTransform> ();
			var _rectTransform = this.GetComponent<RectTransform> ();
			var _left = 0f;
			var _right = 0f;
			var _top = 0f;
			var _bottom = 0f;
			var _ratio = Mathf.RoundToInt ((_canvas.sizeDelta.x / _canvas.sizeDelta.y) * 100);

			switch (_ratio) {
				// iPhone X
				case 46:
					if (this.top == true)
						_top = _canvas.rect.height * 0.04f;
					if (this.bottom == true)
						_bottom = _canvas.rect.height * 0.04f;
					break;
			}

			_rectTransform.hideFlags = HideFlags.NotEditable;
			_rectTransform.anchorMin = Vector2.zero;
			_rectTransform.anchorMax = Vector2.one;
			_rectTransform.sizeDelta = new Vector2 (-(_left + _right), -(_top + _bottom));
			_rectTransform.anchoredPosition = new Vector2 ((_left - _right) / 2f, -(_top - _bottom) / 2f);
		}

		private void Awake () {
			this.Format ();
		}

		private void OnDrawGizmos () {
			if (Application.isPlaying == false)
				this.Format ();
		}
	}
}