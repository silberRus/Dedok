using UnityEngine;
using System.Collections;

public class ScrollingUVs_Layers : MonoBehaviour 
{
	public string textureName = "_MainTex";
	
	[SerializeField] public float cof = 0.1f;

	Vector2 uvOffset = Vector2.zero;
	
	void LateUpdate() 
	{
		uvOffset += (new Vector2(0f, WorldController.instance.worldSpeed * -cof) * Time.deltaTime );
		if( GetComponent<Renderer>().enabled )
		{
			GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, uvOffset );
		}
	}
}