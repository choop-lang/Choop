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
		H .= " world";
	}
}

sprite Other {
	var test = 1;

	event GreenFlag() {
		test = test + 1;
	}
}