
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class Vehicle_selection : MonoBehaviour
{
   
    public  GameObject Level_menu;
    public  GameObject[] vehicle = new GameObject[8];
    public   GameObject[] vehicles_prefab = new GameObject[8];
    public int[] vehicle_price = new int[8];
    public GameObject id;
 
   
    private Vehicle_selection instance;
    [Header("button")]
    public TextMeshProUGUI cashtext;
    public GameObject nofunds;
    public GameObject unlock_button;
    public TextMeshProUGUI cost;
    public GameObject eqip_button;
    [Header("transcition")]
    public GameObject vehicleselection;
    public Camera cam;
    public GameObject point;

    private void Awake()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        
        // Ensure only one instance of this script exists
        if (instance == null&&activeScene==0)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Debug.Log("null");
        }

    }

    public  int v = 0;

    public void Start()
    {
        vehicle[0].SetActive(true);
        
        Player_Data.update_vehicles(vehicles_prefab[0].GetInstanceID());
        eqip_or_unlock(v);


    }
    public void right()
    {
        v = v + 1;
        if (v==vehicle.Length)
        {
            v = 0;
        }
       
        Debug.Log(v);
        vehicle_spawn(v);
        eqip_or_unlock(v);
    }
    public void left()
    {
        if (v == 0)
        {
            v = vehicle.Length;
        }
        v = v - 1;
        Debug.Log(v);
        vehicle_spawn_left(v);
        eqip_or_unlock(v);
    }
    public void vehicle_spawn(int n)
    {


        for (int i = n; i <= n; i++)
        {
            if(i== 0)
            {
                vehicle[vehicle.Length-1].SetActive(false);
                vehicle[i].SetActive(true);
            }
            else if(i == vehicle.Length)
            {

                v = 0;
                vehicle[i - 1].SetActive(false);
                vehicle[0].SetActive(true);
                break;
            }
            else
            {

                vehicle[i - 1].SetActive(false);
                vehicle[i].SetActive(true);

            }


        }
    }
    public void vehicle_spawn_left(int n)
    {


        for (int i = n; i >= n; i--)
        {
            if (i == vehicle.Length - 1)
            {


                vehicle[0].SetActive(false);
                vehicle[i].SetActive(true);
            }
            else
            {

                vehicle[i + 1].SetActive(false);
                vehicle[i].SetActive(true);
            }


        }
    }
    public  void eqip()
    {
        if (vehicles_prefab[v] != null)
            Level_Data.update_vehicle(vehicles_prefab[v]);
        else
            Debug.Log("prefab not");
        vehicleselection.SetActive(false);
        cam.transform.DOMove(point.transform.position, 2);
        cam.transform.rotation.SetLookRotation(new Vector3(0, 0, 0));
        Invoke("trans", 2);
        
        
         


    }
    public void eqip_or_unlock( int v)
    {
        nofunds.SetActive(false);
        bool isowned = Player_Data.isowned(vehicles_prefab[v].GetInstanceID());
        if (isowned)
        {
            unlock_button.SetActive(false);
            eqip_button.SetActive(true);
        }
        else
        {
            
            eqip_button.SetActive(false);
            unlock_button.SetActive(true);
            display_cost();
        }
            

    }
    public void unlock()
    {
       
        int cost = vehicle_price[v];
        if(Player_Data.cash >= cost)
        {

            Player_Data.update_data(-cost);
           
            Player_Data.update_vehicles(vehicles_prefab[v].GetInstanceID());
            Update_cash();
            unlock_button.SetActive(false);
            eqip_button.SetActive(true);
        }
        else
        {
            nofunds.SetActive(true);
            
          

            
        }
    
    }
    public void Update_cash()
    {
        int cash = Player_Data.cash;
        Debug.Log($"Cash: {cash}");
        cashtext.text = cash.ToString();
    }
    public void display_cost()
    {
        cost.text = vehicle_price[v].ToString();
    }
    public void trans()
        {
        Level_menu.SetActive(true);
        gameObject.SetActive(false);
    }
   
   


}
