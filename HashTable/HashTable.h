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
	const int defaultSize = 7;
	int elementsCount;
	int size;

	void Restruct() {
		int oldSize = size;
		size = size * 2 + 1;
			
		auto newStorage = new DoubleList<Element>[size];
		for (int i = 0; i < oldSize; ++i) {
			if (storage[i].GetSize() != 0) {
				auto u = storage[i].Begin();
				do {
					int hash = hashFunc(u.GetValue().key) % size;
					newStorage[hash].InsertBack(Element(u.GetValue().key, u.GetValue().value));
				} while (u.MoveNext());
			}
		}
		delete[] storage;
		storage = newStorage;
	}

public:
	HashTable(int(*_hashFunc)(Key)) : 
		hashFunc(_hashFunc),
		size(defaultSize),
		elementsCount(0)
	{
		storage = new DoubleList<Element>[size];
	}
	void Add(Key key, Value value) {
		int hash = hashFunc(key) % size;
		storage[hash].InsertBack(Element(key, value));
		elementsCount++;
		if (elementsCount > size / 2) {
			Restruct();
		}
	}
	bool GetValue(Key key, Value& value) {
		int hash = hashFunc(key);
		if (storage[hash % size].GetSize() == 0) {
			return false;
		}
		else {
			auto u = storage[hash % size].Begin();
			do {
				if (hashFunc(u.GetValue().key) == hash) {
					value = u.GetValue().value;
					return true;
				}
			} while (u.MoveNext());
		}
		return false;
	}

	int GetCollisions() {
		int collisions = 0;

		for (int i = 0; i < size; ++i) {
			if (storage[i].GetSize() != 0) {
				auto u = storage[i];
				collisions += storage[i].GetSize() - 1;
			}
		}

		return collisions;
	}
};