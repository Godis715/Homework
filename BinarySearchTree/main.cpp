#include "BinTree.h"
#include <iostream>
#include <ctime>

int main() {
	
	/*example of using class BinTree and
	BinTree::Marker (iterator)*/

	BinTree<int> tree;

	/*adding elements*/
	std::cout << "Enter element sequence in which zero means \"end\"\n";
	while(true) {
		int current;
		std::cin >> current;
		if (current == 0) {
			break;
		}
		tree.Add(current);
	}

	/*balancing tree*/
	tree.Balance();

	/*print elements in the adcending order
	using iterator*/
	std::cout << std::endl;
	auto u = tree.GetMarker();
	if (u.IsValid()) {
		std::cout << "Now we'll print out elements in sascending order using iterator:\n";
		do {
			std::cout << u.GetValue() << " ";
		} while (u.MoveNext());
	}
	std::cout << std::endl;
	/*getting tree parameters*/
	std::cout << "Tree height (tree have been balanced): " << tree.GetHeight() << "\n";
	std::cout << "Number of nodes: " << tree.NodesCount() << "\n";
	std::cout << "Number of leaves: " << tree.LeavesCount() << "\n";
	
	/*using marker for getting node's parameters*/

	u = tree.GetMarker();
	if (u.IsValid()) {
		int numerator = 0;
		do {
			++numerator;
			std::cout << numerator << ") ";
			std::cout << " value: " << u.GetValue() << " degree: " <<
				u.GetDegree() << " level: " << u.GetLevel() << "\n";
		} while (u.MoveNext());
	}

	/*deleting node, subtree and tree*/
	u = tree.GetMarker();
	u.DeleteNode();

	u = tree.GetMarker();
	u.DeleteSubtree();

	tree.DeleteTree();

	system("pause");
	return 0;
}