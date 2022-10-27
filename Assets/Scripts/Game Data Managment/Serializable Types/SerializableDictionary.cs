using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();
    public void OnBeforeSerialize()
    {

        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // Update is called once per frame
    public void OnAfterDeserialize()
    {

        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("tried Deserializing a SerializableDictionary, but the ammount of keys does not match the number of values, something went wrong" + "Keys: " + keys.Count + "Values: " + values.Count);

        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
