// Retrive saved cart data and assigning to the variables
let cartList = JSON.parse(localStorage.getItem("cartList")) || [];
let items = JSON.parse(localStorage.getItem("items")) || [];
let totalPrice = localStorage.getItem("totalPrice") || 0;

let parentElement = document.getElementById("orderedItems");

parentElement.innerHTML = "";

// This for loop is use to display cart products
for (let i = 0; i < cartList.length; i++) {
  let newDiv = document.createElement("div");
  newDiv.classList.add("cart");
  let item = items.find((item) => item.id === cartList[i].id);
  newDiv.innerHTML = `
              <img src="${item.img}">
              <p><b>${item.title}</b><br/>
            ${cartList[i].color}&nbsp;&nbsp;&nbsp;&nbsp;
            ${cartList[i].quantity} x ${item.price.toFixed(2)}<br/>
            &#163;${(item.price * cartList[i].quantity).toFixed(2)}</p>
          `;

  parentElement.appendChild(newDiv);
}

// Update html page total and donation amounts
document.getElementById("total").innerHTML = totalPrice;
document.getElementById("donation").innerHTML = (totalPrice * 0.8).toFixed(2);

// Clear locally saved data when web page is colsed
window.onbeforeunload = function () {
  localStorage.setItem("cartList", JSON.stringify([]));
  localStorage.setItem("items", JSON.stringify([]));
  localStorage.setItem("totalPrice", "0");
};

let isFormValid = false;

// Validate form inputs
function checkFormInputs() {
  let year = document.getElementById("exp-year").value;
  let card = document.getElementById("card-no").value;
  let cvv = document.getElementById("cvv").value;
  let month = document.getElementById("exp-month").value;
  let cvv_no = Number(cvv);
  let card_no = Number(card);
  let yearNumber = Number(year);

  let validMonths = [
    "january",
    "february",
    "march",
    "april",
    "may",
    "june",
    "july",
    "august",
    "september",
    "october",
    "november",
    "december",
  ];

  if (isNaN(yearNumber) || year.length != 4) {
    alert("Please enter a valid year.");
    isFormValid = false;
  } else if (yearNumber < 2024) {
    alert("Please enter a valid expiration year.");
    isFormValid = false;
  } else if (isNaN(card_no) || card.length != 16) {
    alert("Please enter a valid card no.");
    isFormValid = false;
  } else if (isNaN(cvv_no) || cvv.length != 3) {
    alert("Please enter a valid cvv no.");
    isFormValid = false;
  } else if (!validMonths.includes(month.toLowerCase())) {
    alert("Please enter a valid expiration month.");
    isFormValid = false;
  } else {
    isFormValid = true;
  }
  return isFormValid;
}

// Add event listner to form
document.querySelector("form").addEventListener("submit", function (event) {
  event.preventDefault();

  if (cartList.length === 0) {
    alert(
      "Your cart is currently empty. Please add some items from shop page."
    );
  } else {
    isFormValid = checkFormInputs();
    if (isFormValid) {
      var confirmation = confirm("Are you sure you want to place the order?");
      if (confirmation) {
        alert("Order is successfully placed");
        this.submit();
        window.location.href = "shop.html";
      }
    }
  }
});

// Below functions are used for nav bar
function showSidebar() {
  const sidebar = document.querySelector(".sidebar");
  sidebar.style.display = "flex";
}
function hideSidebar() {
  const sidebar = document.querySelector(".sidebar");
  sidebar.style.display = "none";
}
