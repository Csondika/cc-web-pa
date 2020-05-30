// variables to group DOM sections
let menuListDivEl;
let cardDeckDivEl;
let checkboxesDivEl;

// variable to parse JSON into
let menuItems;

// filter variables
let isSlim;
let isCheap;
let isVegan;


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
        pEl.innerHTML = item.price + " Ft";
        cardBodyDivEl.appendChild(pEl);

        pEl = document.createElement('p');
        pEl.className = "row font-weight-bold mr-2";
        pEl.innerHTML = item.calories + " kcal";
        cardBodyDivEl.appendChild(pEl);

        pEl = document.createElement('p');
        pEl.className = "row font-weight-bold mr-2";
        if (item.isVegan) {
            pEl.innerHTML = "Vegan";
            pEl.style.color = "green";
        }
        else {
            pEl.innerHTML = "Not Vegan";
            pEl.style.color = "red";
        }
        cardBodyDivEl.appendChild(pEl);

        // Card Footer
        const cardFooterDivEl = document.createElement('div');
        cardFooterDivEl.className = "card-footer text-center";

        const aEl = document.createElement('a');
        aEl.className = "btn btn-dark m-1 text-light";
        aEl.innerText = "Add";
        cardFooterDivEl.appendChild(aEl);

        // Append Card segments together
        cardHeaderDivEl.appendChild(h3El);
        cardDivEl.appendChild(cardHeaderDivEl);
        cardDivEl.appendChild(cardBodyDivEl);
        cardDivEl.appendChild(cardFooterDivEl);
        menuListDivEl.appendChild(cardDivEl);
    }
}

function SortMenuList() {
    const isSlimCheckboxEl = document.getElementById('inlineSlimCheckbox');
    const isCheapCheckboxEl = document.getElementById('inlineCheapCheckbox');
    const isVeganCheckboxEl = document.getElementById('inlineVeganCheckbox');

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Menu/SortedMenuList', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=utf-8');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            DropMenuList();
            LoadMenuList(xhr.responseText);
        }
    }

    const checklist = [isSlimCheckboxEl.checked, isCheapCheckboxEl.checked, isVeganCheckboxEl.checked]

    xhr.send(`checklist=${checklist}`);
}

function DropMenuList() {
    while (menuListDivEl.firstChild) {
        menuListDivEl.removeChild(menuListDivEl.firstChild);
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    menuListDivEl = document.getElementById('menuList');
    checkboxesDivEl = document.getElementById('checkboxesDiv');

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Menu/MenuList', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=utf-8');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            LoadMenuList(xhr.responseText);
        }
    }

    xhr.send();
});