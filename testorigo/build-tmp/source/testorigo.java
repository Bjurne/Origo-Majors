import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class testorigo extends PApplet {

float x, y, z;

 public void setup() {
	
}

public void draw() {


if (mousePressed == true) {
	number();
	println(x);

}

	

}   




public void number () {

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
  public void settings() { 	size(512, 512); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "testorigo" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
