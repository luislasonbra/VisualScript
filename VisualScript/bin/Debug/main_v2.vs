println("_NAME: " + APP._NAME);
println("_VERSION: " + APP._VERSION);

var a = 10, b = 20, c = 30, d = 40;
println("value 'a' : " + a);
println("value 'b' : " + b);
println("value 'c' : " + c);
println("value 'd' : " + d);

println();
println("math.E is " + Math.E);

println();
var ms = "luis".length; // string.length("luis");
println("value 'ms' is " + "'" + ms + "'");

var ms2 = string.length("luis");
println("value 'ms2' is " + "'" + ms2 + "'");

var ab = 10.5, bb = 0.9;
if (ab > 10.5) println("isTrue");

var z = ab == 10.5;
println("value 'z' is " + z);

var vsA, vsB, vsD;
vsA = vsB = vsD = 10;
println("'vsA' is " + vsA);
println("'vsB' is " + vsB);
println("'vsD' is " + vsD);
println();
println("Function Command");

// function command ///////////////////////////////////////////////////////////////////
// llamada anbiguas entre funciones
function callFunction(st)
{
	st--;
	if (st > 0)
	{
		println("Hola Mundo!!!");
		callFunction(st);
	}
}
callFunction(10);

println();
println("For Command");

// for command ///////////////////////////////////////////////////////////////////
for (var i = 0; i < 20; i++)
{
	println("Hola Mundo!!!");
}

println();
println("Foreach Command");

// foreach command ///////////////////////////////////////////////////////////////////
var stri = "luislasonbra";
foreach (var ch in stri)
{
	println("ch: " + ch);
}

println();
println("While Command");

// while command ///////////////////////////////////////////////////////////////////
var j = 10;
var isTrue = true;
while (isTrue)
{
	if (j > 0)
		println("'j' is " + j);
	else
		isTrue = false;
	
	j--;
}
var mp2 = IO;
mp2.write("");
mp2.write("HolaMundo");

println("IO type: " + mp2);

enum dateq
{
	name,
	age
}
var Dateq = dateq.name;
println("Dateq is " + dateq.name);

println();
println("Function param");

// function commands ///////////////////////////////////////////////////////////////////
function say(param...) // ...
{
	if (param.length == 0)
	{
		println("say()");
	}
	else
	{
		println("say(par)");
	}
	
	/*
	println("list: " + param[3].name);
	println("param: " + param);
	
	//if (param.length > 1) println("hola_length");
	
	println("say()");
	println("say(par)");
	
	println();
	if (param.length == 4) println("hola_length");
	
	println();
	println("param.length: " + param.length);
	*/
}

say();
say("luis", "miguel", "paulino", dateq);

function Main(args...)
{
	println("Hola Main ////////////////////////////////////////////////////////");
	if (args.length == 0) println("no hay archivos");
	
	if (args.length > 0)
	{
		println();
		for (var i = 0; i < args.length; i++)
		{
			println("PathFile[" + i + "]: " + args[i] + "\n");
		}
	}
	
	println("//////////////////////////////////////////////////////////////////");
	println();
}
println();
println("array commands");

// array commands ///////////////////////////////////////////////////////////////////
var array = { { 10, 20, 30 }, 20, 50 };
println("array: " + (typeof(array[0]) == "array" ? true : false) );

println();
println("class Commands");

// class commands ///////////////////////////////////////////////////////////////////
class Player
{
	var hola;
	var year;
	
	function Player(param...)
	{
		var luis = 10;
		this.classPrintln(luis);
	}
	
	function jp()
	{
		println("Hola Mundo");
	}
	
	function classPrintln(str)
	{
		println("class_Player: " + str);
	}
}

var player = new Player();
player.hola = "Hola Mundo";
println("player.hola: " + player.hola);
player.classPrintln(100);
println("Player type: " + typeof player);

println();
println("Math Commands");

