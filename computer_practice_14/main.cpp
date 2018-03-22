#include "buffer.h"
#include <conio.h>
#include <iostream>


int main() {

	
		Buffer buff("");
		char str[255];
		int item = -1;

		while (item != 0) {
			try {
			std::cout << "Choose the option\n";
			std::cout << "1) aaa -> AddBack(b) -> aaab\n";
			std::cout << "2) aaa -> AddAt(2, bb) -> aabba\n";
			std::cout << "3) aaa -> GetLength() -> 3\n";
			std::cout << "4) aabbcc -> Cut(3) -> aab\n";
			std::cout << "5) abca -> StrCopy(1, 2) -> bc (only for printing)\n";
			std::cout << "6) Print string\n";
			std::cout << "0) Exit\n";
			std::cin >> item;
			switch (item) {
			case 1: {
				std::cout << "string <- ";
				std::cin >> str;
				buff.AddBack(str);
				break;
			}
			case 2: {
				int pos = -1;
				std::cout << "index <- ";
				std::cin >> pos;
				if (pos == -1) {
					break;
				}
				std::cout << "string <- ";
				std::cin >> str;
				buff.AddAt(pos, str);
				break;
			}
			case 3: {
				std::cout << "length: " << buff.GetLength() << std::endl;
				break;
			}
			case 4: {
				int newSize = -1;
				std::cout << "new size <- ";
				std::cin >> newSize;
				if (newSize == -1) {
					break;
				}
				buff.Cut(newSize);
				break;
			}
			case 5: {
				int l = -1;
				int r = -1;
				std::cout << "begin <- ";
				std::cin >> l;
				std::cout << "end <- ";
				std::cin >> r;
				if (l == -1 || r == -1) {
					break;
				}
				std::cout << buff.StrCopy(l, r) << std::endl;
				break;
			}
			case 6: {
				std::cout << "buffer: " <<buff.StrCopy() << std::endl;
				break;
			}
			case 0: {
				break;
			}
			default: {
				std::cout << "Unknown option\n";
				break;
			}
			}
			if (std::cin.fail()) {
				std::cout << "Bad params\n";
				std::cin.clear();
				char c;
				std::cin.ignore(10000, '\n');
			}
			else {
				std::cout << "operation complete\n";
			}
		}
		catch (std::exception e) {
			std::cout << e.what() << std::endl;
		}
	}
	


	_getch();
	return 0;
}