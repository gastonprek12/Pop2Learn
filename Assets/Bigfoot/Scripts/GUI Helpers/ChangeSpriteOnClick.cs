#if NGUI
using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [RequireComponent (typeof (UI2DSprite))]
    public class ChangeSpriteOnClick : MonoBehaviour {

	    public Sprite PressedSprite;

	    public Sprite startingSprite;

	    public string Key;

	    UI2DSprite spriteComponent;


	    public bool ShouldRemainChanged = false;

	    bool isClicked = false;

	    void Start()
	    {
		    spriteComponent = GetComponent<UI2DSprite> ();
		    if(startingSprite == null)
			    startingSprite = spriteComponent.sprite2D;

		    if(!string.IsNullOrEmpty(Key) && ShouldRemainChanged)
		    {
			    if (PlayerPrefs.GetInt (Key, 1) == 0)
			    {
				    isClicked = true;
				    spriteComponent.sprite2D = PressedSprite;
			    }
		    }
	    }

	    void OnMouseDown()
	    {
		    if(!isClicked || !ShouldRemainChanged)
			    spriteComponent.sprite2D = PressedSprite;
		    else
			    spriteComponent.sprite2D = startingSprite;
	    }
	
	    void OnMouseUp()
	    {
		    if(!ShouldRemainChanged)		
			    spriteComponent.sprite2D = startingSprite;
		    else			
			    isClicked = !isClicked;
		
	    }

	    void OnPress (bool isDown)
	    {
		    if (isDown)
			    OnMouseDown ();
		    else
			    OnMouseUp ();
	    }

    }
}
#endif
