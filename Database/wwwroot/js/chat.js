// The following sample code uses modern ECMAScript 6 features 
// that aren't supported in Internet Explorer 11.
// To convert the sample for environments that do not support ECMAScript 6, 
// such as Internet Explorer 11, use a transpiler such as 
// Babel at http://babeljs.io/. 
//
// See Es5-chat.js for a Babel transpiled version of the following code:

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/dataHub")
    .build();

connection.on("ValidationRequest", (ValidationStatus) => {
    const encodedMsg = "Status: " + ValidationStatus
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
    const username = document.getElementById("userInput").value;
    const password = document.getElementById("messageInput").value;
    connection.invoke("Login", username, password).catch(err => console.error(err.toString()));
    event.preventDefault();
});