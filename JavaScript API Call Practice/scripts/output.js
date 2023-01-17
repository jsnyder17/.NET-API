function outputStatus(msg, color = '') {
    var text = document.createElement("p");
    text.innerHTML = "-> " + msg;
    //text.style = "white-space: pre";
    if (color != '') {
        text.style.color = color;
    }
    
    appendToOutput(text);
}

function appendToOutput(item) {
    output.appendChild(item);
    output.scrollTop = output.scrollHeight;
}