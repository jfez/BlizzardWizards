using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFollow : MonoBehaviour
{
    //public Text nameLabel;
    public Image imageLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;*/
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        imageLabel.transform.position = namePos;
    }
}
