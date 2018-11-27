float x, y, z;

 void setup() {
	size(512, 512);
}

void draw() {


if (mousePressed == true) {
	number();
	println(x);

}

	

}   




void number () {

z =  random(1, 7);
y = round(z);
if (y == 1) {
	x= 1;
}



if (y == 2 ||  y == 5 ) {
	x= 2;
}



if (y == 3 || y == 6 || y == 7) 
{
x = 3;
}

if (y == 4) {
	x=4;
}
}