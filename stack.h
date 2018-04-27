#pragma once

#include "deque.h"

template <typename T> class Stack {
private:
	Deque<T> deq;
public:
	Stack();
	void PushBack(T);
	T PopBack();
	bool IsEmpty();
};

template <typename T> Stack<T>::Stack() { }

template <typename T>void Stack<T>::PushBack(T val) {
	deq.PushBack(val);
}

template <typename T>T Stack<T>::PopBack() {
	try {
		return deq.PopBack();
	}
	catch (std::exception e) {
		throw e;
	}
}

template <typename T> bool Stack<T>::IsEmpty() {
	return deq.IsEmpty();
}