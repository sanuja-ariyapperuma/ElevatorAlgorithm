// See https://aka.ms/new-console-template for more information
using Escalator;

Console.WriteLine("Welcome to My Elevator");

var elevator = new MyElevator();
elevator.SetMaxFloor(10);


elevator.CallElevator(5, true);

elevator.CallElevator(3, true);

elevator.CallElevator(2, false);
elevator.CallElevator(4, false);

elevator.Move();
elevator.Move();
elevator.Move();
elevator.Move();
elevator.Move();

