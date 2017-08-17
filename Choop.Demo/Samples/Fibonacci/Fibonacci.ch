/*
 * Sample Fibonacci project
 * Chooper100
 */

sprite Fibonacci {
	using FibCalculator;
	
	unsafe event GreenFlag() {
		forever {var test = 5 << 2;
			num index = Random(1, 10);
			string ordinal;
			switch (GetLetter(StringLength(index), index)) {
				case 0:
				case >= 4:
					ordinal = "th";
					break;
				case 1:
					ordinal = "st";
					break;
				case 2:
					ordinal = "nd";
					break;
				case 3:
					ordinal = "rd";
					break;
			}

			num fib = Fib(index);
			SayForSecs("The " . index . ordinal . " Fibonacci number is " . fib . "!", 2);
		}
	}

}

module FibCalculator {
	atomic num Fib(num n) {
		if (n == 1) return 0;
		if (n == 2) return 1;

		return Fib(n - 2) + Fib(n - 1);
	}
}