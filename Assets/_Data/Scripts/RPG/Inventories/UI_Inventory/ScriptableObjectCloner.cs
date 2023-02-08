using UnityEngine;
public class ScriptableObjectCloner : MonoBehaviour
{
    [SerializeField] ScriptableObject objectToClone;
    ScriptableObject clonedObject;

    void Start()
    {
        clonedObject = Instantiate(objectToClone);
    }
}