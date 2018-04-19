#include "HashTable.h"
#include <iostream>
#include <fstream>


int GetHash(int a) {
	double temp = ((double)a * 0.471234756844);
	temp = temp - (int)(temp);
	int hash = (int)(temp * (double)(1 << 30));
	int mask = 1;
	return hash;
}

int GetStrHash(char* str) {
	int hash = 0;
	int num = 97;
	for (int i = 0; str[i] != '\0'; ++i) {
		int temp = 1 << 30;
		hash = (hash * 3 + (int)str[i]) & ((1 << 30) - 1);
	}
	return hash;
}

void FillDictionary(HashTable<char*, char*>& dict) {
	std::ifstream words;
	std::ifstream meanings;
	words.open("words.txt", std::ios::in);
	meanings.open("meanings.txt", std::ios::in);
	if (!words.is_open() || !meanings.is_open()) {
		std::cout << "Couldn't open the file\n";
		return;
	}
	
	while (!words.eof()) {
		char* word = new char[1024];
		char* meaning = new char[1024];
		words.getline(word, 1024);
		meanings.getline(meaning, 1024);
		dict.Add(word, meaning);
	}

	words.close();
	meanings.close();
}
void AddToDirectory(HashTable<char*, char*>& dict) {
	char* word = new char[1024];
	char* meaning = new char[1024];
	std::cout << "Enter the word\n";
	std::cin.get();
	std::cin.getline(word, 1024);
	std::cout << "Enter the meaning\n";
	std::cin.getline(meaning, 1024);
	
	while (true) {
		std::cout << "Do you want to save the word?\n";
		std::cout << "1 - yes\n";
		std::cout << "0 - no\n";
		int item = 0;
		std::cin >> item;
		if (item != 1 && item != 0) {
			std::cout << "Incorrect item\n";
			continue;
		}
		if (item == 1) {
			break;
		}
		if (item == 0) {
			delete word;
			delete meaning;
			return;
		}
	}

	std::ofstream wordFile;
	wordFile.open("words.txt", std::ios::app);
	std::ofstream meaningsFile;
	meaningsFile.open("meanings.txt", std::ios::app);

	if (!wordFile.is_open() || !meaningsFile.is_open()) {
		if (wordFile.is_open()) {
			wordFile.close(); 
		}
		if (meaningsFile.is_open()) {
			meaningsFile.close();
		}
		std::cout << "Couldn't open the file\n";
		return;
	}

	wordFile << word << "\n";
	meaningsFile << meaning << "\n";

	dict.Add(word, meaning);

	wordFile.close();
	meaningsFile.close();
}

int main() {

	HashTable<char*, char*> dictionary(GetStrHash);
	FillDictionary(dictionary);
	
	int collisions = dictionary.GetCollisions();
	std::cout << collisions << "\n";

	int item = -1;
	while (item != 0) {
		std::cout << "1 - Find word\n";
		std::cout << "2 - Add word\n";
		std::cin >> item;
		switch (item) {
		case 1: {
			std::cin.get();
			char* word = new char[1024];
			std::cin.getline(word, 1024);
			char* meaning = new char[1024];
			if (dictionary.GetValue(word, meaning)) {
				std::cout << meaning << "\n";
			}
			else {
				std::cout << "There is no that word\n";
			}
			break;
		}
		case 2: {
			AddToDirectory(dictionary);
			break;
		}
		case 0: {
			break;
		}
		default: {
			std::cout << "Incorrect item\n";
			break;
		}
		}
	}
	return 0;
}