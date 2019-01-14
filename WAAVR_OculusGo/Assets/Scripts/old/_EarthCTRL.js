#pragma strict

//rotatation speed
var xSpeed:int;
var ySpeed:int;

// roid
var Roid : Transform;
var distRoid:int;

// rocket
var Rocket:Transform;

//score
var theScore:int;
var score:GameObject;
score = GameObject.Find("score");
;

function Start () {
	// add first roid
	AddRoid();
}

function Update () {
	// rotate
	transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime,0);
	
	if (Input.GetKeyDown ("space")){
		// add first naut
		AddRocket();
	}
	// counter
	theScore++;
	score.GetComponent(TextMesh).text = "Your score:\n"+theScore;
}

// new roid
function AddRoid(){
	var angleInRadians : float = Random.Range(0, 2*Mathf.PI);
	var x : float = Mathf.Cos(angleInRadians);
	var y : float = Mathf.Sin(angleInRadians);
	Instantiate(Roid, Vector3 (x*distRoid, y*distRoid, 0), Quaternion.identity);
}

// new rocket
function AddRocket(){
	Instantiate(Rocket, Vector3.zero, Quaternion.identity);
}

// external call
function MyCall(param : String)
{
    Debug.Log(param);
    AddRocket();
}

// javascript function for webpage

//	function SaySomethingToUnity()
//	{
//		u.getUnity().SendMessage("earth", "MyCall", "Hello from a web page!");
//	}





