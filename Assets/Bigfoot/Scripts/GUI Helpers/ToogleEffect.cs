using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class ToogleEffect : MonoBehaviour
    {

        public Sprite SpriteOn;
        public Sprite SpriteOff;
        public bool IsOn;
        public string PreferenceKey;
        public int PreferenceDefaultValue = 1;
        private SpriteRenderer spriteRenderer;
#if NGUI
	private UI2DSprite ui2DSprite;
#endif
        private bool inCollider = false;
        // Use this for initialization
        void OnEnable()
        {
            if (PreferenceKey != "")
            {
                int _isOn = PlayerPrefs.GetInt(PreferenceKey, PreferenceDefaultValue);
                if (_isOn == 1)
                {
                    IsOn = true;
                }
                else
                {
                    IsOn = false;
                }
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
#if NGUI
		ui2DSprite = GetComponent<UI2DSprite> ();
#endif
            if (IsOn)
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = SpriteOn;
#if NGUI
			else
				ui2DSprite.sprite2D = SpriteOn;
#endif
            }
            else
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = SpriteOff;
#if NGUI
			else
				ui2DSprite.sprite2D = SpriteOff;
#endif
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseEnter()
        {
            inCollider = true;
        }

        void OnMouseExit()
        {
            inCollider = false;
        }

        void OnMouseUp()
        {
            if (inCollider)
                OnClick();
        }

        void OnClick()
        {
            if (IsOn)
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = SpriteOff;
#if NGUI
			else
				ui2DSprite.sprite2D = SpriteOff;
#endif
                IsOn = false;
            }
            else
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = SpriteOn;
#if NGUI
			else
				ui2DSprite.sprite2D = SpriteOn;
#endif
                IsOn = true;

            }
            PlayerPrefs.SetInt(PreferenceKey, IsOn ? 1 : 0);
            ValueChanged();
        }

        public virtual void ValueChanged()
        {

        }
    }
}