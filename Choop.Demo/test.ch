// This is a comment
sprite MySprite {
	const NumericTest = -3.1e-2;

	var BoolTest1 = false;
	var BoolTest2 = true;

	var NumberTest1 = -13;
	var NumberTest2 = 3.159;
	var NumberTest3 = 27e3;

	var StringTest1 = " !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
	var StringTest2 = ' !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~';

	void Test1() {
		var test = "27";
		Test2(3);
		Test3("test", true);
	}

	void Test2(param1) {
		const Scoped_Const = 43;
	}

	void Test3(param1, param2) {
	}
}