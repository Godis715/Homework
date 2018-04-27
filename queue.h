#pragma once

#include "deque.h"

template <typename T> class Queue {
private:
	Deque<T> deq;
public:
	Queue();
	void Push(T);
	T GetFirst();
	bool IsEmpty();
};

template<typename T> Queue<T>::Queue() {
	
}

template<typename T> void Queue<T>::Push(T val) {
	deq.PushBack(val);
}

template<typename T> T Queue<T>::GetFirst() {
	try {
		return deq.PopBegin();
	}
	catch (std::exception e) {
		throw e;
	}
}


template<typename T> bool Queue<T>::IsEmpty() {
	return deq.IsEmpty();
}