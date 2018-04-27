#pragma once

template <typename T> class IMarker {
protected:
	bool isValid;
public:
	virtual T GetValue() const = 0;
	virtual bool MoveNext() = 0;
	virtual void DeleteCurrent() = 0;
	bool IsValid() const {
		return isValid;
	}
};

template <typename T> class INumerable {
private:
public:
	virtual size_t GetSize() const = 0;
	virtual IMarker<T>* GetMarker() = 0;
};