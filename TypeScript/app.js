"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Nationality;
(function (Nationality) {
    Nationality["Belgian"] = "B";
    Nationality["Dutch"] = "NL";
    Nationality["Other"] = "?";
})(Nationality || (Nationality = {}));
;
;
;
const user = {
    name: "Eric",
    age: 42,
    gender: "M",
    nationality: Nationality.Belgian, //This one is optional.
    username: "EGS"
};
console.log(user);
const driver = {
    name: "Evelien",
    age: 42,
    gender: "F",
    position: 1
};
console.log(driver);
function add(a = 5, b) {
    return a + (b || a);
}
function add2(...list) {
    return list.reduce((sum, n) => sum + n, 0);
}
const add3 = (a, b) => a + (b || a);
console.log(add3(5, 12));
const value = add3(5, 12);
const value2 = add3(5, 12);
const a = "305";
const b = a;
console.log(b + 5);
class Vehicle {
    brand;
    constructor(brand) {
        this.brand = brand;
    }
    show() {
        console.log("Vehicle brand: " + this.brand);
    }
}
class Car extends Vehicle {
    brand;
    model;
    year;
    constructor(brand, model, year = 2026) {
        super("Ford");
        this.brand = brand;
        this.model = model;
        this.year = year;
    }
    show() {
        console.log("Car brand: " + this.brand);
    }
}
const tahoe = new Car("Chevrolet", "Tahoe", 1992);
tahoe.show();
const key = "brand";
let uno = new Car("Fiat", "Uno", 1990);
console.log(uno.show());
process.stdin.resume(); //Keeps the console open.
//# sourceMappingURL=app.js.map