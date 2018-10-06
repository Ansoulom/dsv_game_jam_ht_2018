using UnityEngine;

public class GameCamera : MonoBehaviour 
{
#region Variables

    [SerializeField] private Transform player1_;
    [SerializeField] private Transform player2_;

#endregion

#region Private Methods

    private void LateUpdate ()
    {
        var playerPos = (player1_.position + player2_.position) / 2f;
		transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
	}

#endregion
}
