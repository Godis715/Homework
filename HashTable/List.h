#pragma once
#include <stdexcept>

template<typename T> class DoubleList
{
private:
	struct Node {
		Node* prev = nullptr;
		Node* next = nullptr;
		T value;
	};
	int size;
	Node* head;
	Node* tail;
public:
	class Cursor {
	private:
		Node* current;
		DoubleList* list;
	public:
		Cursor(DoubleList&);
		void MoveHead();
		void MoveTail();
		bool MoveNext();
		bool MovePrev();
		T GetValue();
		void DeleteCurrent();
		void AddNext(T);
	};
	DoubleList() {
		tail = nullptr;
		head = nullptr;
		size = 0;
	}
	Cursor Begin() {
		Cursor cur(*this);
		cur.MoveHead();
		return cur;
	}
	Cursor End() {
		Cursor cur(*this);
		cur.MoveTail();
		return cur;
	}
	void InsertBack(T val) {
		Node* newNode = new Node{ tail, nullptr, val };
		if (tail == nullptr) {
			head = newNode;
			tail = newNode;
		}
		else {
			tail->next = newNode;
			tail = newNode;
		}
		++size;
	}
	int GetSize() const {
		return size;
	}
};

template<typename T> DoubleList<T>::Cursor::Cursor(DoubleList& l) {
	this->list = &l;
}
template<typename T> void DoubleList<T>::Cursor::MoveHead() {
	if (list->head == nullptr) {
		throw std::exception("MoveHead: list's head was null");
	}
	current = list->head;
}
template<typename T> void DoubleList<T>::Cursor::MoveTail() {
	if (list->head == nullptr) {
		throw std::exception("MoveHead: list's head was null");
	}
	current = list->tail;
}
template<typename T> bool DoubleList<T>::Cursor::MoveNext() {
	if (this->current == nullptr) {
		throw std::exception("DoubleList::MoveNext: current was null");
	}
	if (this->current->next != nullptr) {
		current = current->next;
		return true;
	}
	return false;
}
template<typename T> bool DoubleList<T>::Cursor::MovePrev() {
	if (this->current == nullptr) {
		throw std::exception("DoubleList::MovePrev: current was null");
	}
	if (this->current->prev != nullptr) {
		current = current->prev;
		return true;
	}
	return false;
}
template<typename T> T DoubleList<T>::Cursor::GetValue() {
	if (this->current == nullptr) {
		throw std::exception("DoubleList::GetValue: current was null");
	}
	return this->current->value;
}
template<typename T> void DoubleList<T>::Cursor::DeleteCurrent() {
	if (this->current == nullptr) {
		throw std::exception("DoubleList::DeleteCurrent: current was null");
	}
	if (current->prev == nullptr) {
		if (current->next == nullptr) {
			list->head = nullptr;
			list->tail = nullptr;
			delete current;
			current = nullptr;
		}
		else {
			list->head = current->next;
			current->next->prev = nullptr;
			auto temp = current->next;
			delete current;
			current = temp;
		}
	}
	else {
		if (current->next == nullptr) {
			current->prev->next = nullptr;
			auto temp = current->prev;
			delete current;
			current = temp;
			list->tail = temp;

		}
		else {
			current->next->prev = current->prev;
			current->prev->next = current->next;
			auto temp = current->next;
			delete current;
			current = temp;
		}
	}
	list->size--;
}
template<typename T> void DoubleList<T>::Cursor::AddNext(T val) {
	if (this->current == nullptr) {
		throw std::exception("DoubleList::GetValue: current was null");
	}
	Node* newNode = new Node{current, current->next, val};
	if (current->next != nullptr) {
		current->next->prev = newNode;
	}
	else {
		list->tail = newNode;
	}
	current->next = newNode;
	list->size++;
}