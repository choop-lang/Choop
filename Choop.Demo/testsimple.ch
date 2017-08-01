// This is a single-line comment

/* This
   is
   a
   multiline
   comment
*/

num GlobalVar = 3;

sprite MySprite {
	string H = "hello";

	event KeyPressed<"space">() {
		if (H == "hello world") {
			GlobalVar = GlobalVar * 2;
		}
		H .= " world";

		for (var test = 0 to 10 step 3) {
			GlobalVar--;
		}

		IfOnEdgeBounce();
		Broadcast("Hello "."world");
	}
}

sprite Other {
	var test = 1;
	var test2 = 0;

	event GreenFlag() {
		if (test < 5) {
			test++;
		} else if (test == 5) {
			test = 10;
		} else {
			test -= 1;
		}
		// Test
		while(test2 < 10) {
			test2 += 2;
		}
	}
}