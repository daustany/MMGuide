# Crew Entered - Dependency Analysis Problem

------------------------------

Success! The gate opens up and the crew steps inside. Once inside, the gate behind them closes. The dimly lit room shows another panel on the wall.

The panel shows connections between symbols. Each connection, shown as an array [a, b], means they need to activate symbol 'b' before symbol 'a'. So the crew required to assess the entire set of dependencies and decide if there exists a sequence that allows for the pressing of all symbols without violating any dependency rules. This involves ensuring that each symbol a is pressed only after its corresponding prerequisite symbol b has been pressed. Determine the feasibility of pressing all symbols in the array while respecting these dependencies. If it is possible to press all symbols according to the given dependencies, the solution should be 1; otherwise, if it is not possible, the solution should be 0.

Example:
Current dependency:
[.,,],[,,.]
The first array indicates that to press ',' you should first press '.' and the second element indicates that to press '.' you should first press ','. So it is impossible.
Result: 0

Sum the results.

[*,<],[<,*]
[+,~],[',+],[-,']
[+,~],[',+],[/,=],[~,']

First dependency:
The first array indicates that to press < you should first press * and the second array indicates that to press * you should first press < . So it is impossible.
Result of first line : 0

Second dependency:
The first array indicates that to press ~ you should first press + The second array indicates that to press + you should first press ' The third array indicates that to press ' you should first press – So it is possible.
Result of second line: 1

Third dependency:
The first array indicates that to press ~ you should first press + The second array indicates that to press + you should first press ' The third array indicates that to press / you should first press = The fourth array indicates that to press ' you should press ~ So it is impossible.
Result of third line: 0

Final result: Sum of results of each line = 0(from the first line) + 1(from the second line) + 0(from the third line) = 1
1
