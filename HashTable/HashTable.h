#pragma once

#include "List.h"

template <typename Key, typename Value> class HashTable {
private:
	class Element {
	private:
	public:
		Key key;
		Value value;
		Element(Key _key, Value _val) : key(_key), value(_val) {
			
		}
	};
	int(*hashFunc)(Key);
	DoubleList<Element>* storage;
	const int defaultSize = 97;
	int size;
public:
	HashTable(int(*_hashFunc)(Key)) : hashFunc(_hashFunc), size(defaultSize) {
		storage = new DoubleList<Element>[size];
	}
	void Add(Key key, Value value) {
		int hash = hashFunc(key) % size;
		storage[hash].InsertBack(Element(key, value));
	}
	bool GetValue(Key key, Value& value) {
		int hash = hashFunc(key) % size;
		if (storage[hash].GetSize() == 0) {
			return false;
		}
		else {
			auto u = storage[hash]->Begin();
			do {
				if (u.GetValue().key == key) {
					value = u.GetValue().value;
					return true;
				}
			} while (u->MoveNext());
		}
		return false;
	}
};