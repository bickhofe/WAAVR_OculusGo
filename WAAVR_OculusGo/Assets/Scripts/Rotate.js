#pragma strict

//rotatation speed
var xSpeed:int;
var ySpeed:int;


function Update () {
	// rotate
	transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime,0);
}