# MyLinq
A lightweight, educational LINQ implementation in C# — built from scratch.

## ✨ Introduction
This project is a hands‑on exploration of how LINQ works under the hood.  

⚠️ Note: This is **not** a replacement for `System.Linq`. It’s an educational toolkit for learning and experimenting.

## 📦 Installation
Clone the repo:
```bash
git clone https://github.com/spinxi/MyLinq.git
```
## 🧑‍💻 Usage
```bash
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var evens = numbers.MyWhere(x => x % 2 == 0);

foreach (var n in evens)
    Console.WriteLine(n); // Output: 2, 4
```
