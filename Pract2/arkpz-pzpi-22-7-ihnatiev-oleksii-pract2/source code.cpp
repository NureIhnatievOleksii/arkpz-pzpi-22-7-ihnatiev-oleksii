#include <iostream>

// До рефакторінгу
void calculate(int length, int width, int height, int depth, bool isRectangular, bool hasHoles) {
    int volume = length * width * height;
    if (isRectangular) {
        volume *= depth;
    }
    if (hasHoles) {
        volume -= 50;  // Знижуємо об'єм на площу отворів
    }
    // Інша логіка
}

// Після рефакторінгу
struct Dimensions {
    int length, width, height, depth;
    bool isRectangular, hasHoles;
};

void calculate(const Dimensions& dims) {
    int volume = dims.length * dims.width * dims.height;
    if (dims.isRectangular) {
        volume *= dims.depth;
    }
    if (dims.hasHoles) {
        volume -= 50;  // Знижуємо об'єм на площу отворів
    }
    // Інша логіка
}


///////////////////////////////////////////////////////////////////////////////

// До рефакторінгу
void display(int x, int y, int width, int height) {
    std::cout << "Coordinates: " << x << ", " << y << ", Dimensions: " << width << "x" << height << std::endl;
}

// Після рефакторінгу
struct Rectangle {
    int x, y, width, height;
};

void display(const Rectangle& rect) {
    std::cout << "Coordinates: " << rect.x << ", " << rect.y << ", Dimensions: " 
        << rect.width << "x" << rect.height << std::endl;
}


////////////////////////////////////////////////////////////////////////////// 

// До рефакторінгу
double calculateSalary(std::string role) {
    if (role == "Manager") return 5000;
    else if (role == "Developer") return 4000;
    else if (role == "Tester") return 3500;
    else return 3000;
}


// Після рефакторінгу
class Employee {
public:
    virtual double getSalary() const = 0;
};

class Manager : public Employee {
public:
    double getSalary() const override { return 5000; }
};

class Developer : public Employee {
public:
    double getSalary() const override { return 4000; }
};

class Tester : public Employee {
public:
    double getSalary() const override { return 3500; }
};

// Додавання нової ролі
class Designer : public Employee {
public:
    double getSalary() const override { return 4500; }
};
