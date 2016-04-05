//these are similar to C# using statements
open canopy
open runner
open System

//start an instance of the firefox browser
start firefox

//"Initial with no cars" &&& fun _ ->
//    url "http://localhost:14729/cars"
//    "#count" == "0"

"Click add link then go to create page" &&& fun _ ->
    url "http://localhost:14729/cars"
    displayed "a#gotoAdd"
    click "a#gotoAdd"
    on "http://localhost:14729/cars/create"

"Add new car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url "http://localhost:14729/cars/create"
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"

    on "http://localhost:14729/cars"
    "td" *= make

//run all tests
run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()