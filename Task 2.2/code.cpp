#include <iostream>
#include <conio.h>
#include <ctime>

using namespace std;

double exp_log_time(double base, int index) {

	double expArr[32];

	expArr[0] = base;
	int expArrPtr = 0;

	int mask = (1 << 31) - 2;

	while ((mask & index) != 0) {
		expArr[expArrPtr + 1] = expArr[expArrPtr] * expArr[expArrPtr];
		++expArrPtr;
		mask = (mask << 1);
	}

	double answer = 1;

	for (int i = 0; i <= expArrPtr; ++i) {
		if (index % 2 == 1) {
			answer *= expArr[i];
		}
		index = index / 2;
	}

	return answer;
}

double exp_line_time(double base, int index) {
	double temp = 1.0;
	for (int i = 0; i < index; ++i) {
		temp = temp * base;
	}
	return temp;
}

int main() {

	double num = 1.0001;
	double t = clock();
	int maxBase = 5000;

	cout << num << " to the power of N" << endl;
	cout << "N = 1, 2 ... " << maxBase << endl;
	cout << "by stupid and smart algorithm" << endl;
	cout << endl;

	for (int i = 0; i < maxBase; ++i) {
		exp_line_time(num, i);
	}
	
	cout << "total time by stupid exp: " << clock() - t <<  endl;

	t = clock();

	for (int i = 0; i < maxBase; ++i) {
		exp_log_time(num, i);
	}

	cout << "Total time by smart exp: " << clock() - t;
	
	_getch();
	return 0;
}