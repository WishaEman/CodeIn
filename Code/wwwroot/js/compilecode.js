"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/compilerHub").build();

document.getElementById("runCode").disabled = true;

connection.on("ReceiveMessage", function (response) {///Receievs the message from Server side And send too client side
    $("#loader").hide();
    outputEditor.setValue(res);
    outputEditor.insert(res + response);
});

connection.start().then(function () {
    document.getElementById("runCode").disabled = false;
   
}).catch(function (err) {
    return console.error(err.toString());
});
=
document.getElementById("runCode").addEventListener("click", function (event) {
    $("#loader").show();
    var message = editor.getValue();
    var compileInp = $("#compileInput").val();
    connection.invoke("SendMessage", message, language, compileInp).catch(function (err) {////sending the message to server side(Myhub SendMessage method)
        return console.error(err.toString());
    });
    event.preventDefault();
});

