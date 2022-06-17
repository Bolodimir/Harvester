using UnityEngine;

public class GridModel : MonoBehaviour
{
    [SerializeField] private GridView view;
    [SerializeField] private GameObject[] MapItems;
    [SerializeField] private int NonBuildings;  // Buildings are put after non-buildings
                                                // Number of non-buildings is specified
    [SerializeField] private float[] _resourcesWeights;
    [SerializeField] private float RandomResourcePlacingPeriod;

    private float LastPlaced;
    private int NumberOfObjectsOnTheMap;
    private MapItem[,] Map;

    void Start()
    {
        NumberOfObjectsOnTheMap = 0;
        Map = new MapItem[11,11];
        BuildRandomResource();
    }

    void Update()
    {
        float speedModifier = 1 + Stats.Instance.Researches.ResourceSpawnFrequency.GetValue();
        if ((RandomResourcePlacingPeriod /  speedModifier) < Time.time - LastPlaced)
        {
            BuildRandomResource();
        }
    }

    private void BuildRandomResource() 
    {
        if (NumberOfObjectsOnTheMap >= Map.GetLength(0) * Map.GetLength(1)) return; // if map is full

        float weightsSum = 0;
        foreach (float weight in _resourcesWeights) weightsSum += weight;
        float randomWeight = Random.Range(0, weightsSum);
        int randomIndex = 0;
        for(int i = 0; i < NonBuildings; i++)
        {
            randomWeight -= _resourcesWeights[i];
            if (randomWeight <= 0)
            {
                randomIndex = i;
                break;
            }
        }

        Vector2 gridPos = new Vector2(Random.Range(0, Map.GetLength(0)), Random.Range(0, Map.GetLength(1)));
        while(Map[(int)gridPos.x, (int)gridPos.y] != null)
        {
            gridPos = new Vector2(Random.Range(0, Map.GetLength(0)), Random.Range(0, Map.GetLength(1)));
        }
        BuildItem(randomIndex, gridPos);
        LastPlaced = Time.time;
    }

    public bool CellIsEmpty(Vector2 position)
    {
        if (Map[(int)position.x, (int)position.y] == null) return true;
        return false;
    }

    public void BuildItem(string Name, Vector2 pos)
    {
        for(int i = 0; i < MapItems.Length; i++)
        {
            if(MapItems[i].GetComponent<MapItem>().Name == Name)
            {
                BuildItem(i,pos);
            }
        }
    }

    public void BuildItem(int index, Vector2 pos)
    {
        GameObject newBuild = Instantiate(MapItems[index]);
        newBuild.GetComponentInChildren<MapItemView>().ChangeIsToBuild();
        view.PlaceObject(pos, newBuild);
        Map[(int)pos.x, (int)pos.y] = newBuild.GetComponent<MapItem>();
        if(Map[(int)pos.x, (int)pos.y] is  Consumable)
        {
            Consumable res = (Consumable)Map[(int)pos.x, (int)pos.y];
            res.SetView(view);
        }
        NumberOfObjectsOnTheMap++;
    }

    public GameObject GetItem(string Name)
    {
        for (int i = 0; i < MapItems.Length; i++)
        {
            if (MapItems[i].GetComponent<MapItem>().Name == Name)
            {
                return MapItems[i];
            }
        }
        return null;
    }

    public bool WithinBoundaries(Vector2 gridPos)
    {
        if (gridPos.x >= 0 && gridPos.x < Map.GetLength(0) && gridPos.y >= 0 && gridPos.y < Map.GetLength(1)) return true;
        else return false;
    }

    public void ExpandGrid(Vector2 expansion)
    {
        view.ExpandGrid(expansion);
        MapItem[,] newMap = new MapItem[Map.GetLength(0) + Mathf.Abs((int)expansion.x),
                                        Map.GetLength(1) + Mathf.Abs((int)expansion.y)];
        int HorShift = Mathf.Abs((int)expansion.x);
        int VerShift = (int)expansion.y;

        if (expansion.x > 0) HorShift = 0;
        if (expansion.y < 0) VerShift = 0;

        for(int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                newMap[HorShift + i, VerShift + j] = Map[i, j];
            }
        }
        Map = newMap;
    }

    public Building[] GetBuildings()
    {
        Building[] result = new Building[MapItems.Length - NonBuildings];
        for(int i = 0; i< result.Length; i++)
        {
            result[i] = MapItems[i + NonBuildings].GetComponent<Building>();
        }
        return result;
    }

    public void DestroyBuilding(Vector2 gridPos)
    {
        Map[(int)gridPos.x, (int)gridPos.y] = null;
        NumberOfObjectsOnTheMap--;
    }

    public float GetSpawnProgress()
    {
        float speedModifier = 1 + Stats.Instance.Researches.ResourceSpawnFrequency.GetValue();
        return (Time.time - LastPlaced) / (RandomResourcePlacingPeriod / speedModifier);
    }
}
