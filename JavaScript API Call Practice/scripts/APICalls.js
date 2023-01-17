const URL_GET_ALL_PERSONS = "https://localhost:7087/getallpersons";
const URL_GET_ALL_ITEMS = "https://localhost:7087/getallitems";
const URL_INSERT_PERSON = "https://localhost:7087/insertperson";
const URL_INSERT_ITEM = "https://localhost:7087/insertitem";
const URL_PING = "https://localhost:7087/ping";
const URL_RESET_DATABASE = "https://localhost:7087/resetdatabase";

var nameInput = document.getElementById("personname");
var ageInput = document.getElementById("personage");
var itemNameInput = document.getElementById("itemname");
var itemTypeInput = document.getElementById("itemtype");
var itemQuantityInput = document.getElementById("itemquantity");
var itemPriceInput = document.getElementById("itemprice");
var submitButton = document.getElementById("submit");
var getAllPersonsButton = document.getElementById("getallpersons");
var getAllItemsButton = document.getElementById("getallitems");
var pingAPIButton = document.getElementById("ping");
var addPersonButton = document.getElementById("addperson");
var addItemButton = document.getElementById("additem");
var output = document.getElementById("output");
var personInput = document.getElementById("personinput");
var itemInput = document.getElementById("iteminput");
var personSubmitButton = document.getElementById("personsubmit");
var itemSubmitButton = document.getElementById("itemsubmit");
var cancelPersonButton = document.getElementById("personsubmitcancel");
var cancelItemButton = document.getElementById("itemsubmitcancel");
var resetDatabaseButton = document.getElementById("reset");

getAllPersonsButton.onclick = async function() {
    outputStatus("Getting all persons ... ");
    getData(URL_GET_ALL_PERSONS, "Persons");
}

getAllItemsButton.onclick = async function() {
    outputStatus("Getting all items ... ");
    getData(URL_GET_ALL_ITEMS, "Items");
}

addPersonButton.onclick = async function() {
    itemInput.setAttribute("hidden", "true");
    personInput.removeAttribute("hidden");
}

addItemButton.onclick = async function() {
    personInput.setAttribute("hidden", "true");
    itemInput.removeAttribute("hidden");
}

cancelPersonButton.onclick = function() {
    personInput.setAttribute("hidden", "true");
}

cancelItemButton.onclick = function() {
    itemInput.setAttribute("hidden", "true");
}



resetDatabaseButton.onclick = async function() {
    await fetch(URL_RESET_DATABASE, {
        method: 'GET'
    })
    //.then(response => response.json())
    .then(() => outputStatus("Successfully reset database", "green"))
    .catch(error => outputStatus(error, "red"));
}

personSubmitButton.onclick = async function() {
    const person = {
        name: nameInput.value,
        age: ageInput.value 
    };

    const persons = [person];

    await fetch(URL_INSERT_PERSON, {
        method: 'POST',
        headers: {
            'accept': '*/*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(persons)
    })
    .then(response => response.json())
    .then(() => outputStatus("Successfully added person", "green"))
    .catch(error => outputStatus(error, "red"));
}

itemSubmitButton.onclick = async function() {
    const item = {
        name:  itemNameInput.value,
        type: itemTypeInput.value,
        price: itemPriceInput.value,
        quantity: itemQuantityInput.value
    };

    const items = [item];

    await fetch(URL_INSERT_ITEM, {
        method: 'POST',
        headers: {
            'accept': '*/*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(items)
    })
    .then(response => response.json())
    .then(() => outputStatus("Successfully added item", "green"))
    .catch(error => outputStatus(error, "red"));
}

pingAPIButton.onclick = async function() {
    outputStatus("Pinging API ... ");

    if (await pingAPI()) {    
        outputStatus("API is available", "green");

    } else {
        outputStatus("API is unavailable", "red");
    }   
}

function clearInputPlaceholder() {
    while (inputPlaceholder.children.length > 0) {
        inputPlaceholder.removeChild(inputPlaceholder.children[0]);
    }
}

async function pingAPI() {
    let status = true;

    await fetch(URL_PING)
    .then(response => response.json())
    .catch(() => status = false);

    return status;
}

async function getData(url = '', dataType = '') {
    await fetch (url)
    .then(response => response.json())
    .then(data => displayItems(data, dataType))
    .catch(error => outputStatus(error, "red"));
}

function displayItems(data, dataType = '') {
    const dataTable = document.createElement("table");
    dataTable.style = 'border: 1px solid black; border-collapse: collapse;';

    if (dataType != '') {
        let headerRow = dataTable.insertRow();
        let td = headerRow.insertCell(0);
        let textNode = document.createTextNode(dataType);

        td.style.fontWeight = 'bold';
        td.appendChild(textNode);
    }

    let createdLabelHeaders = false;

    data.forEach(item => {
        if (!createdLabelHeaders) {
            createLabelHeaders(Object.keys(item), dataTable);
            createdLabelHeaders = true;
        }

        let i = 0;
        let tr = dataTable.insertRow();

        for (const key in item) {
            let td = tr.insertCell(i);
            let textNode = document.createTextNode(item[key]);
            td.style = "border: 1px solid black; border-collapse: collapse;";
            td.appendChild(textNode);
            i++;
        }

        console.log(item);
    });

    appendToOutput(dataTable);
}

function createLabelHeaders(keys, dataTable) {
    let tr = dataTable.insertRow();
    let i = 0;

    for (const key in keys) {
        let td = tr.insertCell(i); 
        let textNode = document.createTextNode(String(keys[key]).toUpperCase());
        td.style = "text-align: center"
        td.appendChild(textNode);
        i++;
    }
}