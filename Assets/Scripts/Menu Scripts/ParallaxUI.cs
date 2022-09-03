using UnityEngine;
using UnityEngine.UI;

public class ParallaxUI : MonoBehaviour
{
    Vector2 StartPos;

    [SerializeField]
    int parallaxModifier;

    // Start is called before the first frame update
    private void Start()
    {
        StartPos = transform.position;    
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, StartPos.x + (pz.x * parallaxModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPos.y + (pz.y * parallaxModifier), 2f * Time.deltaTime);

        transform.position = new Vector3
        (
            posX,
            posY,
            0
        );

    }
}
