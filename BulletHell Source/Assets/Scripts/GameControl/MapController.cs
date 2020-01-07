using UnityEngine;

public class MapController : MonoBehaviour
{
    //Public shit
    public GameObject mapTile; //Map tile to spawn #TODO Change to random tile spawn (based on lvl environment)

    //Private shit
    private Transform mainCam; //Transform of the main camera in the scene
    private GameObject[] mapTiles; //Array of the 3 tiles that will be on the map at one time
    private float distFromCam; //Distance from cam to spawn map tile
    private Vector2 tileBounds;

    // Start is called before the first frame update
    void Start()
    {
        if(mapTile == null)
        {
            Debug.LogError("No mapTile selected");
            return;
        }
        mainCam = GameObject.Find("Main Camera").transform;
        tileBounds = mapTile.GetComponent<SpriteRenderer>().bounds.size;

        mapTiles = new GameObject[3];

        mapTiles[0] = Instantiate(mapTile, new Vector3(mainCam.position.x, mainCam.position.y, 0), mapTile.transform.rotation);

        Vector3 mapSpawn = new Vector3(mapTiles[0].transform.position.x + tileBounds.x, mapTiles[0].transform.position.y, mapTiles[0].transform.position.z);

        mapTiles[1] = Instantiate(mapTile, mapSpawn, mapTile.transform.rotation);

        mapSpawn = new Vector3(mapTiles[0].transform.position.x - tileBounds.x, mapTiles[0].transform.position.y, mapTiles[0].transform.position.z);

        mapTiles[2] = Instantiate(mapTile, mapSpawn, mapTile.transform.rotation);

        distFromCam = tileBounds.x + (tileBounds.x / 2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTileDistFromCam();
    }

    void CheckTileDistFromCam()
    {
        for(int i = 0; i < 3; i++)
        {
            if(mapTiles[i].transform.position.x < mainCam.position.x && Vector3.Distance(mainCam.position, mapTiles[i].transform.position) >= distFromCam)
            {
                GameObject spawnTile = null;
                for(int j = 0; j < 3; j++)
                {
                    if(j == 0)
                    {
                        spawnTile = mapTiles[j];
                    }
                    else if(mapTiles[j].transform.position.x > spawnTile.transform.position.x)
                    {
                        spawnTile = mapTiles[j];
                    }
                }
                Destroy(mapTiles[i]);
                Vector3 mapSpawn = new Vector3(spawnTile.transform.position.x + tileBounds.x, spawnTile.transform.position.y, spawnTile.transform.position.z);
                mapTiles[i] = Instantiate(mapTile, mapSpawn, mapTile.transform.rotation);
                mapTiles[i].transform.parent = GameObject.Find("Map_Tiles").transform;
            }
        }
    }
}
