#include "deque.h"
#include "stack.h"
#include <iostream>
#include <conio.h>
#include <string>
#include "queue.h"
#include "Dictionary.h"

template <class T> class Single {
private:
public:
	virtual T Method() = 0;
};

template <class TVal, class TKey> class Pair : public Single<TVal> {
private:
public:
	TVal Method() {
		return 0;
	}
};

int main() {

	Dict<int, char> dict;
	dict.Add(1, 'a');
	dict.Add(2, 'b');
	dict.Add(3, 'c');
	dict.Add(4, 'd');
	dict.Add(5, 'e');
	dict.Add(6, 'f');
	dict.Add(7, 'g');


	auto u = dict.GetMarker();
	for (int i = 0; i < 3; ++i) {
		u->MoveNext();
	}
	u->DeleteCurrent();
	delete u;
	u = dict.GetMarker();
	while (u->IsValid()) {
		std::cout << u->GetValue() << " ";
		u->MoveNext();
	}

	delete u;
	

	_getch();
	return 0;
}