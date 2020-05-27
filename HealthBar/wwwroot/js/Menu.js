﻿let mainDivEl;
let cardDeckDivEl;
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
        cardDeckDivEl.appendChild(cardDivEl);
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    mainDivEl = document.getElementById('menuList');

    cardDeckDivEl = document.createElement('div');
    cardDeckDivEl.className = "card-deck m-3";

    mainDivEl.appendChild(cardDeckDivEl);

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