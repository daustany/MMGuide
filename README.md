"# 🚀 MMGuide Coding Challenges

<div align="center">

![MMGuide Logo](https://mmguide.nl/wp-content/uploads/2020/12/Logo-Without-Slogan-bigger-logo-1.png)

**A collection of 5 challenging algorithmic problems from MMGuide**

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Console](https://img.shields.io/badge/Console-000000?style=for-the-badge&logo=windows-terminal&logoColor=white)

</div>

---

## 📖 Overview

This repository contains 5 algorithmic challenges provided by **MMGuide**. Each challenge is implemented as a standalone C# console application that reads input from a text file and performs calculations based on the given data.

## 🚀 Quick Start

To run any of the challenges:

1. Navigate to the specific problem folder (01-Quadrant, 02-SmoothLanding, etc.)
2. Execute the following command:
   ```bash
   dotnet run
   ```

Each project reads from its respective `input.txt` file and displays the calculated results.

---

## 🎯 Challenges

### 🎛️ 01 - Iota Quadrant (Engine Dial Optimization)

The USS MM Guide encounters a magnetic field that affects the spaceship's engines. The challenge involves calculating the minimum number of dial ticks needed to adjust 4 circular engine dials (0-9) from current positions to target positions. Each dial can be turned left or right, and they wrap around (0↔9). The goal is to find the optimal rotation direction for each dial and calculate the total cost across multiple dial sets.

**Key Concepts:** Circular arrays, optimization, modular arithmetic

### 📊 02 - Smooth Landing (Sequence Analysis)

Upon landing, the crew discovers a numeric sequence puzzle on an ancient gate. For each number in a sequence, you must count how many numbers to its right are smaller than it. After processing all sequences, calculate the median of each resulting array and sum all medians. This challenge tests array processing and statistical analysis skills.

**Key Concepts:** Array traversal, counting algorithms, median calculation

### 🔗 03 - Crew Entered (Dependency Resolution)

Inside the structure, the crew faces a dependency analysis problem with symbol activation sequences. Given dependency pairs [a,b] where 'b' must be activated before 'a', determine if it's possible to activate all symbols without violating dependencies (detecting circular dependencies). Return 1 for possible sequences, 0 for impossible ones, and sum the results.

**Key Concepts:** Graph theory, cycle detection, topological sorting

### 🪨 04 - Stone Splitting (Recursive Division)

The crew encounters a mysterious object with a stone splitting challenge. Given piles of stones and a set of divisor numbers, recursively split piles into smaller equal-sized piles using available divisors. The goal is to find the maximum number of splits possible for each pile and concatenate the results into a final number.

**Key Concepts:** Recursive algorithms, dynamic programming, mathematical optimization

### 🗺️ 05 - Connected Regions (Grid Analysis)

A voice challenges the crew with a map analysis task. Given a 2D grid of 1s and 0s, find connected regions where cells are considered connected if they're adjacent horizontally, vertically, or diagonally. Calculate and return the size of the largest connected region in the map.

**Key Concepts:** Graph traversal, flood fill, connected components

---

## 🏗️ Architecture & Design

All projects in this repository follow **SOLID principles** and implement clean architecture patterns to ensure maintainable, extensible, and testable code:

- **Single Responsibility Principle**: Each class has a single, well-defined purpose
- **Open/Closed Principle**: Classes are open for extension but closed for modification
- **Liskov Substitution Principle**: Derived classes can replace their base classes
- **Interface Segregation Principle**: Interfaces are focused and client-specific
- **Dependency Inversion Principle**: High-level modules don't depend on low-level modules

Each project is structured with clear separation of concerns using interfaces, services, models, and dependency injection patterns. This approach makes the code highly modular and allows for easy testing and future enhancements.

## 📁 Project Structure

```
MMGuide/
├── 01-Quadrant/
│   ├── USS_MM_Guide_IotaQuadrant.md     # Problem description
│   ├── Program.cs                        # Main application
│   ├── Input/input.txt                   # Test data
│   └── [Core classes and services]
├── 02-SmoothLanding/
│   ├── USS_MM_Guide_SmoothLandingPuzzle.md
│   ├── Program.cs
│   ├── Input/input.txt
│   └── [Services and models]
├── 03-CrewEntered/
│   ├── USS_MM_Guide_CrewEntered.md
│   ├── Program.cs
│   ├── Input/input.txt
│   └── [Analysis services]
├── 04-StoneSpiliting/
│   ├── USS_MM_Guide_StoneSpiliting.md
│   ├── Program.cs
│   ├── Input/input.txt
│   └── [Splitting algorithms]
└── 05-ConnectedRegions/
    ├── USS_MM_Guide_ConnectedRegions.md
    ├── Program.cs
    ├── Input/input.txt
    └── [Grid processing services]
```

## 🛠️ Requirements

- .NET Core/Framework
- C# development environment

## 🎮 How to Use

Each challenge follows the same pattern:

1. **Read the problem description** in the respective `.md` file
2. **Navigate to the project folder**
3. **Run the application** with `dotnet run`
4. **View the results** in the console output

The applications automatically read from their `Input/input.txt` files and process the data according to each challenge's specific requirements.

---

<div align="center">

**Made with ❤️ for algorithmic problem solving**

*Explore, Learn, and Conquer these coding challenges!*

</div>" 
