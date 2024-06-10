using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VehicleSelection : MonoBehaviour
{
    public GameObject[] vehicle;
    public Button next;
    public Button prev;
    [SerializeField] private int index;
    public static VehicleSelection Instance;
    public int temp;
   
    void Start()
    {
        index = 0; // Default to 0 if no index is saved
        UpdateVehicleSelection();
        SaveSelection();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        next.interactable = index < vehicle.Length - 1;
        prev.interactable = index > 0;
        temp = index;
    }

    public void Next()
    {
        index++;
        UpdateVehicleSelection();
        SaveSelection();
    }

    public void Prev()
    {
        index--;
        UpdateVehicleSelection();
        SaveSelection();
    }

    public void Select()
    {
        //VehicleManager.Instance.SelectVehicle(index);
        SceneManager.LoadSceneAsync("Pizza Boy");
    }

    private void UpdateVehicleSelection()
    {
        for (int i = 0; i < vehicle.Length; i++)
        {
            vehicle[i].SetActive(i == index);
        }
    }

    private void SaveSelection()
    {
        PlayerPrefs.SetInt("vehicleIndex", index);
        PlayerPrefs.Save();
    }

    public int CurrentPosition()
    {
        return temp;
    }
}
