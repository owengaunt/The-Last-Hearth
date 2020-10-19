using UnityEngine;

public class MassiveFoliageMeshPlacer : MonoBehaviour
{
    [Header("Step 1: Attach The Terrain")]
    public Terrain terrain;
    [Header("Step 2: Select Target Texture and Source")]
    public int targetTextureLayer = 1;
    public int targetDetailsLayer = 0;
    [Header("Step 3: Adjust Parameters")]
    public bool removePastDetails = true;
    public enum FillType { full, sides };
    public FillType fillType = FillType.full;
    const int MAX_AMOUNT = 10;
    [Range(1, MAX_AMOUNT)]
    public int amount=1;
    [Range(0, 1)]
    public float fallOff = 0.8f;

    public void AddDetails()//Add details by the selected texture and selected presents
    {
        if (removePastDetails)//If player checked remove past details
        {
            CleanDetails(targetDetailsLayer);
        }
        #region Get Terrain Data
        TerrainData terrainData = terrain.terrainData;
        float[,,] alphaMapData = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);//The terrain texture maps
        int[,] detailsMap = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, targetDetailsLayer);//The terrain detail maps(Where everything is placed)
        #endregion
        #region Convert texture map data length to details map length
        Texture2D temp = new Texture2D(terrainData.alphamapWidth, terrainData.alphamapHeight);
        for (int x = 0; x < terrainData.alphamapWidth; x++)
        {
            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                temp.SetPixel(x, y, new Color(0, 0, 0, alphaMapData[x, y, targetTextureLayer]));
            }
        }
        temp.Apply();
        int targetLength = detailsMap.GetLength(0);
        TextureScale.Bilinear(temp, targetLength, targetLength);
        temp.Apply();
        #endregion
        #region Apply detail data by user presents and selected texture
        detailsMap = new int[targetLength, targetLength];
        for (int x = 0; x < targetLength; x+= 1)
        {
            for (int y = 0; y < targetLength; y+= 1)
            {
                if (fillType== FillType.full)
                {
                    detailsMap[x, y] = (temp.GetPixel(x,y).a> fallOff ? amount:0);
                }
                else if(temp.GetPixel(x, y).a == 1)
                {
                    int totalPoints = 0;
                    totalPoints += (int)(temp.GetPixel(x-1, y).a < 1 ? 1:0);
                    totalPoints += (int)(temp.GetPixel(x+1, y).a < 1 ? 1 : 0);
                    totalPoints += (int)(temp.GetPixel(x, y-1).a < 1 ? 1 : 0);
                    totalPoints += (int)(temp.GetPixel(x, y+1).a < 1 ? 1 : 0);
                    if (totalPoints>0)
                    {
                        detailsMap[x, y] = amount;
                    }
                }
            }
        }
        #endregion
        terrainData.SetDetailLayer(0, 0, targetDetailsLayer, detailsMap);
    }
    public void CleanDetails(int layer)//Clear the details of selected layer.
    {
        TerrainData terrainData = terrain.terrainData;
        for (int i = 0; i < terrainData.detailPrototypes.Length; i++)
        {
            int[,] map = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, layer==-1?i:layer);

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = 0;
                }
            }
            terrainData.SetDetailLayer(0, 0, layer == -1 ? i : layer, map);
            if (layer!=-1)
            {
                return;
            }
        }
    }
}
