#pragma once

#include <stdexcept>

template<typename T> class Deque {
private:
	class Node {
	private:
	public:
		Node(Node*, Node*, T);
		Node* prev;
		Node* next;
		T value;
	};
	Node* begin;
	Node* end;
	int size;
public:
	Deque();
	void PushBack(T);
	void PushBegin(T);
	T PopBack();
	T PopBegin();
	bool Find(T) const;
	bool IsEmpty() const;
};

template<typename T> Deque<T>::Node::Node(Node* prv, Node* nxt, T val) {
	prev = prv;
	next = nxt;
	value = val;
}

template<typename T> Deque<T>::Deque() {
	begin = nullptr;
	end = nullptr;
	size = 0;
}

template<typename T> void Deque<T>::PushBack(T val)
{
	if (this->begin == nullptr) {
		Node* newNode = new Node(nullptr, nullptr, val);
		this->begin = newNode;
		this->end = newNode;
	}
	else {
		Node* newNode = new Node(end, nullptr, val);
		this->end->next = newNode;
		end = newNode;
	}
	++size;
}

template<typename T> void Deque<T>::PushBegin(T val) {
	if (this->begin == nullptr) {
		Node* newNode = new Node(nullptr, nullptr, val);
		this->begin = newNode;
		this->end = newNode;
	}
	else {
		Node* newNode = new Node(nullptr, begin, val);
		this->begin->prev = newNode;
		begin = newNode;
	}
	++size;
}

template<typename T> T Deque<T>::PopBack() {
	if (begin == nullptr) {
		throw std::exception("Incorrect action. Container was empty\n");
	}
	Node* temp = end;
	end = end->prev;
	if (temp != begin) {
		end->next = nullptr;
	}
	else {
		begin = nullptr;
	}
	T answer = temp->value;
	delete temp;
	--size;
	return answer;
}

template<typename T> T Deque<T>::PopBegin() {
	if (begin == nullptr) {
		throw std::exception("Incorrect action. Container was empty\n");
	}
	Node* temp = begin;
	begin = begin->next;
	if (temp != end) {
		begin->prev = nullptr;
	}
	else {
		begin = nullptr;
	}
	T answer = temp->value;
	delete temp;
	--size;
	return answer;
}

template<typename T> bool Deque<T>::Find(T valToFind) const {
	Node* current = begin;
	while (current != nullptr) {
		if (current->value == valToFind) {
			return true;
		}
	}
	return false;
}

template<typename T> bool Deque<T>::IsEmpty() const {
	return size == 0;
}