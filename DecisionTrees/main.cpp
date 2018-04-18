#include <iostream>
#include <cmath>

int main() {
	while (true) {
		int n;
		int m;
		std::cin >> n >> m;
		std::cout << (double)n / (double)m * log2((double)n / (double)m) << "\n";
	}
}