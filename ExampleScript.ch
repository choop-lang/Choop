/* This comment spans
multiple lines */

// Single line comment
const MyConst = 12;

var MyVar = 10; // Inline comment

[ResourcesFile(mySprite.res)]
[Import(myModule)]
sprite Sprite1 {
	event GreenFlag() {
		MyVoid(10, 20);
	}

	event KeyPressed<space>() {
		{
			// Mini scope
			var test = 2;
		}
		
		// Should throw a compile error: (out of scope)
		// test = 3;

		if (GetInput()) {
			Move(10);
		}
	}

	atomic void MyVoid(param1, param2, param3 = 10) {
		SayHello();
		
		SomeVar = 0;
		
		if (param1 > param2) {
			MyVar = param1 - param2;
		} elseif (param2 > param1) {
			MyVar = param2 - param1;
		} else {
			MyVar = 0;
			SomeVar = 1;
		}
	}
}

module MyModule {
	var SomeVar = 10;
	
	inline void SayHello() {
		Say("Hello");
	}

	function GetInput() {
		var answer = Ask("Are you a person?");

		switch (answer) {
			case "Yes":
				return true;
			case "No":
				return false;
			case default:
				return false;
		}
	}
}