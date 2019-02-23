
// C#
class Person {
    public string Name { get; }
    public string Age { get; }
    public Person(string name, int age) {
        Name = name;
        Age = age;
    }
}

// F#
type Person = { Name: string; Age: int }


// C#
var name = Tuple.Create("isaac", "abraham")
var firstName = name.Item1
var secondName = name.Item2
var numbers = new List<int>(new []{1,2,3,4,5,6,7,8,9,10})

// F#
let name = "isaac", "abraham"
let firstName, secondName = name
let numbers = [ 1 .. 10 ]


