#pragma once

#include "stack.h"

template <typename T> class BinTree {
private:
	class Node {
	private:
	public:
		T value;
		Node* parent;
		Node* left;
		Node* right;
		Node(Node*, Node*, Node*, const T& val);
	};

	Node* root;

	void DeleteNode(Node*& current);
	void DeleteSubtree(Node* node) {
		if (node == nullptr) {
			return;
		}
		if (node->parent != nullptr) {
			if (node->parent->left == node) {
				node->parent->left = nullptr;
			}
			else {
				node->parent->right = nullptr;
			}
		}
		if (node == nullptr) {
			return;
		}
		if (node->left != nullptr) {
			DeleteSubtree(node->left);
		}
		if (node->right != nullptr) {
			DeleteSubtree(node->right);
		}
		delete node;
	}
	void RightRotation(Node*);
	void LeftRotation(Node*);
	int GetNodeDegree(Node* node) const {
		if (node == nullptr) {
			return -1;
		}
		int degree = 0;
		if (node->left != 0) {
			degree++;
		}
		if (node->right != 0) {
			degree++;
		}
		return degree;
	}
	int GetNodeLevel(Node* node) const {
		if (node == nullptr) {
			return -1;
		}
		int level = 0;
		while (node->parent != nullptr) {
			node = node->parent;
			++level;
		}
		return level;
	}
	int GetHeightRecursive(Node* node) const {

		if (node == nullptr) {
			return -1;
		}

		int left = 0;
		int right = 0;
		if (node->left != nullptr) {
			left = GetHeightRecursive(node->left) + 1;
		}
		if (node->right != nullptr) {
			right = GetHeightRecursive(node->right) + 1;
		}
		return (right > left) ? right : left;
	}
public:
	class Marker {
	private:
		Stack<Node*> stack;
		Node* current;
		bool isValid;
		BinTree* tree;
	public:
		Marker(BinTree*);
		bool MoveNext();
		bool DeleteNode();
		bool DeleteSubtree();
		int GetDegree() const;
		int GetLevel() const;
		T GetValue() const;
		bool IsValid() const {
			return isValid;
		}
	};
	BinTree();
	void Add(const T&);
	bool Find(const T&) const;
	int NodesCount() const;
	int LeavesCount() const;
	int GetHeight() const;
	void DeleteTree();
	Marker GetMarker() {
		return Marker(this);
	}
	void Balance();

};

template <typename T> BinTree<T>::Node::Node(Node* _parent, Node* _left, Node* _right, const T& _val) {
	parent = _parent;
	left = _left;
	right = _right;
	value = _val;
}

template <typename T> BinTree<T>::Marker::Marker(BinTree* _tree) {
	isValid = (_tree != nullptr && _tree->root != nullptr);
	if (!isValid) {
		return;
	}
	current = _tree->root;
	tree = _tree;
	while (current->left != nullptr) {
		stack.PushBack(current);
		current = current->left;
	}
}

template <typename T> bool  BinTree<T>::Marker::MoveNext() {
	if (!isValid) {
		return false;
	}
	if (current->right != nullptr) {
		current = current->right;
		while (current->left != nullptr) {
			stack.PushBack(current);
			current = current->left;
		}
		return true;
	}
	else {
		if (stack.IsEmpty()) {
			return false;
		}
		current = stack.PopBack();
		return true;
	}
}

template <typename T> bool BinTree<T>::Marker::DeleteNode() {
	if (!isValid) {
		return false;
	}
	tree->DeleteNode(current);
	if (!stack.IsEmpty()) {
		Node* temp = stack.PopBack();
		if (current != temp) {
			stack.PushBack(temp);
		}
	}
	isValid = false;
}

template <typename T> bool BinTree<T>::Marker::DeleteSubtree() {
	if (!isValid) {
		return false;
	}
	
	Node* subtree = current;
	
	if (current->parent != nullptr) {
		current = current->parent;
	}
	else {
		current = nullptr;
	}
	tree->DeleteSubtree(subtree);
	isValid = false;
	return true;
}

template <typename T> int BinTree<T>::Marker::GetDegree() const {
	return tree->GetNodeDegree(current);
}

template <typename T> int BinTree<T>::Marker::GetLevel() const {
	return tree->GetNodeLevel(current);
}

template <typename T> T BinTree<T>::Marker::GetValue() const {
	return current->value;
}

template<typename T> BinTree<T>::BinTree() {
	root = nullptr;
}

template<typename T> void BinTree<T>::Add(const T& val) {
	if (root == nullptr) {
		root = new Node(nullptr, nullptr, nullptr, val);
		return;
	}
	Node* current = root;
	while (true) {
		if (val >= current->value) {
			if (current->right != nullptr) {
				current = current->right;
			}
			else {
				current->right = new Node(current, nullptr, nullptr, val);
				return;
			}
		}
		else {
			if (current->left != nullptr) {
				current = current->left;
			}
			else {
				current->left = new Node(current, nullptr, nullptr, val);
				return;
			}
		}
	}
}

template<typename T> bool BinTree<T>::Find(const T& value) const {
	if (root == nullptr) {
		return false;
	}
	Node* current = root;
	while (true) {
		if (current == nullptr) {
			return false;
		}
		if (current->value == value) {
			return true;
		}
		if (current->value < value) {
			current = current->left;
		}
		else {
			current = current->right;
		}
	}
} 

