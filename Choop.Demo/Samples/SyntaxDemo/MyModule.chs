module MyModule {
	const e = 2.718;

	num Diff(input1, input2) {
		if (input2 > input1) {
			return input2 - input1;
		} else {
			return input1 - input2;
		}
	}

	string Concat(string input1, string input2) {
		return input1 . input2;
	}

	bool IsGreaterThan(num input1, num input2) {
		return input1 > input2;
	}
}