//these are similar to C# using statements
open canopy
open runner
open System

let baseUrl = "http://localhost:14729" 
let userEmail = "user" + DateTime.Now.Ticks.ToString() + "@company.com"
let password = "Test999/*"
//start an instance of the firefox browser
start firefox

//"Initial with no cars" &&& fun _ ->
//    url "http://localhost:14729/cars"
//    "#count" == "0"

//"Click add link then go to create page" &&& fun _ ->
//    url "http://localhost:14729/cars"
//    displayed "a#gotoAdd"
//    click "a#gotoAdd"
//    on "http://localhost:14729/cars/create"
//
//"Add new car" &&& fun _ ->
//    let make = "Tesla " + DateTime.Now.Ticks.ToString()
//    url "http://localhost:14729/cars/create"
//    "#Make" << make
//    "#Model" << "Model 3"
//    click "button#btnAdd"
//
//    on "http://localhost:14729/cars"
//    "td" *= make

"Sign Up" &&& fun _ ->
    url (baseUrl + "/Account/Register")
    "#Email" << userEmail
    "#Password" << password
    "#ConfirmPassword" << password
    click "input[type=submit]"
    on baseUrl

"Log in" &&& fun _ ->
    url (baseUrl + "/Account/Login")
    "#Email" << userEmail
    "#Password" << password
    click "input[type=submit]"
    on baseUrl
 
"Click add link then go to create page" &&& fun _ ->
    url (baseUrl + "/cars")
    displayed "a#gotoAdd"
    click "a#gotoAdd"
    on (baseUrl + "/cars/create")
 
"Add new car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *= make

"Add the second car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *= make

"Add the third car should failed" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *!= make
    contains "Cannot add more car." (read ".error")

//run all tests
run()

//printfn "press [enter] to exit"
//System.Console.ReadLine() |> ignore

quit()