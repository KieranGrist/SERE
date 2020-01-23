using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Entity : MonoBehaviour
{
    readonly string[] _firstNames = new string[]
{
        "Kieran",
        "Alex",
        "Gavin",
        "Lawrence",
       "Alice",
        "Sophie",
        "Iona",
        "Chloe",
        "Lucia",
        "Dale",
        "Georgina",
        "Nicole",
        "Kara",
       "Hailee",
       "Helen",
       "Emily",
       "Liberty",
       "Faye",
       "Carrie",
       "Elsie",
       "Crystal",
       "Maria",
       "Ayala",
       "Alanah",
       "Amie",
       "Jack",
       "Ben",
       "Adam",
       "Tegan",
       "Edan",
       "Alison",
       "Merle",
       "Aden",
       "Allyson",
       "Lyndsey",
       "Stacia",
        "Lauren",
        "Sarah",
        "Matt",
        "Paul",
        "Maddie",
        "Lando",
        "Lewis",
        "Sebastian",
        "Carlos",
        "Sergio",
        "Piere",
        "Nicco",
        "Esteban",
        "Robert",
        "George",
        "Charles",
        "Max",
        "Alex",
        "Lance",
        "Kevin",
        "Roman",
        "Jules",
        "Peter",
        "Mikey",
        "Valtteri",
        "Daniel"


};
    /// <summary>
    /// Last Name List
    /// </summary>
    readonly string[] _lastNames = new string[]
     {
    "Stainton",
   "Peters",
   "Stephenson",
    "Field",
   "Evans",
    "Mitchel",
    "Woods",
    "Grist",
    "Mitchell",
    "Newman",
    "Davey",
    "Brown",
    "Shade",
    "Rhodes",
    "Burke",
    "Howells",
    "Morgan",
    "Holland",
    "Flynn",
    "Watts",
    "Knight",
    "Bryant",
    "Leigh",
    "Gibson",
    "Gallagher",
    "Kelly",
    "Smith",
    "Holmes",
    "Bishop",
    "Dennis",
    "Hansen",
    "Spencer",
    "Baldwin",
    "Wilkinson",
    "Wade",
    "Ryan",
    "Williams",
    "Griffiths",
    "Moss",
    "Austin",
    "Hamilton",
    "Vettel",
    "Bottas",
    "Verstappen",
    "Albon",
    "Leclerc",
    "Magnussen",
    "Grosjean",
    "Bottas",
    "Gasly",
    "Ricciardo",
    "Hulkenberg",
    "Norris",
    "Sainz",
    "Perez",
    "Stroll",
    "Raikkonen",
    "Giovinazzi",
    "Albon",
    "Kvyat",
    "Russell",
    "Kubica"
   };
    public int Score;
    public Text t_Health;
    public float Health = 100;
    public float Speed =5;
    public float NextTracer =0;
    protected float gap = 0;
    public float BulletGap = 0.2f;
    public float Sensertivity = 1;
    protected GameObject Tracer;
    protected GameObject Bullet;
    public float BulletMass = 10;
    public float BulletInitialSpeed = 5700;
   

    protected Renderer rend;
    // Start is called before the first frame update
 public   virtual void Start()
    {
        BulletInitialSpeed = 7000;
        Tracer = Resources.Load("Tracer", typeof(GameObject)) as GameObject;
        Bullet = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
        name = _firstNames[Random.Range(0, _firstNames.Length)] + " " + _lastNames[Random.Range(0, _lastNames.Length)];
        Health = 100;
    }
    // Start is called before the first frame update

    // Update is called once per frame
   public virtual void Update()
    {

        t_Health.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
  Camera.main.transform.rotation * Vector3.up);
        t_Health.transform.eulerAngles = (new Vector3(0, t_Health.transform.eulerAngles.y, 0));
        t_Health.text = "Health : " + Health;
        gap += Time.deltaTime;
        if (transform.position.y < -1)
        {
            Health = 0;
        }
        if (Health <= 0)
        {
            RespawnHandler.respawn.HandleRespawn(gameObject);
            Health = 100;
        }
    }
    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }
   public void Fire(float Mass, float InitialSpeed)
    {
   
        GameObject go;
        NextTracer++;
        if (NextTracer >= 2)
        {
            NextTracer = 0;
            go = Instantiate(Tracer, transform.position, transform.rotation);
        }
        else
        {
            go = Instantiate(Bullet, transform.position, transform.rotation);
        }

        go.transform.position += transform.forward * 6.1f;
        go.transform.position += new Vector3(0, 0, 0);
        go.AddComponent<Rigidbody>();
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * InitialSpeed);
        rb.mass = Mass;
        go.GetComponent<Bullet>().Owner = gameObject;
    }
}