template<typename T> int BinTree<T>::LeavesCount() const {
	if (root == nullptr) {
		return 0;
	}
	Node* current = root;
	Stack<Node*> stack;
	stack.PushBack(root);
	int leavesCount = 0;
	while (!stack.IsEmpty()) {
		current = stack.PopBack();
		if (current->left != nullptr) {
			stack.PushBack(current->left);
		}
		if (current->right != nullptr) {
			stack.PushBack(current->right);
		}
		if (current->right == nullptr && current->left == nullptr) {
			leavesCount++;
		}
	}
	return leavesCount;
}

template<typename T> int BinTree<T>::NodesCount() const {
	if (root == nullptr) {
		return 0;
	}
	
	int count = 1;
	Stack<Node*> stack;
	stack.PushBack(root);

	while (!stack.IsEmpty()) {
		Node* current = stack.PopBack();
		if (current->left != nullptr) {
			++count;
			stack.PushBack(current->left);
		}
		if (current->right != nullptr) {
			++count;
			stack.PushBack(current->right);
		}
	}

	return count;
}

template<typename T> int BinTree<T>::GetHeight() const {
	return GetHeightRecursive(root);
}

template<typename T> void BinTree<T>::DeleteTree() {
	DeleteSubtree(root);
	root = nullptr;
}

template<typename T> void BinTree<T>::DeleteNode(Node*& current) {

	if (current->left != nullptr) {

		//find the most right child in the left subtree
		Node* rightChildInLeftSubtree = current->left;
		while (rightChildInLeftSubtree->right != nullptr) {
			rightChildInLeftSubtree = rightChildInLeftSubtree->right;
		}

		if (rightChildInLeftSubtree->left != nullptr) {
			if (rightChildInLeftSubtree->parent->left == rightChildInLeftSubtree) {
				rightChildInLeftSubtree->parent->left = rightChildInLeftSubtree->left;
			}
			else {
				rightChildInLeftSubtree->parent->right = rightChildInLeftSubtree->left;
			}
		}

		Node* nodeToDelete = current;

		current = rightChildInLeftSubtree;

		// current's pointers
		current->right = nodeToDelete->right;
		current->left = nodeToDelete->left;
		current->parent = nodeToDelete->parent;

		if (nodeToDelete == root) {
			root = current;
		}
		// parent's pointers
		else {
			if (current->parent->left == nodeToDelete) {
				current->parent->left = current;
			}
			else {
				current->parent->right = current;
			}
		}

		delete nodeToDelete;
	}
	else {
		Node* nodeToDelete = current;
		if (nodeToDelete == root) {
			root = nodeToDelete->right;
			if (nodeToDelete->right != nullptr) {
				nodeToDelete->parent = root;
			}
			current = root;
		}
		else {
			if (nodeToDelete->parent->left == nodeToDelete) {
				nodeToDelete->parent->left = nodeToDelete->right;
			}
			else {
				nodeToDelete->parent->right = nodeToDelete->right;
			}
			if (nodeToDelete->right != nullptr) {
				nodeToDelete->right->parent = nodeToDelete->parent;
			}
			current = nodeToDelete->parent;
		}
		delete nodeToDelete;
	}
}

template<typename T> void BinTree<T>::RightRotation(Node* node) {
	if (node == nullptr || node->left == nullptr || root == nullptr) {
		return;
	}
	if (node == root) {
		root = node->left;
		root->parent = nullptr;
	}
	else {
		if (node->parent->right == root) {
			node->parent->right = node->left;
		}
		else {
			node->parent->left = node->left;
		}
	}
	Node* temp = node->left;
	node->left = node->left->right;
	if (node->left != nullptr) {
		node->left->parent = node;
	}
	temp->right = node;

	if (node->parent != nullptr) {
		temp->parent = node->parent;
	}

	node->parent = temp;
}

template<typename T> void BinTree<T>::LeftRotation(Node* node) {
	if (node == nullptr || node->right == nullptr || root == nullptr) {
		return;
	}
	if (node == root) {
		root = node->right;
		root->parent = nullptr;
	}
	else {
		if (node->parent->left == node) {
			node->parent->left = node->right;
		}
		else {
			node->parent->right = node->left;
		}
	}
	Node* temp = node->right;
	node->right = node->right->left;
	if (node->right != nullptr) {
		node->right->parent = node;
	}
	temp->left = node;

	if (node->parent != nullptr) {
		temp->parent = node->parent;
	}

	node->parent = temp;
}

template<typename T> void BinTree<T>::Balance() {
	if (root == nullptr) {
		return;
	}

	// convert tree to backbone

	Node* current = root;
	while (current->left != nullptr || current->right != nullptr) {
		while (current->right != nullptr) {
			LeftRotation(current);
			current = current->parent;
		}
		if (current->left != nullptr) {
			current = current->left;
		}
	}

	int count = NodesCount() + 1;
	while (count > 1) {
		count = count / 2;
		current = root;
		for (int i = 0; i < count - 1; ++i) {
			RightRotation(current);
			current = current->parent;
			if (current->left != nullptr) {
				current = current->left;
			}
		}
	}
}