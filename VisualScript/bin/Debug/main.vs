// fixed set value to array

// package com.test; // not implement
// using com.namespace.pc; // implement (50/50)

function Main(args...)
{
	println("Hola Mundo");
	
	var testABS = Math.abs(-10);
	println("testABS: " + testABS);
}

// While Commands ///////////////////////////////////////////////////////////////////////////
var count = 0;
while (true)
{
	if (count >= 10) break;
	
	println("while commands is " + count);
	
	count++;
}
println("fin del while");

println();
println("Switch Commands");

// Switch Commands ///////////////////////////////////////////////////////////////////////////
switch (count)
{
	case 10:
		println("count == 10");
		break;
	case 11:
		println("count == 11");
		break;
	default:
		println("default: count is " + count);
		break;
}
for (var i = 0; i < 10; i++)
{
	println("value 'i' is " + i);
}
println("fin del for");
println();


// Switch Commands ///////////////////////////////////////////////////////////////////////////
try {
	a = 10;
	println("Hola Mundo: " + a);
}
catch(err) {
	println("error: " + err);
}
finally {
	println("El try a terminado con exito!!!!");
}
println("Despues del try");

















