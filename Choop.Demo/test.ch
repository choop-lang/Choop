// This is a single-line comment

/* This
   is
   a
   multiline
   comment
*/

var globalVar = true;
const Pi = 3.141593;

sprite MySprite {
	const NumericTest = -3.1e-2; // Inline comment

	var BoolTest1 = false;
	var BoolTest2 = true;

	var NumberTest1 = -13;
	var NumberTest2 = 3.159;
	var NumberTest3 = 27e3;

	var StringTest1 = " !#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
	var StringTest2 = ' !#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~';

	void Test1() {
		var test = "27";
		Test2(3);
		Test3("test", true);
	}

	atomic void Test2(param1) {
		const Scoped_Const = 43;
		var test = Test4(3, Test4(2, 5));
	}

	void Test3(param1, param2 = "test1") {
	}

	atomic function Test4(param1, param2 = 3, param = -4e2) {
	}
	
	event GreenFlag() {
	}

	event KeyPressed<"space">() {
	}
}