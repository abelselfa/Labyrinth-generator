using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayerActions : MonoBehaviour
{
    //private Ray ray;
    //private RaycastHit hit;
    private List<GameObject> listOfSuelos;
    private Square[][] squares;
    private string suelo = "suelo";
    private int indexMainCamera;
    private GameObject currentHitGameObject; // = new GameObject();
    private Color originalColor;
    public GameObject fakeWall;
    public float fakeWallTransparency = 0.0f;
    private Color fakeWallColor;
    public Material fakeWallMaterial;
    //var col = gameObject.GetComponent<Renderer>().material.color;

    [SerializeField]
    private float rayRange = 10;

    private void Awake()
    {
        currentHitGameObject = new GameObject();
        currentHitGameObject.AddComponent<MeshRenderer>();
        listOfSuelos = GameObject.FindGameObjectsWithTag("gamemode")[0].GetComponent<MazeGeneration>().listOfSuelos;
        squares = GameObject.FindGameObjectsWithTag("gamemode")[0].GetComponent<MazeGeneration>().squares;
        //fakeWallMaterial = GameObject.FindGameObjectsWithTag("gamemode")[0].GetComponent<MazeGeneration>().materialWall;
        fakeWallMaterial.mainTextureScale = new Vector2(fakeWall.transform.localScale.x, fakeWall.transform.localScale.z);
        fakeWall.GetComponent<MeshRenderer>().sharedMaterial = fakeWallMaterial;
        fakeWallColor = fakeWallMaterial.color;
        fakeWallColor.a = fakeWallTransparency;
    }


    // Start is called before the first frame update
    void Start()
    {
        //currentHitGameObject = new GameObject();
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "MainCamera")
            {
                indexMainCamera = i;
            }
                
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.GetChild(indexMainCamera).gameObject.transform.forward);

        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);

        if (Physics.Raycast(ray, out hit, rayRange) && hit.transform.CompareTag(suelo))
        {

            if (currentHitGameObject == null)
            {
                originalColor = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                currentHitGameObject = hit.collider.gameObject;
            }

            //if (currentHitGameObject != hit.collider.gameObject && currentHitGameObject != null)
            if (currentHitGameObject != null)
            {
                currentHitGameObject.GetComponent<Renderer>().material.color = originalColor;
                currentHitGameObject = hit.collider.gameObject;
                originalColor = currentHitGameObject.GetComponent<Renderer>().material.color;

                if (sueloIsAvailable(hit.collider.gameObject))
                {
                    currentHitGameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    currentHitGameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            /*if (hit.collider.gameObject == null && currentHitGameObject != null)
            {
                currentHitGameObject.GetComponent<Renderer>().material.color = originalColor;
            }*/

            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.green)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //hit.rigidbody.useGravity = true;
                    //hit.rigidbody.gameObject.SetActive(false);
                    //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    //Destroy(hit.collider.gameObject);

                    hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
                    hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                    squares[(int)(hit.collider.gameObject.transform.position.x / hit.collider.gameObject.transform.localScale.x)][(int)(hit.collider.gameObject.transform.position.z / hit.collider.gameObject.transform.localScale.z)].setHasTrap(true);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Instantiate(fakeWall, new Vector3(hit.collider.gameObject.transform.position.x, (fakeWall.transform.localScale.y / 2) + hit.collider.gameObject.transform.localScale.y, hit.collider.gameObject.transform.position.z), Quaternion.identity);
                }
            }
  
        }
        else
        {
            if (currentHitGameObject != null)
            {
                currentHitGameObject.GetComponent<Renderer>().material.color = originalColor;
            }
        }
    }
    private bool sueloIsAvailable(GameObject suelo)
    {
        
        float a = suelo.transform.position.x / suelo.transform.localScale.x;
        float b = suelo.transform.position.z / suelo.transform.localScale.z;

        if (listOfSuelos != null)
        {
            if (a > 0 && a < squares.Length - 1)
            {
                if (b > 0 && b < squares[(int)a].Length - 1)
                {
                    if (squares[(int)a - 1][(int)b].getHasTrap()) {

                        return false;
                    }

                    if (squares[(int)a + 1][(int)b].getHasTrap())
                    {

                        return false;
                    }

                    if (squares[(int)a][(int)b - 1].getHasTrap())
                    {

                        return false;
                    }

                    if (squares[(int)a][(int)b + 1].getHasTrap())
                    {

                        return false;
                    }
                }
                else
                {
                    if (b == 0)
                    {
                        if (squares[(int)a - 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a + 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b + 1].getHasTrap())
                        {

                            return false;
                        }
                    }

                    if (b == squares[(int)a].Length - 1)
                    {
                        if (squares[(int)a - 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a + 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b + 1].getHasTrap())
                        {

                            return false;
                        }
                    }
                }
            }
            else
            {
                if (a == 0)
                {
                    if (b > 0 && b < squares[(int)a].Length - 1)
                    {
                        if (squares[(int)a + 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b - 1].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b + 1].getHasTrap())
                        {

                            return false;
                        }
                    }
                    else
                    {
                        if (b == 0)
                        {
                            if (squares[(int)a + 1][(int)b].getHasTrap())
                            {

                                return false;
                            }

                            if (squares[(int)a][(int)b + 1].getHasTrap())
                            {

                                return false;
                            }
                        }

                        if (b == squares[(int)a].Length - 1)
                        {
                            if (squares[(int)a + 1][(int)b].getHasTrap())
                            {

                                return false;
                            }

                            if (squares[(int)a][(int)b - 1].getHasTrap())
                            {

                                return false;
                            }
                        }
                    }
                }

                if (a == squares.Length - 1)
                {
                    if (b > 0 && b < squares[(int)a].Length - 1)
                    {
                        

                        if (squares[(int)a - 1][(int)b].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b - 1].getHasTrap())
                        {

                            return false;
                        }

                        if (squares[(int)a][(int)b + 1].getHasTrap())
                        {

                            return false;
                        }
                    }
                    else
                    {
                        if (b == 0)
                        {
                            if (squares[(int)a - 1][(int)b].getHasTrap())
                            {

                                return false;
                            }

                            if (squares[(int)a][(int)b + 1].getHasTrap())
                            {

                                return false;
                            }
                        }

                        if (b == squares[(int)a].Length - 1)
                        {
                            if (squares[(int)a - 1][(int)b].getHasTrap())
                            {

                                return false;
                            }

                            if (squares[(int)a][(int)b - 1].getHasTrap())
                            {

                                return false;
                            }
                        }
                    }
                }
            }
        }

        return true;
    }
}
