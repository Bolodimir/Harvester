using UnityEngine;

[System.Serializable]
public struct NamedSprite
{
    public string name;
    public Sprite sprite;
}

public class SpriteStorage : MonoBehaviour
{
    public static SpriteStorage Instance;

    [SerializeField] private NamedSprite[] Storage;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetSpriteByName(string name)
    {
        for(int i = 0; i< Storage.Length; i++)
        {
            if (string.Equals(Storage[i].name, name))
                return Storage[i].sprite;
        }
        return null;
    }
}
