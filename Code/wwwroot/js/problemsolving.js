"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/problemHub").build();

document.getElementById("runProblemCode").disabled = true;

connection.on("ReceiveMessage", function (response) 
{///Receievs the message from Server side And send too client side
    outputEditor.setValue(res);
    outputEditor.insert(res + response);
});

connection.start().then(function () {
    document.getElementById("runProblemCode").disabled = false;

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("runProblemCode").addEventListener("click", function (event) {
    var message = editor.getValue();
    var inp = "@Model.input";
    connection.invoke("CheckOutput", message, language, inp).catch(function (err) {////sending the message to server side(Myhub SendMessage method)
        return console.error(err.toString());
    });
    event.preventDefault();
});

