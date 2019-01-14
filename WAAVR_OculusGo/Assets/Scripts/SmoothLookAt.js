var target : Transform;
var damping = 6.0;
var smooth = true;

function Update () {
	if (target) {
		transform.LookAt(target);
	}
}