// Math Commands ///////////////////////////////////////////////////////////////////

// Math Object Properties
println("Math.PI: " + Math.PI); // Returns PI (approx. 3.14)
println("Math.E: " + Math.E); // Returns Euler's number (approx. 2.718)
println("Math.LN2: " + Math.LN2); // Returns the natural logarithm of 2 (approx. 0.693)
println("Math.LN10: " + Math.LN10); // Returns the natural logarithm of 10 (approx. 2.302)
println("Math.LN2E: " + Math.LN2E); // Returns the base-2 logarithm of E (approx. 1.442)
println("Math.LN10E: " + Math.LN10E); // Returns the base-10 logarithm of E (approx. 0.434)
println("Math.SQRT1_2: " + Math.SQRT1_2); // Returns the square root of 1/2 (approx. 0.707)
println("Math.SQRT2: " + Math.SQRT2); // Returns the square root of 2 (approx. 1.414)

println();

// Math Object Methods
println("Math.abs(x): " + Math.abs(-10) ); // Returns the absolute value of x
println("Math.acos(x): " + Math.acos(10) ); // Returns the arccosine of x, in radians
println("Math.asin(x): " + Math.asin(10) ); // Returns the arcsine of x, in radians
println("Math.atan(x): " + Math.atan(10) ); // Returns the arctangent of x as a numeric value between -PI/2 and PI/2 radians
println("Math.atan2(y, x): " + Math.atan2(10, 20) ); // Returns the arctangent of the quotient of its arguments
println("Math.ceil(x): " + Math.ceil(10) ); // Returns x, rounded upwards to the nearest integer
println("Math.cos(x): " + Math.cos(10) ); // Returns the cosine of x (x is in radians)
println("Math.exp(x): " + Math.exp(10) ); // Returns the value of Ex
println("Math.floor(x): " + Math.floor(10) ); // Returns x, rounded downwards to the nearest integer
println("Math.log(x): " + Math.log(10) ); // Returns the natural logarithm (base E) of x
println("Math.max(x, y): " + Math.max(10, 20) ); // Returns the number with the highest value
println("Math.min(x, y): " + Math.min(10, 20) ); // Returns the number with the lowest value
println("Math.pow(x, y): " + Math.pow(10, 20) ); // Returns the value of x to the power of y
println("Math.random(min, max): " + Math.random(0, 10) ); // Returns a random number
println("Math.round(x): " + Math.round(10.5) ); // Rounds x to the nearest integer
println("Math.sin(x): " + Math.sin(10) ); // Returns the sine of x (x is in radians)
println("Math.sqrt(x): " + Math.sqrt(10) ); // Returns the square root of x
println("Math.tan(x): " + Math.tan(10) ); // Returns the tangent of an angle

function testReturn(value) { return value * 10; }

println();
println( "testReturn: " + testReturn(20) );

for (var i = 0; i < 10; i++)
{
	if (i == 3) break;
	println("'i' is " + i);
}
println();
println("salimos del for");
for (var i = 0; i < 10; i++)
{
	if (i == 3) break;
	println("'i' is " + i);
}
println("salimos del for2");
println();
var plus = 0;
while(true)
{
	plus++;
	println("While Loop: value 'plus' is " + plus);
	if (plus > 10) break;
}
println("salimos del while");
plus = 0;
while(true)
{
	plus++;
	println("While Loop: value 'plus' is " + plus);
	if (plus > 10) break;
}
println("salimos del while2");
for (var i = 0; i < 10; i++)
{
	if (i == 3) break;
	println("'i' is " + i);
}
println("salimos del for3");
var nodes = { "hola", "mundo", "ja", "ja2" };
foreach (var item in nodes)
{
	if (item == "ja") break;
	println("item is " + item);
}
println("salimos del foreach");
foreach (var item in nodes)
{
	if (item == "ja") break;
	println("item is " + item);
}
println("salimos del foreach2");