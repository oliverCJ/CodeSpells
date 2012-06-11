using UnityEngine;
using System.Collections;
using System;

public class Evolvable : Waterable {
	public GameObject output;

	private bool converted = false;

	public override void waterEffectOnObject()
	{
		if(nearGround())
		{
			convert(gameObject, output);
		}
	}

	void convert(GameObject first, GameObject second)
	{
		Instantiate(second, first.transform.position, Quaternion.identity);
		
		// Somehow tell the SeedGenerator to generate another seed
		Debug.Log("Destroying: "+first.gameObject);
		
		Destroy(first);
	}
	
	public override void onUpdate()
	{
		if(beingWatered && !isHalfWaterlogged())
		{
			waterlogAmount += increaseWaterlogAmount;
		}
		if(isHalfWaterlogged())
		{
			waterEffectOnObject();
		}
	}
	
	bool nearGround()
	{
		// Get the height of the terrain where I am
		float yTerrain = Terrain.activeTerrain.SampleHeight(transform.position);
		
		// Get the height of the seed
		float ySeed = transform.position.y;
		
		float threshold = 0.8f;
		
		if(Math.Abs(yTerrain-ySeed) < threshold)
			return true;
		
		return false;
	}
}