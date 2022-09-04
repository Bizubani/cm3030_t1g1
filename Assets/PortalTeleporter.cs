using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public bool findReciever = true;
	public string recieverName;
	public Transform reciever;

	private bool playerIsOverlapping = false;

	void Start()
	{
		player = GameObject.Find("Player Character").GetComponent<Transform>();

		// if(findReciever)
		// {
		// 	reciever = GameObject.Find(recieverName).transform.GetChild(1).gameObject.transform;
		// }
	}

	// Update is called once per frame
	void Update () 
    {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			if(findReciever)
			{
				reciever = GameObject.Find(recieverName).transform.GetChild(1).gameObject.transform;
			}
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
