using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private bool doMovement = true;

    public float panSpeed = 30f; //velocidade de movimento da camera
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 90f;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameIsOver)  //caso o jogo acabou
        {
            this.enabled = false;  //desativa este componente para nao controlarmos a camera quando o jogo acabar
            return;
        }

        /*
        if(Input.GetKey(KeyCode.E))  //tecla escape e um toggle de movimento
            doMovement = !doMovement;

        if(!doMovement) //se doMovement for falso a camera nao ira se mover
            return;
        */
        
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) //testando o input
        {
            //new Vector3 (0f, 0f, 1f) * panSpeed * Time.deltaTime -> Vector3.foward
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); //muda a posicao da camera para frente
        }
        if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) //testando o input
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); //muda a posicao da camera para tras
        }
        if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) //testando o input
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); //muda a posicao da camera para a direita
        }
        if(Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) //testando o input
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World); //muda a posicao da camera para a esquerda
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 10000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //limitando o scrolling

        transform.position = pos;
    }
}
