using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square //He quitado el : MonoBehaviour ya que este script no se añadirá a ningún objeto, simplemente necesito su clase
{

    private bool isVisited = false;
    private bool isWall = false;
    private bool hasTrap = false;
    public bool getIsVisited()
    {
        return isVisited;
    }
    public bool getIsWall()
    {
        return isWall;
    }
    public bool getHasTrap()
    {
        return hasTrap;
    }
    public void setIsVisited(bool isVisited)
    {
        this.isVisited = isVisited;
    }
    public void setIsWall(bool isWall)
    {
        this.isWall = isWall;
    }
    public void setHasTrap(bool hasTrap)
    {
        this.hasTrap = hasTrap;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
