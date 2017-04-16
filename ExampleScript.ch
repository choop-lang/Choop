/* This comment spans
multiple lines */

// Single line comment
const myConst = 12; 

var myVar = 10; // Inline comment

[ResourcesFile(mySprite.res)]
[Import(myModule)]
sprite mySprite {
	event greenFlag() {
		myVoid(10, 20);
	}

	event keyPressed<space>() {
		{
			// Mini scope
			var test = 2;
		}
		
		// Should throw a compile error: (out of scope)
		// test = 3 
	}

	atomic void myVoid(param1, param2, param3 = 10) {
		SayHello();
		
		someVar = 0;
		
		if (param1 > param2) {
			myVar = param1 - param2;
		} elseif (param2 > param1) {
			myVar = param2 - param1;
		} else {
			myVar = 0;
			someVar = 1;
		}
	}
}

module myModule {
	var someVar = 10;
	
	inline void SayHello() {
		Say("Hello");
	}
}