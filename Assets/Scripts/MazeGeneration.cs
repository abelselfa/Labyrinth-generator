using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{

	public Square[][] squares;
	private List<Vector2> listOfSquares = new List<Vector2>();
	public GameObject wall;
	public Material materialWall;
	public GameObject player;
	public GameObject suelo;
	public List<GameObject> listOfSuelos;
	public Material materialSuelo;
	public int rows = 30;
	public int columns = 30;

    private void Awake()
    {
		listOfSuelos = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {	
		createMaze(rows, columns);
	}

    // Update is called once per frame
    void Update()
    {
     
    }

	void createMaze(int rows, int columns)
	{
		int numOfSquares = rows * columns;

		int currentIndexListOfSquares;
		int a, b, c;
		int randNeighbour;
		bool correctNeighbour;
		bool correctExit = false;
		List<int> listOfCases;

		materialWall.mainTextureScale = new Vector2(wall.transform.localScale.x, wall.transform.localScale.z);
		wall.GetComponent<MeshRenderer>().material = materialWall;

		/*suelo.transform.localScale = new Vector3((2 * rows + 1) * wall.transform.localScale.x, 1, (2 * columns + 1) * wall.transform.localScale.z);
		suelo.transform.position = new Vector3(rows * wall.transform.localScale.x, (suelo.transform.localScale.y / 2), columns * wall.transform.localScale.z);
		materialSuelo.mainTextureScale = new Vector2((2 * rows + 1) * wall.transform.localScale.x, (2 * columns + 1) * wall.transform.localScale.z);
		suelo.GetComponent<MeshRenderer>().material = materialSuelo;
		Instantiate(suelo, suelo.transform.position, Quaternion.identity);*/

		squares = new Square[2 * rows + 1][];

		for (int i = 0; i < squares.Length; i++)
		{
			squares[i] = new Square[2 * columns + 1]; // Create inner array

			for (int u = 0; u < squares[i].Length; u++)
            {
				squares[i][u] = new Square();
			}	
		}

		for (int i = 0; i < 2 * rows + 1; i++)
		{

			for (int u = 0; u < 2 * columns + 1; u++)
			{

				squares[i][u].setIsVisited(false);
				squares[i][u].setIsWall(true);
				squares[i][u].setHasTrap(false);
			}
		}
		
		a = UnityEngine.Random.Range(1, 2 * rows);

		if (a % 2 == 0)
		{

			a++;
		}

		b = 1;
		
		listOfSquares.Add(new Vector2(a, b));

		squares[(int)a][(int)b].setIsWall(false);
		squares[(int)a][(int)b].setIsVisited(true);
		squares[(int)a][(int)b - 1].setIsWall(false);
		//player.transform.position = new Vector3(a, 1, b - 1);

		Instantiate(player, new Vector3(a * wall.transform.localScale.x, 20, b - 1), Quaternion.identity);
		//player.transform.localPosition = new Vector3(a, 30, b - 1);
		//player.transform.TransformPoint(new Vector3(a, 1, b - 1));
		while (listOfSquares.Count > 0)
		{

			correctNeighbour = false;

			currentIndexListOfSquares = UnityEngine.Random.Range(0, listOfSquares.Count);

			listOfCases = new List<int>();
			int lol = listOfCases.Count;
			listOfCases.Add(0);
			listOfCases.Add(1);
			listOfCases.Add(2);
			listOfCases.Add(3);

			while (!correctNeighbour && listOfCases.Count > 0)
			{

				randNeighbour = listOfCases[UnityEngine.Random.Range(0, listOfCases.Count - 1)];

				switch (randNeighbour)
				{
					case 0:
						if (listOfSquares[currentIndexListOfSquares].x < 2 * rows - 1)
						{
							if (!squares[(int)listOfSquares[currentIndexListOfSquares].x + 2][(int)listOfSquares[currentIndexListOfSquares].y].getIsVisited())
							{
								squares[(int)listOfSquares[currentIndexListOfSquares].x + 1][(int)listOfSquares[currentIndexListOfSquares].y].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x + 2][(int)listOfSquares[currentIndexListOfSquares].y].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x + 2][(int)listOfSquares[currentIndexListOfSquares].y].setIsVisited(true);
								listOfSquares.Add(new Vector2(listOfSquares[currentIndexListOfSquares].x + 2, listOfSquares[currentIndexListOfSquares].y));
								correctNeighbour = true;
							}
						}

						if (listOfCases.Contains(0))
						{
							listOfCases.Remove(0);
						}

						break;
					case 1:
						if (listOfSquares[currentIndexListOfSquares].x > 1)
						{
							if (!squares[(int)listOfSquares[currentIndexListOfSquares].x - 2][(int)listOfSquares[currentIndexListOfSquares].y].getIsVisited())
							{
								squares[(int)listOfSquares[currentIndexListOfSquares].x - 1][(int)listOfSquares[currentIndexListOfSquares].y].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x - 2][(int)listOfSquares[currentIndexListOfSquares].y].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x - 2][(int)listOfSquares[currentIndexListOfSquares].y].setIsVisited(true);
								listOfSquares.Add(new Vector2(listOfSquares[currentIndexListOfSquares].x - 2, listOfSquares[currentIndexListOfSquares].y));
								correctNeighbour = true;
							}
						}

						if (listOfCases.Contains(1))
						{
							listOfCases.Remove(1);
						}

						break;
					case 2:
						if (listOfSquares[currentIndexListOfSquares].y < 2 * columns - 1)
						{
							if (!squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y + 2].getIsVisited())
							{
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y + 1].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y + 2].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y + 2].setIsVisited(true);
								listOfSquares.Add(new Vector2(listOfSquares[currentIndexListOfSquares].x, listOfSquares[currentIndexListOfSquares].y + 2));
								correctNeighbour = true;
							}
						}

						if (listOfCases.Contains(2))
						{
							listOfCases.Remove(2);
						}

						break;
					case 3:
						if (listOfSquares[currentIndexListOfSquares].y > 1)
						{
							if (!squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y - 2].getIsVisited())
							{
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y - 1].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y - 2].setIsWall(false);
								squares[(int)listOfSquares[currentIndexListOfSquares].x][(int)listOfSquares[currentIndexListOfSquares].y - 2].setIsVisited(true);
								listOfSquares.Add(new Vector2(listOfSquares[currentIndexListOfSquares].x, listOfSquares[currentIndexListOfSquares].y - 2));
								correctNeighbour = true;
							}
						}

						if (listOfCases.Contains(3))
						{
							listOfCases.Remove(3);
						}

						break;
				}
			}

			if (!correctNeighbour && listOfCases.Count == 0)
			{
				listOfSquares.RemoveAt(currentIndexListOfSquares);
			}
		}
		
		while (!correctExit)
		{

			c = UnityEngine.Random.Range(1, 2 * rows - 1);

			if (!squares[(int)c][(int)2 * columns - 1].getIsWall())
			{

				squares[(int)c][(int)2 * columns].setIsWall(false);
				correctExit = true;
			}
		}
		
		drawMaze(rows, columns, squares);
	}

	void drawMaze(int rows, int columns, Square[][] squares)
	{
		suelo.transform.localScale = new Vector3(wall.transform.localScale.x, 1, wall.transform.localScale.z);
		materialSuelo.mainTextureScale = new Vector2(wall.transform.localScale.x, wall.transform.localScale.z);
		suelo.GetComponent<MeshRenderer>().material = materialSuelo;

		for (int i = 0; i < 2 * rows + 1; i++)
		{
			for (int u = 0; u < 2 * columns + 1; u++)
			{
				if (squares[i][u].getIsWall())
				{
					Instantiate(wall, new Vector3(i * wall.transform.localScale.x, (wall.transform.localScale.y / 2) + suelo.transform.localScale.y, u * wall.transform.localScale.z), Quaternion.identity);
				}

				//suelo.transform.position = new Vector3(i * wall.transform.localScale.x, (suelo.transform.localScale.y / 2), u * wall.transform.localScale.z);
				//Instantiate(suelo, suelo.transform.position, Quaternion.identity);

				listOfSuelos.Add(Instantiate(suelo, new Vector3(i * wall.transform.localScale.x, (suelo.transform.localScale.y / 2), u * wall.transform.localScale.z), Quaternion.identity));
			}
		}
	}
}


