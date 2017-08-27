/*
 * Sample Fibonacci project
 * Chooper100
 */

module FibCalculator {
	atomic num Fib(num n) {
		if (n == 1) return 0;
		if (n == 2) return 1;

		return Fib(n - 2) + Fib(n - 1);
	}
}