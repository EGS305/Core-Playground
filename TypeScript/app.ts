type Name = string;
type Age = number;
type Gender = "M" | "F"; //union value
enum Nationality { Belgian = "B", Dutch = "NL", Other = "?" };
interface Person { name: Name, age: Age, gender: Gender, nationality?: Nationality };
type User = Person & { username: string }; //intersection type; not possible with interfaces.
interface Driver extends Person { position: number };

const user: User = {
    name: "Eric",
    age: 42,
    gender: "M",
    nationality: Nationality.Belgian, //This one is optional.
    username: "EGS"
};

console.log(user);

const driver: Driver = {
    name: "Evelien",
    age: 42,
    gender: "F",
    position: 1
};

console.log(driver);

type Add = (a: number, b?: number) => number;

function add(a: number = 5, b?: number): number {
    return a + (b || a);
}

function add2(...list: number[]): number {
    return list.reduce((sum, n) => sum + n, 0);
}

const add3: Add = (a, b) => a + (b || a);

console.log(add3(5, 12));

const value: ReturnType<Add> = add3(5, 12);
const value2: Parameters<Add>[0] = add3(5, 12);

const a: string = "305";
const b = a as unknown as number;
console.log(b + 5);

class Vehicle {
    public readonly brand: Name;

    public constructor(brand: Name) {
        this.brand = brand;
    }

    protected show() {
        console.log("Vehicle brand: " + this.brand);
    }
}

class Car extends Vehicle {
    public override readonly brand: Name;
    protected model: Name;
    private year: number;

    public constructor(brand: Name, model: Name, year: number = 2026) {
        super("Ford");
        this.brand = brand;
        this.model = model;
        this.year = year;
    }

    public override show() {
        console.log("Car brand: " + this.brand);
    }
}

const tahoe = new Car("Chevrolet", "Tahoe", 1992);
tahoe.show();

const key: keyof Car = "brand";

let uno: Car | null = new Car("Fiat", "Uno", 1990);
console.log(uno?.show());

process.stdin.resume(); //Keeps the console open.