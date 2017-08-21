[MetaFile("MySprite.chd")]
sprite MySprite {
	using MyModule;

	const num NumConst = -3.1e-2; // Inline comment
	const string StringConst = "test";

	var EmptyTest;

	var BoolTest1 = false;
	var BoolTest2 = true;

	var NumberTest1 = -13;
	var NumberTest2 = 3.159;
	var NumberTest3 = 27e3;

	var StringTest1 = " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
	var StringTest2 = ' !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~';
	string NewLineTest = "Hello\nworld!";

	list[] ListTest1 = {1, 2, 3};
	list[3] ListTest2;
	list<num>[] ListTest3;
	list<string>[] ListTest4 = {'1', '2', '3'};

	inline void Test1() {
		var test = 27;
		string test2 = "hello";

		Test2(3);
		Test3("test", true);

		test = 12;
		test--;
		test++;
		test += 1;
		test -= 1;

		test2 .= " world!";
		test2 = "hello" . (" world" . "!");

		if (false) {
			return;
		}

		if (true || false) {
			bool testing = true;
		} else if (IsGreaterThan(5, 2)) {
			return;
		} else {
			test = 20;
		}

		var newtest;
		if (!true) {
			newtest = 1;
		} else if (false) {
			newtest = 2;
		} else if (true) {
			newtest = (3);
		}

		switch (newtest) {
			case 1:
			case 2:
				Test2(1);
				break;
			case "3":
				Test2(2);
				break;
			case 4:
				break;
			case 5:
				return;
			default:
				Test2(3);
				break;
		}

		var count = 0;
		repeat(10) {
			count++;
		}
		
		inline repeat(10) {
			count--;
		}

		while (count < 10) {
			count++;
		}
	}

	atomic void Test2(bool param1) {
		var test = Test4(3, Test4(2, 5));

		{
			// Mini-scope
			num testscope = 3;
		}
	}

	void Test3(param1, string param2 = "test1") {
		var simple = Test4(1);
	}

	atomic function Test4(param1, num param2 = 3, num param3 = -4e2) {
		if (param2 + param3 >= 10) {
			return param3 - param2;
		}

		return -param1;
	}
	
	event GreenFlag() {
		array[2] myArray = {1, 2};
		num[2] numArray = {-5e2, 12.5};

		myArray[0] = -3;

		myArray = {3, 4};

		var total = 0;
		foreach (var item in myArray) {
			total = total + item;
		}
	}

	event KeyPressed<"space">() {
		var a = 5;
		var b = 4;
		var res = 0;
		for (var i = 1 to a) {
			res += b;
		}
	}

	event Clicked() {
		forever {
			Wait(10);
		}
	}

	event BackdropChanged<"Thumb">() {
		var bidmas = 8 - 4 * (1 + 2);
		Say(bidmas);
	}

	event MessageReceived<"message1">() {
		string[4] strArray;
		bool[3] boolArray = {true, false, true};

		foreach (bool item in boolArray) {
			
		}
	}

	event Cloned() {
		for (num i = 5 to 2 step -1) {
		
		}
	}

	event LoudnessGreaterThan<50>() {
	}

	event TimerGreaterThan<10>() {
	}

	event VideoMotionGreaterThan<30>() {
	}
}