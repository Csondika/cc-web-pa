// variables to group DOM sections
let menuListFormEl;
let cardDeckDivEl;

// variable to parse JSON into
let menuItems;


function LoadMenuList(responseText) {
    menuItems = JSON.parse(responseText);

    for (let i = 0; i < menuItems.length; i++) {
        const item = menuItems[i];
        console.log(item);

        // Card
        const cardDivEl = document.createElement('div');
        cardDivEl.className = "card m-3";
        cardDivEl.style.minWidth = "18rem";
        cardDivEl.style.maxWidth = "30%";

        // Card Header
        const cardHeaderDivEl = document.createElement('div');
        cardHeaderDivEl.className = "card-header";

        const h3El = document.createElement('h3');
        h3El.innerHTML = item.name;

        // Card Body
        const cardBodyDivEl = document.createElement('div');
        cardBodyDivEl.className = "card-body";

        

        let pEl = document.createElement('p');
        pEl.className = "row font-weight-bold mr-2";
        pEl.innerHTML = `price: ${item.price}, cal: ${item.calories}, vegan: ${item.isVegan}`;
        cardBodyDivEl.appendChild(pEl);
        pEl = document.createElement('p');
        pEl.className = "row font-weight-bold mr-2";
        pEl.innerHTML = `active: ${item.isActive}`;
        cardBodyDivEl.appendChild(pEl);

        // Card Footer
        const cardFooterDivEl = document.createElement('div');
        cardFooterDivEl.className = "card-footer text-center";

        const inputEl = document.createElement('input');
        inputEl.type = 'hidden';
        inputEl.id = 'idInput' + i;
        inputEl.value = item.id;
        cardFooterDivEl.appendChild(inputEl);

        const checkboxEl = document.createElement('input');
        checkboxEl.className = "form-check-input ml-3";
        checkboxEl.type = 'checkbox';
        checkboxEl.id = 'activityInput' + i;
        checkboxEl.checked = item.isActive;
        cardFooterDivEl.appendChild(checkboxEl);

        // Append Card segments together
        cardHeaderDivEl.appendChild(h3El);
        cardDivEl.appendChild(cardHeaderDivEl);
        cardDivEl.appendChild(cardBodyDivEl);
        cardDivEl.appendChild(cardFooterDivEl);
        menuListFormEl.appendChild(cardDivEl);
    }
}

function modifyMenuActivity() {
    let activity = [];
    let currIdInput;
    let currActivityInput;
    let children = menuListFormEl.children;

    for (let i = 0; i < children.length-1; i++) {
        currIdInput = document.getElementById('idInput' + i);
        activity.push(currIdInput.value);
        currActivityInput = document.getElementById('activityInput' + i);
        activity.push(currActivityInput.checked);
    }

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Admin/RefreshMenuActivity', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=utf-8');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            DropMenuList();
            LoadMenuList(xhr.responseText);
        }
    }

    xhr.send(`activity=${activity}`);
}

function DropMenuList() {
    while (menuListFormEl.firstChild) {
        menuListFormEl.removeChild(menuListFormEl.firstChild);
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    menuListFormEl = document.getElementById('menuList');

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Admin/MenuList', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=utf-8');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            LoadMenuList(xhr.responseText);
        }
    }

    xhr.send();
});