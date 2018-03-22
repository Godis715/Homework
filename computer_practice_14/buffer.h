#pragma once
#include "List.h"
#include <stdexcept>

class Buffer
{
private:
	const int N = 4;
	DoubleList2<char*> list;
	int size;
public:
	Buffer() {
		size = 0;
	}
	Buffer(char*);
	void AddBack(char*);
	void AddAt(int, char*);
	int GetLength() const;
	char* StrCopy();
	char* StrCopy(int, int);
	char& operator[](int);
	void Cut(int);
	void Erase();
};

int StrLength(char* str) {
	int i = 0;
	for (i = 0; str[i] != '\0'; ++i) { }
	return i;
}

Buffer::Buffer(char* str) {
	int strSize = StrLength(str);
	if (strSize == 0) {
		list.InsertBack(new char[N] {'\0'});
		return;
	}

	for (int i = 0; i < strSize; ++i) {
		if (i % N == 0) {
			char* newStr = new char[N] {'\0'};
			list.InsertBack(newStr);
			list.End().GetValue()[0] = str[i];
			
		}
		else {
			list.End().GetValue()[i % N] = str[i];
		}
	}
	size += strSize;
}

void Buffer::AddBack(char* str) {
	int str_i = 0;

	char* buffEnd = list.End().GetValue();

	int i = 0;
	for (i = 0; i < N && buffEnd[i] != '\0'; ++i) { }

	while (str[str_i] != '\0') {
		if (i % N == 0) {
			char* newStr = new char[N] {'\0'};
			list.InsertBack(newStr);
			list.End().GetValue()[0] = str[str_i];

		}
		else {
			list.End().GetValue()[i % N] = str[str_i];
		}
		++str_i;
		++i;
		++size;
	}
}

void Buffer::AddAt(int pos, char* str) {
	if (pos > size) {
		throw std::exception("Position out of range");
	}
	auto u = list.Begin();
	int str_i = 0;
	char* tempStr = new char[N - pos % N + 1];
	tempStr[N - pos % N] = '\0';
	int strLen = StrLength(str);
	for (int i = 0; i < pos - N; ++i) {
		u.MoveNext();
	}
	for (int i = pos % N; i < N; ++i) {
		tempStr[i - pos % N] = u.GetValue()[i];
		u.GetValue()[i] = str[str_i];
		if (str[str_i] == '\0') {
			break;
		}
		++str_i;
	}
	int j = N;
	for (int i = str_i; i < strLen; i += N) {
		char* newBlock = new char[N] {'\0'};
		for (j = 0; j < N && str[j + i] != '\0'; ++j) {
			newBlock[j] += str[j + i];
		}
		u.AddNext(newBlock);
		u.MoveNext();
	}
	AddBack(tempStr);
	auto left = u;
	auto right = u;
	if (j != N && right.MoveNext()) {
		int i = 0;
		while (true) {
			if (j == N) {
				j = 0;
				left.MoveNext();
			}
			if (i == N) {
				i = 0;
				if (!right.MoveNext()) {
					break;
				}
			}
			left.GetValue()[j] = right.GetValue()[i];
			++i;
			++j;
		}
		if (right.GetValue()[0] == '\0') {
			right.DeleteCurrent();
		}
	}
	size += strLen;
	size -= StrLength(tempStr);
}

int Buffer::GetLength() const {
	return size;
}

char* Buffer::StrCopy() {
	char* str = new char[size + 1];
	str[size] = '\0';
	auto u = list.Begin();
	int str_i = 0;
	do {
		for (int i = 0; i < N && u.GetValue()[i] != '\0'; ++i) {
			str[str_i] = u.GetValue()[i];
			++str_i;
		}
	} while (u.MoveNext());
	return str;
}

char& Buffer::operator[](int pos) {
	auto u = list.Begin();
	for (int i = 0; i < pos - N; i += N) {
		u.MoveNext();
	}
	return u.GetValue()[pos % N];
}

void Buffer::Cut(int newSize) {
	if (newSize >= size) {
		return;
	}
	auto u = list.End();
	while(size >= newSize + N) {
		if (u.GetValue()[N - 1] == '\0') {
			size -= StrLength(u.GetValue());
		}
		else {
			size -= N;
		}
		u.DeleteCurrent();
		
	}
	int left = N - 1;
	while (u.GetValue()[left] == '\0') {
		left--;
	}
	while (size != newSize) {
		u.GetValue()[left] = '\0';
		--left;
		--size;
	}
	
}

void Buffer::Erase() {
	auto u = list.Begin();
	while (list.GetSize() != 0) {
		u.DeleteCurrent();
	}
}

char* Buffer::StrCopy(int l, int r) {
	if (l > r) {
		return new char('\0');
	}
	if (l >= size || r >= size) {
		throw std::exception("Index out of range");
	}
	int strSize = r - l + 1;
	char* newStr = new char[strSize + 1];
	newStr[strSize] = '\0';

	auto u = list.Begin();
	
	for (int i = 0; i < l; i += N) {
		u.MoveNext();
	}


	int str_i = 0;
	for (str_i = 0; str_i < strSize; ++str_i) {
		if (l % N == 0) {
			u.MoveNext();
		}
		newStr[str_i] = u.GetValue()[l % N];
		++l;
	}

	return newStr;

}